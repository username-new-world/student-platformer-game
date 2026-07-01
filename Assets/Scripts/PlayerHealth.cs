using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject gameOverScreen;
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
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        audioManager.playSFX(audioManager.playerDeath);
    }
}