using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Transform target;
   [SerializeField] private Collider swordCollider;
   [SerializeField] private float attackInterval = 5f;

   private float attackTime = 0;
   private NavMeshAgent meshAgent;
   private Animator animator;
   public Transform player;
   private bool isDead = false;

   private void Start()
   {
      swordCollider.enabled = false;
      meshAgent = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();
   }

   public void StartAttacking()
   {
      swordCollider.enabled = true;
   }

   public void EndAttacking()
   {
      swordCollider.enabled = false;
   }

   public void OnDead()
   {
      isDead = true;
      meshAgent.isStopped = true;
   }

   private void Update()
   {
      if (isDead)
         return;
      meshAgent.SetDestination(target.position);
      if (Vector3.Distance(transform.position, player.position) > 0.5f)
      {
         meshAgent.isStopped = false;
         meshAgent.SetDestination(player.position);
         animator.SetBool("isRunning", true);
      }
      else
      {
         meshAgent.isStopped = true;
         animator.SetBool("isRunning", false);
         if (Time.time - attackTime > attackInterval)
         {
            
         }
      }
   }
}
