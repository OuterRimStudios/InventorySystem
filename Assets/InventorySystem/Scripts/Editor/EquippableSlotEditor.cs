using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EquippableSlot))]
public class EquippableSlotEditor : Editor
{
    SerializedProperty equippableSlotTypeProp;
    SerializedProperty equippableSlotTypeIndexProp;

    void OnEnable()
    {
        equippableSlotTypeProp = serializedObject.FindProperty("equippableSlotType");
        equippableSlotTypeIndexProp = serializedObject.FindProperty("equippableSlotTypeIndex");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical("box");
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("Add a reference to an EquippableSlotTypes Scriptable Object if you are going to equip this item.", MessageType.Info);
        EditorGUILayout.Space();

        EquippableSlot _equippableSlot = (EquippableSlot)target;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        if (_equippableSlot.equippableSlotTypeSO != null)
        {
            equippableSlotTypeIndexProp.intValue = EditorGUILayout.Popup(equippableSlotTypeIndexProp.intValue, _equippableSlot.equippableSlotTypeSO.equippableSlotTypes);
            equippableSlotTypeProp.stringValue = _equippableSlot.equippableSlotTypeSO.equippableSlotTypes[equippableSlotTypeIndexProp.intValue];
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
