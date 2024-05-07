using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    public TMP_Dropdown dropdown3;
    public TMP_Dropdown dropdown4;
    public GameObject pauseMenu;

    private bool isPaused = false;
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        HidePauseMenu(); // Ensure the pause menu is initially hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
            ShowPauseMenu();
        }
        else
        {

            ResumeGame();
        }
        Debug.Log("TogglePause: " + isPaused);
    }


    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame called");
        isPaused = false;
        HidePauseMenu();
        Time.timeScale = originalTimeScale;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        // Additional logic to show dropdowns or perform other actions when paused
        dropdown1.gameObject.SetActive(true);
        dropdown2.gameObject.SetActive(true);
        dropdown3.gameObject.SetActive(true);
        dropdown4.gameObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        // Additional logic to hide dropdowns or perform other actions when resumed
        dropdown1.gameObject.SetActive(false);
        dropdown2.gameObject.SetActive(false);
        dropdown3.gameObject.SetActive(false);
        dropdown4.gameObject.SetActive(false);
    }
}
