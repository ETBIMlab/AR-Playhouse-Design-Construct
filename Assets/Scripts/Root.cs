using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;

//This is the root script that defines behavior at the root level
//It is to be used by a GameObject at the root of the contruction simulator
//Parker
public class Root : MonoBehaviour
{
    public AudioRoot audio;
    private AudioSource audioSource;
    public AudioClip spaceSet;
    public GameObject environmentSetter;
    public GameObject environmentContainer;
    public GameObject childToggle;
    public GameObject levelToggle;
    public int shiftAmount;
    public Vector3 environmentOffset;


    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    bool isShiftedUp;
    private bool floorVisible;
    int scaleModeState;
    int currentScale;
    List<scaleState> scaleLevels;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        isShiftedUp = false;//player starts on the ground
        floorVisible = false;//turns the floor visible
        scaleModeState = 1;
        currentScale = 0;

        scaleLevels = new List<scaleState>();
        scaleLevels.Add(new scaleState(new Vector3(1.0f, 1.0f, 1.0f), 1f));
        scaleLevels.Add(new scaleState(new Vector3(2.0f, 2.0f, 2.0f), 2f));
        scaleLevels.Add(new scaleState(new Vector3(3.0f, 3.0f, 3.0f), 3f));
        scaleLevels.Add(new scaleState(new Vector3(4.0f, 4.0f, 4.0f), 4f));
        scaleLevels.Add(new scaleState(new Vector3(5.0f, 5.0f, 5.0f), 5f));
        scaleLevels.Add(new scaleState(new Vector3(6.0f, 6.0f, 6.0f), 6f));
        scaleLevels.Add(new scaleState(new Vector3(7.0f, 7.0f, 7.0f), 7f));
        scaleLevels.Add(new scaleState(new Vector3(8.0f, 8.0f, 8.0f), 8f));
        scaleLevels.Add(new scaleState(new Vector3(9.0f, 9.0f, 9.0f), 9f));
        scaleLevels.Add(new scaleState(new Vector3(10.0f, 10.0f, 10.0f), 10f));//index 9
        scaleLevels.Add(new scaleState(new Vector3(.5f, .5f, .5f), .5f));
        scaleLevels.Add(new scaleState(new Vector3(2.25f, 2.25f, 2.25f), 2.25f));
        scaleLevels.Add(new scaleState(new Vector3(2.5f, 2.5f, 2.5f), 2.5f));
        scaleLevels.Add(new scaleState(new Vector3(2.75f, 2.75f, 2.75f), 2.75f));

        this.toggleVisibility(false, environmentContainer);

        keywords.Add("Set Space", () => { this.setSpace(); audioSource.PlayOneShot(spaceSet,1F); });//set the space for the simulator


        keywords.Add("Shift Level", () => { this.toggleLevel(); audio.shiftLevel(); });//toggle the current level
        keywords.Add("Move down", () =>
        {
            this.toggleLevel(); audio.shiftLevel();
        });
        keywords.Add("Move up", () =>
        {
            this.toggleLevel(); audio.shiftLevel();
        });
        /*keywords.Add("Move up", () => { //move to the upper level if the player is on the ground floor
            if(!isShiftedUp)//player is on the ground floor
            {
                this.shiftLevel();
                audio.shiftLevel();
            }
        });
        keywords.Add("Move down", () => { //move to the upper level if the player is on the ground floor
            if (isShiftedUp)//player is on the upper floor
            {
                this.shiftLevel();
                audio.shiftLevel();
            }
        });*/
       
        keywords.Add("Toggle Level", () => { this.toggleLevel();  });//toggle the  level up or down

        keywords.Add("Toggle floor", () => { this.toggleFloor(); audio.changeView(); });//toggle the  floor on or off


        keywords.Add("Change View", () => { this.toggleButton(); audio.changeView(); });//move through the scaling
        keywords.Add("Scale half", () => { this.scale(0); audio.changeView(); });
        keywords.Add("Scale one", () => { this.scale(1); audio.changeView(); });
        keywords.Add("Toggle view", () => { this.toggleButton(); audio.changeView(); });
        keywords.Add("Normal view", () => { this.toggleButton(); audio.changeView(); });//scale 2 is the aproximate size for the real play house
        keywords.Add("Scale two", () => { this.scale(2); audio.changeView(); });
        keywords.Add("Scale three", () => { this.scale(3); audio.changeView(); });
        keywords.Add("Scale four", () => { this.scale(4); audio.changeView(); });

        keywords.Add("child view", () => { this.toggleButton(); audio.changeView(); });//scale 5 is the sproximate size for a childs view of the playhouse

        keywords.Add("Scale five", () => { this.scale(5); audio.changeView(); });
        keywords.Add("Scale six", () => { this.scale(6); audio.changeView(); });
        keywords.Add("Scale seven", () => { this.scale(7); audio.changeView(); });
        keywords.Add("Scale eight", () => { this.scale(8); audio.changeView(); });
        keywords.Add("Scale nine", () => { this.scale(9); audio.changeView(); });
        keywords.Add("Scale quarter", () => { this.scale(10); audio.changeView(); });//1/2 scale of default
        keywords.Add("Scale one and a quarter", () => { this.scale(11); audio.changeView(); });
        keywords.Add("Scale one and a half", () => { this.scale(12); audio.changeView(); });
        keywords.Add("Scale one and three quarters", () => { this.scale(13); audio.changeView(); });


        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleButton()//toggle the switch button for child view
    {
       
        childToggle.GetComponent<Interactable>().TriggerOnClick(); 
        Debug.Log("Toggle View");
        
    }
    //swap the text for the switch
    public void swapChildText(bool toggle)
    {
        if (toggle)
        {
            childToggle.GetComponentInChildren<TextMesh>().text = "Child \nView";
        }
        else
        {
            childToggle.GetComponentInChildren<TextMesh>().text = "Normal \nView";
        }
    
    }

