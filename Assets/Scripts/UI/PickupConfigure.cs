using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupConfigure : MonoBehaviour
{
    [SerializeField] private GameObject RocketCounter;
    [SerializeField] private TextMeshProUGUI RocketCounterText;
    [SerializeField] private GameObject LaserCounter;
    [SerializeField] private TextMeshProUGUI LaserCounterText;
    private PlayerController playerController;

    void Update()
    {
        if (playerController.BallisticCounter > 0)
        {
            Debug.Log("Hi IF");
            RocketCounter.SetActive(true);
            RocketCounterText.text = playerController.BallisticCounter.ToString();
        }
        else
        {
            Debug.Log("Hi ELSE");
            RocketCounter.SetActive(false);
            RocketCounterText.text = playerController.BallisticCounter.ToString();
        }

        if (playerController.PlayerLaserCounter > 0)
        {
            LaserCounter.SetActive(true);
            LaserCounterText.text = playerController.PlayerLaserCounter.ToString();
        }
        else
        {
            LaserCounter.SetActive(false);
            LaserCounterText.text = playerController.PlayerLaserCounter.ToString();
        }
    }
}
