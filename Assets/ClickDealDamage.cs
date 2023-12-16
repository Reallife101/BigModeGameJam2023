using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDealDamage : MonoBehaviour
{
    newBossAI bossAI;
    bool canClick = false;

    [SerializeField]
    float dmg;

    // Start is called before the first frame update
    void Start()
    {
        bossAI = FindObjectOfType<newBossAI>();

    }

    public void dealDamage()
    {
        if (canClick)
        {
            Debug.Log("Did Damage!");
            bossAI.takeDamage(dmg);
            bossAI.shootModeAnim.SetTrigger("DQHurt");
            Destroy(gameObject);
        }
    }

    public void destroySelf()
    {
        Destroy(gameObject);
    }

    public void canClickTrue()
    {
        canClick = true;
    }

}
