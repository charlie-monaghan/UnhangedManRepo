using TMPro;
using UnityEngine;

public class DrawPlayerItems : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private Item healthUpSO;
    [SerializeField] private Item jumpUpSO;
    [SerializeField] private Item moveUpSO;

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

    private void UpdateItems()
    {
        healthUpText.text = healthUpSO.stackAmount.ToString();
        jumpUpText.text = jumpUpSO.stackAmount.ToString();
        moveUpText.text = moveUpSO.stackAmount.ToString();
    }
}
