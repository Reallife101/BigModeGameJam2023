using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class newBossAI : AI
{
    public static event System.Action BossDies;

    [SerializeField]
    private GameObject warning;

    [SerializeField]
    private healthBar hb;

    [SerializeField]
    private TMP_Text phaseName;

    [SerializeField]
    private List<Attacks> allAttacks;

    private List<Attacks> attackList;

    [SerializeField]
    private List<BossPhaseSO> phases;

    [SerializeField]
    private float delayBetweenAttacks;

    public bool invincible;

    private float timeElapsed;
    private float currentHealth;
    private int currentPhase;

    [SerializeField] private float timeStopLength;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        attackList = new List<Attacks>();
        timeElapsed = 0;
        currentPhase = 0;
        updatePhase();
    }

    public override void takeDamage(float dmg)
    {
        if (invincible)
        {
            return;
        }

        currentHealth -= dmg;
        hb.setSlider(currentHealth);

        if (currentHealth < 0)
        {
            //attempt to stop any attacks
            foreach (Attacks attack in attackList)
            {
                attack.stopAtk();
            }
            TimeManager.Instance.TimeSlow(timeStopLength);
            StartCoroutine(goNextPhase());
        }

    }

    public override void gainHealth(float hlth)
    {
        currentHealth += hlth;
        hb.setSlider(currentHealth);

        if (currentHealth > Health)
        {
            currentHealth = Health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentHealth > Health / 2)
        //{
        //    delayBetweenAttacks = 4;
        //}
        //else if (currentHealth > Health / 4)
        //{
        //    delayBetweenAttacks = .5f;
        //}
        //else
        //{
        //    delayBetweenAttacks = 0f;
        //}

        if (canAttack)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= delayBetweenAttacks)
            {
                anim.SetBool("isAttacking", true);
                timeElapsed = 0;
                canAttack = false;
            }
        }
    }

    public void HorizWaveAttack()
    {
        int currAttack = Random.Range(0, attackList.Count);
        attackList[currAttack].atk();
    }

    IEnumerator goNextPhase()
    {
        invincible = true;
        yield return new WaitForSeconds(1f);
        if (warning != null && currentPhase < phases.Count)
        {
            Instantiate(warning, new Vector3(0, 0, 0), Quaternion.identity);
        }
        yield return new WaitForSeconds(2f);
        currentPhase += 1;
        if (currentPhase >= phases.Count)
        {
            BossDies?.Invoke();
        }
        else
        {
            updatePhase();
            yield return new WaitForSeconds(3f);
            canAttack = true;
            invincible = false;
        }
    }

    public void updatePhase()
    {
        phaseName.text = phases[currentPhase].phaseName;
        Health = phases[currentPhase].maxHealth;
        currentHealth = Health;
        hb.sliderMax(Health);
        //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossPhase", phases[currentPhase].bossPhase);

        // Clear old attacks and add new ones
        attackList.Clear();
        foreach (int index in phases[currentPhase].attackListIndex)
        {
            attackList.Add(allAttacks[index]);
        }
    }
}
