using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("pause"))
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                pausePanel.SetActive(true);
                Time.timeScale= 0f;
            }
        }
    }


}
