using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool gamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject deathMenuUI;

    void Update()
    {
        if (deathMenuUI.gameObject.activeInHierarchy == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Restart()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        SaveSystem.SavePlayer(SceneManager.GetActiveScene().name, pauseMenuUI.GetComponentInChildren<SliderUpdate>().GetMasterVolume());
        SceneManager.LoadScene(0);
    }
}
