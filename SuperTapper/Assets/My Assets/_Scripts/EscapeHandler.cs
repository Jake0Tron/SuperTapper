using UnityEngine;
using System.Collections;

public class EscapeHandler : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Handheld.Vibrate();
            // HANDLE ESCAPE HERE
            if (Application.loadedLevel != 0)
                Application.LoadLevel(0);
            else
            {
                Application.Quit();
            }
        }
    }
}
