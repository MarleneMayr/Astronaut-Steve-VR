using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPrefabHandler : MonoBehaviour {

    private GameObject Spawnhandler;

    private void Start()
    {
        Spawnhandler = GameObject.Find("Spawnhandler");
    }

    private void OnMouseDown()
    {
        Spawnhandler.GetComponent<WhacAMole_notVR>().SpawnObject();
        StartCoroutine(ObjectDestroy());
    }

    IEnumerator ObjectDestroy ()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
