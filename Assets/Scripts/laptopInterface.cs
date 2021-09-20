﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    Boolean istab1 = true;
    String itemstoDisplay = "";
    public bool removeCost = false;

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
        keywords.Add("Switch Tab 1");
        keywords.Add("Switch Tab 2");

        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

        total.text = realtimeConversions(Time.realtimeSinceStartup) + "\n"
            + "Cost: $" + money.ToString() + "\n" + "\n"
            + "Project time (d:h:m):" + "\n"
            + "" + scheduledays.ToString() + ""
             + ":" + schedulehours.ToString() + ""
             + ":" + scheduleminutes.ToString() + "\n";
    }
    // Update is called once per frame
    void Update()
    {
        if(istab1)
        {
            total.fontSize = 7;
            total.text = //realtimeConversions(Time.realtimeSinceStartup) + "\n"
                "Cost: $ " + money.ToString() + "\n" + "\n"
                + "Time Spent (day:hr:min)" + "\n"
                + "" + scheduledays.ToString() + ""
                 + ":" + schedulehours.ToString() + ""
                 + ":" + scheduleminutes.ToString() + "\n";
        }
        else
        {
            total.fontSize = 6;
            total.text = itemstoDisplay;
        }
    }

    public void additem(double mon, int dtime, string iname, int quant, int itime)
    {
        Debug.Log(iname);
        installObjs[itemIndex].name = iname;
        installObjs[itemIndex].instalTime = itime;
        itemIndex++;
        double itemscost = mon * quant;
        money = money + itemscost;
        itemstoDisplay += iname + " x" + quant + " $" + mon + "\n";
        convertTime(itime);
   
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

    public void removeitemCost(double mon, int time)
    {
        money = money - mon;
        itemstoDisplay += " $" + money + "\n";
        removeCost = true;
        convertTime(time);
    }

    //change time in minutes into weeks-minutes
    public void convertTime(int t)
    {
        scheduledays = 0;
        schedulehours = 0;
        scheduleminutes = 0;
        if (removeCost)
        {
            timeinminutes = timeinminutes - t;
            removeCost = false;
        }
        else
        {
            timeinminutes = timeinminutes + t;
        }
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
        if(args.text== "Switch Tab 2")
        {
            istab1 = false;
        }
        else
        {
            istab1 = true;
        }
        //time.text = "Real time:";
    }


}