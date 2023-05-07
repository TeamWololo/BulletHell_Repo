using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [Header("Debug")] 
    [SerializeField] private bool isDebug = true;
    [SerializeField] private float sphereRadiusSize = 0.5f;

    [Header("Astroid")] 
    [SerializeField] private GameObject astroid;
    [SerializeField] private float thresholdAstroidArea = 1.0f;
    [SerializeField] private float astroidTime = 3.0f;
    private Vector3 astroidAreaLeftSide;
    private Vector3 astroidAreaRightSide;
    private float astroidTimer = 0.0f;

    [Header("Laser")] 
    [SerializeField] private GameObject warningSign;
    [SerializeField] private GameObject laser;
    [SerializeField] private float thresholdLaserArea = 1.0f;
    [SerializeField] private float laserTime = 5.0f;
    private Vector3 laserAreaTopSide;
    private Vector3 laserAreaBottomSide;
    private float laserTimer = 0.0f;

    [Header("BallisticWeapon")] 
    [SerializeField] private GameObject ballisticPickup;
    [SerializeField] private float ballisticMinTimeToSpawn = 10.0f;
    [SerializeField] private float ballisticMaxTimeToSpawn = 20.0f;
    private float ballisticPickupTimer = 0.0f;
    private float ballisticTime = 0.0f;
    
    [Header("LaserWeapon")] 
    [SerializeField] private GameObject playerLaserPickup;
    [SerializeField] private float playerLaserMinTimeToSpawn = 15.0f;
    [SerializeField] private float playerLaserMaxTimeToSpawn = 25.0f;
    private float playerLaserPickupTimer = 0.0f;
    private float playerLaserTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        Vector3 upperLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(Vector3.zero);
        
        astroidAreaLeftSide = upperLeft + new Vector3(0.0f, thresholdAstroidArea, 0.0f);
        astroidAreaRightSide = upperRight + new Vector3(0.0f, thresholdAstroidArea, 0.0f);
        laserAreaTopSide = upperLeft + new Vector3(-1.0f * thresholdLaserArea, 0.0f, 0.0f);
        laserAreaBottomSide = lowerLeft + new Vector3(-1.0f * thresholdLaserArea, 0.0f, 0.0f);

        ballisticTime = Random.Range(ballisticMinTimeToSpawn, ballisticMaxTimeToSpawn);
        playerLaserTime = Random.Range(playerLaserMinTimeToSpawn, playerLaserMaxTimeToSpawn);
    }

    void CreateAstroid()
    {
        float randomPos = Random.Range(astroidAreaLeftSide.x, astroidAreaRightSide.x);
        GameObject genAstroid = Instantiate(astroid, new Vector3(randomPos, astroidAreaLeftSide.y, 0.0f), Quaternion.identity);
    }
    
    void CreateLaser()
    {
        StartCoroutine(StartLaser());
    }

    IEnumerator StartLaser()
    {
        float randomPos = Random.Range(laserAreaBottomSide.y + 1.2f, laserAreaTopSide.y - 1.2f);
        GameObject genWarningSign = Instantiate(warningSign, new Vector3(laserAreaTopSide.x + 3 * thresholdLaserArea, randomPos, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(genWarningSign);
        GameObject genLaser = Instantiate(laser, new Vector3(laserAreaTopSide.x, randomPos, 0.0f), laser.transform.rotation);
        yield return new WaitForSeconds(3.0f);
        Destroy(genLaser);
    }

    void CreateBallisticPickup()
    {
        float randomPos = Random.Range(astroidAreaLeftSide.x, astroidAreaRightSide.x);
        GameObject genBallisticPickup = Instantiate(ballisticPickup, new Vector3(randomPos, astroidAreaLeftSide.y, 0.0f), Quaternion.identity);
        ballisticTime = Random.Range(ballisticMinTimeToSpawn, ballisticMaxTimeToSpawn);
        StartCoroutine(DestroyPickup(genBallisticPickup));
    }

    void CreatePlayerLaserPickup()
    {
        float randomPos = Random.Range(astroidAreaLeftSide.x, astroidAreaRightSide.x);
        GameObject genPlayerLaserPickup = Instantiate(playerLaserPickup, new Vector3(randomPos, astroidAreaLeftSide.y, 0.0f), Quaternion.identity);
        playerLaserTime = Random.Range(playerLaserMinTimeToSpawn, playerLaserMaxTimeToSpawn);
        StartCoroutine(DestroyPickup(genPlayerLaserPickup));
    }

    IEnumerator DestroyPickup(GameObject pickup)
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(pickup);
    }

    // Update is called once per frame
    void Update()
    {
        astroidTimer += Time.deltaTime;
        laserTimer += Time.deltaTime;
        ballisticPickupTimer += Time.deltaTime;
        playerLaserPickupTimer += Time.deltaTime;
        
        if (astroidTimer > astroidTime)
        {
            CreateAstroid();
            astroidTimer = 0.0f;
        }

        if (laserTimer > laserTime)
        {
            CreateLaser();
            laserTimer = 0.0f;
        }

        if (ballisticPickupTimer > ballisticTime)
        {
            CreateBallisticPickup();
            ballisticPickupTimer = 0.0f;
        }

        if (playerLaserPickupTimer > playerLaserTime)
        {
            CreatePlayerLaserPickup();
            playerLaserPickupTimer = 0.0f;
        }
    }

    private void OnDrawGizmos()
    {
        if (!isDebug)
        {
            return;
        }
        
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(astroidAreaLeftSide, sphereRadiusSize);
        Gizmos.DrawSphere(astroidAreaRightSide, sphereRadiusSize);
        Gizmos.DrawSphere(laserAreaBottomSide, sphereRadiusSize);
        Gizmos.DrawSphere(laserAreaTopSide, sphereRadiusSize);
    }
}
