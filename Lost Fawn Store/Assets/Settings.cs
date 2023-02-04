using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
public class Settings : MonoBehaviour
{
    public AudioMixer main;
    public Slider musicVolume;
    public Slider ambienceVolume;
    public Slider gameVolume;
    public GameObject pauseMenu;
    private bool paused = false;
    private bool disabled = false;
    public bool saved = true;
   
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists($"{Application.streamingAssetsPath}/preferences.json"))
        {
            LoadSettings(false);
        }
        else
        {
            LoadSettings(true);
        }
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
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
   
    public void SaveSettings()
    {
        Preferences prefs = new Preferences(musicVolume.value, ambienceVolume.value, gameVolume.value);
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
    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
    }

}
[System.Serializable]
public class Preferences
{
    public float music;
    public float ambience;
    public float game;
    public Preferences(float music, float ambience, float game)
    {
        this.music = music;
        this.ambience = ambience;
        this.game = game;
    }
    
}
