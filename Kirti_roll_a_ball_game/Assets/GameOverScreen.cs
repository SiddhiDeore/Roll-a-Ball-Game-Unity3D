using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text countText;
    public void Setup()
    {
        gameObject.SetActive(true);
        
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    
}
