using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;


public class CharacterInteraction : MonoBehaviour
{
    private DialogueRunner dialogBox; // Yarn Dialogue Runner that handles all the lines
    public Text dialogText;
    public string dialogEmpty, dialogWrong, dialogRight;
    public int firstItemID;
    public int secondItemID;
    public string characterName; // The exact name of this specific character. Case-sensitive
    public bool playerInRange;

    private PlayerInventory inventory;

    private void Start()
    {
        dialogBox = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
    }
    // Update is called once per frame
    void Update()
    {
        // Checks whether player has no items, either of the two correct items, or an incorrect item
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange && dialogBox.IsDialogueRunning == false)
        {
            if(inventory.item == 0)
            {
                dialogBox.StartDialogue(characterName + "1");
            }
            else if(inventory.item == firstItemID)
            {
                dialogBox.StartDialogue(characterName + "2");
            }
            else if (inventory.item == secondItemID)
            {
                dialogBox.StartDialogue(characterName + "3");
            }
            else if (inventory.item > 0)
            {
                dialogBox.StartDialogue(characterName + "Incorrect");
            }
            //if(dialogBox.activeInHierarchy)
            //{
            //    dialogBox.SetActive(false);
            //    Time.timeScale = 1f;
            //} else {
            //    dialogBox.SetActive(true);
            //    Time.timeScale = 0f;
            //    if (inventory.item == 0) // has no items
            //    {
            //        dialogText.text = dialogEmpty;

            //    } else { // has an item
            //        if (inventory.item == rightItemID) {
            //            dialogText.text = dialogRight;
            //        } else {
            //            dialogText.text = dialogWrong;
            //        }
            //    }
            //    inventory.item = 0;
            //}
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        inventory = other.gameObject.GetComponent<PlayerInventory>();
        if(other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           playerInRange = false;
           //dialogBox.SetActive(false);
        }
    }
}
