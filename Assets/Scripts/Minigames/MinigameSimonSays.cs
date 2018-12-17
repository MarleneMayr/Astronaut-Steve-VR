using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class MinigameSimonSays : MonoBehaviour
    {

        public GameObject board;
        private ss_board boardScript;

        public GameObject instructions;
        private ss_instructions instructionsScript;

        [HideInInspector] public bool isRunning;
        [HideInInspector] public bool isFinish;
        [HideInInspector] public int timesplayed; //logic of ss game

        public GameObject Indicator;
        private Renderer rendIndicator;

        private void Start()
        {
            boardScript = board.GetComponent<ss_board>();
            instructionsScript = instructions.GetComponent<ss_instructions>();
            isRunning = false;
            isFinish = false;

            rendIndicator = Indicator.GetComponent<Renderer>();
            rendIndicator.material.color = new Color(1, 1, 1, 0.8f);
            rendIndicator.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));
        }

        public void StartGame()
        {
            StopAllCoroutines();
            isRunning = true;
            timesplayed = 0;
            instructionsScript.InitializeSSInstructions();
        }


        public void EndGame()
        {
            isRunning = false;
            isFinish = true;
            rendIndicator.material.color = new Color(0, 1, 0, 1);
            rendIndicator.material.SetColor("_EmissionColor", new Color(0, 6, 0));
        }

        public void EndGameUnsuccessful()
        {
            isRunning = false;
        }
    }
}
