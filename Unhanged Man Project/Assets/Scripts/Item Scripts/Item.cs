using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject itemPrefab;
    GameObject playerRef;
    public int stackAmount;

    public event Action<string> onItemPickup;


    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void AssignOwner(GameObject GO)
    {
        playerRef = GO;
    }

    public void ActivateItem()
    {
        if(stackAmount > 0)
        {
            stackAmount++;
            onItemPickup?.Invoke(itemName);
        }
        else
        {
            stackAmount++;
            GameObject itemObject = Instantiate(itemPrefab, playerRef.transform);
        }
    }
}
