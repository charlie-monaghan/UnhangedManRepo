using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    [SerializeField] Item itemSO;
    //public int stackAmount;

    void Start()
    {
        ItemEffect();
    }

    void Update()
    {
        
    }

    public virtual void ItemEffect()
    {

    }

    public void UpdateItem(string _itemName)
    {
        if(_itemName == itemSO.GetItemName())
        {
            ItemEffect();
        }
    }
}
