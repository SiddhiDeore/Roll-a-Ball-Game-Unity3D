using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DropdownHandler : MonoBehaviour
{
    public TMP_Text Textbox;
    private TMP_Dropdown dropdown;

    public Material Object;
    private Color originalColor;
    private Color selectedColor;

    [SerializeField]
    private ColorOptions colorOptions;

    [System.Serializable]
    public class ColorOptions
    {
        public List<ColorOption> options = new List<ColorOption>();
    }

    [System.Serializable]
    public class ColorOption
    {
        public string name;
        public Color color;
    }

    void Start()
    {
        dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        originalColor = Object.color;

        UpdateDropdownOptions();

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void UpdateDropdownOptions()
    {
        dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "default" });

        foreach (var option in colorOptions.options)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = option.name });
        }
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        if (index == 0)
        {
            ResetColorToOriginal();
            Textbox.text = gameObject.name;
        }
        else
        {
            Color selectedColor = colorOptions.options[index - 1].color;
            ChangeWallColor(selectedColor);
        }
    }

    public void ChangeWallColor(Color newColor)
    {
        Object.color = newColor;
    }

    public void ResetColorToOriginal()
    {
        Object.color = originalColor;
    }

    // Additional methods for editor script
    public int GetCurrentColorIndex()
    {
        return dropdown.value;
    }

    // Additional methods for editor script
    public void SetSelectedColorIndex(int index)
    {
        dropdown.value = index;
    }

    public void SetSelectedColor(Color color)
    {
        selectedColor = color;
    }

    public Color GetSelectedColor()
    {
        int index = dropdown.value;
        return index == 0 ? Color.white : colorOptions.options[index - 1].color;
    }

    public string[] GetColorOptions()
    {
        List<string> colorNames = new List<string> { "Choose Color" };

        foreach (var option in colorOptions.options)
        {
            colorNames.Add(option.name);
        }

        return colorNames.ToArray();
    }

}

/*using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    public TMP_Text Textbox;
    private TMP_Dropdown dropdown;

    public Material walls;
    private Color originalColor;
    private Color selectedColor;

    private Dictionary<string, Color> colorOptions = new Dictionary<string, Color>();

    void Start()
    {
        dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        originalColor = walls.color;

        colorOptions.Add("Choose Color", Color.white); // Default color
        colorOptions.Add("Red", Color.red);
        colorOptions.Add("Blue", Color.blue);
        colorOptions.Add("Black", Color.black);
        colorOptions.Add("Green", Color.green);

        UpdateDropdownOptions();

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void UpdateDropdownOptions()
    {
        foreach (var item in colorOptions.Keys)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        string selectedColorName = dropdown.options[index].text;

        if (colorOptions.ContainsKey(selectedColorName))
        {
            Color selectedColor = colorOptions[selectedColorName];
            if (selectedColorName == "Choose Color")
            {
                ResetColorToOriginal();
                Textbox.text = "Choose the Color";
            }
            else
            {
                ChangeWallColor(selectedColor);
            }
        }
    }

    public void ChangeWallColor(Color newColor)
    {
        walls.color = (newColor == Color.white) ? selectedColor : newColor;
    }

    public void ResetColorToOriginal()
    {
        walls.color = originalColor;
    }

    // Additional methods for editor script
    public int GetCurrentColorIndex()
    {
        return dropdown.value;
    }

    public string[] GetColorOptions()
    {
        List<string> colorNames = new List<string>(colorOptions.Keys);
        return colorNames.ToArray();
    }

    public void SetSelectedColorIndex(int index)
    {
        dropdown.value = index;
    }

    public void SetSelectedColor(Color color)
    {
        selectedColor = color;

        // Additional logic to update dictionary if needed
        string selectedColorName = dropdown.options[dropdown.value].text;
        colorOptions[selectedColorName] = selectedColor;
    }

    public Color GetSelectedColor()
    {
        string selectedColorName = dropdown.options[dropdown.value].text;
        return colorOptions.ContainsKey(selectedColorName) ? colorOptions[selectedColorName] : Color.white;
    }

    // Additional method for editor script
    public void AddCustomColor(Color customColor)
    {
        string colorName = "Custom Color";
        colorOptions[colorName] = customColor;

        // Clear and update dropdown options
        dropdown.options.Clear();
        UpdateDropdownOptions();
    }
}
*/



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    public TMP_Text Textbox;
    private TMP_Dropdown dropdown;
    public GameObject TargetObject; // Single GameObject or Parent GameObject containing walls
    private Color defaultColor;
    private List<Color> optionsColors = new List<Color>();
    private List<string> items = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        // Get the material color from the first Renderer found in children
        Renderer[] renderers = TargetObject.GetComponentsInChildren<Renderer>();
        Debug.Log(renderers.Length);
        if (renderers.Length > 1)
        {
            //Debug.Log(renderers.Length);
            items.Add("Choose color");
            items.Add("Yellow");
            items.Add("Cyan");
            items.Add("Magnenta");
            defaultColor = renderers[0].material.color;
            optionsColors.Add(defaultColor);
            // Add additional colors if needed
            optionsColors.Add(Color.yellow);
            optionsColors.Add(Color.cyan);
            optionsColors.Add(Color.magenta);

        }

        else
        {
            items.Add("Choose color");
            items.Add("Red");
            items.Add("Black");
            items.Add("Grey");
            
            optionsColors.Add(defaultColor);
            // Add additional colors if needed
            optionsColors.Add(Color.red);
            optionsColors.Add(Color.black);
            optionsColors.Add(Color.grey);


        }

        foreach (var item in items)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        if (index >= 0 && index < optionsColors.Count)
        {
            Textbox.text = dropdown.options[index].text;

            // Change the color of the target object
            ChangeColor(TargetObject, optionsColors[index]);
        }
        else
        {
            Textbox.text = "No such option available";
        }
    }

    // Change the color of a GameObject
    void ChangeColor(GameObject obj, Color color)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer objRenderer in renderers)
        {
            objRenderer.material.color = color;
        }
    }
}*/
