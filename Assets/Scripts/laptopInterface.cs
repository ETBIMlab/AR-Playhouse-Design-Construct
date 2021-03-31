﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Windows.Speech;


public class laptopInterface : MonoBehaviour
{
    public TextMeshPro total;
    KeywordRecognizer keywordRecognizer = null;
    List<string> keywords = new List<string>();

    double money = 0.0;
    int timeinminutes = 0;
    int scheduledays = 0;
    int schedulehours = 0;
    int scheduleminutes = 0;

    [Serializable]
    public struct installObj
    {
        public string name;
        public int instalTime;
    }
    public installObj[] installObjs;
    int itemIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        keywords.Add("Switch to tab 1");
        keywords.Add("Switch to tab 2");

        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

        total.text = realtimeConversions(Time.realtimeSinceStartup) + "\n"
            + "Cost: $" + money.ToString() + "\n"
            + "Project time (d/h/m):" + "\n"
            + "" + scheduledays.ToString() + ""
             + "/" + schedulehours.ToString() + ""
             + "/" + scheduleminutes.ToString() + "\n";
    }
    // Update is called once per frame
    void Update()
    {
        total.text = realtimeConversions(Time.realtimeSinceStartup) + "\n"
            + "Cost: $ " + money.ToString() + "\n"
            + "Project time (d/h/m):" + "\n"
            + "" + scheduledays.ToString() + ""
             + "/" + schedulehours.ToString() + ""
             + "/" + scheduleminutes.ToString() + "\n";
    }

    public void additem(double mon, int dtime, string iname, int quant, int itime)
    {
        Debug.Log(itemIndex);
        installObjs[itemIndex].name = iname;
        installObjs[itemIndex].instalTime = itime;
        itemIndex++;
        double itemscost = mon * quant;
        money = money + itemscost;

        convertTime(dtime);
   
    }

    public void iteminstalled(string objName)
    {
        for (int i = 0; i < installObjs.Length; i++)
        {
            if (string.Equals(objName, installObjs[i].name))
            {
                convertTime(installObjs[i].instalTime);
            }
        }

    }
    //change time in minutes into weeks-minutes
    public void convertTime(int t)
    {

        scheduledays = 0;
        schedulehours = 0;
        scheduleminutes = 0;
        timeinminutes = timeinminutes + t;
        int temp = timeinminutes;
        while (temp > 1440)
        {
            temp = temp - 1440;
            scheduledays++;
        }
        while (temp >= 60)
        {
            temp = temp - 60;
            schedulehours++;
        }
        scheduleminutes = temp;
    }




    public String realtimeConversions(float seconds)
    {
        float mins = Mathf.Floor(seconds / 60);
        float secs = Mathf.RoundToInt(seconds % 60);
        String minss="";
        String secss="";

        if (mins < 10)
        {
            minss = "0" + mins.ToString();
        }
        else
        {
            minss = mins.ToString();
        }
        if (secs < 10)
        {
            secss = "0" + Mathf.RoundToInt(secs).ToString();
        }
        else
        {
            secss= Mathf.RoundToInt(secs).ToString();
        }
        String temp = minss + ":" +secss;
        return temp;
    }
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //time.text = "Real time:";
    }


}