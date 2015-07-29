using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CountdownController : MonoBehaviour
{
    /// <summary>
    ///  Controls the logic of the Countdown game, timer and click counting
    /// </summary>

    // UI
    public Button clickButton;
    public Text timerText, scoreText, notificationTitle, notificationContent;
    public Image phteven;
    public SplitController splitC;

    // NOTIFICATION
    public GameObject notiPanel;
    public Button notiClose;

    public bool countingDown;
    public bool playerWonThisRound;
    public bool roundStarted;

    public float countdownTime;
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
        this.playerWonThisRound = false;
        this.roundStarted = false;
        this.splitC.EnableStats();
        this.clickButton.gameObject.SetActive(false);
        this.notiPanel.gameObject.SetActive(true);
        Invoke("ShowButton", buttonShowCooldown);
        this.notificationContent.text = "Welcome to Countdown! The time you have to click the button will decrease every round!\nReady?";
        this.notificationTitle.text = "COUNT DOWN";
    }

    private void NotifyVictory()
    {
        this.clickButton.gameObject.SetActive(false);
        this.splitC.splits.Add(score);
        this.playerWonThisRound = true;
        this.splitC.EnableStats();
        Invoke("ShowButton", buttonShowCooldown);
        this.notiPanel.gameObject.SetActive(true);
        //HideButton();
        score = this.countdownTime - this.splitTime;
        this.notificationContent.text = "You Tapped " + goalClicks + " times in less than " + this.countdownTime + " seconds! Get ready for the next level!";
        this.notificationTitle.text = "Congrats!";
    }

    private void NotifyLoss()
    {
        Invoke("ShowButton", buttonShowCooldown);
        this.clickButton.gameObject.SetActive(false);
        this.notiPanel.gameObject.SetActive(true);
        this.splitC.EnableStats();
        this.notificationContent.text = "Oh no! You only tapped " + this.numberOfClicks + " times this round! Better luck Next time!";
        this.notificationTitle.text = "GOOD TRY!";
    }

    private void ShowButton()
    {
        this.notiClose.gameObject.SetActive(true);
    }

    private void HideButton()
    {
        this.notiClose.gameObject.SetActive(false);
    }

    // LOGIC IN HERE
    // for notification button
    public void NotificationAccept()
    {
        // add score to list
        if (playerWonThisRound && roundStarted)
        {
            // Finished round and won
            // set up for new round and close panel
            // reset counter
            this.numberOfClicks = 0;

            /// TODO: DYNAMIC NUMBERS
            this.countdownTime *= 0.85f;
            //this.goalClicks += 10;

            //timer reset
            this.startTime = Time.time;
            this.endTime = this.startTime + this.countdownTime;

            this.notiPanel.gameObject.SetActive(false);
            HideButton();
            this.clickButton.gameObject.SetActive(true);
            this.splitC.DisableStats();
            // start counting
            this.countingDown = true;
        }
        else if (!playerWonThisRound && !roundStarted)
        {
            // Start Game, round has not begun
            this.roundStarted = true;
            this.startTime = Time.time;
            // STAR VARS
            this.countdownTime = 40.0f;
            this.goalClicks = 60;
            this.endTime = this.startTime + this.countdownTime;

            this.splitC.DisableStats();
            this.notiPanel.gameObject.SetActive(false);
            HideButton();
            this.clickButton.gameObject.SetActive(true);
        }
        else if (!playerWonThisRound && roundStarted)
        {
            // player lost, restart the game
            this.splitC.DisableStats();
            this.startTime = Time.time;
            this.countdownTime = 20.0f;
            this.endTime = this.startTime + this.countdownTime;
            this.goalClicks = 10;
            this.scoreText.text = "0";
        }
    }
    #endregion Notifiers
    #region Updaters
    private void UpdatePhteven()
    {
        if (roundStarted)
        {
            float phtevenFill = (float)this.numberOfClicks / (float)this.goalClicks;
            this.phteven.fillAmount = phtevenFill;
        }
        else
        {
            this.phteven.fillAmount = 0f;
        }
    }

    private void UpdateText()
    {
        this.scoreText.text = this.numberOfClicks.ToString() + " / " + this.goalClicks.ToString();
    }

    private void UpdateTimer()
    {
        string timer = "";
        if (countingDown)
        {
            splitTime = (this.endTime - this.curTime);

            TimeSpan timeSpan = TimeSpan.FromSeconds(splitTime);
            timer = string.Format("{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            timer = this.splitTime.ToString();
        }

        this.timerText.text = timer;
    }
    private void UpdateLoss()
    {
        if (this.curTime >= this.endTime && this.roundStarted)
        {
            this.splitTime = 0.0f;
            NotifyLoss();

        }
    }
    #endregion
    #region Mono
    void Start()
    {
        this.notiPanel.gameObject.SetActive(false);
        this.scoreText.text = "0";
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

        UpdatePhteven();

        UpdateLoss();
        // keep time current
        this.curTime = Time.time;
        if (this.curTime < this.endTime)
            this.countingDown = true;
        else
            this.countingDown = false;

        if (countingDown)
            // while playing
            if (this.numberOfClicks >= this.goalClicks)
            {
                this.countingDown = false;
                NotifyVictory();
            }
    }
}
    #endregion
