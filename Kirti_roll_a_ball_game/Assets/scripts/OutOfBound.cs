using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public Transform player;
    public float x, y, z;
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.position = new Vector3(x, y, z);

        }
    }

    internal bool IsWithinBound(Vector3 position)
    {
        throw new NotImplementedException();
    }
}
