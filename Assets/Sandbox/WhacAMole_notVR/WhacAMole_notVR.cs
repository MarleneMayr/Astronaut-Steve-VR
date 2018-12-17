using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhacAMole_notVR : MonoBehaviour {

    public GameObject AlienPrefab;
    GameObject AlienPrefabClone;
    public GameObject[] SpawnPositions;

    void Start()
    {
        SpawnPositions = GameObject.FindGameObjectsWithTag("Spawn");
        SpawnObject();
    }

    public void SpawnObject()
    {
        Vector3 SpawnPosition = SpawnPositions[Random.Range(0, SpawnPositions.Length)].transform.position;
        AlienPrefabClone = Instantiate(AlienPrefab, SpawnPosition, Quaternion.identity);
    }

}
