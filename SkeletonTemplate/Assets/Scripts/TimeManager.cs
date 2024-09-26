using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float bestTime;
 
    public float timeLimit;
    public float timer;

    public TMP_Text TimeText;

    // Start is called before the first frame update
    void Start()
    {
        timeLimit = 121;
        timer = timeLimit;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Timer();
        }
        else
        {
            TimesUp();
        }
        
    }

    //public void updateScore()
    //{
    //    if (currentTime >= bestTime)
    //    {
    //        bestTime = currentTime;
    //    }
    //}

    public void Timer()
    {
        timer -= Time.deltaTime;

        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer % 60);
        TimeText.text = "Time Limit: " + string.Format("{0:00}:{1:00}", mins, secs);
    }

    public void TimesUp()
    {
        SceneManager.LoadScene("Finished");
    }
}
