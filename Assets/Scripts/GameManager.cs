using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NewtonVR
{
    public class GameManager : MonoBehaviour
    {
        public Button tutorial;
        public Button startGame;
        public Button quitGame;
        public Button exitGame;
        public Button continueGame;
        public GameObject menu;
        public GameObject audioSource;
        public AudioClip clip;
        private AudioSource buttonSound;
        public GameObject tutManager;
        private tutorial_MinigameManager managerscript;

        //public Animation m_introAnimation;
        //public Button yourButton;
        private AudioSource AddClip(AudioClip clip, GameObject obj)
        {
            AudioSource newAudio = obj.AddComponent<AudioSource>();
            newAudio.clip = clip;
            newAudio.playOnAwake = false;

            return newAudio;
        }

        void Start()
        {

            exitGame.onClick.AddListener(ExitGame);
            quitGame.onClick.AddListener(QuitGame);
            tutorial.onClick.AddListener(Tutorial);
            startGame.onClick.AddListener(StartGame);
            continueGame.onClick.AddListener(ContinueGame);

            exitGame.onClick.AddListener(Sound);
            quitGame.onClick.AddListener(Sound);
            tutorial.onClick.AddListener(Sound);
            startGame.onClick.AddListener(Sound);
            continueGame.onClick.AddListener(Sound);

            managerscript = tutManager.GetComponent<tutorial_MinigameManager>();

        }

        // Kind of like a Singleton. Easy way to access MonoBehaviors that only exist once in a game. 
        public static GameManager instance;

        // Awake() is like Start() but will be called earlier. See https://docs.unity3d.com/Manual/ExecutionOrder.html
        void Awake()
        {
            GameManager.instance = this;
            buttonSound = AddClip(clip);
        }


        void Update()
        {

        }

        IEnumerator LoadYourAsyncScene(string scene)
        {
            // The Application loads the Scene in the background at the same time as the current Scene.
            //This is particularly good for creating loading screens. You could also load the Scene by build //number.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

            //Wait until the last operation fully loads to return anything
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        private AudioSource AddClip(AudioClip clip)
        {
            AudioSource newAudio = gameObject.AddComponent<AudioSource>();
            newAudio.clip = clip;
            newAudio.playOnAwake = false;
            newAudio.spatialBlend = 1;
            newAudio.minDistance = 0.3f;

            return newAudio;
        }

        void Sound()
        {
            buttonSound.Play();
        }

        void ExitGame()
        {
            StartCoroutine(LoadYourAsyncScene("Start"));
        }

        void QuitGame()
        {
            Application.Quit();
        }

        void StartGame()
        {
            StartCoroutine(LoadYourAsyncScene("Main"));
        }

        void Tutorial()
        {
            StartCoroutine(LoadYourAsyncScene("Tutorial"));
            managerscript.ResetBool();
        }

        void ContinueGame()
        {
            menu.SetActive(false);
        }



    }
}