﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine;
public class TestActivityLogger : MonoBehaviour
{
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
       
        // global command
        keywords.Add("Export Activity Log", ExportActivityLog);


        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

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

    void ExportActivityLog()
    {
        Debug.Log("Creating Activity Log");
        Debug.Log(Application.persistentDataPath);
        string fileContents = "Testing export activity log";
        // testing various commands to figure out hololens directory path
        //File.WriteAllText("./ActivityLog.txt", fileContents);
        // string path = @"c:\Documents\DiagnosticLogs\ActivityLog.txt";
        //string path = Path.Combine(Application.persistentDataPath, "ActivityLog.txt");
        
        // to test
        string path = "./DiagnosticLogs/ActivityLog.txt";
        //string path = "C:/Documents/DiagnosticLogs/ActivityLog.txt";

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
    public void LogPosition(string activity)
    {
        listOfPositions.Add(activity);
    }
}
