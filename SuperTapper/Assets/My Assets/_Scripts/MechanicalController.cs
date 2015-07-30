using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MechanicalController : MonoBehaviour {

    public Button[] buttons;    // populated in inspector
    public AudioSource[] sounds; // populated in inspector

    public void TestSwitch(int index)
    {
        if (index < 5 && index >= 0)
        {
            Handheld.Vibrate();
        }
        sounds[index].Play();

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
