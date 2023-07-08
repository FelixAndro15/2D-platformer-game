using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth, maxHealth;
    public float invincibleLength;
    private float invincibleCounter;
    private SpriteRenderer theSR;
    public GameObject deathEffect;
    private void Awake()
    
    {
        instance = this;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if(invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }

    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Instantiate(deathEffect, transform.position, transform.rotation);
                //gameObject.SetActive(false);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);

                PlayerController.instance.KnockBack();
                AudioManager.instance.PlaySFX(9);
            }
            UIController.instance.UpdateHealthDisplay();
        }
    }
    public void HealPlayer()
    {
        //currentHealth = maxHealth;
        currentHealth++;
        if(currentHealth > maxHealth)
        {
            currentHealth=maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }
}
