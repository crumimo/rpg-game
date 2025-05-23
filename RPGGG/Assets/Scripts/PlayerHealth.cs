using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private float hitInterval = 5f;
    [SerializeField] private int healthGainedPerLevel = 10;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource dieSound;
    
    private float lastHitTime = 0;
    private int currentHealth;
    private Animator animator;
    private int currentMaxHealth;
    
    public static bool isAlive = true;

    void Awake()
    {
        currentHealth = startingHealth;
        currentMaxHealth = startingHealth;
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    public void OnLevelGained(int newLevel)
    {
        currentMaxHealth = startingHealth + (newLevel-1) * healthGainedPerLevel;
        currentHealth = currentMaxHealth;
    }

    public float GetHealthRatio()
    {
        return (float)currentHealth / (float) currentMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon") && isAlive &&
            Time.time - lastHitTime > hitInterval)
        {
            TakeDamage(5);
        }
    }

    private void TakeDamage(int damage)
    {
        lastHitTime = Time.time;
        currentHealth -= damage;
        Debug.Log("Current health " + currentHealth);
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
            hitSound.Play();
        }
        else
        {
            isAlive = false;
            animator.SetTrigger("Death");
            dieSound.Play();
            DeathScreen ds = FindObjectOfType<DeathScreen>();
            if(ds != null)
            {
                ds.ShowDeathScreen();
            }
        }
    }

}
