using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shrapnelPrefab;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createShrapnel());
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    IEnumerator createShrapnel()
    {;
        yield return new WaitForSeconds(4f);
        GameObject go = Instantiate(shrapnelPrefab, firePoint.position, Quaternion.identity);
        ShrapnelMove goShrapnel = go.GetComponent<ShrapnelMove>();
        goShrapnel.direction = direction;
    }
}
