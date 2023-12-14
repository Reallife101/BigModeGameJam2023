using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelMove : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public Vector2 direction;

    void Start()
    {
        rb.velocity = direction * speed;
        StartCoroutine(DestroyShrapnel());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "borders")
        {
            Destroy(this.gameObject);
        }
            
    }

    IEnumerator DestroyShrapnel()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}
