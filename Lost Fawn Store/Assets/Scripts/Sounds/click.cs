using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    public AudioSource clickSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            clickSound.enabled = true;
        } else {
            clickSound.enabled = false;
        }
    }
}