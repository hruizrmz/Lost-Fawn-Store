using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance { get; set; }

    // character data
    public int firstItemID;
    public int secondItemID;
    public bool characterEntered;
    public bool firstItemDone;

    // player data
    public int itemHeld;
    public List<string> itemsGiven = new List<string>(); // tracks which objects have been given already to not spawn them
    public List<string> characterNames = new List<string>(); // The exact name of this specific character. Case-sensitive
    public List<Sprite> characterSprites = new List<Sprite>();
    public int currentCharacter;
    public int maxCharacters;

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

    public void ResetCharacter(GameObject character)
    {
        character.SetActive(false);
        if (this.currentCharacter < (maxCharacters - 1))
        {
            this.currentCharacter++;
            this.firstItemID += 2;
            this.secondItemID += 2;
            this.characterEntered = false;
            this.firstItemDone = false;
            this.itemHeld = 0;
            character.SetActive(true);
        }
    }
}
