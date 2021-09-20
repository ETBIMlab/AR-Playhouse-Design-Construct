using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Windows.Speech;
/// <summary>
/// RETIRED
/// </summary>
public class SurfaceChanger : MonoBehaviour
{
    /*//public Renderer ren;
    // public Material[] mat;
    public GameObject Floor;
    public Material RubberTexture;
    public Material SandTexture;
    public Material GrassTexture;
    public Material ConcreteTexture;
    public Material MulchTexture;

    public GameObject laptopinterface;
    private TestActivityLogger logger;

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
 /*   public OrderableObj buildAnObject(String name, double price, int deliveryTime, int installTime, double sustainabilityFactor, int funFactor, GameObject floorGameObject)
    {
            public string name = this.name;
            public double price = this.price;
            public int deliveryTime = this.deliveryTime;
            public int instalTime = installTime;
            public double sustainability = sustainabilityFactor;
            public int fun = funFactor;
            public GameObject obj = floorGameObject;
            return OrderableObj
        }*/
        
}