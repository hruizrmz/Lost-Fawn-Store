using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public Vector2 playerPos;
    public VectorValue oldPlayerPos;

    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeTime;

    private bool playerInRange = false;

    private void Awake()
    {
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
            oldPlayerPos.initialValue = playerPos;
            StartCoroutine(FadeCo()); // replaces the normal LoadScene function
        }
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

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeTime); // waits for animation to play before loading
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone) // async indicates when the scene is done loading
        {
            yield return null;
        }
    }
}
