using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MechanicalController : MonoBehaviour {

    public Button[] buttons;    // populated in inspector

    public void TestSwitch(int index)
    {
        switch (index){
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
