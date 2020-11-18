using System.Collections;
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

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Start is called before the first frame update
    void Start()
    {

        this.toggleVisibility(false, environmentContainer);

        keywords.Add("Set Space", () => { this.setSpace(); });
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
        environmentContainer.transform.position = environmentSetter.transform.position;

        this.toggleVisibility(true, environmentContainer);

        Debug.Log("\n \n________________________________\nSETTING ENV: " + environmentContainer.transform.position.ToString());
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
