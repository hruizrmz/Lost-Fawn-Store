using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance { get; set; }

    public bool characterEntered = false;
    public int itemHeld = 0;
    public bool firstItemDone = false;
    public List<string> itemsGiven = new List<string>(); // tracks which objects have been given already to not spawn them

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }
}
