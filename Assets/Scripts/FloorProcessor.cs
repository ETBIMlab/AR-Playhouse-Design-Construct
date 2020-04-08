using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class FloorProcessor : MonoBehaviour
{
    private bool isLocked;
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public FloorProcessor()
    {
        this.isLocked = true;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        keywords.Add("unlock floor", () => 
        {
            if (this.isLocked)
            {
                this.isLocked = false;
            }
        });
        
        keywords.Add("lock floor", () =>
        {
            if (!this.isLocked)
            {
                this.isLocked = true;
            }
        });




        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();     // if speech command recognized, then invoke the action
        }
    }

    public bool getFloorState()
    {
        return this.isLocked;
    }
}
