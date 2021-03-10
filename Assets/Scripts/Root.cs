﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

//This is the root script that defines behavior at the root level
//It is to be used by a GameObject at the root of the contruction simulator
//Parker
public class Root : MonoBehaviour
{

    public GameObject environmentSetter;
    public GameObject environmentContainer;
    public int shiftAmount;
    public Vector3 environmentOffset;


    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    bool isShiftedUp;
    int scaleModeState;
    List<scaleState> scaleLevels;

    // Start is called before the first frame update
    void Start()
    {
        isShiftedUp = false;
        scaleModeState = 0;

        scaleLevels = new List<scaleState>();
        scaleLevels.Add(new scaleState(new Vector3(1.0f, 1.0f, 1.0f), 1f));
        scaleLevels.Add(new scaleState(new Vector3(2.0f, 2.0f, 2.0f), 2f));
        scaleLevels.Add(new scaleState(new Vector3(3.0f, 3.0f, 3.0f), 3f));
        scaleLevels.Add(new scaleState(new Vector3(4.0f, 4.0f, 4.0f), 4f));
        scaleLevels.Add(new scaleState(new Vector3(.5f, .5f, .5f), .5f));

        this.toggleVisibility(false, environmentContainer);

        keywords.Add("Set Space", () => { this.setSpace(); });
        keywords.Add("Shift Level", () => { this.shiftLevel(); });
        keywords.Add("Change View", () => { this.changeView(); });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSpace()
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = environmentSetter.transform.position.x + environmentOffset.x;
        newPosition.y = environmentSetter.transform.position.y + environmentOffset.y;
        newPosition.z = environmentSetter.transform.position.z + environmentOffset.z;

        environmentContainer.transform.position = newPosition;

        this.toggleVisibility(true, environmentContainer);

        Debug.Log("\n \n________________________________\nSETTING ENV: " + environmentContainer.transform.position.ToString());
    }

    public void shiftLevel()
    {
        Vector3 newPosition = new Vector3();

        if (!isShiftedUp)
        {
            newPosition.x = environmentSetter.transform.position.x + environmentContainer.transform.position.x + (float)shiftAmount;
            newPosition.y = environmentSetter.transform.position.y + environmentContainer.transform.position.y + (float)shiftAmount;
            newPosition.z = environmentSetter.transform.position.z + environmentContainer.transform.position.z + (float)shiftAmount;
        }
        else
        {
            newPosition.x = environmentSetter.transform.position.x + environmentContainer.transform.position.x - (float)shiftAmount;
            newPosition.y = environmentSetter.transform.position.y + environmentContainer.transform.position.y - (float)shiftAmount;
            newPosition.z = environmentSetter.transform.position.z + environmentContainer.transform.position.z - (float)shiftAmount;
        }

        environmentContainer.transform.position = newPosition;
        Debug.Log("\n \n________________________________\nSHIFTING ENV: " + environmentContainer.transform.position.ToString());
    }

    public void changeView()
    {
        Vector3 newPosition = new Vector3();

        newPosition.x = environmentOffset.x * scaleLevels[scaleModeState].shift;
        newPosition.y = environmentOffset.y * scaleLevels[scaleModeState].shift;
        newPosition.z = environmentOffset.z * scaleLevels[scaleModeState].shift;

        environmentContainer.transform.localScale = scaleLevels[scaleModeState].scale;
        environmentContainer.transform.position = newPosition;

        Debug.Log("\n \n________________________________\nSETTING scale: " + environmentContainer.transform.localScale.ToString() +"\n MODE: "+scaleModeState);

        scaleModeState++;
        if (scaleModeState > 4) scaleModeState = 0;
    }


    public void toggleVisibility(bool visible, GameObject target)
    {
        foreach (Renderer r in target.GetComponentsInChildren(typeof(Renderer)))
        {
            r.enabled = visible;
        }
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