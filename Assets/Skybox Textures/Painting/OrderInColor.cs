using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
/// <summary>
/// RETIRED
/// </summary>
public class OrderInColor : MonoBehaviour
{ }
 /*
    //Tube Bridge
    //public GameObject AssignToYellowTubeBridge;
    private GameObject AssignToRedTubeBridge;
    private GameObject AssignToGreenTubeBridge;
    private GameObject AssignToBlueTubeBridge;
    //Small Chimes: Chimes Panel 5' by 3'6in 
    //public GameObject AssignToYellowSmallChimes;
    private GameObject AssignToRedSmallChimes;
    private GameObject AssignToGreenSmallChimes;
    private GameObject AssignToBlueSmallChimes;
    //Large Chimes: Chimes Panel 5' by 5'4in
    // public GameObject AssignToYellowBigChimes;
    private GameObject AssignToRedBigChimes;
    private GameObject AssignToGreenBigChimes;
    private GameObject AssignToBlueBigChimes;
    //Single Slide: 
    // public GameObject AssignToYellowSingleSlide;
    private GameObject AssignToRedSingleSlide;
    private GameObject AssignToGreenSingleSlide;
    private GameObject AssignToBlueSingleSlide;
    //Double Slide
    // public GameObject AssignToYellowDoubleSlide;
    private GameObject AssignToRedDoubleSlide;
    private GameObject AssignToGreenDoubleSlide;
    private GameObject AssignToBlueDoubleSlide;
    //Firepole
    // public GameObject AssignToYellowFirepole;
    private GameObject AssignToRedFirepole;
    private GameObject AssignToGreenFirepole;
    private GameObject AssignToBlueFirepole;
    //Coil Climber
    // public GameObject AssignToYellowCoilClimber;
    private GameObject AssignToRedCoilClimber;
    private GameObject AssignToGreenCoilClimber;
    private GameObject AssignToBlueCoilClimber;
    //Stairs
    // public GameObject AssignToYellowStairs;
    private GameObject AssignToRedStairs;
    private GameObject AssignToGreenStairs;
    private GameObject AssignToBlueStairs;
    
    public GameObject objectOrdered;
    //voice command variables
    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    void Awake()
    {
      // AssignToYellowTubeBridge = GameObject.Find("Tube Bridge");
        AssignToRedTubeBridge = GameObject.Find("Tube Bridge Red");
        AssignToGreenTubeBridge = GameObject.Find("Tube Bridge Green");
        AssignToBlueTubeBridge = GameObject.Find("Tube Bridge Blue");
        AssignToBlueTubeBridge = GameObject.Find("Tube Bridge Blue");
       // AssignToYellowSmallChimes = GameObject.Find("Chimes Panel 5' by 3'6in");
        AssignToRedSmallChimes = GameObject.Find("Chimes Panel 5' by 3'6in Red");
        AssignToGreenSmallChimes = GameObject.Find("Chimes Panel 5' by 3'6in Green");
        AssignToBlueSmallChimes = GameObject.Find("Chimes Panel 5' by 3'6in Blue");
        //AssignToYellowBigChimes = GameObject.Find("Chimes Panel 5' by 5'4in");
        AssignToRedBigChimes = GameObject.Find("Chimes Panel 5' by 5'4in Red");
        AssignToGreenBigChimes = GameObject.Find("Chimes Panel 5' by 5'4in Green");
        AssignToBlueBigChimes = GameObject.Find("Chimes Panel 5' by 5'4in Blue");
        //AssignToYellowSingleSlide = GameObject.Find("Single Slide");
        AssignToRedSingleSlide = GameObject.Find("Single Slide Red");
        AssignToGreenSingleSlide = GameObject.Find("Single Slide Green");
        AssignToBlueSingleSlide = GameObject.Find("Single Slide Blue");
        //AssignToYellowDoubleSlide = GameObject.Find("DoubleSlideYellow");
        AssignToRedDoubleSlide = GameObject.Find("Double Slide Red");
        AssignToGreenDoubleSlide = GameObject.Find("Double Slide Green");
        AssignToBlueDoubleSlide = GameObject.Find("Double Slide Blue");
       // AssignToYellowFirepole = GameObject.Find("Firepole");
        AssignToRedFirepole = GameObject.Find("Firepole Red");
        AssignToGreenFirepole = GameObject.Find("Firepole Green");
        AssignToBlueFirepole = GameObject.Find("Firepole Blue 1");
        //AssignToYellowCoilClimber = GameObject.Find("Sprial Climber");
        AssignToRedCoilClimber = GameObject.Find("Sprial Climber Red");
        AssignToGreenCoilClimber = GameObject.Find("Sprial Climber Green");
        AssignToBlueCoilClimber = GameObject.Find("Sprial Climber Blue");
       // AssignToYellowStairs = GameObject.Find("Stair A (Fixed)");
        AssignToRedStairs = GameObject.Find("Stair A (Fixed) Red");
        AssignToGreenStairs = GameObject.Find("Stair A (Fixed) Green");
        AssignToBlueStairs = GameObject.Find("Stair A (Fixed) Blue");
    }
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
       // keywords.Add("Order Chimes Panel 5' by 3'6in", () => { this.BroadcastMessage("SmallChimesYellow"); });
       // keywords.Add("Order Chimes Panel 5' by 5'4in", () => { this.BroadcastMessage("BigChimesYellow"); });
     //   keywords.Add("Order Tube Bridge", () => { this.BroadcastMessage("TubeBridgeYellow"); });
     //   keywords.Add("Order Single Slide", () => { this.BroadcastMessage("SingleSlideYellow"); });
      //  keywords.Add("Order Double Slide", () => { this.BroadcastMessage("DoubleSlideYellow"); });
        //keywords.Add("Order Tube Slide", () => { this.BroadcastMessage("TubeSlideYellow"); });
      //  keywords.Add("Order Firepole", () => { this.BroadcastMessage("FirepoleYellow"); });
      //  keywords.Add("Order Coil Climber", () => { this.BroadcastMessage("CoilClimberYellow"); });
      //  keywords.Add("Order Stairs", () => { this.BroadcastMessage("StairsYellow"); });
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
        /*
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
    void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}
*/