using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Paintable : MonoBehaviour
{
    Color orderColor; //what color did we voice command we want?

    // KeywordRecognizer keywordRecognizer = null;
    // Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Start is called before the first frame update
    // void Start()
    // {
    // keywords.Add("Paint", () => { OpenMenu(); });
    // keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
    // keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
    // keywordRecognizer.Start();
    // }

    // Update is called once per frame
    // void Update()
    // {

    // }

    // private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    // {
    // System.Action keywordAction;
    // if (keywords.TryGetValue(args.text, out keywordAction))
    // {
    // keywordAction.Invoke();
    // }
    // }

    // public void OpenMenu()
    // {
    // Debug.Log("Opening Paint Menu");
    // GameObject paintMenu = GameObject.Find("PaintMenu");
    // paintMenu.SetActive(true);
    // paintMenu.GetComponent<Painter>().objectToBePainted = gameObject;

    // }

    public Color ColorWheel(Color orderColor)
     {
        Color orderRed = Color.red;
        Color orderOrange = new Vector4(255, 165, 0, 1);
        Color orderYellow = Color.yellow;
        Color orderGreen = Color.green;
        Color orderBlue = Color.blue;
        Color orderIngdigo = new Vector4(75, 0, 130, 1);
        Color orderViolet = new Vector4(238, 130, 238, 1);
        return orderColor; 
    }
}
