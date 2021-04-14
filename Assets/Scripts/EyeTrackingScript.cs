using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class EyeTrackingScript : MonoBehaviour
{
    public float capturePositionWaitTime = 1;
    float nextTime = 0;
    string positions = "";

    KeywordRecognizer keywordRecognizer = null;
    List<string> keywords = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        keywords.Add("Export position logger");

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTime)
        {
            //ActivityLogger logScript = GetComponent<ActivityLogger>();
            positions += CoreServices.InputSystem.EyeGazeProvider.HitPosition.ToString() + "\n";
            //gameObject.GetComponent<ActivityLogger>().LogPosition(CoreServices.InputSystem.EyeGazeProvider.HitPosition.ToString());
            nextTime += capturePositionWaitTime;
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        ExportPositionLog();
    }

    void ExportPositionLog()
    {
        Debug.Log("Printing Position Log");
        File.WriteAllText("./PositionLog.txt", positions);
    }
}
