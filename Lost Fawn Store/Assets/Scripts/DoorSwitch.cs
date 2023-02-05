using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public DoorController door;

    public AudioSource doorSound;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && ItemController.Instance.itemHeld == 0)
        {
            // show door options dialogue
            // if player chose correct one, show right dialogue and openDoor()
            // else, show wrong dialogue
            openDoor();
        }
    }

    public void openDoor()
    {
        door.transitionPoint.SetActive(true);
        door.GetComponent<SpriteRenderer>().sprite = door.openSprite;
        doorSound.enabled = true;
    }
}
