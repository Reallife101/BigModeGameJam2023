using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamHorizAttack : Attacks
{
    [SerializeField]
    private GameObject horizAttackPrefab;
    [SerializeField]
    private float atkWaitTime = 4.8f;
    private AI ai;

    void Start()
    {
        ai = GetComponent<AI>();
    }

    public override void atk()
    {
        coroutine = waitEnable();
        StartCoroutine(coroutine);
    }

    public override void stopAtk()
    {
        if (coroutine != null)
        {

            StopCoroutine(coroutine);
        }
    }

    IEnumerator waitEnable()
    {
        Instantiate(horizAttackPrefab);

        yield return new WaitForSeconds(atkWaitTime);
        ai.canAttack = true;
    }
}
