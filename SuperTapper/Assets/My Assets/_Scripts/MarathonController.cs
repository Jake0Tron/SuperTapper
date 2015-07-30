using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MarathonController : MonoBehaviour
{
    /// <summary>
    ///  Controls the logic of the Countdown game, timer and click counting
    /// </summary>

    // UI
    public Button clickButton;
    public Text timerText, scoreText, notificationTitle, notificationContent;
    public SplitController splitC;

    // NOTIFICATION
    public GameObject notiPanel;
    public Button notiClose;

    public bool countingDown;
    public bool roundStarted;
    private bool statsSaved;

    public float countdownTime = 25.0f;
    private float buttonShowCooldown = 2.0f;

    // COUNTS
    public int goalClicks;
    public int numberOfClicks;
    // TIMES
    public float startTime, curTime, endTime, splitTime;
    // Scores
    public float score;

    public void AddClick()
    {
        this.numberOfClicks++;
    }
    #region Notifiers
    private void NotifyStart()
    {
        this.countingDown = false;
        this.roundStarted = false;
        this.splitC.EnableStats();
        this.clickButton.gameObject.SetActive(false);
        this.notiPanel.gameObject.SetActive(true);
        Invoke("ShowButton", buttonShowCooldown);
        this.notificationContent.text = "Welcome to Marathon! Click the button as may times as you can in " + this.countdownTime + " seconds!\nReady?";
        this.notificationTitle.text = "MARATHON";
    }

    private void NotifyVictory()
    {
        this.clickButton.gameObject.SetActive(false);
        this.splitC.marathons.Add(numberOfClicks);
        this.splitC.EnableStats();
        Invoke("ShowButton", buttonShowCooldown);
        this.notiPanel.gameObject.SetActive(true);
        //HideButton();
        this.notificationContent.text = "You Tapped " + numberOfClicks + " times in " + this.countdownTime + " seconds! Try again?";
        this.notificationTitle.text = "Congrats!";
    }

    private void ShowButton()
    {
        this.notiClose.gameObject.SetActive(true);
    }

    private void HideButton()
    {
        this.notiClose.gameObject.SetActive(false);
    }

    private void SaveStats()
    {
        if (!statsSaved && roundStarted)
        {
            NotifyVictory();
            this.statsSaved = true;
        }
    }
    // LOGIC IN HERE
    // for notification button
    public void NotificationAccept()
    {
        Handheld.Vibrate();
        this.numberOfClicks = 0;
        this.startTime=this.curTime;
        this.endTime = this.startTime + this.countdownTime;
        HideButton();
        this.notiPanel.gameObject.SetActive(false);
        this.clickButton.gameObject.SetActive(true);
        this.scoreText.gameObject.SetActive(true);
        this.countingDown = true;
        this.roundStarted = true;
        this.statsSaved = false;
    }
    #endregion Notifiers
    #region Updaters

    private void UpdateText()
    {
        this.scoreText.text = this.numberOfClicks.ToString();
    }

    private void UpdateTimer()
    {
        string timer = "";
        if (countingDown)
        {
            splitTime = (this.endTime - this.curTime);

            TimeSpan timeSpan = TimeSpan.FromSeconds(splitTime);
            timer = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(countdownTime);
            timer = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
        this.timerText.text = timer;
    }

    #endregion
    #region Mono
    void Start()
    {
        this.splitC.showSplits = false;
        this.notiPanel.gameObject.SetActive(false);
        this.scoreText.gameObject.SetActive(false);
        this.scoreText.text = "";
        this.countdownTime = 25.0f;
        NotifyStart();
    }

    void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        UpdateText();
        UpdateTimer();

        // keep time current
        this.curTime = Time.time;
        if (this.curTime <= this.endTime && roundStarted)
        {
            this.countingDown = true;
        }
        else if (this.curTime > this.endTime)
        {
            this.countingDown = false;
        }

        if (!countingDown)
            SaveStats();
    }
}
    #endregion
