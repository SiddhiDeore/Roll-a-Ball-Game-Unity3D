using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownOperations : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    //public TMP_Text Textbox;
    public GameObject Player;
    public GameObject walls;
    private List<Color> playerOptionsColors = new List<Color>();
    private List<Color> wallOptionsColors = new List<Color>();

    private void Awake() => dropdown = GetComponent<TMP_Dropdown>();

    void Start()
    {
        playerOptionsColors.Add(Color.red);
        playerOptionsColors.Add(Color.green);
        playerOptionsColors.Add(Color.blue);

        wallOptionsColors.Add(Color.gray);
        wallOptionsColors.Add(Color.white);
        wallOptionsColors.Add(Color.black);

        dropdown.onValueChanged.AddListener(delegate { OnColorSelected(); });

        // Default initialization, you can change this according to your needs
        LoadPlayerColorOptions();
    }

    public void LoadPlayerColorOptions()
    {
        Debug.Log("LoadPlayerColorOptions called.");
        LoadColorOptions(playerOptionsColors);
    }

    public void LoadWallColorOptions()
    {
        Debug.Log("LoadPlayerwallOptions called.");
        LoadColorOptions(wallOptionsColors);
    }

    public void LoadColorOptions(List<Color> optionsColors)
    {
        if (dropdown == null)
        {
            Debug.LogError("Dropdown is null. Make sure it's assigned in the Inspector.");
            return;
        }

        dropdown.ClearOptions();

        foreach (var color in optionsColors)
        {
            string colorName = ColorUtility.ToHtmlStringRGB(color);
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = colorName });
        }

        dropdown.RefreshShownValue();
    }

    public void OnColorSelected()
    {
        int index = dropdown.value;

        if (index >= 0 && index < dropdown.options.Count)
        {
            //Textbox.text = dropdown.options[index].text;

            if (dropdown.CompareTag("Button") && Player != null)
            {
                Renderer playerRenderer = Player.GetComponent<Renderer>();
                if (playerRenderer != null)
                {
                    playerRenderer.material.color = playerOptionsColors[index];
                }
            }
            else if (dropdown.CompareTag("Button1") && walls != null)
            {
                Renderer wallRenderer = walls.GetComponent<Renderer>();
                if (wallRenderer != null)
                {
                    wallRenderer.material.color = wallOptionsColors[index];
                }
            }
        }
        
    }
}
