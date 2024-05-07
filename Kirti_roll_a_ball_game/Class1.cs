using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text Timer;
    public TMP_Text gameOver;


    public float StartTime;
    public float speed = 10.0f;
    public float jumpForce = 5.0f; // Adjust the jump force as needed

    public GameObject cubePrefab; // Reference to the cube prefab

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float elapsedTime;
    private bool isGameOver;
    public Button Restart;
    public bool cubeIsOnTheGround = true;
    private bool timerPaused;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + "NAME");

        count = 0;
        SetCountText();
        rb = GetComponent<Rigidbody>();

        winText.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        elapsedTime = StartTime;
        isGameOver = false;
        Restart.gameObject.SetActive(false);
        timerPaused = false;

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            cubeIsOnTheGround = false;
        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (!timerPaused)
        {
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                if (minutes == 0 && seconds == 0)
                {
                    isGameOver = true;
                    Timer.text = "00:00"; // Ensure that the displayed timer is 00:00

                }

            }

            else
            {
                elapsedTime = 0;
                Timer.text = "00:00";
            }
        }

        if (transform.position.y < -4)
        {
            isGameOver = true;

        }

        if (isGameOver)
        {
            gameOver.gameObject.SetActive(true);
            timerPaused = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            Restart.gameObject.SetActive(isGameOver);
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            Debug.Log("trigger");
        }
    }

    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();
        if (count >= 12)
        {
            winText.gameObject.SetActive(true);
            Restart.gameObject.SetActive(true);
            timerPaused = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("iscollision");
        if (collision.gameObject.name == "Plane")
        {
            cubeIsOnTheGround = true;
        }
    }

    bool isGrounded()
    {
        // Check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}


Vector3 randomSpawnPosition = new Vector3(UnityEngine.Random.Range(-4, 4), 1f, UnityEngine.Random.Range(-4, 4));
transform.position = randomSpawnPosition;