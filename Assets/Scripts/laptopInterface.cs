using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class laptopInterface : MonoBehaviour
{
    public TextMeshPro total;
    public TextMeshPro listofitems;
    public TextMeshPro time;

    double money=0.0;
    int timeinminutes = 0;
    int scheduleweeks = 0;
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
        total.text = "Total Spent: "+money.ToString()+"$";
        time.text = "Weeks: "+ scheduleweeks.ToString()+"\n"
            + "Days: " + scheduledays.ToString() + "\n"
            + "Hours:" + schedulehours.ToString() + "\n"
            + "Minutes:" + scheduleminutes.ToString() + "\n";
        listofitems.text = "Items Ordered \n"+
            "Item Name  Quantity  Cost  Time Arrived\n";
    }
    // Update is called once per frame
    void Update()
    {}

    public void additem(double mon,int dtime,string iname,int quant,int itime)
    {
        installObjs[itemIndex].name = iname;
        installObjs[itemIndex].instalTime = itime;
        itemIndex++;
        double itemscost = mon * quant;
        money = money+ itemscost;

        convertTime(dtime);

        string orderedTime = scheduleweeks.ToString() + "/" + scheduledays.ToString()
            + "/" + schedulehours.ToString() + "/" + scheduleminutes.ToString();

        listofitems.text = listofitems.text + iname + "                 " + quant.ToString() + "           "
        + itemscost.ToString() + "          " + orderedTime + "\n";

        total.text = "Total Spent: " + money.ToString() + "$";
        time.text = "Weeks: " + scheduleweeks.ToString() + "\n"
            + "Days: " + scheduledays.ToString() + "\n"
            + "Hours:" + schedulehours.ToString() + "\n"
            + "Minutes:" + scheduleminutes.ToString() + "\n";
    }



    public void iteminstalled(string objName)
    {
        for (int i = 0; i < installObjs.Length; i++)
        {
            if(string.Equals(objName,installObjs[i].name))
            {
                convertTime(installObjs[i].instalTime);
                time.text = "Weeks: " + scheduleweeks.ToString() + "\n"
                     + "Days: " + scheduledays.ToString() + "\n"
                     + "Hours:" + schedulehours.ToString() + "\n"
                     + "Minutes:" + scheduleminutes.ToString() + "\n";
            }
        }

    }
    //change time in minutes into weeks-minutes
    public void convertTime(int t)
    {
        scheduleweeks = 0;
        scheduledays = 0;
        schedulehours = 0;
        scheduleminutes = 0;
        timeinminutes = timeinminutes + t;
        int temp = timeinminutes;
        while(temp>10080)
        {
            temp = temp - 10080;
            scheduleweeks++;
        }
        while(temp> 1440)
        {
            temp = temp - 1440;
            scheduledays++;
        }
        while(temp>=60)
        {
            temp = temp - 60;
            schedulehours++;
        }
        scheduleminutes = temp;
    }

}
