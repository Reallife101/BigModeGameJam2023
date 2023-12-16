using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject parentPrefab;
    public void DestroyObj()
    {
        Destroy(gameObject);
    }

    public void DestroyParent()
    {
        Destroy(parentPrefab);
    }
}
