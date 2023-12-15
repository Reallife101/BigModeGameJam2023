using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shrapnel : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public Transform firePoint;
    public GameObject shrapnelPrefab;
    Vector2 direction;
    
    public float shrapnelWaitTime; // must be the same duration as rocketExplosionTime
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createShrapnel());
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    IEnumerator createShrapnel()
    {;
        yield return new WaitForSeconds(shrapnelWaitTime);
        GameObject go = Instantiate(shrapnelPrefab, firePoint.position, Quaternion.identity);
        ShrapnelMove goShrapnel = go.GetComponent<ShrapnelMove>();
        goShrapnel.direction = direction;
        dialogueTrigger.TriggerBeforeAttackDialogue();
    }    
}
