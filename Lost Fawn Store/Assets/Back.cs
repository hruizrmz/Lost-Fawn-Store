using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Back : MonoBehaviour
{
    public Settings settings;
    public GameObject dialog;
    public GameObject blocker;

    // Start is called before the first frame update
    void Start()
    {
        //settings = GameObject.Find("SettingsObject").GetComponent<Settings>();
        //dialog = GameObject.Find("SaveDialog");
        //blocker = GameObject.Find("SaveBlocker");
    }
    public void Disable()
    {
        if(settings.saved == false)
        {
            dialog.SetActive(true);
            blocker.SetActive(true);
        }
        else if(settings.saved == true)
        {
            gameObject.SetActive(false);
        }
    }
    
   
}
