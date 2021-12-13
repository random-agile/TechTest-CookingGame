using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DataManager : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text knifeText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Objects")]
    [SerializeField] List<GameObject> allObjects;
    [SerializeField] List<GameObject> scoreObjects;
    [SerializeField] GameObject timerObject;
    [SerializeField] GameObject ingredientsObjects;
    [SerializeField] GameObject startButton;

    [Header("Data")]
    public int score;
    public int highScore;
    private string rank;
    private float timer = 0;
    public int KnifeLvl = 1;
    private bool isStarted;

    [SerializeField] AudioSource audioSrcBgm;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    //update UI text information on each frames
    void Update()
    {
        scoreText.text = score.ToString();
        knifeText.text = KnifeLvl.ToString();
        timer -= Time.deltaTime;
        TimerDisplay(timer);

        if(timer <= 30 && isStarted)
        {
            timerText.color = new Color(1f, 0f, 0f, 1f);
            ingredientsObjects.SetActive(false);
        }

        if(timer <= -1 & isStarted)
        {
            timerObject.SetActive(false);
            enabled = false;
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
        SoundManager.PlaySound("CookingDone");
        foreach (var all in allObjects)
        {all.SetActive(false);}
        foreach (var score in scoreObjects)
        {score.SetActive(true);}
        DetermineRank();
        DetermineHighScore();
    }

    //get rank based on player score
    void DetermineRank()
    {
        if(score >= 8000)
        {rank = "S"; rankText.text = rank;}
        else if(score >= 6000)
        {rank = "A"; rankText.text = rank;}
        else if(score >= 4000)
        {rank = "B"; rankText.text = rank;}
        else if(score >= 2000)
        {rank = "C"; rankText.text = rank;}
        else if(score < 2000)
        {rank = "D"; rankText.text = rank;}
    }

    //save or load a new highscore at the end of the game
    void DetermineHighScore()
    {
        if(score > highScore)
        {
        highScoreText.text = score.ToString();
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save();
        }
        else
        {
        highScoreText.text = highScore.ToString();
        }
    }

    //activate all interactables object after pressing start
    public void StartGame()
    {
        SoundManager.PlaySound("CookingDone");
        audioSrcBgm.Play();
        timer = 240;
        isStarted = true;
        startButton.SetActive(false);
        foreach (var all in allObjects)
        {all.SetActive(true);}
    }

    //replay the game
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
