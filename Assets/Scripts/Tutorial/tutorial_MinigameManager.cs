using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewtonVR
{
    public class tutorial_MinigameManager : MonoBehaviour
    {
        public TextMesh instructionsText;
        [HideInInspector] public bool isFinish;
        public bool preventTextUpdate;

        public GameObject minigameSimonSays;
        private MinigameSimonSays minigameSimonSaysScript;
        [HideInInspector] public bool ssPlayable = true;
        [HideInInspector] public int ssPlayed = 0;

        private AudioSource source;
        private AudioSource leverSource;

        public Material off;
        public Material on;







        public NVRLever lever;
        public NVRInteractableItem controlKnob;
        public GameObject ss_script;
        //public bool preventTextUpdate;
        public List<AudioClip> audioList;
        //public GameObject minigameSimonSays;
        //public TextMesh instructionsText;
        public GameObject audioSource;


        //private MinigameSimonSays minigameSimonSaysScript;
        private Scene scene;
        private int currentKnobState;
        private bool isLeverPulled;
        private bool lever1Pulled;
        private bool lever2Pulled;
        private bool lever3Pulled;
        private bool stop;
        private AudioSource tutBegin;
        private AudioSource tutSimon;
        private AudioSource tutInvasion;
        //private AudioSource tutCombat;
        private AudioSource tutLed;
        private AudioSource tutEnd;
        private GameObject spaceDoor;
        private Animation spaceAni;
        private AudioSource spaceS;
        private Animation hallway;
        private ss_tutorial_start script;
        [HideInInspector] public string[] selectionText;

        private void Awake()
        {
            minigameSimonSaysScript = minigameSimonSays.GetComponent<MinigameSimonSays>();

        }
        private void Start()
        {
            minigameSimonSaysScript = minigameSimonSays.GetComponent<MinigameSimonSays>();
            isFinish = false;







            selectionText = new string[] { "NAVIGATION Simulation Selected!", "INVASION Simulation Selected!", "404 - Simulation not found!" };
            scene = SceneManager.GetActiveScene();
            
            if (scene.name == "Tutorial")
            {
                StartCoroutine(SetStateDelayed(2.0f, GameState.tutorialStart));
                //StartCoroutine(SetStateDelayed(2.0f, GameState.simon));
            }
            script = ss_script.GetComponent<ss_tutorial_start>();

            stop = true;
            isLeverPulled = false;
            lever1Pulled = false;
            lever2Pulled = false;
            //lever3Pulled = false;

            tutBegin = AddClip(audioList[0], audioSource);
            tutSimon = AddClip(audioList[1], audioSource);
            tutInvasion = AddClip(audioList[2], audioSource);
            //tutCombat = AddClip(audioList[3], audioSource);
            tutLed = AddClip(audioList[4], audioSource);
            tutEnd = AddClip(audioList[5], audioSource);
            spaceDoor = GameObject.FindGameObjectWithTag("SpaceDoor");
            spaceS = spaceDoor.GetComponent<AudioSource>();
            spaceAni = spaceDoor.GetComponent<Animation>();
            hallway = GameObject.FindGameObjectWithTag("Hallway").GetComponent<Animation>();

        }

        
        private void Update()
        {
            isFinish = minigameSimonSaysScript.isFinish;
            bool isInGame = minigameSimonSaysScript.isRunning;







            KnobState();

            //bool isInGame = minigameSimonSaysScript.isRunning;
            if (!isInGame && !preventTextUpdate)
            {
                instructionsText.text = selectionText[currentKnobState];
            }

            if (isLeverPulled)
            {
                if (lever.LeverEngaged && !isInGame && !preventTextUpdate)
                {
                    switch (currentKnobState)
                    {
                        case 0:
                            SetState(GameState.simon);
                            script.StartGame();
                            break;
                        case 1:
                            SetState(GameState.invasion);
                            break;
                        case 2:
                            SetState(GameState.combat);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (lever1Pulled && lever2Pulled/* && lever3Pulled*/)
            {
                if (stop)
                {
                    SetState(GameState.led);
                }
            }

        }

        public void ResetBool()
        {
            lever1Pulled = false;
            lever2Pulled = false;

        }

        public IEnumerator DisplayWait(float sec)
        {
            preventTextUpdate = true;

            yield return new WaitForSeconds(sec);

            preventTextUpdate = false;

            yield break;
        }


        private AudioSource AddClip(AudioClip clip, GameObject obj)
        {
            AudioSource newAudio = obj.AddComponent<AudioSource>();
            newAudio.clip = clip;
            newAudio.playOnAwake = false;

            return newAudio;
        }



        #region State Machine

        public enum GameState { undefined, tutorialStart, invasion, combat, simon, gameStart, led, tutorialEnd, flying };

        GameState m_gameState = GameState.undefined;

        public GameState GetGameState()
        {
            return m_gameState;
        }

        void SetState(GameState theState)
        {
            switch (theState)
            {
                case GameState.tutorialStart:
                    tutBegin.Play();
                    StartCoroutine(SetStateDelayed(audioList[0].length, GameState.gameStart));
                    break;
                case GameState.simon:
                    if (!tutSimon.isPlaying && !tutInvasion.isPlaying/* && !tutCombat.isPlaying*/)
                    {
                        tutSimon.Play();
                    }
                    break;
                case GameState.invasion:
                    if (!tutSimon.isPlaying && !tutInvasion.isPlaying/* && !tutCombat.isPlaying*/)
                    {
                        tutInvasion.Play();
                    }
                    StartCoroutine(SetBool2(audioList[2].length));
                    break;
                /*
            case GameState.combat:
                if (!tutSimon.isPlaying && !tutInvasion.isPlaying && !tutCombat.isPlaying)
                {
                    tutCombat.Play();
                }
                StartCoroutine(SetBool3(audioList[3].length));
                break;
                */
                case GameState.gameStart:
                    isLeverPulled = true;
                    break;
                case GameState.led:
                    stop = false;
                    tutLed.Play();
                    StartCoroutine(SetStateDelayed(audioList[4].length, GameState.tutorialEnd));
                    break;
                case GameState.tutorialEnd:
                    tutEnd.Play();
                    spaceAni.Play();
                    spaceS.Play();
                    StartCoroutine(SetStateDelayed(spaceAni.clip.length, GameState.flying));
                    break;
                case GameState.flying:
                    hallway.Play();
                    break;
            }

            m_gameState = theState;
        }

        public void KnobState()
        {
            int rotation = ((int)controlKnob.transform.localEulerAngles.y);

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

        }

        // Coroutine, that will set the game state with a delay
        IEnumerator SetStateDelayed(float delayTime, GameState state)
        {
            yield return new WaitForSeconds(delayTime);

            SetState(state);
        }

        public IEnumerator SetBool1(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            lever1Pulled = true;
            //Debug.Log("true");

        }

        IEnumerator SetBool2(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            lever2Pulled = true;
            //Debug.Log("true");

        }

        /*
        IEnumerator SetBool3(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            lever3Pulled = true;
            Debug.Log("true");

        }
        */

            /*
        public IEnumerator DisplayWait(float sec)
        {
            preventTextUpdate = true;

            yield return new WaitForSeconds(sec);

            preventTextUpdate = false;

            yield break;
        }
        */
        #endregion

    }
}

