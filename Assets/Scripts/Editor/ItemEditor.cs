using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {

	SerializedProperty itemNameProp;
	SerializedProperty stackSizeProp;
	SerializedProperty slotCountProp;
	SerializedProperty uiIconProp;
	SerializedProperty modelProp;
	SerializedProperty equippableProp;

	void OnEnable()
	{
		itemNameProp = serializedObject.FindProperty("itemName");
		stackSizeProp = serializedObject.FindProperty("stackSize");
		slotCountProp = serializedObject.FindProperty("slotCount");
		uiIconProp = serializedObject.FindProperty("uiIcon");
		modelProp = serializedObject.FindProperty("model");
		equippableProp = serializedObject.FindProperty("equippable");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		itemNameProp.stringValue = EditorGUILayout.TextField("Item Name", itemNameProp.stringValue);
		stackSizeProp.intValue = EditorGUILayout.IntField("Stack Size", stackSizeProp.intValue);
		slotCountProp.intValue = EditorGUILayout.IntField(new GUIContent("Slot Count", "The number of inventory slots this item takes up."), slotCountProp.intValue);
		uiIconProp.objectReferenceValue = EditorGUILayout.ObjectField("UI Icon", uiIconProp.objectReferenceValue, typeof(Sprite), false);
		modelProp.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Item Model", "Leave blank if this item does not need a model"), modelProp.objectReferenceValue, typeof(GameObject), false);

		equippableProp.boolValue = EditorGUILayout.Toggle(new GUIContent("Equippable", "Check this box if this item can be equipped."), equippableProp.boolValue);
		
		serializedObject.ApplyModifiedProperties();
	}
}
