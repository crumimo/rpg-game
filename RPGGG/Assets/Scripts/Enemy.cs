using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Transform target;
   private NavMeshAgent meshAgent;

   private void Start()
   {
      meshAgent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
      meshAgent.SetDestination(target.position);
   }
}
