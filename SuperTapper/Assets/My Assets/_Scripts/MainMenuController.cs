using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        
    }

    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
