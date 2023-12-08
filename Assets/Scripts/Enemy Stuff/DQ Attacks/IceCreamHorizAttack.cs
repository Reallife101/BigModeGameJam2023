using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamHorizAttack : Attacks
{
    [SerializeField]
    private GameObject horizAttackPrefab;
    [SerializeField]
    private float atkWaitTime = 4.8f;

    [SerializeField]
    private AI ai;
    public Animator anim;

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
            ai.canAttack = true;
        }
    }

    IEnumerator waitEnable()
    {
        Instantiate(horizAttackPrefab);

        yield return new WaitForSeconds(atkWaitTime);
        ai.canAttack = true;
    }

    public void resumeAnim()
    {
        anim.SetBool("isAttacking", false);
    }
}
