using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class combat_Spawnhandler : MonoBehaviour
    {

        public List<GameObject> ShipPrefabs;
        private GameObject ShipPrefabClone;
        [HideInInspector] public int amountSpawned;

        private int[] spawnsY;
        private List<int> spawned;
        private int randX;
        private int randY;
        private int intZ;

        private int randShip;
        private Vector3 SpawnPosition;
        private Quaternion SpawnRotation;

        void Start()
        {
            spawnsY = new int[] { 0, 5, 10, 15, 20 };
            intZ = 40;
            spawned = new List<int>();
        }

        public void SpawnObject()
        {
            amountSpawned++;

            do
            {
                randY = Random.Range(0, spawnsY.Length);
            }
            while (spawned.Contains(randY));
            spawned.Add(randY);
            if (spawned.Count > 2)
            {
                spawned.RemoveAt(0);
            }

            randX = Random.Range(0, 2); //returns 0 or 1
            randX = (randX == 0) ? -40 : 40;
            Debug.Log("randX: " + randX);

            SpawnPosition = new Vector3(randX, spawnsY[randY], intZ);
            //SpawnPosition = new Vector3(0, 0, 0);

            if (randX == -40)
            {
                SpawnRotation.eulerAngles = new Vector3(0, 130, 0);
            }
            else
            {
                SpawnRotation.eulerAngles = new Vector3(0, -130, 0);
            }

            randShip = Random.Range(0, ShipPrefabs.Count);
            ShipPrefabClone = Instantiate(ShipPrefabs[randShip], SpawnPosition, SpawnRotation);

            /*
            if (randX == -40)
            {
                ShipPrefabClone.GetComponent<Animator>().Play("flyRight");
            }
            else
            {
                ShipPrefabClone.GetComponent<Animator>().Play("flyLeft");
            }
            */
        }
    }
}
