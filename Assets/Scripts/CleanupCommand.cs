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
    //private Vector3 drillPos = new Vector3(10.4f, 9f, -41.6f);
    private Vector3 drillOgPos;
    public GameObject assignToObjectToCleanup;
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Start is called before the first frame update
    void Start()
    {
        //drillOgPos = new Vector3(assignToDrillInScene.transform.position.x, assignToDrillInScene.transform.position.y, assignToDrillInScene.transform.position.z);
        //alternatively, just: 
        //drillOgPos = assignToDrillInScene.transform.position;
        // drillOgPos = assignToTableInScene.transform.position;
        Debug.Log("drillogPos: " + drillOgPos.ToString());
        // keywords.Add("Cleanup", () => { this.CleanUp(); });//transports the drill and paintbrush back to the workbench

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
        Debug.Log("back 2 drillogPos: " + drillOgPos.ToString());


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
