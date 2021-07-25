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
    public GameObject AssignToRedTubeBridge; //check
    public GameObject AssignToGreenTubeBridge;
    public GameObject AssignToBlueTubeBridge;
    //Small Chimes: Chimes Panel 5' by 3'6in 
    public GameObject AssignToYellowSmallChimes;
    public GameObject AssignToRedSmallChimes; // :)
    public GameObject AssignToGreenSmallChimes;
    public GameObject AssignToBlueSmallChimes;
    //Large Chimes: Chimes Panel 5' by 5'4in
    public GameObject AssignToYellowBigChimes;
    public GameObject AssignToRedBigChimes; //:)
    public GameObject AssignToGreenBigChimes;
    public GameObject AssignToBlueBigChimes;
    //Single Slide: 
    public GameObject AssignToYellowSingleSlide;
    public GameObject AssignToRedSingleSlide;
    public GameObject AssignToGreenSingleSlide;
    public GameObject AssignToBlueSingleSlide;
    //Double Slide
    public GameObject AssignToYellowDoubleSlide;
    public GameObject AssignToRedDoubleSlide;
    public GameObject AssignToGreenDoubleSlide;
    public GameObject AssignToBlueDoubleSlide;
    //Firepole
    public GameObject AssignToYellowFirepole;
    public GameObject AssignToRedFirepole;
    public GameObject AssignToGreenFirepole;
    public GameObject AssignToBlueFirepole;
    //Coil Climber
    public GameObject AssignToYellowCoilClimber;
    public GameObject AssignToRedCoilClimber;
    public GameObject AssignToGreenCoilClimber;
    public GameObject AssignToBlueCoilClimber;
    //Stairs
    public GameObject AssignToYellowStairs;
    public GameObject AssignToRedStairs;
    public GameObject AssignToGreenStairs;
    public GameObject AssignToBlueStairs;


    public GameObject objectOrdered;
    //voice command variables
    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    // Start is called before the first frame update
    void Start()
    {
       //red
        keywords.Add("Order Chimes Panel 5' by 3'6in in Red", () => { this.BroadcastMessage("SmallChimesRed"); });
        keywords.Add("Order Chimes Panel 5' by 5'4in in Red", () => { this.BroadcastMessage("BigChimesRed"); });
        keywords.Add("Order Tube Bridge in Red", () => { this.BroadcastMessage("TubeBridgeRed"); });
        keywords.Add("Order Single Slide in Red", () => { this.BroadcastMessage("SingleSlideRed"); });
        keywords.Add("Order Double Slide in Red", () => { this.BroadcastMessage("DoubleSlideRed"); });
        //keywords.Add("Order Tube Slide in Red", () => { this.BroadcastMessage("TubeSlideRed"); });
        keywords.Add("Order Firepole in Red", () => { this.BroadcastMessage("FirepoleRed"); });
        keywords.Add("Order Coil Climber in Red", () => { this.BroadcastMessage("CoilClimberRed"); });
        keywords.Add("Order Stairs in Red", () => { this.BroadcastMessage("StairsRed"); });

        //yellow
        keywords.Add("Order Chimes Panel 5' by 3'6in", () => { this.BroadcastMessage("SmallChimesYellow"); });
        keywords.Add("Order Chimes Panel 5' by 5'4in", () => { this.BroadcastMessage("BigChimesYellow"); });
        keywords.Add("Order Tube Bridge", () => { this.BroadcastMessage("TubeBridgeYellow"); });
        keywords.Add("Order Single Slide", () => { this.BroadcastMessage("SingleSlideYellow"); });
        keywords.Add("Order Double Slide", () => { this.BroadcastMessage("DoubleSlideYellow"); });
        //keywords.Add("Order Tube Slide", () => { this.BroadcastMessage("TubeSlideYellow"); });
        keywords.Add("Order Firepole", () => { this.BroadcastMessage("FirepoleYellow"); });
        keywords.Add("Order Coil Climber", () => { this.BroadcastMessage("CoilClimberYellow"); });
        keywords.Add("Order Stairs", () => { this.BroadcastMessage("StairsYellow"); });
        //green
        keywords.Add("Order Chimes Panel 5' by 3'6in in Green", () => { this.BroadcastMessage("SmallChimesGreen"); });
        keywords.Add("Order Chimes Panel 5' by 5'4in in Green", () => { this.BroadcastMessage("BigChimesGreen"); });
        keywords.Add("Order Tube Bridge in Green", () => { this.BroadcastMessage("TubeBridgeGreen"); });
        keywords.Add("Order Single Slide in Green", () => { this.BroadcastMessage("SingleSlideGreen"); });
        keywords.Add("Order Double Slide in Green", () => { this.BroadcastMessage("DoubleSlideGreen"); });
        //keywords.Add("Order Tube Slide in Green", () => { this.BroadcastMessage("TubeSlideGreen"); });
        keywords.Add("Order Firepole in Green", () => { this.BroadcastMessage("FirepoleGreen"); });
        keywords.Add("Order Coil Climber in Green", () => { this.BroadcastMessage("CoilClimberGreen"); });
        keywords.Add("Order Stairs in Green", () => { this.BroadcastMessage("StairsGreen"); });
        //Blue
        keywords.Add("Order Chimes Panel 5' by 3'6in in Blue", () => { this.BroadcastMessage("SmallChimesBlue"); });
        keywords.Add("Order Chimes Panel 5' by 5'4in in Blue", () => { this.BroadcastMessage("BigChimesBlue"); });
        keywords.Add("Order Tube Bridge in Blue", () => { this.BroadcastMessage("TubeBridgeBlue"); });
        keywords.Add("Order Single Slide in Blue", () => { this.BroadcastMessage("SingleSlideBlue"); });
        keywords.Add("Order Double Slide in Blue", () => { this.BroadcastMessage("DoubleSlideBlue"); });
       // keywords.Add("Order Tube Slide in Blue", () => { this.BroadcastMessage("TubeSlideBlue"); });
        keywords.Add("Order Firepole in Blue", () => { this.BroadcastMessage("FirepoleBlue"); });
        keywords.Add("Order Coil Climber in Blue", () => { this.BroadcastMessage("CoilClimberBlue"); });
        keywords.Add("Order Stairs in Blue", () => { this.BroadcastMessage("StairsBlue"); });
        
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
        Debug.Log("Command Received" + objectOrdered.name);
        
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Action keywordAction;
        //change the color based on the color we want
        //red
        if (args.text.Substring(32) == "SmallChimesRed")
        {
            objectOrdered = AssignToRedSmallChimes;
            Debug.Log("Red Small Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "BigChimesRed")
        {
            objectOrdered = AssignToRedBigChimes;
            Debug.Log("Red Big Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "TubeBridgeRed")
        {
            objectOrdered = AssignToRedTubeBridge;
            Debug.Log("Red Tube Bridge Ordered!");
        }
        else if (args.text.Substring(32) == "SingleSlideRed")
        {
            objectOrdered = AssignToRedSingleSlide;
            Debug.Log("Red Slide Ordered!");
        }
        else if (args.text.Substring(32) == "DoubleSlideRed")
        {
            objectOrdered = AssignToRedDoubleSlide;
            Debug.Log("Red Double Slide Ordered!");
        }
        else if (args.text.Substring(32) == "FirepoleRed")
        {
            objectOrdered = AssignToRedFirepole;
            Debug.Log("Red Firepole Ordered!");
        }
        else if (args.text.Substring(32) == "CoilClimberRed")
        {
            objectOrdered = AssignToRedCoilClimber;
            Debug.Log("Red Coil Climber Ordered!");
        }
        //yellow
        if (args.text.Substring(32) == "SmallChimesYellow")
        {
            objectOrdered = AssignToYellowSmallChimes;
            Debug.Log("Yellow Small Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "BigChimesYellow")
        {
            objectOrdered = AssignToYellowBigChimes;
            Debug.Log("Yellow Big Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "TubeBridgeYellow")
        {
            objectOrdered = AssignToYellowTubeBridge;
            Debug.Log("Yellow Tube Bridge Ordered!");
        }
        else if (args.text.Substring(32) == "SingleSlideYellow")
        {
            objectOrdered = AssignToYellowSingleSlide;
            Debug.Log("Yellow Slide Ordered!");
        }
        else if (args.text.Substring(32) == "DoubleSlideYellow")
        {
            objectOrdered = AssignToYellowDoubleSlide;
            Debug.Log("Yellow Double Slide Ordered!");
        }
        else if (args.text.Substring(32) == "FirepoleYellow")
        {
            objectOrdered = AssignToYellowFirepole;
            Debug.Log("Yellow Firepole Ordered!");
        }
        else if (args.text.Substring(32) == "CoilClimberYellow")
        {
            objectOrdered = AssignToYellowCoilClimber;
            Debug.Log("Yellow Coil Climber Ordered!");
        }
        //green
        if (args.text.Substring(32) == "SmallChimesGreen")
        {
            objectOrdered = AssignToGreenSmallChimes;
            Debug.Log("Green Small Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "BigChimesGreen")
        {
            objectOrdered = AssignToGreenBigChimes;
            Debug.Log("Green Big Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "TubeBridgeGreen")
        {
            objectOrdered = AssignToGreenTubeBridge;
            Debug.Log("Green Tube Bridge Ordered!");
        }
        else if (args.text.Substring(32) == "SingleSlideGreen")
        {
            objectOrdered = AssignToGreenSingleSlide;
            Debug.Log("Green Slide Ordered!");
        }
        else if (args.text.Substring(32) == "DoubleSlideGreen")
        {
            objectOrdered = AssignToGreenDoubleSlide;
            Debug.Log("Green Double Slide Ordered!");
        }
        else if (args.text.Substring(32) == "FirepoleGreen")
        {
            objectOrdered = AssignToGreenFirepole;
            Debug.Log("Green Firepole Ordered!");
        }
        else if (args.text.Substring(32) == "CoilClimberGreen")
        {
            objectOrdered = AssignToGreenCoilClimber;
            Debug.Log("Green Coil Climber Ordered!");
        }
        //blue
        if (args.text.Substring(32) == "SmallChimesBlue")
        {
            objectOrdered = AssignToBlueSmallChimes;
            Debug.Log("Blue Small Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "BigChimesBlue")
        {
            objectOrdered = AssignToBlueBigChimes;
            Debug.Log("Blue Big Chimes Ordered!");
        }
        else if (args.text.Substring(32) == "TubeBridgeBlue")
        {
            objectOrdered = AssignToBlueTubeBridge;
            Debug.Log("Blue Tube Bridge Ordered!");
        }
        else if (args.text.Substring(32) == "SingleSlideBlue")
        {
            objectOrdered = AssignToBlueSingleSlide;
            Debug.Log("Blue Slide Ordered!");
        }
        else if (args.text.Substring(32) == "DoubleSlideBlue")
        {
            objectOrdered = AssignToBlueDoubleSlide;
            Debug.Log("Blue Double Slide Ordered!");
        }
        else if (args.text.Substring(32) == "FirepoleBlue")
        {
            objectOrdered = AssignToBlueFirepole;
            Debug.Log("Blue Firepole Ordered!");
        }
        else if (args.text.Substring(32) == "CoilClimberBlue")
        {
            objectOrdered = AssignToBlueCoilClimber;
            Debug.Log("Blue Coil Climber Ordered!");
        }
        //the following is from ObjectOrderer, from my understanding it loads the object into the truck
        var orderPos = GameObject.Find("IndustrialSmallTruck").transform.position;
        Debug.Log("truck is at " + orderPos);
        Instantiate(objectOrdered, new Vector3(orderPos.x + 1.24f, orderPos.y, orderPos.z - 2.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);
    }
}
