using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {

	SerializedProperty itemNameProp;
	SerializedProperty uiIconProp;
	//SerializedProperty addonsProp;

	readonly string[] objectTypes = new string[] {"int", "float", "string", "GameObject", "Object"};

	void OnEnable()
	{
		itemNameProp = serializedObject.FindProperty("itemName");
		uiIconProp = serializedObject.FindProperty("uiIcon");
		//addonsProp = serializedObject.FindProperty("addons");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		Item _item = (Item)target;

		itemNameProp.stringValue = EditorGUILayout.TextField("Item Name", itemNameProp.stringValue);
		uiIconProp.objectReferenceValue = EditorGUILayout.ObjectField("UI Icon", uiIconProp.objectReferenceValue, typeof(Sprite), false);

		if(_item.Addons != null && _item.Addons.Count > 0)
		{
			EditorGUILayout.BeginVertical("box");

			for(int i = 0; i < _item.Addons.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();

				int selectionIndex = 0;
				selectionIndex = EditorGUILayout.Popup(selectionIndex, objectTypes);

				switch(selectionIndex)
				{
					case 0:
						_item.Addons[i] = Convert.ToInt32(EditorGUILayout.IntField("Value", (int)_item.Addons[i]));
						break;
					case 1:
						_item.Addons[i] = Convert.ToDecimal(EditorGUILayout.FloatField("Value", (float)_item.Addons[i]));
						break;
					case 2:
						_item.Addons[i] = Convert.ToString(EditorGUILayout.TextField("Value", (string)_item.Addons[i]));
						break;
					case 3:
						_item.Addons[i] = (GameObject)EditorGUILayout.ObjectField((UnityEngine.Object)_item.Addons[i], typeof(GameObject), false);
						break;
					case 4:
						_item.Addons[i] = (UnityEngine.Object)EditorGUILayout.ObjectField((UnityEngine.Object)_item.Addons[i], typeof(UnityEngine.Object), false);
						break;
					default:
						break;
				}
				//DrawTypeField(_item.Addons[i]);

				if (GUILayout.Button("-", GUILayout.MinWidth(15f), GUILayout.MaxWidth(50f)))
					_item.RemoveAddon(i);

				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.EndVertical();
		}

		EditorGUILayout.BeginHorizontal("box");
		if (GUILayout.Button("+", GUILayout.MinWidth(15f), GUILayout.MaxWidth(50f)))
			_item.AddAddon();

		if (GUILayout.Button("-", GUILayout.MinWidth(15f), GUILayout.MaxWidth(50f)))
			_item.RemoveAddon(_item.Addons.Count - 1);
			
		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();
	}

	void DrawTypeField(object addon)
	{
		int selectionIndex = 0;
		selectionIndex = EditorGUILayout.Popup(selectionIndex, objectTypes);

		switch(selectionIndex)
		{
			case 0:
				addon = 0;
				addon = EditorGUILayout.IntField("Value", (int)addon);
				break;
			case 1:
				addon = 0;
				addon = (float)EditorGUILayout.FloatField("Value", (float)addon);
				break;
			case 2:
				addon = "";
				addon = (string)EditorGUILayout.TextField("Value", (string)addon);
				break;
			case 3:
				addon = new GameObject();
				addon = (GameObject)EditorGUILayout.ObjectField((UnityEngine.Object)addon, typeof(GameObject), false);
				break;
			case 4:
				addon = new UnityEngine.Object();
				addon = (UnityEngine.Object)EditorGUILayout.ObjectField((UnityEngine.Object)addon, typeof(UnityEngine.Object), false);
				break;
			default:
				break;
		}
	}
}
