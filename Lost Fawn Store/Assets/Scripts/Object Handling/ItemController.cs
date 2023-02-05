using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance { get; set; }

    // character data
    public int firstItemID = -1;
    public int secondItemID = 0;
    public bool characterEntered = false;
    public bool firstItemDone = false;

    // player data
    public int itemHeld = 0;
    public List<string> itemsGiven = new List<string>(); // tracks which objects have been given already to not spawn them
    public List<string> characterNames = new List<string>(); // The exact name of this specific character. Case-sensitive
    public List<Sprite> characterSprites = new List<Sprite>();
    public int currentCharacter = 0;
    public int maxCharacters = 2;

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
            this.characterEntered = false;
            character.SetActive(true);
        }
    }
}
