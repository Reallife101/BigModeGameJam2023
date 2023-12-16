using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangerZoneSpeedUp : MonoBehaviour
{
    PhaseBar pb;

    private PlayerHealth pHealth;
    private SpriteRenderer sr;
    private void Start()
    {
        pb = FindObjectOfType<PhaseBar>();
        pHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        transform.position = pHealth.transform.position;
        sr = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "hurt")
        {
            sr.enabled = true;
            pb.addCurrentHealth(Time.deltaTime);
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.enabled = false;
    }
}
