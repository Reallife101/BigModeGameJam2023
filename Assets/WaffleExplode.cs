using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleExplode : MonoBehaviour
{
    CircleCollider2D coll;
    Shrapnel shrapnel;
    [SerializeField] private float rocketExplosionTime;

    public GameObject explosionAnimPrefab;
    void Start()
    {
        StartCoroutine(InitiateExplosion());
        coll = GetComponent<CircleCollider2D>();
    }

    IEnumerator InitiateExplosion()
    {
        yield return new WaitForSeconds(rocketExplosionTime);
        Instantiate(explosionAnimPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(explosionAnimPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
            
    }

    
}
