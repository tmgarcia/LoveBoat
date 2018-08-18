using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Resource))]
public class ResourceDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var indent = EditorGUI.indentLevel;
        //EditorGUI.indentLevel = 0;
        //GUILayout.ExpandWidth(false);
        var labelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 50;

        EditorGUI.LabelField(position, label);

        var currentY = position.y;
        currentY += EditorGUIUtility.singleLineHeight;
        var currentX = position.x;
        var quarterWidth = position.width / 4f;
        var height = EditorGUIUtility.singleLineHeight;

        var currentPos = new Rect(currentX, currentY, quarterWidth, height);
        EditorGUI.PropertyField(currentPos, property.FindPropertyRelative("Id"));
        currentX += quarterWidth;
        currentPos = new Rect(currentX, currentY, quarterWidth, height);
        EditorGUI.PropertyField(currentPos, property.FindPropertyRelative("Label"));
        currentX += quarterWidth;
        currentPos = new Rect(currentX, currentY, quarterWidth, height);
        EditorGUI.PropertyField(currentPos, property.FindPropertyRelative("StartingValue"));
        currentX += quarterWidth;
        currentPos = new Rect(currentX, currentY, quarterWidth, height);
        EditorGUI.PropertyField(currentPos, property.FindPropertyRelative("MaxValue"));

        EditorGUI.indentLevel = indent;
        EditorGUIUtility.labelWidth = labelWidth;
        EditorGUI.EndProperty();
    }

}

