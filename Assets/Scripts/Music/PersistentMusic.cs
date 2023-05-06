using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    [Tooltip("List of scenes this object should transition to")]
    public List<string> sceneNames;

    [Tooltip("Unique string that must be shared between scenes")]
    public string instanceName;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckForDuplicateInstances();
        CheckIfSceneInList();
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
}
