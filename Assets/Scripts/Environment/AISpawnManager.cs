using UnityEngine;
using Random = UnityEngine.Random;

public class AISpawnManager : MonoBehaviour
{
    enum AIType
    {
        LASERAI,
        HEMISPHEREAI,
        SPIRALAI,
        DOUBLESPIRALAI
    }
    
    [Header("Debug")] 
    [SerializeField] private bool isDebug = true;
    [SerializeField] private float sphereRadiusSize = 0.5f;

    [SerializeField] private GameObject laserAI;
    [SerializeField] private bool spawnLaserAI;
    [SerializeField] private GameObject fireHemisphereAI;
    [SerializeField] private bool spawnHemisphereAI;
    [SerializeField] private GameObject fireSpiralAI;
    [SerializeField] private bool spawnSpiralAI;
    [SerializeField] private GameObject fireDoubleSpiralAI;
    [SerializeField] private bool spawnDoubleSpiralAI;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float thresholdForBounds = 1.0f;
    
    private Vector3 bottomLeft;
    private Vector3 upperLeft;
    private Vector3 upperRight;
    private Vector3 bottomRight;

    private float aiTimer = 0.0f;
    private float aiTime = 3.0f;
    private AIType aiType = AIType.LASERAI;
    private bool canSpawn = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;

        Vector3 leftThreshold = new Vector3(-1.0f * thresholdForBounds, thresholdForBounds, 0.0f);
        Vector3 rightTreshold = new Vector3(thresholdForBounds, thresholdForBounds, 0.0f);
        
        bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)) + leftThreshold;
        upperLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 0.0f)) + leftThreshold;
        upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f)) + rightTreshold;
        bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)) + rightTreshold;
    }

    Vector3 GetPosition()
    {
        float side = Random.Range(0.0f, 100.0f);
        float x = 0.0f;
        float y = 0.0f;

        if (aiType == AIType.LASERAI)
        {
            if (side < 33.0f)
            {
                x = bottomLeft.x;
                y = Random.Range(bottomLeft.y, upperLeft.y);
            }
            else if (side < 66.0f)
            {
                x = Random.Range(upperLeft.x, upperRight.x);
                y = upperLeft.y;
            }
            else
            {
                x = upperRight.x;
                y = Random.Range(upperRight.y, bottomRight.y);
            }
        }
        else
        {
            x = Random.Range(upperLeft.x, upperRight.x);
            y = upperLeft.y;
        }
        
        return new Vector3(x, y, 0.0f);
    }

    GameObject GetAI()
    {
        float random = Random.Range(0.0f, 100.0f);

        switch (random)
        {
            case < 65.0f:
                aiType = AIType.LASERAI;
                if (!spawnLaserAI)
                {
                    canSpawn = false;
                    return null;
                }
                canSpawn = true;
                return laserAI;
            case < 85.0f:
                aiType = AIType.HEMISPHEREAI;
                if (!spawnHemisphereAI)
                {
                    canSpawn = false;
                    return null;
                }
                canSpawn = true;
                return fireHemisphereAI;
            case < 95.0f:
                aiType = AIType.SPIRALAI;
                if (!spawnSpiralAI)
                {
                    canSpawn = false;
                    return null;
                }
                canSpawn = true;
                return fireSpiralAI;
            default:
                aiType = AIType.DOUBLESPIRALAI;
                if (!spawnDoubleSpiralAI)
                {
                    canSpawn = false;
                    return null;
                }
                canSpawn = true;
                return fireDoubleSpiralAI;
        }
    }

    void AISpawn()
    {
        if (!playerTransform)
        {
            return;
        }

        GameObject ai;

        do
        {
            ai = GetAI();    
        } while (!canSpawn);
        
        Vector3 aiPosition = GetPosition();
        Vector3 direction = playerTransform.transform.position - aiPosition;
        GameObject genAI = Instantiate(ai, aiPosition, ai.transform.rotation);
        if (aiType == AIType.LASERAI)
        {
            genAI.transform.rotation = Quaternion.FromToRotation(genAI.transform.up, direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        aiTimer += Time.deltaTime;

        if (aiTimer >= aiTime)
        {
            AISpawn();
            aiTimer = 0.0f;
        }
    }

    private void OnDrawGizmos()
    {
        if (!isDebug)
        {
            return;
        }
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(bottomLeft, sphereRadiusSize);
        Gizmos.DrawSphere(upperLeft, sphereRadiusSize);
        Gizmos.DrawSphere(upperRight, sphereRadiusSize);
        Gizmos.DrawSphere(bottomRight, sphereRadiusSize);
    }
}
