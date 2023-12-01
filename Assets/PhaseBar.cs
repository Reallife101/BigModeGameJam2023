using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PhaseBar : MonoBehaviour
{
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

    [SerializeField]
    private PlayerHealth ph;

    [SerializeField]
    private newBossAI bossAI;

    [SerializeField]
    float damageTimeLength;
    [SerializeField]
    GameObject clickers;


    // Start is called before the first frame update
    void Start()
    {
        hb.sliderMax(maxHealth);
        hb.setSlider(0);
        currentHealth = 0f;

        bossCam.Priority = 20;
        damageCam.Priority = 5;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth += Time.deltaTime;
        hb.setSlider(currentHealth);

        if (Input.GetKeyDown(KeyCode.E) && currentHealth >= maxHealth)
        {
            
            StartCoroutine(waitEnable());

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
            if (ticker>1f)
            {
                ticker = 0;
                Instantiate(clickers, Vector3.zero, Quaternion.identity);
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
    }

  
}
