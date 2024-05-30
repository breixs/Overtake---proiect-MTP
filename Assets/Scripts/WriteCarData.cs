using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WriteCarData : MonoBehaviour
{
    private string filePath;
    private string scorePath;
    private int topSpeed;
    private int acceleration;
    private int brakePower;
    private int credits;
    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI upgradeTextTop, upgradeCostTop;
    public TextMeshProUGUI upgradeTextAcc, upgradeCostAcc;
    public TextMeshProUGUI upgradeTextBrake, upgradeCostBrake;
    private int costTop, costAcc, costBrake, countTop, countAcc, countBrake;


    private void Start()
    {
        filePath = Application.dataPath + "/TxtData/CarData.txt";
        scorePath = Application.dataPath + "/TxtData/Score.txt";
        writeCredits();
        acceleration = GetAccelerationData();
        topSpeed = GetTopSpeedData();
        brakePower = GetBrakeData();
        costAcc = GetCredits(1);
        costTop=GetCredits(2);
        costBrake=GetCredits(3);
        countAcc=GetCredits(4);
        countTop=GetCredits(5);
        countBrake=GetCredits(6);
        textUpgrade(upgradeTextTop, countTop, 5);
        textUpgrade(upgradeTextAcc, countAcc, 10);
        textUpgrade(upgradeTextBrake, countBrake, 10);
        textCost(upgradeCostTop, costTop);
        textCost(upgradeCostAcc, costAcc);
        textCost(upgradeCostBrake, costBrake);

    }

    private void writeCredits()
    {
        creditsText.text = "Current Credits: " + GetCredits(0).ToString();
    }

    public void WriteText()
    {
        using(var writer=new StreamWriter(filePath, false))
        {
            writer.WriteLine(topSpeed.ToString()+","+acceleration.ToString()+","+brakePower.ToString());
        }
    }

    public void AddTopSpeed()
    {
        credits = GetCredits(0);
        topSpeed = GetTopSpeedData();
        if(topSpeed<20)
            topSpeed = 20;

        Debug.Log(topSpeed.ToString());
        if (countTop < 5)
        {
            if (credits >= costTop)
            {
                if (topSpeed < 50)
                    topSpeed += 5;

                credits -= costTop;
                costTop += 5;
                countTop++;

                using (var writer = new StreamWriter(scorePath, false))
                {
                    writer.WriteLine(credits + "," + costTop + "," + costAcc + "," + costBrake + "," + countTop + "," + countAcc + "," + countBrake);
                }
                writeCredits();
                textUpgrade(upgradeTextTop, countTop, 5);
                textCost(upgradeCostTop, costTop);
            }
            else
            {
                Debug.Log("prea scump");
            }
        }
        else
        {
            Debug.Log("Max upgrade");
        }
    }

    public void AddAcceleration()
    {
        credits = GetCredits(0);
        acceleration = GetAccelerationData();
        if(acceleration<900)
            acceleration = 900;
        
        Debug.Log(acceleration.ToString());
        if (countAcc < 10)
        {
            if (credits >= costAcc)
            {
                if (acceleration < 3900)
                    acceleration += 300;

                credits -= costAcc;
                costAcc += 5;
                countAcc++;
                using (var writer = new StreamWriter(scorePath, false))
                {
                    writer.WriteLine(credits + "," + costTop + "," + costAcc + "," + costBrake + "," + countTop + "," + countAcc + "," + countBrake);
                }
                writeCredits();
                textUpgrade(upgradeTextAcc, countAcc, 10);
                textCost(upgradeCostAcc, costAcc);
            }
            else
            {
                Debug.Log("prea scump");
            }
        }
        else
        {
            Debug.Log("max Upgrade");
        }
    }

    public void AddBrake()
    {
        credits = GetCredits(0);
        brakePower = GetBrakeData();
        if (brakePower < 9000)
            brakePower = 9000;

        Debug.Log(brakePower.ToString());
        if (countBrake < 10)
        {
            if (credits >= costBrake)
            {
                if (brakePower < 59000)
                    brakePower += 5000;

                credits -= costBrake;
                costBrake += 5;
                countBrake++;

                using (var writer = new StreamWriter(scorePath, false))
                {
                    writer.WriteLine(credits + "," + costTop + "," + costAcc + "," + costBrake + "," + countTop + "," + countAcc + "," + countBrake);
                }
                writeCredits();
                textUpgrade(upgradeTextBrake, countBrake, 10);
                textCost(upgradeCostBrake, costBrake);
            }
            else
            {
                Debug.Log("prea scump");
            }
        }
        else
        {
            Debug.Log("Max Upgrade");
        }

    }

    private int GetTopSpeedData()
    {
        string[] lines = File.ReadAllLines(filePath);
        string[] data = lines[0].Split(',');
        return Convert.ToInt32(data[0]);
    }

    private int GetAccelerationData()
    {
        string[] lines = File.ReadAllLines(filePath);
        string[] data = lines[0].Split(',');
        return Convert.ToInt32(data[1]);
    }

    private int GetBrakeData()
    {
        string[] lines = File.ReadAllLines(filePath);
        string[] data = lines[0].Split(',');
        return Convert.ToInt32(data[2]);
    }

    private int GetCredits(int index)
    {
        string[] lines = File.ReadAllLines(scorePath);
        string[] data = lines[0].Split(',');
        return Convert.ToInt32(data[index]);
    }

    private void textUpgrade(TextMeshProUGUI txt, int count, int max)
    {
        txt.text = "Upgrade: " + count + "/"+ max;
    }

    private void textCost(TextMeshProUGUI txt, int cost)
    {
        txt.text = "Cost: " + cost;
    }

}
