using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
/// <summary>
/// Hi, so welcome to stage 2 of painting. We have IsWoodOrPlastic which will sort items into items made of wood (paint w/ paintbrush), plastic (orderable in color)
/// metal (no touchy with color), and paintbuckets (need the color for paint). Paintable is stage 1 where it is supposed to paint wood if you are holding a paintbrush.
/// this script, as its name implies, is supposed to let you order objects in color (for the ones that can). I looked at ObjectOrderer and wanted to try to make something
/// simplier so here we are. The code is more based off of SurfaceChanger bc it seemed like a simpler solution. 
/// </summary>
public class OrderInColor : MonoBehaviour
{
  public Color objCurrentColor; //much like in Painter, we  need to render that so we can change it.
  //Color Wheel bc ROYGBIV isn't completely built into unity
    Color orderRed = Color.red;
    Color orderOrange = new Vector4(255, 165, 0, 1);
    Color orderYellow = Color.yellow;
    Color orderGreen = Color.green;
    Color orderBlue = Color.blue;
    Color orderIngdigo = new Vector4(75, 0, 130, 1);
    Color orderViolet = new Vector4(238, 130, 238, 1);
    Color orderGrey = Color.grey;
    Color orderWhite = Color.white;

    public GameObject objectOrdered;
    Renderer objectRen; //renderer for the object
    //voice command variables
    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    void Start()
     {
        //objectRen = objectOrdered.GetComponent<Renderer>();
        if (objectOrdered.GetComponent<IsWoodOrPlastic>().orderableInColor == true) //we only want the plastic stuff to get different colors
        {
            keywords.Add("Order " + objectOrdered.name + "in Red", () => { this.BroadcastMessage("Red"); });
            keywords.Add("Order " + objectOrdered.name + "in Orange", () => { this.BroadcastMessage("Orange"); });
            keywords.Add("Order " + objectOrdered.name + "in Yellow", () => { this.BroadcastMessage("Yellow"); });
            keywords.Add("Order " + objectOrdered.name + "in Green", () => { this.BroadcastMessage("Green"); });
            keywords.Add("Order " + objectOrdered.name + "in Blue", () => { this.BroadcastMessage("Blue"); });
            keywords.Add("Order " + objectOrdered.name + "in Indigo", () => { this.BroadcastMessage("Indigo"); });
            keywords.Add("Order " + objectOrdered.name + "in Violet", () => { this.BroadcastMessage("Violet"); });

            keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
            keywordRecognizer.Start();
            Debug.Log("Command Received" + objectOrdered.name);
        }
     }


    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
     Action keywordAction;
        //change the color based on the color we want
        if (args.text.Substring(18) == "Red")
        {
            objectOrdered.GetComponent<Renderer>().material.color = orderRed;
            Debug.Log("Color changed to red");

        }
        else if (args.text.Substring(18) == "Orange")
        {
            objectOrdered.GetComponent<Renderer>().material.color = orderOrange;
            Debug.Log("Color changed to orange");

        }
        else if (args.text.Substring(18) == "Yellow")
        {
            objectOrdered.GetComponent<Renderer>().material.color = orderYellow;
            Debug.Log("Color changed to yellow");

        }
        else if (args.text.Substring(18) == "Green")
        {
            objectOrdered.GetComponent<Renderer>().material.color = orderGreen;
            Debug.Log("Color changed to green");

        }
        else if (args.text.Substring(18) == "Blue")
        {
            objectOrdered.GetComponent<Renderer>().material.color = orderBlue;
            Debug.Log("Color changed to blue");

        }
        else if (args.text.Substring(18) == "Indigo")
        {
            objectOrdered.GetComponent<Renderer>().material.color = orderIngdigo;
            Debug.Log("Color changed to indigo");

        }
        else if (args.text.Substring(18) == "Violet")
        {
            objectOrdered.GetComponent<Renderer>().material.color = orderViolet;
            Debug.Log("Color changed to violet");

        }
        //the following is from ObjectOrderer, from my understanding it loads the object into the truck
        var orderPos = GameObject.Find("IndustrialSmallTruck").transform.position;
        Debug.Log("truck is at " + orderPos);
        Instantiate(objectOrdered, new Vector3(orderPos.x + 1.24f, orderPos.y, orderPos.z - 2.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);
    }

}
