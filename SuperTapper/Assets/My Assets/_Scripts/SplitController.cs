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
        if (state == statsToShow.SPLITS){
            this.splits.Sort();
            // top 3 splits
            this.time1.text = this.splits[0].ToString();
            this.time2.text = this.splits[1].ToString();
            this.time3.text = this.splits[2].ToString();
        }else if (state == statsToShow.MARATHONS)
        {
            this.marathons.Sort();
            this.marathons.Reverse();
            this.time1.text = this.marathons[0].ToString();
            this.time2.text = this.marathons[1].ToString();
            this.time3.text = this.marathons[2].ToString();
        }
        else if (state == statsToShow.REACTIONS)
        {
            this.reactions.Sort();
            this.time1.text = this.reactions[0].ToString();
            this.time2.text = this.reactions[1].ToString();
            this.time3.text = this.reactions[2].ToString();

        }

        this.splitPanel.gameObject.SetActive(true);
    }

    public void DisableStats()
    {
        this.splitPanel.gameObject.SetActive(false);
    }

    void UpdateText()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        this.splits = new List<float>();
        this.reactions = new List<float>();
        this.marathons = new List<int>();

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
        // sort lowest to highest
        this.splits.Sort();
        this.reactions.Sort();
        // highest to lowest
        this.marathons.Sort();
        this.marathons.Reverse();

        // set start
        EnableStats(statsToShow.REACTIONS);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }
}
