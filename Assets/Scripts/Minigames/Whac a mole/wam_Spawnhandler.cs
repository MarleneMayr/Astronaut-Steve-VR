using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class wam_Spawnhandler : MonoBehaviour
    {

        public List<GameObject> AlienPrefabs;
        private GameObject AlienPrefabClone;
        [HideInInspector] public GameObject[] SpawnPositions;
        [HideInInspector] public int amountSpawned;
        [HideInInspector] public int amountKilled;

        private List<int> spawned;
        private int rand;

        void Start()
        {
            SpawnPositions = GameObject.FindGameObjectsWithTag("Spawn");
            spawned = new List<int>();
        }

        public void SpawnObject()
        {
            amountSpawned++;

            do
            {
                rand = Random.Range(0, SpawnPositions.Length);
            }
            while (spawned.Contains(rand));

            spawned.Add(rand);
            if (spawned.Count > 4)
            {
                spawned.RemoveAt(0);
            }

            Vector3 SpawnPosition = SpawnPositions[rand].transform.position;
            Quaternion SpawnRotation = SpawnPositions[rand].transform.rotation;

            rand = Random.Range(0, AlienPrefabs.Count);
            AlienPrefabClone = Instantiate(AlienPrefabs[rand], SpawnPosition, SpawnRotation);
        }
    }
}
