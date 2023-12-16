using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class newBossAI : AI
{
    public static event System.Action BossDies;

    [SerializeField]
    private GameObject warning;

    public Slider slider;

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
    [SerializeField]
    private float currentHealth;
    private int currentPhase;

    [SerializeField] private float timeStopLength;

    [SerializeField] FMODUnity.EventReference BossAttackSound;
    [SerializeField] FMODUnity.EventReference BossHurtSound;

    public Animator anim;
    public Animator shootModeAnim;

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
        FMODUnity.RuntimeManager.PlayOneShot(BossHurtSound);
        slider.value = currentHealth;

        //if (currentHealth < 0)
        //{
        //    //attempt to stop any attacks
        //    foreach (Attacks attack in attackList)
        //    {
        //        attack.stopAtk();
        //    }
        //    TimeManager.Instance.TimeSlow(timeStopLength);
        //    StartCoroutine(goNextPhase());
        //}

    }

    public override void gainHealth(float hlth)
    {
        currentHealth += hlth;
        slider.value = currentHealth;

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

        if (currentHealth <= 0)
        {
            SceneManagement.WinScreen();
        }

        if (canAttack)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= delayBetweenAttacks)
            {
                if (anim != null)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(BossAttackSound);
                    anim.SetBool("isAttacking", true);
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShot(BossAttackSound);
                    HorizWaveAttack();
                }
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
        //phaseName.text = phases[currentPhase].phaseName;
        currentHealth = Health;
        slider.maxValue = Health;
        slider.value = currentHealth;
        //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossPhase", phases[currentPhase].bossPhase);

        // Clear old attacks and add new ones
        attackList.Clear();
        foreach (int index in phases[currentPhase].attackListIndex)
        {
            attackList.Add(allAttacks[index]);
        }
    }
}
