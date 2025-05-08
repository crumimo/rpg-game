using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private float hitInterval = 1f;
    [SerializeField] private int xpToGive = 50;

    public UnityEvent OnDead;
    
    private float lastHitTime = 0;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    public bool IsDead
    {
        get { return isDead; }
    }

    void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon") && !isDead && Time.time - lastHitTime > hitInterval)
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        lastHitTime = Time.time;
        currentHealth -= damage;
        if(currentHealth > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            LevelManager.instance.GiveXP(xpToGive);
            animator.SetTrigger("Dead");
            OnDead.Invoke();
            isDead = true;
        }
    }
}
