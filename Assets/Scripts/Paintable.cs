using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Paintable : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Start is called before the first frame update
    void Start()
    {
        keywords.Add("Paint", () => { OpenMenu(); });
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
            keywordAction.Invoke();
        }
    }

    public void OpenMenu()
    {
        Debug.Log("Opening Paint Menu");
        GameObject paintMenu = GameObject.Find("PaintMenu");
        paintMenu.SetActive(true);
        paintMenu.GetComponent<Painter>().objectToBePainted = gameObject;

    }

    public void ChangeColor(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}
