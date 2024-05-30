using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    int overtakes, currentOvertakes = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI multiplierText;

    public TextMeshProUGUI accText;
    public TextMeshProUGUI multText;
    public TextMeshProUGUI fiveText;
    public TextMeshProUGUI brakesText;
    public TextMeshProUGUI oneText;
    public TextMeshProUGUI purgeText;

    private string writePath;
    private int costTop, costAcc, costBrake, countTop, countAcc, countBrake;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        writePath = Application.dataPath + "/TxtData/Score.txt";
        getScoreData();
        overtakes = getOvertakes();
        scoreText.text = "Overtakes: " + currentOvertakes.ToString();
        gameOverText.text = "Overtakes: " + currentOvertakes.ToString();
     
    }
    public void DeactivateMe(TextMeshProUGUI textObject)
    {
        StartCoroutine(RemoveAfterSeconds(2, textObject));
    }

    IEnumerator RemoveAfterSeconds(int seconds, TextMeshProUGUI textObject)
    {
        yield return new WaitForSeconds(seconds);
        textObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(int multiplier)
    {
        multiplierText.text = "Multiplier: x" + multiplier;
        if(overtakes==0)
        {
            overtakes++;
        }
        else
        overtakes+=multiplier;
        if(currentOvertakes==0)
            currentOvertakes++;
        else
        currentOvertakes+=multiplier;
        using (var writer = new StreamWriter(writePath, false))
        {
            writer.WriteLine(overtakes + "," + costTop + "," + costAcc + "," + costBrake + "," + countTop + "," + countAcc + "," + countBrake);
        }
        scoreText.text = "Overtakes: " + currentOvertakes.ToString();
        gameOverText.text = "Overtakes: " + currentOvertakes.ToString();
    }

    private void getScoreData()
    {
        string[] lines = File.ReadAllLines(writePath);
        string[] data = lines[0].Split(',');
        costAcc = Convert.ToInt32(data[1]);
        costTop = Convert.ToInt32(data[2]);
        costBrake = Convert.ToInt32(data[3]);
        countAcc = Convert.ToInt32(data[4]);
        countTop = Convert.ToInt32(data[5]);
        countBrake = Convert.ToInt32(data[6]);
    }

    private int getOvertakes()
    {
        string[] lines = File.ReadAllLines(writePath);
        string[] data = lines[0].Split(',');
        return Convert.ToInt32(data[0]);
    }

    public void activateText(int n)
    {
        switch(n)
        {
            case 1:
                accText.gameObject.SetActive(true);
                DeactivateMe(accText);
                break;
            case 2:
                multText.gameObject.SetActive(true);
                DeactivateMe(multText);
                break;
            case 3:
                fiveText.gameObject.SetActive(true);
                DeactivateMe(fiveText);
                break;
            case 4:
                brakesText.gameObject.SetActive(true);
                DeactivateMe(brakesText);
                break;
            case 5:
                oneText.gameObject.SetActive(true);
                DeactivateMe(oneText);
                break;
            case 6:
                purgeText.gameObject.SetActive(true);
                DeactivateMe(purgeText);
                break;
        }
    }
}
