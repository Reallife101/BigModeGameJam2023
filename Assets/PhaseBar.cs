using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PhaseBar : MonoBehaviour
{
    public DialogueTrigger dialogue;
    [SerializeField]
    private healthBar hb;

    [SerializeField]
    float maxHealth;

    float currentHealth;

    [SerializeField]
    CinemachineVirtualCamera bossCam;
    [SerializeField]
    CinemachineVirtualCamera damageCam;

    private float elapsedTime;

    public SpriteRenderer bossShootModeSprite;

    [SerializeField]
    private PlayerHealth ph;

    [SerializeField]
    private newBossAI bossAI;

    [SerializeField]
    float damageTimeLength;
    [SerializeField]
    GameObject clickers;

    [SerializeField]
    private float summonShootRate;

    [SerializeField]
    private Canvas canvas;

    public GameObject animPrefab;
    public Animator animMeter;
    public void addCurrentHealth(float f)
    {
        currentHealth += f;
    }


    // Start is called before the first frame update
    void Start()
    {
        hb.sliderMax(maxHealth);
        hb.setSlider(0);
        currentHealth = 0; 

        bossCam.Priority = 20;
        damageCam.Priority = 5;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth += Time.deltaTime;
        hb.setSlider(currentHealth);

        if (currentHealth >= maxHealth)
        {
            animMeter.SetBool("isMeter", true);
        }

        if (Input.GetKeyDown(KeyCode.E) && currentHealth >= maxHealth)
        {
            Instantiate(animPrefab);
            bossShootModeSprite.enabled = true;
            StartCoroutine(waitEnable());
            animMeter.SetBool("isMeter", false);

        }
    }

    IEnumerator waitEnable()
    {
        ph.invul = true;
        bossAI.canAttack = false;

        bossCam.Priority = 5;
        damageCam.Priority = 20;

        float ticker=0;
        elapsedTime = 0;
        while (elapsedTime < damageTimeLength)
        {
            currentHealth = Mathf.Lerp(maxHealth, 0, (elapsedTime / damageTimeLength));
            hb.setSlider(currentHealth);
            elapsedTime += Time.deltaTime;
            bossAI.canAttack = false;

            
            ticker += Time.deltaTime;
            if (ticker>summonShootRate)
            {
                ticker = 0;
                float x = Random.Range(150f, Screen.width-150f);
                float y = Random.Range(150f, Screen.height- 150f);
                Vector2 newPosition = Camera.main.ScreenToWorldPoint(new Vector2(x, y));

                //Instantiate(clickers, newPosition, Quaternion.identity);
                
                Instantiate(clickers, newPosition, Quaternion.identity, canvas.transform);
            }
            
            // Yield here
            yield return null;
        }
        hb.setSlider(0);

        bossCam.Priority = 20;
        damageCam.Priority = 5;

        currentHealth = 0;
        yield return new WaitForSeconds(2.5f);
        ph.invul = false;
        bossAI.canAttack = true;
        bossShootModeSprite.enabled = false;
        int dialogueNum = Random.Range(1, 5);
        if(dialogueNum == 1)
        {
            dialogue.TriggerDialogue1();
        }
        else if(dialogueNum == 2)
        {
            dialogue.TriggerDialogue2();
        }
         else if(dialogueNum == 3)
        {
            dialogue.TriggerDialogue3();
        }
        else if(dialogueNum == 4)
        {
            dialogue.TriggerDialogue4();
        }
    }



  
}
