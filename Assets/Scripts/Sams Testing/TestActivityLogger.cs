﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine;
using System.Collections;
using TMPro;
using static ObjectOrderer;

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
    private double totalCost = 0.0f;
    private int totalTime = 0;
    private int totalFun = 0;
    private double avgSus = 0.0f;
    private int objectCount = 0;

   

    // gets a handle on the applications localfolder to save to.
    // uses async function to wait
    // then creates the txt file and appends saveInformation string

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

   

    // function is called in Object orderer when an item is ordered.
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
       
        // for testing in hololens
        //string path = Application.persistentDataPath + "/ActivityLog.txt";

        // for testing on computer  file should show up in capstone folder
        string path = "./ActivityLog.txt";
        string fileContents =   "---------------------------------------\n" +
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

        // TO TEST > Do i need to set file contents = to a blank string?
        if (!File.Exists(path))
        {
            File.WriteAllText(path, fileContents);
            fileContents = "";
        }
        else
        {
            File.AppendAllText(path, fileContents);
            fileContents = "";
        }

#if WINDOWS_UWP
        WriteData();
#endif
    }

    // TO ADD   Remove Export Function(oderableobj obj)  this is called from ObjectOrderer script/ this is to log items returned and or thrown away.



    // Update is called once per frame
    void Update()
    {

    }

    // These comments are to add a voice commend to export something... probably will not use.  that something is defined in theExportActivityLog() function.

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
}
