using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    private int score;
    private string rank;
    [SerializeField] GameObject timerObject;
    private float timer = 10;
    [SerializeField] private TMP_Text timerText;
    private int KnifeLvl = 1;
    private int KnifeExp;

    void Update()
    {
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

    void TimerDisplay(float displayTime)
    {
        displayTime += 1;
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnd()
    {

    }

}
