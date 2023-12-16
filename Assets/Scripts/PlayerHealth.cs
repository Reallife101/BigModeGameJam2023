using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static event Action PlayerHitEvent;
    public static event Action PlayerHealEvent;
    public static event Action PlayerDeathEvent;

    public DialogueTrigger dialogue;
    [SerializeField] int maxHealth;
    int currentHealth;
    [SerializeField] string enemyProjectileTag;
    [SerializeField] string borderTag;
    public bool invul = false;
    [SerializeField] int invulTime;
    [SerializeField] float timeSlowTime = 0.8f;

    [SerializeField]
    private GameObject explosion;
    [SerializeField] FMODUnity.EventReference takeDamageSound;

    public Slider slider;
    [SerializeField]
    private float deathWaitTime = 2;

    [SerializeField] FMODUnity.EventReference playerSound;

    private Animator anim;
    private SpriteRenderer sr;
    private CapsuleCollider2D ccol;
    public GameObject dZonePrefab;
    private dangerZoneSpeedUp dzone;

    private void Start()
    {
        anim = GetComponent<Animator>();
        slider.maxValue = maxHealth;
        currentHealth = maxHealth;
        slider.value = currentHealth;
        sr = GetComponent<SpriteRenderer>();
        ccol = GetComponent<CapsuleCollider2D>();
        dzone = dZonePrefab.GetComponent<dangerZoneSpeedUp>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == enemyProjectileTag || collision.gameObject.tag == borderTag) && !invul)
        {
            StartCoroutine(invincibilityCoroutine()); 
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == enemyProjectileTag || collision.gameObject.tag == borderTag) && !invul)
        {
            StartCoroutine(invincibilityCoroutine());
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        anim.SetTrigger("PlayerHurt");
        PlayerHitEvent?.Invoke();
        currentHealth--;
        TimeManager.Instance.TimeSlow(timeSlowTime);
        FMODUnity.RuntimeManager.PlayOneShot(takeDamageSound);
        slider.value = currentHealth;
        if (currentHealth <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(playerSound);
            dzone.enabled = false;
            sr.enabled = false;
            ccol.enabled = false;
            anim.SetTrigger("Dead");
            anim.SetBool("isDead", true);
            StartCoroutine(DeathWaitTime());
            
            //dialogue.TriggerPlayerDeathDialogue();
        }
    }

    private void Heal()
    {
        currentHealth++;
        PlayerHealEvent?.Invoke();
    }

    public int getHealth()
    {
        return currentHealth;
    }

    IEnumerator invincibilityCoroutine()
    {
        invul = true;
        yield return new WaitForSeconds(invulTime);
        invul = false;
    }

    IEnumerator DeathWaitTime()
    {
        yield return new WaitForSeconds(deathWaitTime);
        SceneManagement.GameOver();
    }
}
