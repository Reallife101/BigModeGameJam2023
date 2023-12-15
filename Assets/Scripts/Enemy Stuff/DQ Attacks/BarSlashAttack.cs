using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlashAttack : Attacks
{
    [SerializeField]
    private GameObject slashAttackPrefab;

    [SerializeField]
    private float atkWaitTime = 4.8f;

    private AI ai;
    private Animator anim;

    [SerializeField]
    private GameObject[] slashWarningPrefabList;

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
        int flipSide = Random.Range(0, 2);
        if (flipSide == 0)
        {
            GameObject newObject = Instantiate(slashAttackPrefab, transform.position, Quaternion.identity);
            Instantiate(slashWarningPrefabList[0]);
        }
        if (flipSide == 1)
        {
            GameObject newObject = Instantiate(slashAttackPrefab, transform.position, Quaternion.identity);
            newObject.transform.localScale = new Vector3(-1, 1, 1);
            Instantiate(slashWarningPrefabList[1]);
        }
        yield return new WaitForSeconds(atkWaitTime);
        ai.canAttack = true;
    }
}
