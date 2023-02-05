using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public AudioSource InteractionSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            InteractionSound.enabled = true;
        } else {
            InteractionSound.enabled = false;
        }
    }
}
