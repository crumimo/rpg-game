using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private float hitInterval = 1f;
    [SerializeField] private int xpToGive = 50;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource dieSound;

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
            hitSound.Play();
        }
        else
        {
            LevelManager.instance.GiveXP(xpToGive);
            animator.SetTrigger("Dead");
            dieSound.Play();
            OnDead.Invoke();
            if(currentHealth <= 0)
            {
                LevelManager.instance.GiveXP(xpToGive);
                animator.SetTrigger("Dead");
                dieSound.Play();
                OnDead.Invoke();
                isDead = true;

                EnemyManager manager = FindObjectOfType<EnemyManager>();
                if (manager != null)
                {
                    manager.EnemyKilled(); 
                }
                
                StartCoroutine(DestroyEnemyAfterDelay(3f)); 
            }
            isDead = true;
        }
    }
    private IEnumerator DestroyEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); 
    }

}
