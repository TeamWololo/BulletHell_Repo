using System.Collections;
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
    [SerializeField] private GameObject laser;
    [SerializeField] private float thresholdLaserArea = 1.0f;
    [SerializeField] private float laserTime = 5.0f;
    private Vector3 laserAreaTopSide;
    private Vector3 laserAreaBottomSide;

    private float laserTimer = 0.0f;

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
    }

    void CreateAstroid()
    {
        float randomPos = Random.Range(astroidAreaLeftSide.x, astroidAreaRightSide.x);
        GameObject genAstroid = Instantiate(astroid, new Vector3(randomPos, astroidAreaLeftSide.y, 0.0f), Quaternion.identity);
    }

    void CreateLaser()
    {
        float randomPos = Random.Range(laserAreaTopSide.y, laserAreaBottomSide.y);
        GameObject genLaser = Instantiate(laser, new Vector3(laserAreaTopSide.x, randomPos, 0.0f), Quaternion.identity);
        StartCoroutine(DestroyLaser(genLaser));
    }

    IEnumerator DestroyLaser(GameObject _laser)
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(_laser);
    }

    // Update is called once per frame
    void Update()
    {
        astroidTimer += Time.deltaTime;
        laserTimer += Time.deltaTime;
        
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
