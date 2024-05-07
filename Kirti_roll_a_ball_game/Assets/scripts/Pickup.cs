using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure your player GameObject has the tag "Player"
        {
            RandomSpawner.Instance.PickupCollected();
            Destroy(gameObject);  // Destroy this pickup
        }
    }
}
