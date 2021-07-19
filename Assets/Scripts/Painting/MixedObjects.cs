using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MixedObjects : MonoBehaviour
{
    /// <summary>
    /// so these three objects you can't grab the different components like you can with the bell prefab (based on how they were uploaded)
    /// We need to be able to order them in different colors so I made prefabs in the standard colors: Red, Yellow, Blue, and Green
    /// This script is going to allow users to order them in different colors but it's different than OrderInColor by inidividually assigning each command to an object, not an object in a color.
    /// Default is yellow but we still need the commands in there. 
    /// </summary>
    //Tube Bridge
    public GameObject AssignToYellowTubeBridge;
    public GameObject AssignToRedTubeBridge;
    public GameObject AssignToGreenTubeBridge;
    public GameObject AssignToBlueTubeBridge;
    //Small Chimes: Chimes Panel 5' by 3'6in 
    public GameObject AssignToYellowSmallChimes;
    public GameObject AssignToRedSmallChimes;
    public GameObject AssignToGreenSmallChimes;
    public GameObject AssignToBlueSmallChimes;
    //Large Chimes: Chimes Panel 5' by 5'4in
    public GameObject AssignToYellowBigChimes;
    public GameObject AssignToRedBigChimes;
    public GameObject AssignToGreenBigChimes;
    public GameObject AssignToBlueBigChimes;

    public GameObject objectOrdered;
    //voice command variables
    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    // Start is called before the first frame update
    void Start()
    {
       //red
            keywords.Add("Order Chimes Panel 5' by 3'6in in Red", () => { this.BroadcastMessage("Red"); });
            keywords.Add("Order Chimes Panel 5' by 5'4in in Red", () => { this.BroadcastMessage("Red"); });
            keywords.Add("Order Tube Bridge in Red", () => { this.BroadcastMessage("Red"); });
        //yellow
        keywords.Add("Order Chimes Panel 5' by 3'6in", () => { this.BroadcastMessage("Yellow"); });
        keywords.Add("Order Chimes Panel 5' by 5'4in", () => { this.BroadcastMessage("Yellow"); });
        keywords.Add("Order Tube Bridge", () => { this.BroadcastMessage("Yellow"); });
        //green
        keywords.Add("Order Chimes Panel 5' by 3'6in in Green", () => { this.BroadcastMessage("Green"); });
        keywords.Add("Order Chimes Panel 5' by 5'4in in Green", () => { this.BroadcastMessage("Green"); });
        keywords.Add("Order Tube Bridge in Green", () => { this.BroadcastMessage("Green"); });
        //Blue
        keywords.Add("Order Chimes Panel 5' by 3'6in in Blue", () => { this.BroadcastMessage("Blue"); });
        keywords.Add("Order Chimes Panel 5' by 5'4in in Blue", () => { this.BroadcastMessage("Blue"); });
        keywords.Add("Order Tube Bridge in Blue", () => { this.BroadcastMessage("Blue"); });
        
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
        Debug.Log("Command Received" + objectOrdered.name);
        
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Action keywordAction;
        //change the color based on the color we want
        if (args.text.Substring(18) == "Red")
        {
            if(objectOrdered.GetComponent<IsWoodOrPlastic>().isTubeBridge == true)
            {
                objectOrdered = AssignToRedTubeBridge;
            }
            else if(objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesSmall == true)
            {
                objectOrdered = AssignToRedSmallChimes;
            }
            else if (objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesLarge == true)
            {
                objectOrdered = AssignToRedBigChimes;
            }
            Debug.Log("Color changed to red");

        }
      
        else if (args.text.Substring(18) == "Yellow")
        {
            if (objectOrdered.GetComponent<IsWoodOrPlastic>().isTubeBridge == true)
            {
                objectOrdered = AssignToYellowTubeBridge;
            }
            else if (objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesSmall == true)
            {
                objectOrdered = AssignToYellowSmallChimes;
            }
            else if (objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesLarge == true)
            {
                objectOrdered = AssignToYellowBigChimes;
            }
            Debug.Log("Ordered default yellow");

        }
        else if (args.text.Substring(18) == "Green")
        {
            if (objectOrdered.GetComponent<IsWoodOrPlastic>().isTubeBridge == true)
            {
                objectOrdered = AssignToGreenTubeBridge;
            }
            else if (objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesSmall == true)
            {
                objectOrdered = AssignToGreenSmallChimes;
            }
            else if (objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesLarge == true)
            {
                objectOrdered = AssignToGreenBigChimes;
            }
            Debug.Log("Color changed to green");

        }
        else if (args.text.Substring(18) == "Blue")
        {
            if (objectOrdered.GetComponent<IsWoodOrPlastic>().isTubeBridge == true)
            {
                objectOrdered = AssignToBlueTubeBridge;
            }
            else if (objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesSmall == true)
            {
                objectOrdered = AssignToBlueSmallChimes;
            }
            else if (objectOrdered.GetComponent<IsWoodOrPlastic>().isChimesLarge == true)
            {
                objectOrdered = AssignToBlueBigChimes;
            }
            Debug.Log("Color changed to blue");

        }
        //the following is from ObjectOrderer, from my understanding it loads the object into the truck
        var orderPos = GameObject.Find("IndustrialSmallTruck").transform.position;
        Debug.Log("truck is at " + orderPos);
        Instantiate(objectOrdered, new Vector3(orderPos.x + 1.24f, orderPos.y, orderPos.z - 2.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);
    }
}
