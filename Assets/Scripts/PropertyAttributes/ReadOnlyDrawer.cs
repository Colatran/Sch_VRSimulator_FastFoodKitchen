using UnityEditor;
using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;

        label.text = label.text.ToLower();

        string text = GetLabel(property);
        if(text.Length > 0) EditorGUI.LabelField(position, label.text, text);
        else EditorGUI.PropertyField(position, property, label, true);

        GUI.enabled = true;
    }


    private string GetLabel(SerializedProperty prop)
    {
        switch (prop.propertyType)
        {
            case SerializedPropertyType.ObjectReference:
                if (prop.objectReferenceValue == null) return "null";
                return prop.objectReferenceValue.ToString();

            case SerializedPropertyType.Integer:
                return prop.intValue.ToString();

            case SerializedPropertyType.Boolean:
                return prop.boolValue.ToString();

            case SerializedPropertyType.Float:
                return prop.floatValue.ToString();

            case SerializedPropertyType.String:
                return prop.stringValue;

            case SerializedPropertyType.Enum:
                return prop.enumDisplayNames[prop.enumValueIndex];

            default:
                return "";
        }
    }
}