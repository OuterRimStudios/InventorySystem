using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {

	SerializedProperty itemNameProp;
	SerializedProperty uiIconProp;
	SerializedProperty selectionIndiciesProp;
	//SerializedProperty addonsProp;

	readonly string[] objectTypes = new string[] {"int", "float", "string", "GameObject", "Object"};

	void OnEnable()
	{
		itemNameProp = serializedObject.FindProperty("itemName");
		uiIconProp = serializedObject.FindProperty("uiIcon");
		selectionIndiciesProp = serializedObject.FindProperty("selectionIndicies");
		//addonsProp = serializedObject.FindProperty("addons");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		Item _item = (Item)target;

		itemNameProp.stringValue = EditorGUILayout.TextField("Item Name", itemNameProp.stringValue);
		uiIconProp.objectReferenceValue = EditorGUILayout.ObjectField("UI Icon", uiIconProp.objectReferenceValue, typeof(Sprite), false);

		EditorGUILayout.BeginVertical("box");

		if(_item.Addons != null && _item.Addons.Count > 0)
		{
			EditorGUILayout.BeginVertical();

			for(int i = 0; i < _item.Addons.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				DrawTypeField(_item.Addons[i], i);

				if (GUILayout.Button("-", GUILayout.MinWidth(15f), GUILayout.MaxWidth(50f)))
				{
					_item.RemoveAddon(i);
					selectionIndiciesProp.DeleteArrayElementAtIndex(i);
				}

				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.EndVertical();
		}

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("+", GUILayout.MinWidth(15f), GUILayout.MaxWidth(50f)))
		{
			_item.AddAddon();
			if(selectionIndiciesProp.arraySize > 0)
				selectionIndiciesProp.InsertArrayElementAtIndex(selectionIndiciesProp.arraySize - 1);
			else
				selectionIndiciesProp.InsertArrayElementAtIndex(0);
		}

		if (GUILayout.Button("-", GUILayout.MinWidth(15f), GUILayout.MaxWidth(50f)))
		{
			_item.RemoveAddon(_item.Addons.Count - 1);
			selectionIndiciesProp.DeleteArrayElementAtIndex(selectionIndiciesProp.arraySize - 1);
		}
			
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		EditorUtility.SetDirty(target);
		serializedObject.ApplyModifiedProperties();
	}

	void DrawTypeField(object addon, int index)
	{
		selectionIndiciesProp.GetArrayElementAtIndex(index).intValue = EditorGUILayout.Popup(selectionIndiciesProp.GetArrayElementAtIndex(index).intValue, objectTypes);

		switch(selectionIndiciesProp.GetArrayElementAtIndex(index).intValue)
		{
			case 0:
				int tempInt = 0;
				tempInt = EditorGUILayout.IntField("", tempInt);
				addon = tempInt;
				break;
			case 1:
				float tempFloat = 0f;
				tempFloat = EditorGUILayout.FloatField("", tempFloat);
				addon = tempFloat;
				break;
			case 2:
				string tempString = "";
				tempString = EditorGUILayout.TextField("", tempString);
				addon = tempString;
				break;
			case 3:
				Object tempGO = new Object();
				tempGO = EditorGUILayout.ObjectField(tempGO, typeof(GameObject), false);
				addon = tempGO;
				break;
			case 4:
				Object tempObj = new Object();
				tempObj = EditorGUILayout.ObjectField(tempObj, typeof(Object), false);
				addon = tempObj;
				break;
			default:
				break;
		}
	}
}
