using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class MinigameManager : MonoBehaviour
    {
        public TextMesh instructionsText;
        public NVRLever ControlLever;
        public NVRInteractableItem ControlKnob;
        private int currentKnobState;
        public GameObject[] Indicators;
        private Renderer[] rendIndicators;
        [HideInInspector] public bool isFinish;
        public bool preventTextUpdate;
        [HideInInspector] public string[] selectionText;

        public GameObject minigameSimonSays;
        private MinigameSimonSays minigameSimonSaysScript;
        [HideInInspector] public bool ssPlayable = true;
        [HideInInspector] public int ssPlayed = 0;

        public GameObject minigameWhacAMole;
        private MinigameWhacAMole minigameWhacAMoleScript;
        [HideInInspector] public bool wamPlayable = true;
        [HideInInspector] public int wamPlayed = 0;

        public GameObject minigameCombat;
        private MinigameCombat minigameCombatScript;
        [HideInInspector] public bool combatPlayable = true;
        [HideInInspector] public int combatPlayed = 0;

        private AudioSource source;
        private AudioSource leverSource;

        public Material off;
        public Material on;

        private void Start()
        {
            minigameSimonSaysScript = minigameSimonSays.GetComponent<MinigameSimonSays>();
            minigameWhacAMoleScript = minigameWhacAMole.GetComponent<MinigameWhacAMole>();
            minigameCombatScript = minigameCombat.GetComponent<MinigameCombat>();

            rendIndicators = new Renderer[Indicators.Length];
            for (int i = 0; i < Indicators.Length; i++)
            {
                rendIndicators[i] = Indicators[i].GetComponent<Renderer>();
            }

            selectionText = new string[] {"NAVIGATION Simulation Selected!", "INVASION Simulation Selected!", "404 - Simulation not found!" }; //"COMBAT Simulation Selected!"
            isFinish = false;

            source = ControlKnob.GetComponent<AudioSource>();
            leverSource = ControlLever.GetComponent<AudioSource>();

            currentKnobState = 1;
        }

        private void Update()
        {
            UpdateKnobState();
            isFinish = minigameSimonSaysScript.isFinish && minigameWhacAMoleScript.isFinish;// && minigameCombatScript.isFinish;
            bool isInGame = minigameSimonSaysScript.isRunning || minigameWhacAMoleScript.isRunning; // || minigameCombatScript.isRunning;

            if (!isInGame && !preventTextUpdate)
            {
                instructionsText.text = selectionText[currentKnobState];
                switch (currentKnobState)
                {
                    case 0:
                        if (!ssPlayable)
                        {
                            instructionsText.text += "\nDisabled due to 3 unsuccessful tries.";
                        }
                        break;
                    case 1:
                        if (!wamPlayable)
                        {
                            instructionsText.text += "\nDisabled due to 3 unsuccessful tries.";
                        }
                        break;
                }
            }

            if (ControlLever.LeverEngaged == true && !isInGame && !preventTextUpdate)
            {
                switch (currentKnobState)
                {
                    case 0:
                        if (ssPlayable)
                        {
                            StartCoroutine(StartSSDelay());
                        } else
                        {
                            instructionsText.text += "\nDisabled due to 3 unsuccessful tries.";
                        }
                        break;
                    case 1:
                        if (wamPlayable)
                        {
                            StartCoroutine(StartWAMDelay());
                        } else
                        {
                            instructionsText.text += "\nDisabled due to 3 unsuccessful tries.";
                        }
                        break;
                        /*
                    case 2:
                        if (combatPlayable)
                        {
                            StartCoroutine(StartCOMBATDelay());
                        }
                        break;*/
                }
                leverSource.Play();
            }
        }

        private void UpdateKnobState()
        {
            int previousKnobState = currentKnobState;

            int rotation = ((int)ControlKnob.transform.localEulerAngles.y);

            foreach (Renderer renderer in rendIndicators)
            {
                renderer.material = off;
            }

            if ((rotation >= 317) && (rotation < 358))
            {
                currentKnobState = 0;
                
            }
            else if ((rotation > 36) && (rotation <= 76))
            {
                currentKnobState = 2;
            }
            else
            {
                currentKnobState = 1;
            }

            rendIndicators[currentKnobState].material = on;

            if (currentKnobState != previousKnobState)
            {
                source.Play();
            }

        }

        private IEnumerator StartSSDelay()
        {
            ssPlayed++;
            instructionsText.text = "You'll now have to navigate\nto the next Destination...";
            StartCoroutine(DisplayWait(3.0f));
            yield return new WaitForSeconds(3.0f);

            minigameSimonSaysScript.StartGame();
        }

        private IEnumerator StartWAMDelay()
        {
            wamPlayed++;
            instructionsText.text = "WARNING\nAlien Invasion Detected!\nYou have 60 seconds\nto save your ship from the Aliens.\nDestroy at least 15 of them!";
            StartCoroutine(DisplayWait(6.0f));
            yield return new WaitForSeconds(6.0f);

            minigameWhacAMoleScript.StartGame();
        }


        private IEnumerator StartCOMBATDelay()
        {
            combatPlayed++;
            instructionsText.text = "Enemy Ships in Sight.\nDestroy them!";
            StartCoroutine(DisplayWait(3.0f));
            yield return new WaitForSeconds(3.0f);

            minigameCombatScript.StartGame();
        }

        public IEnumerator DisplayWait(float sec)
        {
            preventTextUpdate = true;

            yield return new WaitForSeconds(sec);

            preventTextUpdate = false;

            yield break;
        }

    }
}

