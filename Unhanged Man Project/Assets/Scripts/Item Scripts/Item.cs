using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] Sprite sprite;
    [SerializeField] public GameObject itemPrefab;
    GameObject playerRef;
    public int stackAmount;

    public event Action<string> onItemPickup;
    public static event Action onUpdateItemCount;

    private void OnEnable()
    {
        stackAmount = 0;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public GameObject GetPlayer()
    {
        return playerRef;
    }

    public void AssignOwner(GameObject GO)
    {
        playerRef = GO;
    }

    public void ActivateItem()
    {
        if (stackAmount > 0)
        {
            stackAmount++;
            onUpdateItemCount?.Invoke();
            Debug.Log("stackamount = " + stackAmount);
            onItemPickup?.Invoke(itemName);
        }
        else
        {
            stackAmount++;
            onUpdateItemCount?.Invoke();
            Debug.Log("" + stackAmount);
            GameObject itemObject = Instantiate(itemPrefab, playerRef.transform);
        }
    }
}
