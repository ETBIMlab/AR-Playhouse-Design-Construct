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
        keywords.Add("Order Two By Four", () => { this.BroadcastMessage("OrderTwoByFour"); });
        keywords.Add("Order Ladder", () => { this.BroadcastMessage("OrderLadder"); });
        keywords.Add("Order Screw", () => { this.BroadcastMessage("OrderScrew"); });
        keywords.Add("Order Slide", () => { this.BroadcastMessage("OrderSlide"); });
        keywords.Add("Change Size", () => { this.BroadcastMessage("ChangeSize"); });


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