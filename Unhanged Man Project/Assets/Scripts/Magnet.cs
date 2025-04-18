using Unity.VisualScripting;
using UnityEngine;
using System;
using Unity.Cinemachine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Magnet : MonoBehaviour
{
    private bool flag = false;
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            flag = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Player")
            other.attachedRigidbody.AddForce((this.transform.position - other.transform.position).normalized * 3f / (float)Math.Sqrt(Vector2.Distance(this.transform.position, other.transform.position)));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            flag = false;
    }

    private void Update()
    {
        if (flag)
        {
            player.GetComponent<Rigidbody2D>().AddForce((this.transform.position - player.transform.position).normalized / (float)Math.Sqrt(Vector2.Distance(this.transform.position, player.transform.position)));
        }
    }
}
