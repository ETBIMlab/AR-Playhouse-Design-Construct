using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SurfaceChanger : MonoBehaviour
{
    public Renderer ren;
    public Material[] mat;
    public GameObject Floor;
    public Material RubberTexture;
    public Material SandTexture;
    public Material GrassTexture;
    public Material ConcreteTexture;
    public Material MulchTexture;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();


    // Use this for initialization
    void Start()
    {
        // global command
        keywords.Add("Change surface to Rubber", () => { this.BroadcastMessage("Rubber"); });
        keywords.Add("Change surface to Sand", () => { this.BroadcastMessage("Sand"); });
        keywords.Add("Change surface to Grass", () => { this.BroadcastMessage("Grass"); });
        keywords.Add("Change surface to Concrete", () => { this.BroadcastMessage("Concrete"); });
        keywords.Add("Change surface to Mulch", () => { this.BroadcastMessage("Mulch"); });


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
        if (args.text.Substring(18) == "Rubber")
        {
            Floor.GetComponent<MeshRenderer>().material = RubberTexture;
        }

        else if (args.text.Substring(18) == "Sand")
        {
            Floor.GetComponent<MeshRenderer>().material = SandTexture;
        }

        else if (args.text.Substring(18) == "Grass")
        {
            Floor.GetComponent<MeshRenderer>().material = GrassTexture;
        }

        else if (args.text.Substring(18) == "Concrete")
        {
            Floor.GetComponent<MeshRenderer>().material = ConcreteTexture;
        }

        else if (args.text.Substring(18) == "Mulch")
        {
            Floor.GetComponent<MeshRenderer>().material = MulchTexture;
        }
    }
    void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}