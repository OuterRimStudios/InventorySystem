using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCustomization : MonoBehaviour
{
    public Transform characterRootBone;
    public Item itemToEquip;

    [Space, Header("Inventory Templates")]
    public SkinnedMeshRenderer helmMeshRenderer;
    public Transform helmSpawnLocation;

    [Space]
    public SkinnedMeshRenderer bodyMeshRenderer;
    public Transform bodySpawnLocation;

    [Space]
    public SkinnedMeshRenderer gloveBootsMeshRenderer;
    public Transform gloveBootsSpawnLocation;

    private void Start()
    {
        EquipItem(itemToEquip.model);
    }

    public void EquipItem(GameObject item)
    {
        //GameObject newItem = Instantiate(item, itemSpawnLocation);
        //SkinnedMeshRenderer rend = newItem.GetComponent<SkinnedMeshRenderer>();
        //rend.rootBone = characterRootBone;
        //rend.bones = BuildBonesArray();
        //UpdateMeshRenderer(rend);
    }

    Transform[] BuildBonesArray()
    {
        List<Transform> boneList = new List<Transform>();
        ExtractBonesRecursively(characterRootBone, ref boneList);
        return boneList.ToArray();
    }

    void ExtractBonesRecursively(Transform bone, ref List<Transform> boneList)
    {
        boneList.Add(bone);

        for (int i = 0; i < bone.childCount; i++)
        {
            ExtractBonesRecursively(bone.GetChild(i), ref boneList);
        }
    }

     public void UpdateMeshRenderer(SkinnedMeshRenderer newMeshRenderer)
    {
        //newMeshRenderer.sharedMesh = sharedMeshExample.sharedMesh;

        //Transform[] childrens = transform.GetComponentsInChildren<Transform>(true);

        //// sort bones.
        //Transform[] bones = new Transform[sharedMeshExample.bones.Length];
        //for (int boneOrder = 0; boneOrder < sharedMeshExample.bones.Length; boneOrder++)
        //{
        //    bones[boneOrder] = Array.Find<Transform>(childrens, c => c.name == sharedMeshExample.bones[boneOrder].name);
        //}
        //newMeshRenderer.bones = bones;
    }

}
