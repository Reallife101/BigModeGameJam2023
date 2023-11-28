using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBar : MonoBehaviour
{
    [SerializeField]
    private healthBar hb;

    [SerializeField]
    float maxHealth;

    float currentHealth;

    [SerializeField]
    GameObject[] cameras;

    private float elapsedTime;

    [SerializeField]
    private PlayerHealth ph;

    [SerializeField]
    private newBossAI bossAI;

    [SerializeField]
    float damageTimeLength;


    // Start is called before the first frame update
    void Start()
    {
        hb.sliderMax(maxHealth);
        hb.setSlider(0);
        currentHealth = 0f;

        foreach (GameObject go in cameras)
        {
            go.SetActive(false);
        }
        cameras[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth += Time.deltaTime;
        hb.setSlider(currentHealth);
        
        if (Input.GetKeyDown(KeyCode.E) && currentHealth >= maxHealth)
        {
            foreach (GameObject go in cameras)
            {
                go.SetActive(false);
            }
            cameras[1].SetActive(true);
            StartCoroutine(waitEnable());

        }
    }

    IEnumerator waitEnable()
    {
        ph.invul = true;
        bossAI.canAttack = false;
        while (elapsedTime < damageTimeLength)
        {

            currentHealth = Mathf.Lerp(maxHealth, 0, (elapsedTime / damageTimeLength));
            hb.setSlider(currentHealth);
            elapsedTime += Time.deltaTime;
            bossAI.canAttack = false;

            // Yield here
            yield return null;
        }
        hb.setSlider(0);

        foreach (GameObject go in cameras)
        {
            go.SetActive(false);
        }
        cameras[0].SetActive(true);

        yield return new WaitForSeconds(2.5f);
        ph.invul = false;
        bossAI.canAttack = true;
    }
}
