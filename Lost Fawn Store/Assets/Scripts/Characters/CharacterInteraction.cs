using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;


public class CharacterInteraction : MonoBehaviour
{
    private DialogueRunner dialogBox; // Yarn Dialogue Runner that handles all the lines
    private int firstItemID;
    private int secondItemID;
    private string characterName;
    private Sprite characterSprite;

    public bool playerInRange;

    private Vector3 charPos1 = new Vector3(-7.3f,-1.4f,0f); // by the door
    private Vector3 charPos2 = new Vector3(0f, -1.4f, 0f); // by the desk

    public GameObject fadingPanel;
    public float fadeTime;

    private void Start()
    {
        dialogBox = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
        characterName = ItemController.Instance.characterNames[ItemController.Instance.currentCharacter];
        characterSprite = ItemController.Instance.characterSprites[ItemController.Instance.currentCharacter];
        this.gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        if (!ItemController.Instance.characterEntered)
        {
            this.transform.position = charPos1;
            ItemController.Instance.firstItemID += 2;
            ItemController.Instance.secondItemID += 2;
            dialogBox.StartDialogue(characterName + "1");
            ItemController.Instance.firstItemDone = false;
            ItemController.Instance.itemHeld = 0;
            ItemController.Instance.characterEntered = true;
        }
        else
        {
            this.transform.position = charPos2;
        }

        firstItemID = ItemController.Instance.firstItemID;
        secondItemID = ItemController.Instance.secondItemID;
    }

    private void OnEnable()
    {
        if (ItemController.Instance != null)
        {
            characterName = ItemController.Instance.characterNames[ItemController.Instance.currentCharacter];
            characterSprite = ItemController.Instance.characterSprites[ItemController.Instance.currentCharacter];
            this.gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
            if (!ItemController.Instance.characterEntered)
            {
                this.transform.position = charPos1;
                ItemController.Instance.firstItemID += 2;
                ItemController.Instance.secondItemID += 2;
                dialogBox.StartDialogue(characterName + "1");
                ItemController.Instance.firstItemDone = false;
                ItemController.Instance.itemHeld = 0;
                ItemController.Instance.characterEntered = true;
            }
            else
            {
                this.transform.position = charPos2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            YarnGoodbye();
        }
        // Checks whether player has no items, either of the two correct items, or an incorrect item
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange && dialogBox.IsDialogueRunning == false)
        {
            if ((ItemController.Instance.itemHeld == firstItemID) && (!ItemController.Instance.firstItemDone)) // avoids repeating same response
            {
                dialogBox.StartDialogue(characterName + "2");
                ItemController.Instance.firstItemDone = true;
            }
            else if ((ItemController.Instance.itemHeld == secondItemID) && (ItemController.Instance.firstItemDone)) // avoids getting 2nd response too early
            {
                dialogBox.StartDialogue(characterName + "3");
            }
            else if (ItemController.Instance.itemHeld > 0) // Anything else is the wrong item and should indicate it
            {
                dialogBox.StartDialogue(characterName + "Incorrect");
                ItemController.Instance.itemsGiven.RemoveAt(ItemController.Instance.itemsGiven.Count - 1);
            }
            // If 0, it will not show any dialogue since the player is not holding anything
            ItemController.Instance.itemHeld = 0;
        }
    }

    [YarnCommand("transition")]
    public void YarnTransition()
    {
        StartCoroutine(FadeCo());
    }
    public IEnumerator FadeCo()
    {
        if (fadingPanel != null)
        {
            fadingPanel.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
            Instantiate(fadingPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeTime); // waits for animation to play before loading
        this.transform.position = charPos2;
    }

    [YarnCommand("goodbye")]
    public void YarnGoodbye()
    {
        StartCoroutine(GoodbyeCo());
    }
    public IEnumerator GoodbyeCo()
    {
        if (fadingPanel != null)
        {
            fadingPanel.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
            Instantiate(fadingPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeTime); // waits for animation to play before loading
        ItemController.Instance.ResetCharacter(this.gameObject);
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
        }
    }
}
