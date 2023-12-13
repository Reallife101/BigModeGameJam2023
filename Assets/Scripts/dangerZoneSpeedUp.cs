using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangerZoneSpeedUp : MonoBehaviour
{
    PhaseBar pb;
    private void Start()
    {
        pb = FindObjectOfType<PhaseBar>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        pb.addCurrentHealth(Time.deltaTime);
    }
}
