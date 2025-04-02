using UnityEngine;
using UnityEngine.UI;

public class DrawWeapons : MonoBehaviour
{
    [SerializeField] private Image currentSprite;
    [SerializeField] private Image secondSprite;
    [SerializeField] private GameObject playerRef;

    private PlayerAttack playerAttack;

    void Start()
    {
        playerAttack = playerRef.GetComponent<PlayerAttack>();
        playerAttack.onWeaponChange += UpdateWeapons;
        UpdateWeapons();
    }

    private void OnDestroy()
    {
        playerAttack.onWeaponChange -= UpdateWeapons;
    }

    void Update()
    {

    }

    void UpdateWeapons()
    {
        if (playerAttack.currentWeapon != null)
        {
            currentSprite.sprite = playerAttack.currentWeapon.GetSprite();
            currentSprite.enabled = true;
        }

        if (playerAttack.secondWeapon != null)
        {
            secondSprite.sprite = playerAttack.secondWeapon.GetSprite();
            secondSprite.enabled = true;
        }
    }
}