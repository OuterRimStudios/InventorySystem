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
	SerializedProperty equippableSlotTypeProp;
	SerializedProperty equippableSlotTypeIndexProp;

	void OnEnable()
	{
		itemNameProp = serializedObject.FindProperty("itemName");
		stackSizeProp = serializedObject.FindProperty("stackSize");
		slotCountProp = serializedObject.FindProperty("slotCount");
		uiIconProp = serializedObject.FindProperty("uiIcon");
		modelProp = serializedObject.FindProperty("model");
		equippableProp = serializedObject.FindProperty("equippable");
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
		
		Item _item = (Item)target;

		itemNameProp.stringValue = EditorGUILayout.TextField("Item Name", itemNameProp.stringValue);
		stackSizeProp.intValue = EditorGUILayout.IntField("Stack Size", stackSizeProp.intValue);
		slotCountProp.intValue = EditorGUILayout.IntField(new GUIContent("Slot Count", "The number of inventory slots this item takes up."), slotCountProp.intValue);
		uiIconProp.objectReferenceValue = EditorGUILayout.ObjectField("UI Icon", uiIconProp.objectReferenceValue, typeof(Sprite), false);
		modelProp.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Item Model", "Leave blank if this item does not need a model"), modelProp.objectReferenceValue, typeof(GameObject), false);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		equippableProp.boolValue = EditorGUILayout.Toggle(new GUIContent("Equippable", "Check this box if this item can be equipped."), equippableProp.boolValue);

		if(equippableProp.boolValue)
		{
			//DrawDefaultInspector();
			if(_item.equippableSlotTypeSO != null)
			{
				equippableSlotTypeIndexProp.intValue = EditorGUILayout.Popup(equippableSlotTypeIndexProp.intValue, _item.equippableSlotTypeSO.equippableSlotTypes);
				equippableSlotTypeProp.stringValue = _item.equippableSlotTypeSO.equippableSlotTypes[equippableSlotTypeIndexProp.intValue];
			}
		}

		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
		
		serializedObject.ApplyModifiedProperties();
	}
}
