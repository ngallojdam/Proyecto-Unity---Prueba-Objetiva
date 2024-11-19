using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player
    private Rigidbody rb;

    // Variable to keep track of collected "PickUp" objects
    private int count;

    // Movement along X and Y axes
    private float movementX;
    private float movementY;

    // Speed at which the player moves
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected
    public TextMeshProUGUI countText;

    public TextMeshProUGUI totalCountText;

    // UI object to display next level text
    public GameObject nextLevelTextObject;

    // UI object to display winning text
    public GameObject winTextObject;

    // Button for to play again
    public Button buttonPlayAgain;

    // Button for to go the next level
    public Button buttonNextLevel;

    // Jump force variable
    public float jumpForce = 10;

    // Gravity modification variable
    public float gravityModifier = 2;

    // Variables for the two AudioSource
    public AudioSource audioSourcePickup;
    public AudioSource audioSourceJump;

    // Start is called before the first frame update
    void Start()
    {
        // Get and store the Rigidbody component attached to the player    
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Cannot find Rigidbody in PlayerController");
        }

        // Modify Unity's gravity property
        Physics.gravity *=  gravityModifier;

        // Assign each AudioSource to the corresponding variables
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.clip != null)
            {
                if (source.clip.name == "Boing")
                {
                    audioSourceJump = source;
                    Debug.Log("audioSourceJump OK");
                }
                else if (source.clip.name == "Ñam")
                {
                    audioSourcePickup = source;
                    Debug.Log("audioSourcePickup OK");
                }
            }
        }


        // Initialize count to zero
        count = 0;

        // Update the count display
        SetCountText();

        // Initially set the next level text to be inactive  
        if (nextLevelTextObject != null)
        {
            nextLevelTextObject.SetActive(false);
        }

        // Initially set the win text to be inactive
        if (winTextObject != null)
        {
            winTextObject.SetActive(false);
        }
    }

    // This function is called when a move input is detected
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement    
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement    
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame
    void FixedUpdate()
    {
        // Verify that the Rigidbody's is assigned
        if (rb == null)
        {
            Debug.LogError("Rigidbody (rb) is not assigned in the PlayerController.");
            return;
        }

        // Create a 3D movement vector using the X and Y inputs 
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player  
        rb.AddForce(movement * speed);

        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Verify that audioSourceJump is assigned
            if (audioSourceJump != null)
            {
                // Play the jump sound
                audioSourceJump.Play();
            }
            else
            {
                Debug.LogError("audioSourceJump no está asignado.");
            }
            // Apply jump force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called.");

        if (CountController.Instance == null)
        {
            Debug.LogError("CountController instance is null");
            return;
        }
            // Check if the object the player collided with has the "PickUp" tag
            if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear)        
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected locally  and plays the pickup sound
            count++;
            audioSourcePickup.Play();

            // Increment the global count in CountController
            CountController.Instance.AddCount(1);
            totalCountText.text = "Total Count: " + CountController.Instance.Count.ToString();
            
            // Update the count display        
            SetCountText();
           
        }
    } 

    // Function to update the displayed count of "Pickup" objects collected
    void SetCountText()
    {
        if (countText != null)
        {
            // Update the count text with the current count
            countText.text = "Count: " + count.ToString();
        }
        else
        {
            Debug.LogError("CountText is not assigned in the inspector.");
        }

        // Check if the count has reached or exceeded the next level condition    
        if (count >= 12)
        {
            // Display the next level text            
            if (nextLevelTextObject != null)
            {
                nextLevelTextObject.SetActive(true);
                buttonNextLevel.gameObject.SetActive(true);
                totalCountText.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("NextLevelTextObject is not assigned in the inspector");
            }

            // Destroy the enemy GameObject
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);

            // Update the nextLevelText to display "You Lose!"
            if (nextLevelTextObject != null)
            {
                nextLevelTextObject.SetActive(true);
                nextLevelTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                buttonPlayAgain.gameObject.SetActive(true);
            }
        }
    }
}