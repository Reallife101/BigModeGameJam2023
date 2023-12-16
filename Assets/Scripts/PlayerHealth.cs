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


    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        slider.maxValue = maxHealth;
        currentHealth = maxHealth;
        slider.value = currentHealth;
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
            Instantiate(explosion, transform.position, Quaternion.identity);
            PlayerDeathEvent?.Invoke();
            Destroy(gameObject);
            dialogue.TriggerPlayerDeathDialogue();
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
}
