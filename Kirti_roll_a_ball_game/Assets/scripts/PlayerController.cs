using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Text objects for displaying game information
    public TMP_Text countText; // Text for displaying the collected count
    public TMP_Text winText;   // Text for displaying win message
    public TMP_Text Timer;      // Text for displaying the game timer
    public TMP_Text gameOver;   // Text for displaying game over message
    public Button Restart;      // Button for restarting the game

    // Player movement parameters
    public float StartTime;     // Initial game time
    public float speed = 10.0f;  // Player movement speed
    public float jumpForce = 5.0f; // Force applied when the player jumps
    public float boostSpeed = 20.0f;
    private float currentSpeed;

    // Player state and control variables
    private Rigidbody rb;        // Reference to the player's rigidbody
    private int count;            // Current count of collected objects
    private float movementX;      // Horizontal movement input
    private float movementY;      // Vertical movement input
    private float elapsedTime;    // Remaining time in the game
    private bool isGameOver;      // Flag indicating if the game is over
    private bool cubeIsOnTheGround = true ; // Flag indicating if the player's cube is on the ground
    private bool timerPaused;      // Flag indicating if the game timer is paused
     
    public EnemiesManager enemiesManager;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource collectsound;

    // Initialization
    void Start()
    {
        InitializeGame();
        currentSpeed = speed;
    }

    // Initialize game state
    void InitializeGame()
    {
        // Log the name of the game object (useful for debugging)
        Debug.Log(gameObject.name + "NAME");

        // Initialize game variables
        count = 0;
        SetCountText();
        rb = GetComponent<Rigidbody>(); //Retrieves the Rigidbody component attached to the same GameObject as this script. 
        //Deactivates (hides) the GameObject associated with the variables
        winText.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        elapsedTime = StartTime;
        isGameOver = false;
        Restart.gameObject.SetActive(false);
        timerPaused = false;

    }

    //extracts the horizontal and vertical components of the input, storing them in movementX and movementY variables,
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Update()
    {        
        MovePlayer();
        UpdateTimer();
        CheckOutOfBounds();
        CheckGameStatus();
        BoostPlayer();
    }

    // Handle player movement
    void MovePlayer()
    {
        // Check if the jump key is pressed and the player is on the ground
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded())
        {
            jumpSoundEffect.Play();
            // Apply upward force for jumping 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);//Impulse means the force is applied
            cubeIsOnTheGround = false;
        }

        // Get movement input
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        // Apply force for player movement
        rb.AddForce(movement * currentSpeed);
    }

    void BoostPlayer()
    {
        // Check if the boost key (e.g., plus key) is pressed
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            StartCoroutine(ApplyBoost());
        }
    }

    IEnumerator ApplyBoost()
    {
        // Temporarily increase the player's speed
        currentSpeed = boostSpeed;

        // Wait for a certain duration (e.g., 3 seconds)
        yield return new WaitForSeconds(3.0f);

        // Reset the speed back to normal
        currentSpeed = speed;
    }


    // Handle the game timer
    void UpdateTimer()
    {
        // Check if the timer is not paused
        if (!timerPaused)
        {
            // Update the elapsed time
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(elapsedTime / 60);//the number of whole minutes from the remaining time 
                int seconds = Mathf.FloorToInt(elapsedTime % 60);//the remaining seconds after accounting for the minutes

                // Format and display the timer
                Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                // Check if time is up
                if (minutes == 0 && seconds == 0)
                {
                    isGameOver = true;
                    Timer.text = "00:00";
                }
            }
            else
            {
                elapsedTime = 0;
                Timer.text = "00:00";
            }
        }
    }

    // Check if the player is out of bounds
    void CheckOutOfBounds()
    {
        if (transform.position.y < -4)
        {
            isGameOver = true;
        }
    }

    // Check the game status
    void CheckGameStatus()
    {
        if (isGameOver)
        {
            GameOver();
        }
    }

    // Handle game over
    public void GameOver()
    {
        // Display the game over message
        gameOver.gameObject.SetActive(true);
        // Pause the timer and stop player movement
        timerPaused = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        // Display the restart button
        Restart.gameObject.SetActive(true);
    }

    // Handle pickup collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            collectsound.Play();
            // Handle the pickup collision
            Pickup(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        }
    }

    // Handle pickup logic
    void Pickup(GameObject pickup)
    {
        // Deactivate the pickup object
        pickup.SetActive(false);
        // Increment the count and update the UI
        count++;
        SetCountText();
        Debug.Log("trigger");
    }

    // Update the count text
    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();
        // Check for win condition using the MaxPickups from RandomSpawner
        if (count >= RandomSpawner.Instance.MaxPickups)
        {
            GameWin();
        }
    }

    // Handle game win
    void GameWin()
    {
        // Display the win message and restart button
        winText.gameObject.SetActive(true);
        RandomSpawner.Instance.SignalGameWon();
        Restart.gameObject.SetActive(true);
        // Pause the timer and game
        timerPaused = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    // Handle collisions with the ground
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("iscollision");
        if (collision.gameObject.name == "Plane")
        {
            cubeIsOnTheGround = true;
        }
    }

    // Check if the player is on the ground
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    // Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
