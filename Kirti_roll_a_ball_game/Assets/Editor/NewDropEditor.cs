/*#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DropdownHandler))]
public class NewDropEditor : Editor
{
    SerializedProperty colorOptions;

    private void OnEnable()
    {
        colorOptions = serializedObject.FindProperty("colorOptions");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Color Options", EditorStyles.boldLabel);

        // Display the ColorOptions list
        EditorGUILayout.PropertyField(colorOptions, true);

        serializedObject.ApplyModifiedProperties();

        DropdownHandler dropScript = (DropdownHandler)target;

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Color Selection", EditorStyles.boldLabel);

        // Dropdown for selecting color
        int selectedColorIndex = EditorGUILayout.Popup("Selected Color", dropScript.GetCurrentColorIndex(), dropScript.GetColorOptions());

        // Color field for changing color in the Inspector
        dropScript.SetSelectedColorIndex(selectedColorIndex);
        dropScript.SetSelectedColor(EditorGUILayout.ColorField("Custom Color", dropScript.GetSelectedColor()));

        if (GUILayout.Button("Apply Color"))
        {
            dropScript.ChangeWallColor(dropScript.GetSelectedColor());
        }

        if (GUILayout.Button("Reset Color"))
        {
            dropScript.ResetColorToOriginal();
        }
    }
}
#endif*/

/*using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DropdownHandler))]
public class NewDropEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DropdownHandler dropScript = (DropdownHandler)target;

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Color Options", EditorStyles.boldLabel);

        // Dropdown for selecting color
        int selectedColorIndex = EditorGUILayout.Popup("Selected Color", dropScript.GetCurrentColorIndex(), dropScript.GetColorOptions());

        // Color field for changing color in the Inspector
        dropScript.SetSelectedColorIndex(selectedColorIndex);
        dropScript.SetSelectedColor(EditorGUILayout.ColorField("Custom Color", dropScript.GetSelectedColor()));

        if (GUILayout.Button("Apply Color"))
        {
            dropScript.ChangeWallColor(dropScript.GetSelectedColor());
        }

        if (GUILayout.Button("Reset Color"))
        {
            dropScript.ResetColorToOriginal();
        }
    }
}*/