using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class ss_tutorial_start : MonoBehaviour
    {
        public GameObject Minigame;
        private MinigameSimonSays MinigameScript;

        //public GameObject MinigameManager;
        //private MinigameManager MinigameManagerScript;

        public GameObject tutMiniGameManager;
        private tutorial_MinigameManager tutMiniGameManagerScript;

        [Header("Audio")]
        public GameObject audiosourceObject;
        private AudioSource audiosource;
        public AudioClip WinSound;
        public AudioClip LoseSound;

        [Header("Stuff")]
        private TextMesh instructionsText;

        public GameObject board;
        private ss_board boardScript;

        public NVRSlider slider;
        private int reqSlidervalue;

        public List<NVRLever> flipList;
        private string[] flipTexts;
        private List<bool> reqList; //required List, the User has to follow

        private bool success;

        public Material[] skyboxMaterial;
        private int matIndex;

        public GameObject Indicator;
        private Renderer rendIndicator;

        //Countdown
        /*
        private int time;
        [HideInInspector] public int timeLeft;
        public GameObject TimerObj;
        private Timer timer;
        */

        private bool ss_InstructionsEnabled = false;
        public bool Ss_InstructionsEnabled
        {
            get { return ss_InstructionsEnabled; }
            set { ss_InstructionsEnabled = value; }
        }

        private void Start()
        {
            matIndex = 0;
            RenderSettings.skybox = skyboxMaterial[matIndex++];
            //time = 60;
            //timeLeft = time;
            //timer = TimerObj.GetComponent<Timer>();
            audiosource = audiosourceObject.GetComponent<AudioSource>();
            instructionsText = GameObject.Find("Screentext").GetComponent<TextMesh>();
            MinigameScript = Minigame.GetComponent<MinigameSimonSays>();
            //MinigameManagerScript = MinigameManager.GetComponent<MinigameManager>();
            tutMiniGameManagerScript = tutMiniGameManager.GetComponent<tutorial_MinigameManager>();
            boardScript = board.GetComponent<ss_board>();
            flipTexts = new string[] { "Warp Mode", "Defensive Shields", "Space-Time Continuum", "Death Ray" };
            rendIndicator = Indicator.GetComponent<Renderer>();
        }

        public void StartGame()
        {
            StartCoroutine(StartSSDelay());
            MinigameScript.isRunning = true;
            MinigameScript.timesplayed = 0;
        }

        public void SS_StartGame()
        {
            StopAllCoroutines();
            InitializeSSInstructions();
        }

        public void InitializeSSInstructions()
        {
            /*
            switch (MinigameScript.timesplayed)
            {
                case 0:
                    {
                        time = 30;
                        break;
                    }
                case 2:
                    {
                        time = 25;
                        break;
                    }
                case 4:
                    {
                        time = 20;
                        break;
                    }
            }
            timer.enable();
            timer.setMax(time);
            timeLeft = time;
            StartCoroutine(LoseTime());
            Time.timeScale = 1; //Just making sure that the timeScale is right
            */

            ss_InstructionsEnabled = true;
            string output = "Set the values to the following:\n";

            reqList = new List<bool>();
            for (int i = 0; i < flipList.Count; i++)
            {
                int rand = Random.Range(0, 2); //0 or 1
                reqList.Add((rand < 1) ? false : true);
                if (rand < 1)
                {
                    output = output + (flipTexts[i] + ": OFF\n"); //i+1 because of the liststart at 0
                }
                else
                {
                    output = output + (flipTexts[i] + ": ON\n"); //i+1 because of the liststart at 0
                }
            }

            reqSlidervalue = Random.Range(1, 6); //1, 2, 3, 4, or 5
            output += "slider: " + reqSlidervalue;

            instructionsText.text = output;

            ss_InstructionsEnabled = true;
        }

        public void InstructionsCorrect()
        {
            success = true;
            for (int i = 0; i < reqList.Count; i++)
            {
                //if the current position of the lever flip is on, ie down, it counts a false which is a Off in our case.
                bool leverpos = (flipList[i].CurrentLeverPosition == NVRLever.LeverPosition.On) ? false : true;

                //if the lever's position and the required position don't match, the submit is wrong
                if (leverpos != reqList[i])
                {
                    success = false;
                    break;
                }
            }

            //Todo: wenn status des Sliders NICHT mit der Anweisung übereinstimmt
            int slidervalue;
            if (slider.CurrentValue == 1)
            {
                slidervalue = 5;
            }
            else
            {
                slidervalue = Mathf.FloorToInt(slider.CurrentValue * 5 + 1);
            }

            Debug.Log(slidervalue);

            if (slidervalue != reqSlidervalue)
            {
                success = false;
            }

            //Responds to the submit
            if (success == true)
            {
                GameWon();
            }
            else
            {
                GameOver("Wrong Input");
            }
        }

        public void GameOver(string message)
        {
            StartCoroutine(tutMiniGameManagerScript.SetBool1(1.0f));
            audiosource.clip = LoseSound;
            audiosource.Play();
            instructionsText.text = message;
            ss_InstructionsEnabled = false;
            boardScript.ss_ButtonsEnabled = false;
            MinigameScript.EndGameUnsuccessful();
            MinigameScript.timesplayed = 0;

            //timer.disable();
            StopAllCoroutines();

            StartCoroutine(boardScript.ColorPopAll(boardScript.wrong));
            StartCoroutine(tutMiniGameManagerScript.DisplayWait(2.0f));

            /*
            if (MinigameManagerScript.ssPlayed >= 3)
            {
                instructionsText.text = "You messed this Simulation up 3 times.\nYou won't be able to try again.";
                MinigameManagerScript.ssPlayable = false;
                rendIndicator.material.color = new Color(1, 0, 0, 1);
                rendIndicator.material.SetColor("_EmissionColor", new Color(6, 0, 0));
            }
            else
            {
                instructionsText.text += "You messed up your " + MinigameManagerScript.ssPlayed + ". try.\n" + (3 - MinigameManagerScript.ssPlayed) + " left.";
            }
            */
        }

        public void GameWon()
        {
            StartCoroutine(tutMiniGameManagerScript.SetBool1(1.0f));
            audiosource.clip = WinSound;
            audiosource.Play();
            StartCoroutine(boardScript.ColorPopAll(boardScript.right));
            StartCoroutine(tutMiniGameManagerScript.DisplayWait(2.0f));
            instructionsText.text = "Correct Input";
            ss_InstructionsEnabled = false;
            boardScript.ss_ButtonsEnabled = false;

            Debug.Log("timesplayed " + MinigameScript.timesplayed);

            if (MinigameScript.timesplayed >= 1)
            {
                RenderSettings.skybox = skyboxMaterial[matIndex++];
                MinigameScript.EndGame();
                MinigameScript.isFinish = false;
                MinigameScript.timesplayed = 0;
            }

            /*
            else if (MinigameScript.timesplayed == 1 || MinigameScript.timesplayed == 3)
            {
                if (MinigameScript.timesplayed == 1)
                {
                    RenderSettings.skybox = skyboxMaterial[matIndex++];
                }
                if (MinigameScript.timesplayed == 3)
                {
                    RenderSettings.skybox = skyboxMaterial[matIndex++];
                }

                MinigameScript.timesplayed++;
                InitializeSSInstructions();
            }
            */

            else if (MinigameScript.timesplayed == 0 || MinigameScript.timesplayed == 2) // || MinigameScript.timesplayed == 4)
            {
                //timer.disable();
                StopAllCoroutines();
                MinigameScript.timesplayed++;
                StartCoroutine(StartSSButtonsDelay());
            }
            if (matIndex >= 3)
            {
                matIndex = 0;
            }
        }

        /*
        IEnumerator LoseTime()
        {
            while (timeLeft > 0)
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
                timer.decrease();
            }
            if (timeLeft == 0)
            {
                GameOver("Time is over!");
                timer.disable();
                //StopAllCoroutines();
            }
        }
        */

        private IEnumerator StartSSDelay()
        {
            instructionsText.text = "You'll now have to navigate\nto the next Destination...";
            StartCoroutine(tutMiniGameManagerScript.DisplayWait(5.0f));
            yield return new WaitForSeconds(5.0f);
            Debug.Log("Game should start");

            SS_StartGame();
        }

        private IEnumerator StartSSButtonsDelay()
        {
            instructionsText.text = "Watch the button combination\non your right.\nRepeat it correctly afterwards!";
            yield return new WaitForSeconds(5f);

            boardScript.InitializeSSButtons();
        }
    }
}
