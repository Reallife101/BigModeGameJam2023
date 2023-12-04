using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject conePrefab;
    private Transform t;

    private void Start()
    {
        t = GetComponentInParent<Transform>();
    }
    void SummonCone()
    {
        //shoots cone attack coming from Left to Right
        Instantiate(conePrefab, gameObject.transform.position, Quaternion.Euler(0, 0, -90 * transform.parent.localScale.x));
    }
}
