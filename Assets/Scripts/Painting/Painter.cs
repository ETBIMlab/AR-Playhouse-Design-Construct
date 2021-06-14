using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Painter : MonoBehaviour
{
    public GameObject objectToBePainted;
    public Material material;
    public GameObject AssignToBrush;
    public bool colorPicked = false;
    private bool toolGrabbed = false;
    public Color currentColor;
    public Color objCurrentColor;
    public Material[] mat;
    public Renderer ren;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Start is called before the first frame update
   /* void Start()
    {
        keywords.Add("Paint surface Red", () => { this.BroadcastMessage("Red"); });
        keywords.Add("Paint surface Orange", () => { this.BroadcastMessage("Orange"); });
        keywords.Add("Paint surface Yellow", () => { this.BroadcastMessage("Yellow"); });
        keywords.Add("Paint surface Green", () => { this.BroadcastMessage("Green"); });
        keywords.Add("Paint surface Blue", () => { this.BroadcastMessage("Blue"); });
        keywords.Add("Paint surface Indigo", () => { this.BroadcastMessage("Indigo"); });
        keywords.Add("Paint surface Purple", () => { this.BroadcastMessage("Purple"); });


        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
         
    }*/

    public void OnGrab()
    {
        toolGrabbed = true;
    }


    public void PaintObject(Material paint)
    {
        Debug.Log("Painter trying to paint");
        if (objectToBePainted != null && objectToBePainted.GetComponent<Paintable>() != null && toolGrabbed == true)
        {
            //objectToBePainted.GetComponent<Painter>().KeywordRecognizer_OnPhraseRecognized();
        }
        else if (objectToBePainted == null)
        {
            Debug.Log("Object to be painted is null");
        }
        else
        {
            Debug.Log("Paintable is null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Paintable>() != null && colorPicked == true && toolGrabbed == true)
        {
            other.GetComponent<MeshRenderer>().material = material;
        }

        else if (other.GetComponent<isPaintBucket>() != null && toolGrabbed == true)
        {
            colorPicked = true;
            material = other.GetComponent<isPaintBucket>().material;
            ren = AssignToBrush.GetComponent<Renderer>();
            mat = ren.materials;
            mat[3] = material;
            AssignToBrush.GetComponent<MeshRenderer>().materials = mat;
        }
    }

    public void OnRelease()
    {
        toolGrabbed = false;
    }

    // Update is called once per frame
    /* private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
     {
         System.Action keywordAction;

         if (objCurrentColor != currentColor)
         {
             if (args.text.Substring(18) == "Red")
             {
                 ren.AssignToBrush.SetColor = ("_Color", Color.red);

             }
             if (args.text.Substring(18) == "Orange")
             {
                 ren.AssignToBrush.SetColor = ("_Color", (255, 165, 0));

             }
             if (args.text.Substring(18) == "Yellow")
             {
                 ren.AssignToBrush.SetColor = ("_Color", Color.yellow);

             }
             if (args.text.Substring(18) == "Green")
             {
                 ren.AssignToBrush.SetColor = ("_Color", Color.green);

             }
             if (args.text.Substring(18) == "Blue")
             {
                 ren.AssignToBrush.SetColor = ("_Color", Color.blue);

             }
             if (args.text.Substring(18) == "Indigo")
             {
                 ren.AssignToBrush.SetColor = ("_Color", (75, 0, 130));

             }
             if (args.text.Substring(18) == "Violet")
             {
                 ren.AssignToBrush.SetColor = ("_Color", (238, 130, 238));

             }

         }
     }*/
}
