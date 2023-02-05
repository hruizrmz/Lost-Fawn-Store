using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ObjectInteraction : MonoBehaviour
{
    public bool playerInRange;
    public int itemValue;
    public DoorController door;
    private DialogueRunner dialogBox; // Yarn Dialogue Runner that handles all the lines

    private void Start()
    {
        dialogBox = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (ItemController.Instance.itemHeld == 0)
            {
                // dialogBox.StartDialogue("object"+itemValue); object description

                // don't do all of this until dialogue is finished
                ItemController.Instance.itemHeld = itemValue;
                Destroy(gameObject);
                door.transitionPoint.SetActive(true);
                door.GetComponent<SpriteRenderer>().sprite = door.openSprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           playerInRange = false;
        }
    }
}
