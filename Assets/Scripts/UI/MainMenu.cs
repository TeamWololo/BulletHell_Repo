using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private float defaultVolume = -30f;
    private PlayerData playerData;
    void Start()
    {
        optionsMenu.SetVolume(defaultVolume);
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
        File.Delete(Application.persistentDataPath + "/player.bin");
        StartCoroutine(MenuUpdate());
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(playerData.level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator MenuUpdate()
    {
        yield return new WaitForSeconds(1);
        playButton.SetActive(true);
        continueButton.SetActive(false);
        newGameButton.SetActive(false);
    }
}
