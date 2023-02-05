using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;


public class CharacterInteraction : MonoBehaviour
{
    private DialogueRunner dialogBox; // Yarn Dialogue Runner that handles all the lines
    // public Text dialogText;
    // public string dialogEmpty, dialogWrong, dialogRight;
    public int firstItemID;
    public int secondItemID;

    public string characterName; // The exact name of this specific character. Case-sensitive
    public bool playerInRange;

    private Vector3 charPos1 = new Vector3(-7.3f,-1.4f,0f);
    private Vector3 charPos2 = new Vector3(0f, -1.4f, 0f);

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
            // Time.timeScale = 0f;
            if (ItemController.Instance.itemHeld == -1)
            {
                dialogBox.StartDialogue(characterName + "1");
                ItemController.Instance.firstItemDone = false;
                ItemController.Instance.itemHeld = 0;
            }
            else if((ItemController.Instance.itemHeld == firstItemID) && (!ItemController.Instance.firstItemDone)) // avoids repeating same response
            {
                dialogBox.StartDialogue(characterName + "2");
                ItemController.Instance.firstItemDone = true;
                ItemController.Instance.itemHeld = 0;
            }
            else if ((ItemController.Instance.itemHeld == secondItemID) && (ItemController.Instance.firstItemDone)) // avoids getting 2nd response too early
            {
                dialogBox.StartDialogue(characterName + "3");
                ItemController.Instance.itemHeld = -1; // leaves variable ready for next character to spawn
            }
            else if (ItemController.Instance.itemHeld == 0) // If 0, should not show dialogue since player is not holding anything
            {
                dialogBox.StartDialogue(characterName + "Incorrect");
                ItemController.Instance.itemHeld = 0;
            }
            else // Anything else is the wrong item and should indicate it
            {
                dialogBox.StartDialogue(characterName + "Incorrect");
                ItemController.Instance.itemHeld = 0;
            }
            // Time.timeScale = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
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
