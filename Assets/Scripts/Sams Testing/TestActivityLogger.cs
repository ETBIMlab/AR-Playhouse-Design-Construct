using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine;
using System.Collections;
using TMPro;

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
    private static string fileName = timeStamp + ".txt";

    private static bool firstSave = true;

    //private string saved line;




    public AudioRoot audio;
        private AudioSource audioSource;
        public AudioClip spaceSet;
        //public GameObject activityItem1;
        //public GameObject activityItem2;
        //public GameObject activityItem3;
        //public GameObject activityItem4;
        //public GameObject activityItem5;
        public GameObject[] activityItems = new GameObject[5];

        private ArrayList listOfActions = new ArrayList();
        private ArrayList listOfPositions = new ArrayList();

        KeywordRecognizer keywordRecognizer = null;
        Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();



            // global command
            keywords.Add("Export Activity Log", ExportActivityLog);
       
       

            // Tell the KeywordRecognizer about our keywords.
            keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

            // Register a callback for the KeywordRecognizer and START recognizing!!!!
            keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
            keywordRecognizer.Start();



        }


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

    // Update is called once per frame
    void Update()
        {

        }

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
            string fileContents = "Testing export activity log";
            audioSource.PlayOneShot(spaceSet, 1F);
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
            for (int i = 0; i < listOfActions.Count; i++)
            {
                fileContents += listOfActions[i];
                if (i < listOfActions.Count - 1)
                {
                    fileContents += "\n";
                }
            }
            File.AppendAllText(path, fileContents);

        }


    }
/*
      public void LogPosition(string activity)
        {
            listOfPositions.Add(activity);
        }

    
        public void LogItem(string activity)
        {
            listOfActions.Add(activity);
            for (int i = 0; i < 5; i++)
            {
                TextMeshPro text = activityItems[i].GetComponent<TextMeshPro>();
                if (listOfActions.Count - i > 0)
                {
                    text.text = listOfActions[listOfActions.Count - (i + 1)].ToString();
                }
                else
                {
                    text.text = "";
                }
            }
        }

*/
    }
