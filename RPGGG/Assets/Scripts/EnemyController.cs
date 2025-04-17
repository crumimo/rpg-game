using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    
    
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        //Destroy(gameObject);
    }
}
