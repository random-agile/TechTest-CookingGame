using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public int score;
    [SerializeField] private TMP_Text scoreText;
    private string rank;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] GameObject timerObject;
    [SerializeField] GameObject allObjects;
    private float timer = 120;
    [SerializeField] private TMP_Text timerText;
    private int KnifeLvl = 1;
    private int KnifeExp;

    void Update()
    {
        scoreText.text = score.ToString();
        timer -= Time.deltaTime;
        TimerDisplay(timer);

        if(timer <= 30)
        {
            timerText.color = new Color(1f, 0f, 0f, 1f);
        }

        if(timer <= -1)
        {
            timerObject.SetActive(false);
            TimerEnd();
        }
    }

    //format time display to digital format
    void TimerDisplay(float displayTime)
    {
        displayTime += 1;
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //display results when time is up
    void TimerEnd()
    {
        allObjects.SetActive(false);
    }

    //get rank based on player score
    void DetermineRank()
    {
        if(score >= 10000)
        {rank = "S"; rankText.text = rank;}
        else if(score >= 8000)
        {rank = "A"; rankText.text = rank;}
        else if(score >= 6000)
        {rank = "B"; rankText.text = rank;}
        else if(score >= 4000)
        {rank = "C"; rankText.text = rank;}
        else if(score < 4000)
        {rank = "D"; rankText.text = rank;}
    }

}
