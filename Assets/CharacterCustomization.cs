using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCustomization : MonoBehaviour
{
    [Space, Header("Default Inventory Templates")]
    public Item defaultHelmItem;
    public SkinnedMeshRenderer defaultHelmMeshTemplate;
    public Transform defaultHelmRootBone;
    public Transform defaultHelmSpawnLocation;

    [Space]
    public Item defaultSuitItem;
    public SkinnedMeshRenderer defaultSuitMeshTemplate;
    public Transform defaultSuitRootBone;
    public Transform defaultSuitSpawnLocation;

    [Space]
    public Item defaultGloveBootsItem;
    public SkinnedMeshRenderer defaultGloveBootsMeshTemplate;
    public Transform defaultGloveBootsRootBone;
    public Transform defaultGloveBootsSpawnLocation;

    [Space, Header("Inventory Templates")]
    public SkinnedMeshRenderer helmMeshTemplate;
    public Transform helmRootBone;
    public Transform helmSpawnLocation;

    [Space]
    public SkinnedMeshRenderer suitMeshTemplate;
    public Transform suitRootBone;
    public Transform suitSpawnLocation;

    [Space]
    public SkinnedMeshRenderer gloveBootsMeshTemplate;
    public Transform gloveBootsRootBone;
    public Transform gloveBootsSpawnLocation;

    SkinnedMeshRenderer meshTemplate;
    Transform itemSpawnLocation;
    Transform rootBone;
    Item defaultItem;

    Dictionary<string, GameObject> equippedItems = new Dictionary<string, GameObject>();

    private void Start()
    {
        EquipDefaultItem("Helmet");
        EquipDefaultItem("Suit");
        EquipDefaultItem("GlovesBoots");
    }

    public void EquipItem(Item item)
    {
        switch (item.equippableSlotType)
        {
            case "Helmet":
                meshTemplate = helmMeshTemplate;
                itemSpawnLocation = helmSpawnLocation;
                rootBone = helmRootBone;
                break;
            case "Suit":
                meshTemplate = suitMeshTemplate;
                itemSpawnLocation = suitSpawnLocation;
                rootBone = suitRootBone;
                break;
            case "GlovesBoots":
                meshTemplate = gloveBootsMeshTemplate;
                itemSpawnLocation = gloveBootsSpawnLocation;
                rootBone = gloveBootsRootBone;
                break;
        }

        GameObject newItem = Instantiate(item.model, itemSpawnLocation);

        if (equippedItems.ContainsKey(item.equippableSlotType))
        {
            Destroy(equippedItems[item.equippableSlotType]);
            equippedItems[item.equippableSlotType] = newItem;
        }
        else
            equippedItems.Add(item.equippableSlotType, newItem);

        SkinnedMeshRenderer rend = newItem.GetComponent<SkinnedMeshRenderer>();
        rend.rootBone = rootBone;
        rend.bones = BuildBonesArray();
        UpdateMeshRenderer(rend, meshTemplate);
    }

    public void EquipDefaultItem(string slotType)
    {
        switch (slotType)
        {
            case "Helmet":
                defaultItem = defaultHelmItem;
                meshTemplate = helmMeshTemplate;
                itemSpawnLocation = helmSpawnLocation;
                rootBone = helmRootBone;
                break;
            case "Suit":
                defaultItem = defaultSuitItem;
                meshTemplate = suitMeshTemplate;
                itemSpawnLocation = suitSpawnLocation;
                rootBone = suitRootBone;
                break;
            case "GlovesBoots":
                defaultItem = defaultGloveBootsItem;
                meshTemplate = gloveBootsMeshTemplate;
                itemSpawnLocation = gloveBootsSpawnLocation;
                rootBone = gloveBootsRootBone;
                break;
        }

        GameObject newItem = Instantiate(defaultItem.model, itemSpawnLocation);

        if (equippedItems.ContainsKey(slotType))
        {
            Destroy(equippedItems[slotType]);
            equippedItems[slotType] = newItem;
        }
        else
            equippedItems.Add(slotType, newItem);

        SkinnedMeshRenderer rend = newItem.GetComponent<SkinnedMeshRenderer>();
        rend.rootBone = rootBone;
        rend.bones = BuildBonesArray();
        UpdateMeshRenderer(rend, meshTemplate);
    }


    Transform[] BuildBonesArray()
    {
        List<Transform> boneList = new List<Transform>();
        ExtractBonesRecursively(rootBone, ref boneList);
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

     public void UpdateMeshRenderer(SkinnedMeshRenderer meshRenderer, SkinnedMeshRenderer _meshTemplate)
    {
        meshRenderer.sharedMesh = _meshTemplate.sharedMesh;

        Transform[] childrens = transform.GetComponentsInChildren<Transform>(true);

        // sort bones.
        Transform[] bones = new Transform[_meshTemplate.bones.Length];
        for (int boneOrder = 0; boneOrder < _meshTemplate.bones.Length; boneOrder++)
        {
            bones[boneOrder] = Array.Find<Transform>(childrens, c => c.name == _meshTemplate.bones[boneOrder].name);
        }
        meshRenderer.bones = bones;
    }
}
