using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public bool playerInRange;
    public int itemValue;
    public DoorController door;

    private ItemController itemController;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            itemController = ItemController.Instance;
            if (itemController.itemHeld == 0)
            {
                itemController.itemHeld = itemValue;
                print("Player inventory picked up item #" + itemController.itemHeld);
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
