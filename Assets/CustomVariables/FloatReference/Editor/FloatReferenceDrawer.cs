using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Get the bool to prompt good field
        bool useConstant = property.FindPropertyRelative("useConstant").boolValue;

        // Label
        position = EditorGUI.PrefixLabel(position, label);

        // Box to choose constant or reference
        var box = new Rect(position.position, Vector2.one * 20);
        if (EditorGUI.DropdownButton(box, GUIContent.none, FocusType.Passive))
        {
            // Create the menu and add items
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Constant"), useConstant, () => SetBoolValue(property, true));
            menu.AddItem(new GUIContent("Reference"), !useConstant, () => SetBoolValue(property, false));
            menu.ShowAsContext();
        }

        // Move position so the field do not go on top of the dropdown button
        position.position += Vector2.right * 25;

        if (useConstant)
        {
            float constantValue = property.FindPropertyRelative("constant").floatValue;

            // Prompt the current constant value
            string value = EditorGUI.TextField(position, constantValue.ToString());

            // Get the value from the text field and apply it to the property
            float.TryParse(value, out constantValue);
            property.FindPropertyRelative("constant").floatValue = constantValue;
        }
        else
            EditorGUI.ObjectField(position, property.FindPropertyRelative("reference"), GUIContent.none);

        EditorGUI.EndProperty();
    }

    // Set the bool value of the property
    void SetBoolValue(SerializedProperty property, bool useConstant)
    {
        property.FindPropertyRelative("useConstant").boolValue = useConstant;
        property.serializedObject.ApplyModifiedProperties();
    }
}
