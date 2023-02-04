using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    public int itemValue;

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

                if (inventory.item == 0) {
                inventory.item = itemValue;
                print("Player inventory picked up item #" + inventory.Item);
                Destroy(gameObject);
            }
            } else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
                Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            inventory = other.GetComponent<PlayerInventory>();
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
