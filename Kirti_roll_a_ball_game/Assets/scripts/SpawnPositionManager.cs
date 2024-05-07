using UnityEngine;
using System.Collections.Generic;

public class SpawnPositionManager : MonoBehaviour
{
    //public static SpawnPositionManager Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private static List<Vector3> occupiedPositions = new List<Vector3>();
    public static float minDistance = 1.0f; // Minimum distance between any two spawnable objects

    public static bool IsPositionFree(Vector3 position)
    {
        foreach (var occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(position, occupiedPosition) < minDistance)
            {
                return false; // Position is too close to an occupied position
            }
        }
        return true; // Position is free
    }

    public static void RegisterPosition(Vector3 position)
    {
        occupiedPositions.Add(position);
        Debug.Log("Position registered: " + position);
    }

    public static void UnregisterPosition(Vector3 position)
    {
        occupiedPositions.Remove(position);
        Debug.Log("Position unregistered: " + position);
    }
}

