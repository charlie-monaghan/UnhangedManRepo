using System.Collections;
using UnityEngine;

public class GivePlayerItems : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(Item i in PlayerManager.instance.playerItems)
             {
                i.AssignOwner(gameObject);
                Instantiate(i.itemPrefab, gameObject.transform);
             }
        
    }

    void Update()
    {
        
    }
}
