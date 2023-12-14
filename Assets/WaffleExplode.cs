using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleExplode : MonoBehaviour
{
    CircleCollider2D coll;
    Shrapnel shrapnel;
    [SerializeField] private float rocketExplosionTime;
    void Start()
    {
        StartCoroutine(InitiateExplosion());
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InitiateExplosion()
    {
        yield return new WaitForSeconds(rocketExplosionTime);
        Destroy(this.gameObject);
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
            
    }

    
}
