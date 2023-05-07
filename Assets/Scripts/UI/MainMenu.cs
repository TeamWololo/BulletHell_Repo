using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject newGameButton;
    private PlayerData playerData;

    void Start()
    {
        playerData = SaveSystem.LoadPlayer();
        
        if (playerData != null)
        {
            playButton.SetActive(false);
            continueButton.SetActive(true);
            newGameButton.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(playerData.level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
