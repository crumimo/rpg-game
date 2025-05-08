using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField] private GameObject toSpawn;
   [SerializeField] private float spawnInterval = 2f;
   [SerializeField] private int spawnCount = 3;
   [SerializeField] private Transform target;


   void Start()
   {
      StartSpawn();
   }

   public virtual void StartSpawn()
   {
      for (int i = 0; i < spawnCount; i++)
      {
         Invoke("SpawnObject", i * spawnInterval);
      }
   }
   
   private void SpawnObject()
   {
      GameObject spawnedObject = Instantiate(toSpawn, transform.position, Quaternion.identity);
      Enemy enemy = spawnedObject.GetComponent<Enemy>();
      enemy.target = target;
   }
}
