using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item givenItem;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            givenItem.AssignOwner(collision.gameObject);
            givenItem.ActivateItem();
        }
    }
}
