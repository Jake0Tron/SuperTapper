using UnityEngine;
using UnityEngine.UI;
using System;

public class ReactionController : MonoBehaviour {
    /// <summary>
    /// measure how fast/slow reaction time is vased on click of button after countdown
    /// </summary>
    public Button clickButton;
    public SplitController splitC;
    public GameObject notiP;
    public Text notificationTitle, notificationContent;
    public Text timerText;

    public bool countingDown;
    public bool roundStarted;

    public float countdownTime;
    public float startTime, curTime, endTime, splitTime;


    public void NotifyStart()
    {
        this.notiP.gameObject.SetActive(true);
        this.notificationTitle.text = "REACTION";
        this.notificationContent.text = "Welcome to the reaction timer! You'll be given a countdown, then you have to click the button as fast as you can after the signal!";
    }


    public void NotifyEnd()
    {
        this.notiP.gameObject.SetActive(true);
        this.notificationTitle.text = "WOW!";
        this.notificationContent.text = "Your reaction time was " + this.splitTime + " seconds!";
    }

    public void NotificationAccept()
    {
        if (!roundStarted && !countingDown)
        {
            this.notiP.gameObject.SetActive(false);
            this.roundStarted = true;
            this.countingDown = true;
            this.startTime = curTime;
            this.endTime = this.startTime + this.countdownTime;
        }
        if (roundStarted && !countingDown)
        {
            NotifyEnd();
        }

    }

    void UpdateTimer()
    {
        // take split outside countdown
        splitTime = (this.endTime - this.curTime);
        string timer = "";
        if (countingDown)
        {

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

	// Use this for initialization
	void Start () {
        this.countdownTime = 5.0f;
        NotifyStart();
	}
	
	// Update is called once per frame
	void Update () {
        this.curTime = Time.time;
        UpdateTimer();

        if (this.curTime >= this.endTime)
        {
            countingDown = false;
        }
        else
        {
            countingDown = true;
        }

	}
}
