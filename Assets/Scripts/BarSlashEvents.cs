using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlashEvents : MonoBehaviour
{
    public GameObject parentPrefab;

    void DestroyParentObject()
    {
        Destroy(parentPrefab);
    }
}
