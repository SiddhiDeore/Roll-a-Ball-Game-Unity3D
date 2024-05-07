/*using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int numberOfCubes = 5;
    public float minDistance = 2.0f;

    private void Start()
    {
        SpawnCubes();
    }

    void SpawnCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;

        // Keep generating random positions until a suitable one is found
        do
        {
            randomPosition = new Vector3(
                UnityEngine.Random.Range(-5f, 5f),
                1f,
                UnityEngine.Random.Range(-5f, 5f)
            );
        } while (IsTooCloseToOtherCubes(randomPosition));

        return randomPosition;
    }

    bool IsTooCloseToOtherCubes(Vector3 position)
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("PickUp");

        foreach (var pickup in pickups)
        {
            float distance = Vector3.Distance(position, pickup.transform.position);

            if (distance < minDistance)
            {
                return true; // Too close to another pickup
            }
        }

        return false; // Not too close to any other pickups
    }
}*/

using UnityEngine;
using UnityEngine.SceneManagement;


public class RandomSpawner : MonoBehaviour
{
    /*public GameObject cubePrefab;
    public int numberOfCubes = 12;

    private void Start()
    {
        SpawnCubes();
    }

    void SpawnCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            if (randomSpawnPosition != Vector3.zero) // Check if a valid position was found
            {
                Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
                SpawnPositionManager.Instance.RegisterPosition(randomSpawnPosition);
            }
        }
    }
*/
    public static RandomSpawner Instance { get; private set; }
    public GameObject cubePrefab; // The prefab for the pickups.
    [SerializeField] private int maxPickups = 5; // The maximum number of pickups to spawn.
    private int pickupsSpawned = 0; // The number of pickups that have been spawned.
    private bool gameWon = false; // Flag to indicate if the game has been won.

    // Public property to access the maxPickups.
    public int MaxPickups => maxPickups;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pickupsSpawned = 0;
        gameWon = false;
        if (!gameWon)
        {
            SpawnCube(); // Spawn the first cube after the scene loads.
        }
    }

    public void SpawnCube()
    {
        // Check if the game has been won or the max number of pickups have been spawned.
        if (gameWon || pickupsSpawned >= maxPickups)
        {
            Debug.Log("No more pickups will be spawned. Game won or maximum reached.");
            return;
        }

        if (cubePrefab == null)
        {
            Debug.LogError("Cube Prefab is not assigned in RandomSpawner");
            return;
        }

        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        if (randomSpawnPosition != Vector3.zero)
        {
            Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
            pickupsSpawned++; // Increment the counter for each successful spawn.
            Debug.Log($"Spawned Pickup {pickupsSpawned} at: {randomSpawnPosition}");
        }
        else
        {
            Debug.Log("Failed to find a valid spawn position.");
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        int attempts = 100;
        while (attempts-- > 0)
        {
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(-5f, 5f),
                0.5f,
                UnityEngine.Random.Range(-5f, 5f)
            );

            if (SpawnPositionManager.IsPositionFree(randomPosition))
            {
                SpawnPositionManager.RegisterPosition(randomPosition);
                return randomPosition;
            }
        }
        return Vector3.zero; // Return zero if no free position is found
    }

    public void PickupCollected()
    {
        // Check if the game has been won.
        if (gameWon)
        {
            Debug.Log("Pickup collected but game is already won.");
            return;
        }

        if (pickupsSpawned >= maxPickups)
        {
            gameWon = true; // Set the game won flag.
            Debug.Log("All pickups collected, game won.");
            return;
        }
        SpawnCube(); // Attempt to spawn a new pickup when one is collected.
    }

    // Method to signal that the game has been won externally.
    public void SignalGameWon()
    {
        gameWon = true; // Set the flag when the game is won.
    }
}

