using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player GameObject
    public GameObject player;
    
    // The distance between the camera and the player
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the initial offset between the camera's position and the player's position.
        if (player != null)
        {
            offset = transform.position - player.transform.position;
        }
        else
        {
            Debug.LogWarning("The 'player' object is not assigned in the inspector");
        }
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate()
    {
        if (player != null)
        {
            // Maintain the same offset between the camera and player throughout the game    
            transform.position = player.transform.position + offset;
        }
        else
        {
            Debug.LogWarning("The 'player' object has been destroyed, the camera has no target to follow");
        }
    }
}
