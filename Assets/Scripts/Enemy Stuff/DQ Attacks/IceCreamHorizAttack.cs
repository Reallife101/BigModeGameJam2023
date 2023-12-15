using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamHorizAttack : Attacks
{
    [SerializeField]
    private GameObject horizAttackPrefab;
    [SerializeField]
    private float atkWaitTime;

    private AI ai;
    private Animator anim;

    private void Start()
    {
        ai = GetComponent<newBossAI>();
        anim = GetComponent<Animator>();
    }

    public override void atk()
    {
        coroutine = waitEnable();
        StartCoroutine(coroutine);
    }

    public override void stopAtk()
    {
        anim.SetBool("isAttacking", false);
    }

    IEnumerator waitEnable()
    {
        Instantiate(horizAttackPrefab);
        yield return new WaitForSeconds(atkWaitTime);
        ai.canAttack = true;
    }
}
