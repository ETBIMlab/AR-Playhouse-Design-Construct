using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class ObjectOrderer : MonoBehaviour
{

    //private int distFromCamera = 3;

    [Serializable]

    public struct OrderableObj
    {
        public string name;
        public double price;
        public int deliveryTime;
        public int instalTime;
        public double sustainability;
        public int fun;
        public GameObject obj;

    }
    public GameObject Floor;
    public Material RubberTexture;
    public Material SandTexture;
    public Material GrassTexture;
    public Material ConcreteTexture;
    public Material MulchTexture;
    bool didFunction = false;
    //string objName = "";
    public GameObject laptopinterface;
    private TestActivityLogger logger;
    // public TruckScript truckScriptReference;
    // TruckScript ts = new TruckScript();

    KeywordRecognizer keywordRecognizer = null;
    List<string> keywords = new List<string>();
    public OrderableObj[] orderableObjs; 
    
    //TruckScript truckScript = new TruckScript;

    List<string> numlist = new List<string>()  {
                        "zero","one","two","three","four","five","six","seven","eight","nine","ten","eleven","twelve"};



    //add the keywords, knows the name of each object in the structure. You iterate through it and instantiate it as a keyword
 
    void Start()
    {
        // GameObject[] itemObjects = GameObject.FindGameObjectsWithTag("item");
        // for (int i = 0; i < itemObjects.Length; i++) { Debug.Log(itemObjects[i].GetComponent<ItemInfo>().itemPrice); }
       
        for (int i = 0; i < orderableObjs.Length; i++)
        {
            keywords.Add("Order " + orderableObjs[i].name);

            keywords.Add("Order a dozen " + orderableObjs[i].name + "s");

            for (int j = 0; j < numlist.Count; j++)
            {
                keywords.Add("Order " + numlist[j] + " " + orderableObjs[i].name + "s");
            }
 
        }
        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
        
        //sam added
        logger = GetComponent<TestActivityLogger>();

        
    }

    //string objName;
    public void Update()
    {
 
    }

    //getting object name from the TruckScript once obj is in collision with truck
    public string GetObjName(string objName)
    {
        didFunction = true;
        Debug.Log(objName + " deleted from scene");
       // ReturnValues(objName);
        return objName;
    }


    public void ReturnValues(string objName)
    {
        for (int i = 0; i < orderableObjs.Length; i++)
        {
            if (orderableObjs[i].name.Equals(objName))
            {
                Debug.Log("found a match to delete");
            }
        }
    }
    //iterate through the Main Camera array on this script to find the objName match and remove price it from the laptop


    // important
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //call reference to laptop script 
        laptopInterface li = (laptopInterface)laptopinterface.GetComponent(typeof(laptopInterface));
        //looking for object after the order command
        string temp = args.text.Substring(args.text.LastIndexOf(" ") + 1);
        temp = temp.Substring(0, temp.Length - 1);
        for (int i = 0; i < orderableObjs.Length; i++)
        {
            if (orderableObjs[i].name.Equals(args.text.Substring(6))) //moving over the word, notifying, updating the laptop interface, adding it to the scene, and exporting the data
            {
                Debug.Log(orderableObjs[i].name + " ordered");
                li.additem(orderableObjs[i].price, orderableObjs[i].deliveryTime, orderableObjs[i].name, 1, orderableObjs[i].instalTime);
                AddObjectToScene(orderableObjs[i].obj);

                //Sam Added
                logger.ExportActivityLog(orderableObjs[i]);
                return;
            }
            else if (temp == orderableObjs[i].name)
            {
                string h = args.text.Substring(6);
                h = h.Substring(0, h.IndexOf(" "));
                Debug.Log(h);
                int hold = numlist.IndexOf(h);
                if (hold != -1) //ordered a custom amount of objects
                {
                    li.additem(orderableObjs[i].price, orderableObjs[i].deliveryTime, orderableObjs[i].name, hold, orderableObjs[i].instalTime);
                    for (int j = 0; j < hold; j++)
                    {
                        AddObjectToScene(orderableObjs[i].obj);
                        //Sam Added
                        logger.ExportActivityLog(orderableObjs[i]);
                    }
                }
                else if (h.Equals("a")) //order a dozen objects
                {
                    li.additem(orderableObjs[i].price, orderableObjs[i].deliveryTime, orderableObjs[i].name, 12, orderableObjs[i].instalTime);
                    for (int j = 0; j < 12; j++)
                    {
                        AddObjectToScene(orderableObjs[i].obj);
                        //Sam Added
                        logger.ExportActivityLog(orderableObjs[i]);
                    }
                }
                return;
            }
        }
    }

    public Boolean AddObjectToScene(string objectName)
    {
        for (int i = 0; i < orderableObjs.Length; i++)
        {
            if (orderableObjs[i].name == objectName)
            {
                Instantiate(orderableObjs[i].obj, new Vector3(0, 0, 0), Quaternion.identity);

                return true;
                //
            }
        }
        return false;
    }

    private void AddObjectToScene(GameObject newObj)
    {
        //the following line spawns the object in front of the user

        //get position of the ordering truck
        var orderPos = GameObject.Find("IndustrialSmallTruck").transform.position;
        Debug.Log("truck is at " + orderPos);
        //add surface changer if else here
        if (newObj.name == "Rubber Surface")
        {
            Debug.Log("Changing to rubber surface");
            Instantiate(newObj, new Vector3(orderPos.x - 0.1f, orderPos.y - .6f, orderPos.z - 3.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);

            Floor.GetComponent<MeshRenderer>().material = RubberTexture;
        }

        else if (newObj.name == "Sand Surface")
        {
            Debug.Log("Changing to sand surface");
            Instantiate(newObj, new Vector3(orderPos.x - 0.1f, orderPos.y - .6f, orderPos.z - 3.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);
            Floor.GetComponent<MeshRenderer>().material = SandTexture;
        }

        else if (newObj.name == "Grass Surface")
        {
            Debug.Log("Changing to grass surface");
            Instantiate(newObj, new Vector3(orderPos.x - 0.1f, orderPos.y - .6f, orderPos.z - 3.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);

            Floor.GetComponent<MeshRenderer>().material = GrassTexture;
        }

        else if (newObj.name == "Concrete Surface")
        {
            Debug.Log("Changing to concrete surface");
            Instantiate(newObj, new Vector3(orderPos.x - 0.1f, orderPos.y - .6f, orderPos.z - 3.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);

            Floor.GetComponent<MeshRenderer>().material = ConcreteTexture;
        }

        else if (newObj.name == "Mulch Surface")
        {
            Debug.Log("Changing to mulch surface");
            Instantiate(newObj, new Vector3(orderPos.x - 0.1f, orderPos.y - .6f, orderPos.z - 3.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);

            Floor.GetComponent<MeshRenderer>().material = MulchTexture;
        }
        else
        {
            Instantiate(newObj, new Vector3(orderPos.x - 0.1f, orderPos.y - .6f, orderPos.z - 3.6f), Quaternion.identity, GameObject.Find("EnvironmentContainer").transform);

        }
    }

}