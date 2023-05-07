using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    [SerializeField] private List<string> sceneNames;
    [SerializeField] private string instanceName;
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip LevelOneMusic;
    [SerializeField] private AudioClip LevelTwoMusic;
    [SerializeField] private AudioClip LevelThreeMusic;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckForDuplicateInstances();
        CheckIfSceneInList();
        ChangeSceneMusic();
    }

    void CheckForDuplicateInstances()
    {
        PersistentMusic[] collection = FindObjectsOfType<PersistentMusic>();

        foreach (PersistentMusic obj in collection)
        {
            if (obj != this)
            {
                if (obj.instanceName == instanceName)
                {
                    DestroyImmediate(obj.gameObject);
                }
            }
        }
    }

    void CheckIfSceneInList()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (!sceneNames.Contains(currentScene))
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            DestroyImmediate(this.gameObject);
        }
    }

    void ChangeSceneMusic()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MainMenu")
        {
            Source.Stop();
            Source.clip = LevelOneMusic;
            Source.Play();
        }

        else if (currentScene == "Level_01")
        {
            Source.Stop();
            Source.clip = LevelOneMusic;
            Source.Play();
        }

        else if (currentScene == "Level_02")
        {
            Source.Stop();
            Source.clip = LevelTwoMusic;
            Source.Play();
        }

        else if (currentScene == "Level_03")
        {
            Source.Stop();
            Source.clip = LevelThreeMusic;
            Source.Play();
        }
    }
}
