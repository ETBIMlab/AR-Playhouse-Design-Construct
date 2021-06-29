using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Paintable : MonoBehaviour
{
  public string name;
  public double price;
  public int deliveryTime;
  public int installTime;
  public Color objCurrentColor;
  //Color Wheel bc ROYGBIV isn't completely built into unity
    Color orderRed = Color.red;
    Color orderOrange = new Vector4(255, 165, 0, 1);
    Color orderYellow = Color.yellow;
    Color orderGreen = Color.green;
    Color orderBlue = Color.blue;
    Color orderIngdigo = new Vector4(75, 0, 130, 1);
    Color orderViolet = new Vector4(238, 130, 238, 1);
    public GameObject objectOrdered; 
    Renderer objectRen; //renderer for the object
    //voice command variables
    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    // Start is called before the first frame update

    void Start()
     {
        objectRen = objectOrdered.GetComponent<Renderer>();
        keywords.Add("Order" + objectOrdered.name + "in Red", () => { this.BroadcastMessage("Red"); });
        keywords.Add("Order" + objectOrdered.name + "in Orange", () => { this.BroadcastMessage("Orange"); });
        keywords.Add("Order" + objectOrdered.name + "in Yellow", () => { this.BroadcastMessage("Yellow"); });
        keywords.Add("Order" + objectOrdered.name + "in Green", () => { this.BroadcastMessage("Green"); });
        keywords.Add("Order" + objectOrdered.name + "in Blue", () => { this.BroadcastMessage("Blue"); });
        keywords.Add("Order" + objectOrdered.name + "in Indigo", () => { this.BroadcastMessage("Indigo"); });
        keywords.Add("Order" + objectOrdered.name + "in Violet", () => { this.BroadcastMessage("Violet"); });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
     }


    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
     Action keywordAction;
        if (args.text.Substring(18) == "Red")
        {
            objectRen.material.color = orderRed;
        }
        else if (args.text.Substring(18) == "Orange")
        {
            objectRen.material.color = orderOrange;
        }
        else if (args.text.Substring(18) == "Yellow")
        {
            objectRen.material.color = orderYellow;
        }
        else if (args.text.Substring(18) == "Green")
        {
            objectRen.material.color = orderGreen;
        }
        else if (args.text.Substring(18) == "Blue")
        {
            objectRen.material.color = orderBlue;
        }
        else if (args.text.Substring(18) == "Indigo")
        {
            objectRen.material.color = orderIngdigo;
        }
        else if (args.text.Substring(18) == "Violet")
        {
            objectRen.material.color = orderViolet;
        }
    }

}
