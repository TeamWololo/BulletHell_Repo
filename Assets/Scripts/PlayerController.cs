using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minSpeed = 10.0f;
    [SerializeField] private float boundaryThreshold = 0.69f;
    [SerializeField] private float speedIncDecSize = 0.5f;

    private Player _player;

    private float leftEdgeX = 0.0f;
    private float rightEdgeX = 0.0f;
    private float upEdgeY = 0.0f;
    private float bottomEdgeY = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!Camera.main)
        {
            Debug.Log("Cannot found main camera");
            return;
        }

        Camera mainCamera = Camera.main;
        
        leftEdgeX  = mainCamera.ViewportToWorldPoint(Vector3.zero).x + boundaryThreshold;
        rightEdgeX = mainCamera.ViewportToWorldPoint(Vector3.right).x - boundaryThreshold;
        upEdgeY = mainCamera.ViewportToWorldPoint(Vector3.up).y - boundaryThreshold;
        bottomEdgeY = mainCamera.ViewportToWorldPoint(Vector3.zero).y + boundaryThreshold;

        _player = GetComponent<Player>();
    }

    void ControlSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _player.Speed += speedIncDecSize;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.Speed = _player.Speed - speedIncDecSize < minSpeed ? minSpeed : _player.Speed - speedIncDecSize;
        }
    }
    
    Vector3 GetNewPosition(Vector3 currentPosition)
    {
        if (Input.GetKey(KeyCode.D))
        {
            currentPosition.x += (_player.Speed * Time.deltaTime);

            if (currentPosition.x >= rightEdgeX)
            {
                currentPosition.x = rightEdgeX;
            }
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            currentPosition.x -= (_player.Speed * Time.deltaTime);
            
            if (currentPosition.x <= leftEdgeX)
            {
                currentPosition.x = leftEdgeX;
            }
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            currentPosition.y += (_player.Speed * Time.deltaTime);
            
            if (currentPosition.y >= upEdgeY)
            {
                currentPosition.y = upEdgeY;
            }
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            currentPosition.y -= (_player.Speed * Time.deltaTime);
            
            if (currentPosition.y <= bottomEdgeY)
            {
                currentPosition.y = bottomEdgeY;
            }
        }

        return currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        ControlSpeed();
        Vector3 currentPosition = GetNewPosition(this.transform.position);
        this.transform.position = currentPosition;
    }
    
}// end of PlayerController
