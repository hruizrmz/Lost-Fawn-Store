using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Sprite openSprite, closedSprite;
    public GameObject transitionPoint;

    void Awake()
    {
        transitionPoint.SetActive(false);
        this.GetComponent<SpriteRenderer>().sprite = closedSprite;
    }
}
