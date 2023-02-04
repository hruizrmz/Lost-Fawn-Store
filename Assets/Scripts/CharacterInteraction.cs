using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterInteraction : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialogEmpty, dialogWrong, dialogRight;
    public int rightItemID;
    public bool playerInRange;

    private PlayerInventory inventory;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                Time.timeScale = 1f;
            } else {
                dialogBox.SetActive(true);
                Time.timeScale = 0f;
                if (inventory.item == 0) // has no items
                {
                    dialogText.text = dialogEmpty;
                    
                } else { // has an item
                    if (inventory.item == rightItemID) {
                        dialogText.text = dialogRight;
                    } else {
                        dialogText.text = dialogWrong;
                    }
                }
                inventory.item = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inventory = other.GetComponent<PlayerInventory>();
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
           dialogBox.SetActive(false);
        }
    }
}
