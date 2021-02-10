using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

//MOST CODE WAS COPIED FROM "SpeechManager.cs" SCRIPT

public class GarrettSpeechManager : MonoBehaviour
{
    [Serializable]
    //object orderable object holds its name and original gameobject
    public struct OrderableObj
    {
        public string name;
        public GameObject obj;
    }
    //creates an object that uses speech recognition to find keywords
    KeywordRecognizer keywordRecognizer = null;
    //Dictionary use is <keytype>,<objecttype>
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public OrderableObj[] orderableObjs;


    // Start is called before the first frame update
    void Start()
    {
        // global commands for speech recognizer, uses anonymous functions to broadcast message to speech recognizer.
        keywords.Add("Order Two By Four", () => { this.BroadcastMessage("OrderTwoByFour"); });
        keywords.Add("Order Ladder", () => { this.BroadcastMessage("OrderLadder"); });
        keywords.Add("Order Screw", () => { this.BroadcastMessage("OrderScrew"); });
        keywords.Add("Order Slide", () => { this.BroadcastMessage("OrderSlide"); });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a onPhraseRecognized event
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        //start recognizing speech
        keywordRecognizer.Start();
    }

    //called when keyword recognizer recognizes one of the given keywords
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();     // if speech command recognized, then invoke the action
        }
    }
}