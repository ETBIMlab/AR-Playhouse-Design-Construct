using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        // global command
        keywords.Add("Create Cube", () =>   // added the "Create Cube" command to dictionary of commands (keywords)
        {
            //Debug.Log("Create Cube");
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("CreateCube");    // when this keyword is invoked, this will be broadcasted, calling the method CreateCube
     
        });
        keywords.Add("Shift", () =>   // added the "Create Cube" command to dictionary of commands (keywords)
        {
            //Debug.Log("Create Cube");
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("Shift");    // when this keyword is invoked, this will be broadcasted, calling the method CreateCube
        });


        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // important
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();     // if speech command recognized, then invoke the action
        }
    }
}