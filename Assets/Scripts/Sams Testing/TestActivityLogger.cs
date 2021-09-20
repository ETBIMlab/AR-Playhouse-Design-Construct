using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine;
using System.Collections;
using TMPro;
using static ObjectOrderer;
using static TruckScript;
using Microsoft.MixedReality.Toolkit;

#if WINDOWS_UWP
using Windows.Storage;
using Windows.System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
#endif

// in the windows device portal (enter ip4 adddress from network hololens is using into search bar) 
// Locate the data at User Folders > LocalAppData > Template3D_1.0.0.0_arm64__pzq3xp76mxafg > LocalState > activitylog.txt or the timestamp  
//Template3D is the playhouse 
// there are two files being created, there are two functions currently being called at the same time through verbal command export activity log.

public class TestActivityLogger : MonoBehaviour
    {
    //this string gets added to the txt file printed
    private string saveInformation = "";

    // Creates a timestamp.txt file to append to.
    private static string timeStamp = System.DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
    private static string timeStamp1 = System.DateTime.Now.ToString();
    private static string fileName = timeStamp + ".txt";
  
    private static bool firstSave = true;
    private static double totalCost = 0.0d;
    private static int totalTime = 0;
    private static int totalFun = 0;
    private static double avgSus = 0.0d;
    private static int objectCount = 0;
    
    public float capturePositionWaitTime = 1.0f; // changes how many vector 3 locations are printed, lower time = more locations.
    private float nextTime = 0.0f;
    private string positions = "";
 
    public string path;
    public string path2;
    // gets a handle on the applications localfolder to save to.
    // uses async function to wait
    // then creates the txt file and appends saveInformation string


    void Start()
    {
    // for testing in hololens 
   //path = Application.persistentDataPath + "/ActivityLog.txt";
    // for testing on computer  file should show up in capstone folder
     path = "./ActivityLog.txt";

     path2 = Application.persistentDataPath + "/PositionLog.txt";
     //string path2 = "/PositionLog.txt";
    }



#if WINDOWS_UWP

    Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
    Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
     async void WriteData()
    {
        if (firstSave){
        StorageFile localFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        await FileIO.AppendTextAsync(localFile, saveInformation + "\r\n");
        firstSave = false;
        }
    else{
        StorageFile localFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
        await FileIO.AppendTextAsync(localFile, saveInformation + "\r\n");
        }
    }
#endif





    // function is called in Object orderer when an item is ordered. This logs the attributes of the objects ordered.
    public void ExportActivityLog(OrderableObj obj)
    {
        totalCost += obj.price;
        totalTime += obj.instalTime;
        totalFun += obj.fun;

        if(obj.sustainability != 0)
        {
            objectCount++;
            avgSus += obj.sustainability;
        }
       
       
        string fileContents1 =   "---------------------------------------\n" +
                                "User ordered " + obj.name + "\n" +
                                "Item cost " + obj.price + " $" +"\n" +
                                "The time required is " + obj.instalTime + " minutes \n" +
                                "The Item has a sustainability rank of " + obj.sustainability +"\n" +
                                "And a fun rank  of " + obj.fun + "\n" +
                                "This was ordered at " + timeStamp1 + "\n" +
                                "Total Cost = "+ totalCost + " $"+ "\n" +
                                "Total Time = "+ totalTime + " Minutes" +"\n" +
                                "Total Fun = " + totalFun + "\n" +
                                "Average Sustainability = " + (avgSus / objectCount) + "\n---------------------------------------\n\n";

        saveInformation =       "---------------------------------------\n" +
                                "User ordered " + obj.name + "\n" +
                                "Item cost " + obj.price + " $" + "\n" +
                                "The time required is " + obj.instalTime + " minutes \n" +
                                "The Item has a sustainability rank of " + obj.sustainability + "\n" +
                                "And a fun rank  of " + obj.fun + "\n" +
                                "This was ordered at " + timeStamp1 + "\n" +
                                "Total Cost = " + totalCost + " $" + "\n" +
                                "Total Time = " + totalTime + " Minutes" + "\n" +
                                "Total Fun = " + totalFun + "\n" +
                                "Average Sustainability = " + (avgSus / objectCount) + "\n---------------------------------------\n\n";


        if (!File.Exists(path))  { File.WriteAllText(path, fileContents1); }
        else { File.AppendAllText(path, fileContents1);    }

        #if WINDOWS_UWP
        WriteData();
        #endif
    }





    // TO ADD   Remove Export Function(oderableobj obj)  this is called from ObjectOrderer script/ this is to log items returned and or thrown away.
    public void ReturnObjectLog(ReturnableObj obj)
    {
         totalCost = totalCost - obj.price;
        totalTime -= obj.instalTime;
        totalFun -= obj.fun;

        if(obj.sustainability != 0)
        {
            objectCount--;
            avgSus -= obj.sustainability;
        }

        string fileContents =   "---------------------------------------\n" +
                                "User returned " + obj.name + "\n" +
                                "Item cost refunded " + obj.price + " $" +"\n" +
                                "The time retunred is " + obj.instalTime + " minutes \n" +
                                "The Item had a sustainability rank of " + obj.sustainability +"\n" +
                                "And a fun rank  of " + obj.fun + "\n" +
                                "This was returned at " + timeStamp1 + "\n" +
                                "Total Cost = "+ totalCost + " $"+ "\n" +
                                "Total Time = "+ totalTime + " Minutes" +"\n" +
                                "Total Fun = " + totalFun + "\n" +
                                "Average Sustainability = " + (avgSus / objectCount) + "\n---------------------------------------\n\n";

         saveInformation =   "---------------------------------------\n" +
                                "User returned " + obj.name + "\n" +
                                "Item cost refunded " + obj.price + " $" +"\n" +
                                "The time retunred is " + obj.instalTime + " minutes \n" +
                                "The Item had a sustainability rank of " + obj.sustainability +"\n" +
                                "And a fun rank  of " + obj.fun + "\n" +
                                "This was returned at " + timeStamp1 + "\n" +
                                "Total Cost = "+ totalCost + " $"+ "\n" +
                                "Total Time = "+ totalTime + " Minutes" +"\n" +
                                "Total Fun = " + totalFun + "\n" +
                                "Average Sustainability = " + (avgSus / objectCount) + "\n---------------------------------------\n\n";


        if (!File.Exists(path))  { File.WriteAllText(path, fileContents); }
        else { File.AppendAllText(path, fileContents);    }      

        #if WINDOWS_UWP
        WriteData();
        #endif
    }





        // This keeps track of the Vector 3 location that the user is looking at. 
    void ExportPositionLog(string pos)
    {    
        //string path1 = "./PositionLog.txt";
        

        if (!File.Exists(path2)) { File.WriteAllText(path2, pos); }
        else { File.AppendAllText(path2, pos); }

    }

    // Update is called once per frame unless changed in unity
    void Update()
    {
        if (Time.time >= nextTime)
        {
            positions += CoreServices.InputSystem.EyeGazeProvider.HitPosition.ToString() + "\n";
            ExportPositionLog(positions);
            nextTime += capturePositionWaitTime;
        }
    }





}

    // These comments are to add a voice commend to export something... probably will not use.  that something is defined in the ExportActivityLog() function.

    /* 
        private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            System.Action keywordAction;
            if (keywords.TryGetValue(args.text, out keywordAction))
            {
                keywordAction.Invoke();     // if speech command recognized, then invoke the action
            }
        }

    

    void ExportActivityLog()
    {

           // Debug.Log("Creating Activity Log");
           // Debug.Log(Application.persistentDataPath);
            string fileContents = "Export Activity Log \n";
            audioSource.PlayOneShot(spaceSet, 1F); // for testing
            string path =  Application.persistentDataPath + "/ActivityLog.txt";
       
    
     //calls async function to write data   
#if WINDOWS_UWP
                WriteData();
#endif
        
        // checks to see if file exists, then writes or appends to activitylog.txt
        if (!File.Exists(path))
        {
            File.WriteAllText(path, fileContents);
        }
        else
        {
            File.AppendAllText(path, fileContents);
        }


    }
    
   //for testing to delete 
   public AudioRoot audio;
   private AudioSource audioSource;
   public AudioClip spaceSet;


       KeywordRecognizer keywordRecognizer = null;
       Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

       // Start is called before the first frame update
       void Start()
       {

           audioSource = GetComponent<AudioSource>(); //for testing to delete 


       // global command
       keywords.Add("Export Activity Log", ExportActivityLog);



           // Tell the KeywordRecognizer about our keywords.
           keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

           // Register a callback for the KeywordRecognizer and START recognizing!!!!
           keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
           keywordRecognizer.Start();
       }
*/

