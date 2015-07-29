using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SplitController : MonoBehaviour {
    /// <summary>
    /// Controls all the split highScores
    /// </summary>
    /// 

    ///TODO: MAKE THIS A DICTIONARY FOR NAMES
    public List<float> splits;
    public List<int> marathons;
    public GameObject splitPanel;
    public Text time1, time2, time3;

    public bool showSplits;

    public void EnableStats()
    {
        if (showSplits)
            this.splits.Sort();
        else
        {
            this.marathons.Sort();
            this.marathons.Reverse();
        }
        this.splitPanel.gameObject.SetActive(true);
    }

    public void DisableStats()
    {
        this.splitPanel.gameObject.SetActive(false);
    }

    void UpdateText()
    {
        if (showSplits){
        // top 3 splits
            this.time1.text = this.splits[0].ToString();
            this.time2.text = this.splits[1].ToString();
            this.time3.text = this.splits[2].ToString();
        }
        else{
            this.time1.text = this.marathons[0].ToString();
            this.time2.text = this.marathons[1].ToString();
            this.time3.text = this.marathons[2].ToString();
        }
    }

	// Use this for initialization
	void Start () {
        this.splits = new List<float>();
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

        // sort lowest to highest
        this.splits.Sort();
        
        // highest to lowest
        this.marathons.Sort();
        this.marathons.Reverse();

	}
	
	// Update is called once per frame
	void Update () {
        UpdateText();
	}
}
