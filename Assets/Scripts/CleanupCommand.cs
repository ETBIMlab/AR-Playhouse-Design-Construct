using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;
public class CleanupCommand : MonoBehaviour
{
    /// <summary>
    /// So this is my cleanup command for if you lose your tools you can say "Cleanup" and they will spawn back at the 
    /// workbench. How it works: I made a duplicate of each tool that sits in the og position and then I gave those a 
    /// tag. These duplicates have their mesh renderers and all other functions deleted because we just need them to sit
    /// and look pretty (invisibly). Why no empty gameobject? This is easier, trust me. So after you say cleanup our tools
    /// go back to where these tagged invisible tools reside.
    /// </summary>
    //private Vector3 drillPos = new Vector3(10.4f, 9f, -41.6f);
    private Vector3 drillOgPos;
    public GameObject assignToDrillInScene;
    public GameObject assignToPbInScene;
    public GameObject assignToTableInScene;
    public GameObject assignToHDrillInScene;
    public GameObject assignToHPbInScene;
    public float speed = 1.0f;
    public Vector3 myTargetPosition;
    private Transform target;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Start is called before the first frame update
   /*void Awake()
    {
        // Grab cylinder values and place on the target.
        target = assignToHDrillInScene.transform;
        target.transform.localScale = new Vector3(0.15f, 1.0f, 0.15f);
        target.transform.position = new Vector3(14.8f, 3.0f, 16.8f);
    }*/
    void Start()
    {
        //drillOgPos = new Vector3(assignToDrillInScene.transform.position.x, assignToDrillInScene.transform.position.y, assignToDrillInScene.transform.position.z);
        //alternatively, just: 
        //drillOgPos = assignToDrillInScene.transform.position;
        // drillOgPos = assignToTableInScene.transform.position;
        //drillHpos = assignToHDrillInScene.transform.position;
        // drillPos = assignToDrillInScene.transform.position;
        //Vector3 myTargetPosition = GameObject.Find("Table").transform.position;
        assignToHDrillInScene = GameObject.FindWithTag("drillOpos");
        assignToHPbInScene = GameObject.FindWithTag("paintOpos");

        Debug.Log("intiating cleanup...");
         
        keywords.Add("Cleanup", () => { this.CleanUp(); });//transports the drill and paintbrush back to the workbench

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

   
    public void CleanUp()
    {
        //assignToDrillInScene.transform.position = new Vector3(10.41f, 9.02f, -40.61f);
        //assignToPaintBrushInScene.transform.position = new Vector3(6.36f, 9.32f, -41.4f);
        //assignToDrillInScene.transform.SetPositionAndRotation(new Vector3(10.41f, 9.02f, -40.61f), assignToDrillInScene.transform.rotation);
        //assignToDrillInScene.transform.position = drillPos;
        // assignToDrillInScene.transform.position = drillOgPos;
        //assignToDrillInScene.transform.position = new Vector3(myTargetPosition.x, myTargetPosition.y, myTargetPosition.z);
        //= drillHpos;
        // float step = speed * Time.deltaTime; // calculate distance to move
        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        assignToDrillInScene.gameObject.transform.position = assignToHDrillInScene.transform.position;
        assignToPbInScene.gameObject.transform.position = assignToHPbInScene.transform.position;

        Debug.Log("back 2 drillogPos: ");


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
