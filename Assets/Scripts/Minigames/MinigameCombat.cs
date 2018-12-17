using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class MinigameCombat : MonoBehaviour
    {
        public GameObject spawner;
        combat_Spawnhandler spawnhandler;

        [HideInInspector] public bool isRunning;
        [HideInInspector] public bool isFinish;

        public GameObject Indicator;
        private Renderer rendIndicator;

        private void Start()
        {
            spawnhandler = spawner.GetComponent<combat_Spawnhandler>();
            isRunning = false;
            isFinish = false;

            rendIndicator = Indicator.GetComponent<Renderer>();
            rendIndicator.material.color = new Color(1, 1, 1, 0.8f);
            rendIndicator.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));
        }

        public void StartGame()
        {
            isRunning = true;
            spawnhandler.amountSpawned = 0;
            spawnhandler.SpawnObject();
        }

        public void EndGame()
        {
            isRunning = false;
            isFinish = true;

            rendIndicator.material.color = new Color(0, 1, 0, 1);
            rendIndicator.material.SetColor("_EmissionColor", new Color(0, 6, 0));
        }
    }
}
