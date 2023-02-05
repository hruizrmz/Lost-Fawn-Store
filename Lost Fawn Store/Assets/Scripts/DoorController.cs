using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Sprite openSprite, closedSprite;
    public GameObject transitionPoint;
    public bool closeDoor = true;

    void Awake()
    {
        if (ItemController.Instance.itemHeld > 0)
        {
            transitionPoint.SetActive(true);
            this.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else
        {
            transitionPoint.SetActive(false);
            this.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }
}
