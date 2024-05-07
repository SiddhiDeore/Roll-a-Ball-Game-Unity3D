using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AudioSource EnemycollisionSound;
    private void OnCollisionEnter(Collision collision)
    {
        // Access the PlayerController script and call its GameOver function
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {            
            playerController.GameOver();
            playerController.gameObject.GetComponent<MeshRenderer>().enabled=false;
            EnemycollisionSound.Play();

            Debug.Log("collision enemy");
        }
    }

}
