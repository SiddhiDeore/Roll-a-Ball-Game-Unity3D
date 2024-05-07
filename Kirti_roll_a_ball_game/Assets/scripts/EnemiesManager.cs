using UnityEngine;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 5;
    public float minDistanceToPlayer = 2.0f; // Minimum distance to spawn enemies away from the player
    public float minDistanceBetweenEnemies = 1.0f; // Minimum distance between spawned enemies

    private GameObject player;
    private List<Vector3> spawnedEnemyPositions = new List<Vector3>();

    void Start()
    {
        // Assuming the player is tagged as "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }
        else
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);

            // Set the layer of the spawned enemy to "Enemy"
            enemy.layer = LayerMask.NameToLayer("Enemy");

            // Add the position of the spawned enemy to the list
            spawnedEnemyPositions.Add(randomSpawnPosition);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;

        // Keep generating random positions until a suitable one is found
        do
        {
            randomPosition = new Vector3(
                Random.Range(-5f, 5f),
                1f,
                Random.Range(-5f, 5f)
            );
        } while (Vector3.Distance(randomPosition, player.transform.position) < minDistanceToPlayer || IsTooCloseToOtherEnemies(randomPosition));

        return randomPosition;
    }

    bool IsTooCloseToOtherEnemies(Vector3 position)
    {
        // Check if the new position is too close to any previously spawned enemies
        foreach (Vector3 enemyPosition in spawnedEnemyPositions)
        {
            if (Vector3.Distance(position, enemyPosition) < minDistanceBetweenEnemies)
            {
                return true; // Too close to another enemy
            }
        }

        return false; // Not too close to any other enemies
    }
}

