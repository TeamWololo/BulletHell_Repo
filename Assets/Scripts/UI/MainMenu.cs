using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerData playerData = SaveSystem.LoadPlayer();
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject newGameButton;

    void Start()
    {
        if (playerData.level != null)
        {
            playButton.SetActive(false);
            continueButton.SetActive(true);
            newGameButton.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(playerData.level).buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
