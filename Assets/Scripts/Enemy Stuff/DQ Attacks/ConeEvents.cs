using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject conePrefab;
    private Transform t;

    [SerializeField]
    private Transform spawnPos;

    public GameObject parentPrefab;

    private void Start()
    {
        t = GetComponentInParent<Transform>();
    }
    void SummonCone()
    {
        //shoots cone attack coming from Left to Right
        Instantiate(conePrefab, spawnPos.position, Quaternion.Euler(0, 0, -90 * transform.parent.localScale.x));
    }

    void DestroyParentObject()
    {
        Destroy(parentPrefab);
    }
}
