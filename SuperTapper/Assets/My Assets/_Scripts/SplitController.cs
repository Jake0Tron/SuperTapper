using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SplitController : MonoBehaviour
{
    /// <summary>
    /// Controls all the split highScores
    /// </summary>
    /// 

    ///TODO: MAKE THIS A DICTIONARY FOR NAMES
    public List<float> splits;
    public List<float> reactions;
    public List<int> marathons;

    public List<string> top3;
    public enum statsToShow
    {
        SPLITS,
        REACTIONS,
        MARATHONS
    }

    public GameObject splitPanel;
    public Text time1, time2, time3;

    public void EnableStats(statsToShow state)
    {
        this.top3.Clear();

        if (state == statsToShow.SPLITS)
        {
            this.splits.Sort();

            this.top3.Add(this.splits[0].ToString());
            this.top3.Add(this.splits[1].ToString());
            this.top3.Add(this.splits[2].ToString());
        }
        else if (state == statsToShow.MARATHONS)
        {
            this.marathons.Sort();
            this.marathons.Reverse();

            this.top3.Add(this.marathons[0].ToString());
            this.top3.Add(this.marathons[1].ToString());
            this.top3.Add(this.marathons[2].ToString());
        }
        else if (state == statsToShow.REACTIONS)
        {
            this.reactions.Sort();

            this.top3.Add(this.reactions[0].ToString());
            this.top3.Add(this.reactions[1].ToString());
            this.top3.Add(this.reactions[2].ToString());

        }
        this.splitPanel.gameObject.SetActive(true);
    }

    public void DisableStats()
    {
        this.splitPanel.gameObject.SetActive(false);
    }

    void UpdateText()
    {
        this.time1.text = this.top3[0];
        this.time2.text = this.top3[1];
        this.time3.text = this.top3[2];
    }

    // Use this for initialization
    void Start()
    {
        this.splits = new List<float>();
        this.reactions = new List<float>();
        this.marathons = new List<int>();
        this.top3 = new List<string>();

        // dummy scores
        this.splits.Add(35.2345f);
        this.splits.Add(37.1425f);
        this.splits.Add(34.1415f);
        this.splits.Add(35.22345f);
        this.splits.Add(33.1425f);
        this.splits.Add(34.1415f);

        this.marathons.Add(150);
        this.marathons.Add(155);
        this.marathons.Add(153);
        this.marathons.Add(156);
        this.marathons.Add(151);
        this.marathons.Add(159);

        this.reactions.Add(0.5f);
        this.reactions.Add(0.51f);
        this.reactions.Add(0.512f);
        this.reactions.Add(0.512f);

        this.top3.Add("");
        this.top3.Add("");
        this.top3.Add("");

        // sort lowest to highest
        this.splits.Sort();
        this.reactions.Sort();
        // highest to lowest
        this.marathons.Sort();
        this.marathons.Reverse();


        // set start
        // EnableStats(statsToShow.MARATHONS);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }
}
