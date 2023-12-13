using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAttack : Attacks
{
    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private GameObject spinAttackPrefab;

    public Animator anim;

    [SerializeField]
    private int bulletCount = 10;

    [SerializeField]
    private float bulletWaitTime = 0.1f;

    [SerializeField]
    private float atkWaitTime = 1;

    [SerializeField]
    private AI ai;
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
        //Instantiate(spinAttackPrefab);
        //Transform spawnPos = spinAttackPrefab.GetComponentInChildren<Transform>();
        
        for (int i = 0; i < bulletCount; i++)
        {
            Instantiate(projectile, spawnPos.transform.position, spawnPos.transform.rotation);
            yield return new WaitForSeconds(bulletWaitTime);
        }
        Debug.Log("DONE SHOOTING WAHOO");
        yield return new WaitForSeconds(atkWaitTime);
        ai.canAttack = true;
    } 

    public override void stopAtk()
    {
        anim.SetBool("isAttacking", false);
    }
}
