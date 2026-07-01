using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBar;

    AudioManager audioManager;
    int currentHealth;


    void Awake()
    {   
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        audioManager.playSFX(audioManager.playerDamage);

        if(currentHealth <= 0)
        {
            Dead();

        }
    }

    public void Dead()
    {
        GameManager.Instance.Lose();
        audioManager.playSFX(audioManager.playerDeath);
    }
}