    public void toggleLevel()
    {
        levelToggle.GetComponent<Interactable>().TriggerOnClick();
        Debug.Log("Toggle Level");
    }

    public void swapLevelText(bool toggle)
    {
        if (toggle)
        {
            levelToggle.GetComponentInChildren<TextMesh>().text = "Move \nUp";
        }
        else
        {
            levelToggle.GetComponentInChildren<TextMesh>().text = "Move \nDown";
        }
    }

    public void setSpace()
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = environmentSetter.transform.position.x + environmentOffset.x;
        newPosition.y = environmentSetter.transform.position.y + environmentOffset.y;
        newPosition.z = environmentSetter.transform.position.z + environmentOffset.z;

        environmentContainer.transform.position = newPosition;

        this.toggleVisibility(true, environmentContainer);
        this.scale(1);

        Debug.Log("\n \n________________________________\nSETTING ENV: " + environmentContainer.transform.position.ToString());
    }

    public void shiftLevel()
    {
        Vector3 newPosition = new Vector3();

        if (isShiftedUp)
        {
            Debug.Log("shifted up");
            newPosition.x = environmentContainer.transform.position.x;//environmentSetter.transform.position.x + environmentOffset.x; // + environmentContainer.transform.position.x + (float)shiftAmount;
            newPosition.y =  environmentContainer.transform.position.y + (float)shiftAmount*scaleLevels[currentScale].shift;
            newPosition.z = environmentContainer.transform.position.z;//environmentSetter.transform.position.z + environmentOffset.z;// + environmentContainer.transform.position.z + (float)shiftAmount;
            isShiftedUp = false;
        }
        else
        {
            Debug.Log("shifted down");
            newPosition.x = environmentContainer.transform.position.x;//environmentSetter.transform.position.x + environmentOffset.x; //+ environmentContainer.transform.position.x - (float)shiftAmount;
            newPosition.y = environmentContainer.transform.position.y - (float)shiftAmount*scaleLevels[currentScale].shift;
            newPosition.z = environmentContainer.transform.position.z;//environmentSetter.transform.position.z + environmentOffset.z;// + environmentContainer.transform.position.z - (float)shiftAmount;
            isShiftedUp = true;
        }

        environmentContainer.transform.position = newPosition;
        Debug.Log("\n \n________________________________\nSHIFTING ENV: " + environmentContainer.transform.position.ToString());
    }

    public void changeView()
    {
        Vector3 newPosition = new Vector3();

        //newPosition.x = environmentOffset.x * scaleLevels[scaleModeState].shift;
        //newPosition.y = environmentOffset.y * scaleLevels[scaleModeState].shift;
        //newPosition.z = environmentOffset.z * scaleLevels[scaleModeState].shift;
        //scale the level and move the level to the environmentSetter
        newPosition.x = environmentSetter.transform.position.x + environmentOffset.x * scaleLevels[scaleModeState].shift;
        newPosition.y = environmentSetter.transform.position.y + environmentOffset.y * scaleLevels[scaleModeState].shift;
        newPosition.z = environmentSetter.transform.position.z + environmentOffset.z * scaleLevels[scaleModeState].shift;

        //environmentContainer.transform.position = newPosition;
        environmentContainer.transform.localScale = scaleLevels[scaleModeState].scale;
        environmentContainer.transform.position = newPosition;

        Debug.Log("\n \n________________________________\nSETTING scale: " + environmentContainer.transform.localScale.ToString() +"\n MODE: "+scaleModeState);

        scaleModeState++;
        if (scaleModeState > 13) scaleModeState = 0;
    }

    public void scale(int scaler)
    {
        Vector3 newPosition = new Vector3();

        //newPosition.x = environmentOffset.x * scaleLevels[scaleModeState].shift;
        //newPosition.y = environmentOffset.y * scaleLevels[scaleModeState].shift;
        //newPosition.z = environmentOffset.z * scaleLevels[scaleModeState].shift;
        //scale the level and move the level to the environmentSetter
        newPosition.x = environmentSetter.transform.position.x + environmentOffset.x * scaleLevels[scaler].shift;
        newPosition.y = environmentSetter.transform.position.y + environmentOffset.y * scaleLevels[scaler].shift;
        newPosition.z = environmentSetter.transform.position.z + environmentOffset.z * scaleLevels[scaler].shift;

        environmentContainer.transform.localScale = scaleLevels[scaler].scale;
        environmentContainer.transform.position = newPosition;

        Debug.Log("\nSETTING scale: " + environmentContainer.transform.localScale.ToString() + "\n SIZE: " + scaler);
        currentScale = scaler;
        if(isShiftedUp)//player is currently on upper floor
        {
            isShiftedUp = false;//since scale returns to ground floor, reset the shifted state
            this.shiftLevel();
        }

        scaleModeState++;
        if (scaleModeState > 13) scaleModeState = 0;
    }


    public void toggleVisibility(bool visible, GameObject target)
    {
        foreach (Renderer r in target.GetComponentsInChildren(typeof(Renderer)))
        {
            r.enabled = visible;
        }
    }

    public void toggleFloor()
    {
        Debug.Log("Changing floor");
        toggleVisibility(floorVisible, GameObject.Find("Floor"));
        floorVisible = !floorVisible;
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}

public class scaleState 
{
    public scaleState(Vector3 newScale, float newShift)
    {
        scale = newScale;
        shift = newShift;
    }
    public Vector3 scale { get; set; }
    public float shift { get; set; }
}
