using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HopscotchController : MonoBehaviour {
    ///
    /// Display a specific button based on values in an array, select in time
    ///


    // set in inspector
    public Button start, finish, single, left, right;

    private int[] steps;

    void HideAllButtons()
    {
        this.start.gameObject.SetActive(false);
        this.finish.gameObject.SetActive(false);
        this.left.gameObject.SetActive(false);
        this.right.gameObject.SetActive(false);
        this.single.gameObject.SetActive(false);
    }

    void ReadSteps()
    {
        
    }

	// Use this for initialization
	void Start () {

        HideAllButtons();
        steps = new int[25];

        for (int i = 0; i < steps.Length; i++)
        {
            int r1 = Random.Range(0, 3);
            steps[i] = r1;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
