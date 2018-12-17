using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class MinigameWhacAMole : MonoBehaviour
    {
        public GameObject TimerObj;
        private Timer timer;

        public GameObject MinigameManager;
        private MinigameManager MinigameManagerScript;

        public GameObject spawner;
        wam_Spawnhandler spawnhandler;

        [HideInInspector] public bool isRunning;
        [HideInInspector] public bool isFinish;

        public GameObject Indicator;
        private Renderer rendIndicator;

        private TextMesh instructionsText;

        //Countdown
        private int time = 60;
        [HideInInspector] public int timeLeft;

        private void Start()
        {
            timer = TimerObj.GetComponent<Timer>();
            timeLeft = time;
            spawnhandler = spawner.GetComponent<wam_Spawnhandler>();
            isRunning = false;
            isFinish = false;

            rendIndicator = Indicator.GetComponent<Renderer>();
            rendIndicator.material.color = new Color(1, 1, 1, 0.8f);
            rendIndicator.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));

            instructionsText = GameObject.Find("Screentext").GetComponent<TextMesh>();
            MinigameManagerScript = MinigameManager.GetComponent<MinigameManager>();
        }

        IEnumerator LoseTime()
        {
            while (timeLeft > 0)
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
                timer.decrease();
            }
        }

        public void StartGame()
        {
            StopAllCoroutines();

            timer.enable();
            timer.setMax(time);
            timeLeft = time;
            StartCoroutine("LoseTime");
            Time.timeScale = 1; //Just making sure that the timeScale is right

            isRunning = true;
            spawnhandler.amountSpawned = 0;
            spawnhandler.amountKilled = 0;
            StartCoroutine(spawnAliens());
        }

        public void EndGameUnsuccessfull()
        {
            isRunning = false;
            timer.disable();
            StopAllCoroutines();
            StartCoroutine(MinigameManagerScript.DisplayWait(4f));

            if (!isFinish)
            {
                if (MinigameManagerScript.wamPlayed >= 3)
                {
                    instructionsText.text = "You messed this Simulation up 3 times.\nYou won't be able to try again.";
                    MinigameManagerScript.wamPlayable = false;
                    rendIndicator.material.color = new Color(1, 0, 0, 1);
                    rendIndicator.material.SetColor("_EmissionColor", new Color(6, 0, 0));
                }
                else
                {
                    instructionsText.text += "You messed up your " + MinigameManagerScript.ssPlayed + ". try.\n" + (3 - MinigameManagerScript.ssPlayed) + " tries left.";
                }
            }
        }

        public void EndGame()
        {
            isRunning = false;
            isFinish = true;
            timer.disable();
            StopAllCoroutines();
            StartCoroutine(MinigameManagerScript.DisplayWait(4f));

            rendIndicator.material.color = new Color(0, 1, 0, 1);
            rendIndicator.material.SetColor("_EmissionColor", new Color(0,6,0));
        }

        IEnumerator spawnAliens()
        {
            while (timeLeft > 0)
            {
                yield return new WaitForSeconds(0.5f);

                int rand = Random.Range(0, 5); //0 to 4
                if (rand == 0) // % chance of spawning
                {
                    spawnhandler.SpawnObject();
                }
            }

            if (timeLeft == 0) {
                if (spawnhandler.amountKilled < 15)
                {
                    instructionsText.text += "\nTime is over!";
                    EndGameUnsuccessfull();
                } else
                {
                    instructionsText.text += "\nInvasion successfully stopped!";
                    EndGame();
                }
            }
        }
    }
}