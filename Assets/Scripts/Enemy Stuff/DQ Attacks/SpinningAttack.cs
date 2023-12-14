using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAttack : Attacks
{
    [SerializeField]
    private Transform[] spawnPosList;

    [SerializeField]
    private Transform parentSpawnPos;

    [SerializeField]
    private GameObject bnanaFellasPrefab;
    public Animator anim;

    [SerializeField]
    private float animWaitTime; // cries

    [SerializeField]
    private int bulletCount = 10;

    [SerializeField]
    private float bulletWaitTime = 0.1f;

    [SerializeField]
    private float atkWaitTime;

    [SerializeField]
    private AI ai;

    public Animator upDownStart;

    private void Start()
    {
        ai = GetComponent<AI>();
    }

    public override void atk()
    {
        coroutine = spray1();
        StartCoroutine(coroutine);
    }

    IEnumerator spray1()
    {
        Transform spawnPosL = spawnPosList[0];
        Transform spawnPosR = spawnPosList[1];

        GameObject bnaFella1 = Instantiate(bnanaFellasPrefab, spawnPosL.transform.position, Quaternion.identity) ;
        bnaFella1.transform.parent = parentSpawnPos;

        GameObject bnaFella2 = Instantiate(bnanaFellasPrefab, spawnPosR.transform.position, Quaternion.identity);
        bnaFella2.transform.parent = parentSpawnPos;
        upDownStart.enabled = true;

        yield return new WaitForSeconds(animWaitTime);
        for (int i = 0; i < bulletCount; i++)
        {
            Instantiate(projectile, spawnPosL.transform.position, spawnPosL.transform.rotation);
            yield return new WaitForSeconds(bulletWaitTime);
            Instantiate(projectile, spawnPosR.transform.position, spawnPosR.transform.rotation);
            yield return new WaitForSeconds(bulletWaitTime);
        }
        upDownStart.enabled = false;
        yield return new WaitForSeconds(atkWaitTime);
        ai.canAttack = true;
    } 

    public override void stopAtk()
    {
        anim.SetBool("isAttacking", false);
    }
}
