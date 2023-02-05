using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;


public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    private Vector2 playerPos;
    public VectorValue oldPlayerPos;

    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    private DialogueRunner dRun;
    public float fadeTime;

    public List<string> scenes;
    private bool playerInRange = false;

    private void Awake()
    {
        if(GameObject.Find("Dialogue System") != null)
        {
            dRun = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
        }

        scenes = new List<string>() { "FrontDesk", "Ballroom", "FoodRoom", "LostMedia" };
        string currentScene = SceneManager.GetActiveScene().name;
        scenes.Remove(currentScene);

        if (fadeInPanel != null) // if fade animation is defined, play and then destroy
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            /*StartCoroutine(FadeCo(true));*/ // replaces the normal LoadScene function
            dRun.StartDialogue("RoomChoice");
        }
    }

    [YarnCommand("sceneswap")]
    public void SceneChoice(string sceneName)
    {
        switch (sceneName)
        {
            case "FrontDesk":
                playerPos = new Vector2(0.8f, 2.5f);
                break;
            case "FoodRoom":
                playerPos = new Vector2(-7.0f, 2.8f);
                break;
            case "LostMedia":
                playerPos = new Vector2(0.8f, 2.3f);
                break;
            case "Ballroom":
                playerPos = new Vector2(4.5f, 1.0f);
                break;
            default:
                playerPos = new Vector2(0.0f, 0.0f);
                break;
        }
        oldPlayerPos.initialValue = playerPos;
        StartCoroutine(FadeCo(false, sceneName));
    }

    [YarnFunction("getScenes")]
    public static string GetAllScenes(int sceneNum)
    {
        SceneTransition st = GameObject.Find("Transition").GetComponent<SceneTransition>();
        List<string> sceneList = st.scenes;
        return sceneList[sceneNum];
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public IEnumerator FadeCo(bool defaultScene, string scene = "")
    {
        string sceneName = sceneToLoad;
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeTime); // waits for animation to play before loading
        if (defaultScene == false)
        {
            sceneName = scene;
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone) // async indicates when the scene is done loading
        {
            yield return null;
        }
    }
}
