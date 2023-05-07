using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private GameObject deathMenuUI;

    void Update()
    {
        if (Player.Health <= 0)
        {
            deathMenuUI.SetActive(true);
        }
    }

    public void Restart()
    {
        deathMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        deathMenuUI.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
