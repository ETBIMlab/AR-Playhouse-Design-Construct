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
    //public Renderer ren;
   // public Material[] mat;
    public GameObject Floor;
    public Material RubberTexture;
    public Material SandTexture;
    public Material GrassTexture;
    public Material ConcreteTexture;
    public Material MulchTexture;
 
    public GameObject laptopinterface;

    KeywordRecognizer skeywordRecognizer = null;
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
        skeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        skeywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        skeywordRecognizer.Start();
    }

    // important
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        laptopInterface li = (laptopInterface)laptopinterface.GetComponent(typeof(laptopInterface));

        if (args.text.Substring(18) == "Rubber")
        {
            li.additem(2025.00, 0, "Rubber Floor", 1, 3);

            Floor.GetComponent<MeshRenderer>().material = RubberTexture;
        }

        else if (args.text.Substring(18) == "Sand")
        {
            li.additem(1425.00, 0, "Sand Floor", 1, 2);

            Floor.GetComponent<MeshRenderer>().material = SandTexture;
        }

        else if (args.text.Substring(18) == "Grass")
        {
            li.additem(0.00, 0, "Grass Floor", 1, 0);

            Floor.GetComponent<MeshRenderer>().material = GrassTexture;
        }

        else if (args.text.Substring(18) == "Concrete")
        {
            li.additem(5325.00, 0, "Concrete Floor", 1, 5);

            Floor.GetComponent<MeshRenderer>().material = ConcreteTexture;
        }

        else if (args.text.Substring(18) == "Mulch")
        {
            li.additem(1275.00, 0, "Mulch Floor", 1, 2);

            Floor.GetComponent<MeshRenderer>().material = MulchTexture;
        }
    }
    
}