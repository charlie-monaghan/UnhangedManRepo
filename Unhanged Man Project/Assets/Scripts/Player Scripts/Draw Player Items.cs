using TMPro;
using UnityEngine;

public class DrawPlayerItems : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerManager manager;

    [Header("Item Texts")]
    [SerializeField] private TextMeshProUGUI healthUpText;
    [SerializeField] private TextMeshProUGUI jumpUpText;
    [SerializeField] private TextMeshProUGUI moveUpText;

    void Start()
    {
        Item.onUpdateItemCount += UpdateItems;
        UpdateItems();
    }

    private void OnDestroy()
    {
        Item.onUpdateItemCount -= UpdateItems;

    }

    void Update()
    {
        
    }

    private void UpdateItems()
    {
        Debug.Log("Update Items called");
        if(player != null && manager != null)
        {
            int healthUpStack = manager.GetItemStack("Health Up");
            int jumpUpStack = manager.GetItemStack("Jump Up");
            int moveUpStack = manager.GetItemStack("Move Up");
            Debug.LogWarning("item stacks = " + healthUpStack + " " + jumpUpStack + " " + moveUpStack);

            healthUpText.text = healthUpStack.ToString();
            jumpUpText.text = jumpUpStack.ToString();
            moveUpText.text = moveUpStack.ToString();
        }
    }
}
