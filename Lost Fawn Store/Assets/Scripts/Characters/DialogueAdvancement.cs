using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueAdvancement : MonoBehaviour
{
    private LineView lineView;
    private DialogueRunner dRun;
    private bool inputAllowed = false;
    // Start is called before the first frame update
    void Start()
    {
        lineView = GetComponent<LineView>();
        dRun = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
    }

    public IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        inputAllowed = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (dRun.IsDialogueRunning)
        {
            StartCoroutine(InputDelay());
        }
        else if(dRun.IsDialogueRunning == false)
        {
            inputAllowed = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && inputAllowed)
        {
            lineView.OnContinueClicked();
        }
    }
}
