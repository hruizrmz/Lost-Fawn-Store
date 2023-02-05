using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using Yarn.Unity;
public class Settings : MonoBehaviour
{
    public AudioMixer main;
    public Slider musicVolume;
    public Slider ambienceVolume;
    public Slider gameVolume;
    public Slider textSpeed;
    public GameObject pauseMenu;
    private GameObject container;
    public GameObject dialog;
    public GameObject blocker;
    private bool paused = false;
    private bool disabled = false;
    public bool saved = true;
    private LineView lView;
   
    // Start is called before the first frame update
    void Start()
    {
        //if (File.Exists($"{Application.streamingAssetsPath}/preferences.json"))
        //{
        //    LoadSettings(false);
        //}
        //else
        //{
        //    LoadSettings(true);
        //}
        lView = GameObject.Find("LineView").GetComponent<LineView>();
        container = GameObject.Find("SettingsContainer");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && disabled == false && paused == false && pauseMenu != null)
        {
            Pause(0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && disabled == false && paused == true)
        {
            Pause(1);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && disabled == true && paused == true)
        {
            Disable();
        }
    }
  
    public void SetVolumeAmbience(float value)
    {
        main.SetFloat("Ambience", value);
        if(value < -20)
        {
            main.SetFloat("Ambience", -60);
        }
        saved = false;

    }
    public void SetVolumeMusic(float value)
    {
        main.SetFloat("Music", value);
        if (value < -20)
        {
            main.SetFloat("Music", -60);
        }
        saved = false;

    }
    public void SetVolumeGame(float value)
    {
        main.SetFloat("Game", value);
        if (value < -20)
        {
            main.SetFloat("Game", -60);
        }
        saved = false;

    }

    public void SetTextSpeed(float value)
    {
        lView.typewriterEffectSpeed = value;
        saved = false;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Disable()
    {
        if (saved == false)
        {
            dialog.SetActive(true);
            blocker.SetActive(true);
        }
        else if (saved == true)
        {
            container.SetActive(false);
        }
    }

    public void SaveSettings()
    {
        Preferences prefs = new Preferences(musicVolume.value, ambienceVolume.value, gameVolume.value, textSpeed.value);
        string filepath = $"{Application.streamingAssetsPath}/preferences.json";
        string json = JsonUtility.ToJson(prefs);
        File.WriteAllText(filepath, json);
        saved = true;
    }
    public void LoadSettings(bool defaults)
    {
        string filepath = $"{Application.streamingAssetsPath}/defaults.json";
        if (defaults == false && File.Exists($"{Application.streamingAssetsPath}/preferences.json"))
        {
            filepath = $"{Application.streamingAssetsPath}/preferences.json";
        }
        string json = File.ReadAllText(filepath);
        Preferences prefs = JsonUtility.FromJson<Preferences>(json);
        musicVolume.value = prefs.music;
        ambienceVolume.value = prefs.ambience;
        gameVolume.value = prefs.game;
        textSpeed.value = prefs.textSpeed;
        saved = true;
        SetVolumeMusic(musicVolume.value);
        SetVolumeAmbience(ambienceVolume.value);
        SetVolumeGame(gameVolume.value);
    }
      
    public void Pause(int time)
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        paused = !paused;
        Time.timeScale = time;       
    }
    public void DisableControl()
    {
        disabled = !disabled;
    }

}
[System.Serializable]
public class Preferences
{
    public float music;
    public float ambience;
    public float game;
    public float textSpeed;
    public Preferences(float music, float ambience, float game, float textSpeed)
    {
        this.music = music;
        this.ambience = ambience;
        this.game = game;
        this.textSpeed = textSpeed;
    }

}
