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
    public Button notiClose;
    public Text notificationTitle, notificationContent;
    public Text timerText;

    public bool countingDown;
    public bool roundStarted;

    public float countdownTime;
    public float startTime, curTime, endTime, splitTime, saveTime;
    private float buttonShowCooldown = 2.0f;



    public void React()
    {
        this.roundStarted = false;
        this.clickButton.gameObject.SetActive(false);
        this.saveTime = this.splitTime;
        NotifyEnd();
    }

    public void NotifyStart()
    {
        this.notiP.gameObject.SetActive(true);
        this.clickButton.gameObject.SetActive(false);
        this.notificationTitle.text = "REACTION";
        this.notificationContent.text = "Welcome to the reaction timer! You'll be given a countdown, then you have to click the button as fast as you can after the signal!";
        Invoke("ShowDelayed", buttonShowCooldown);
    }


    public void NotifyEnd()
    {
        this.notiP.gameObject.SetActive(true);
        this.notificationTitle.text = "WOW!";
        if (this.splitTime < 0.0f)
        {
            this.notificationContent.text = "Your reaction time was " + this.saveTime + " seconds! Just a little too slow!";
        }
        else if (this.splitTime > 0.0f)
        {
            this.notificationContent.text = "Your reaction time was +" + this.saveTime + " seconds! Just a little too fast!";
        }
        else
        {
            this.notificationContent.text = "Your reaction time was " + this.splitTime + " seconds! WOW! PERFECT!";
        }
        this.splitC.reactions.Add(saveTime);

        Invoke("ShowDelayed", buttonShowCooldown);
    }

    public void NotificationAccept()
    {
        if (!roundStarted && !countingDown)
        {
            this.clickButton.gameObject.SetActive(false);
            this.notiP.gameObject.SetActive(false);
            this.splitC.gameObject.SetActive(false);
            this.notiClose.gameObject.SetActive(false);
            this.roundStarted = true;
            this.countingDown = true;
            this.startTime = curTime;
            this.endTime = this.startTime + this.countdownTime;
        }
    }

    private void ShowDelayed()
    {
        this.splitC.EnableStats(SplitController.statsToShow.REACTIONS);
        this.splitC.splitPanel.gameObject.SetActive(true);
        this.notiClose.gameObject.SetActive(true);
    }

    void UpdateTimer()
    {
        // take split outside countdown
        this.splitTime = (this.endTime - this.curTime);

        string timer = "";
        if (countingDown)
        {
            if (splitTime <= 1.0f)
            {
                timer = "...";
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(splitTime);
                timer = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            }   

        }
        else
        {
            timer = "GO!";
            this.clickButton.gameObject.SetActive(true);
        }
        this.timerText.text = timer;
    }

	// Use this for initialization
	void Start () {
        this.countdownTime = 5.0f;
        this.splitC.splitPanel.gameObject.SetActive(false);
        this.notiP.gameObject.SetActive(false);
        this.notiClose.gameObject.SetActive(false);
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
