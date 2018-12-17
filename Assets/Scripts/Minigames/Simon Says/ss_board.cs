 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace NewtonVR
{
    public class ss_board : MonoBehaviour
    {
        public GameObject Minigame;
        private MinigameSimonSays MinigameScript;

        //Public variables to be changed in Unity
        [Range(0, 10)] public int ButtonsToClick = 5; //amount of buttons that have to be clicked correctly

        //Private variables without getters
        private Renderer rend;
        public Material right;
        public Material wrong;
        public Material basic;

        private int rand;
        private List<int> usedRandomNumbers;
        private List<GameObject> buttonList; //List of all involved Buttons on the Console
        
        //Private variables with getters
        private int count;
        [HideInInspector] public bool ss_ButtonsEnabled = false;
        private List<GameObject> reqList; //required List, the User has to follow

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public bool Ss_ButtonsEnabled
        {
            get { return ss_ButtonsEnabled; }
            set { ss_ButtonsEnabled = value; }
        }

        public List<GameObject> ReqList
        {
            get { return reqList; }
            set { reqList = value; }
        }

        private void Start()
        {
            MinigameScript = Minigame.GetComponent<MinigameSimonSays>();
            GameObject[] getButtons = GameObject.FindGameObjectsWithTag("SSButton");
            buttonList = new List<GameObject>();
            ClearButtonsToWhite();
            foreach (GameObject btn in getButtons)
            {
                buttonList.Add(btn);
            }
            Ss_ButtonsEnabled = false;
        }

        public void InitializeSSButtons()
        {
            StopAllCoroutines();
            reqList = new List<GameObject>();
            usedRandomNumbers = new List<int>();
            Count = 0;
            ClearButtonsToWhite();

            //Function for showing the klick pattern
            StartCoroutine(ColorShow());
        }

        //resets each button's color
        public void ClearButtonsToWhite()
        {
            foreach (GameObject cube in buttonList)
            {
                rend = cube.GetComponent<Renderer>();
                rend.material = basic;
            }
        }

        public IEnumerator ColorShow()
        {
            for (int i = 0; i < ButtonsToClick; i++)
            {
                //Find a new random number in the range and save, that it has been used
                do
                {
                    rand = Random.Range(0, buttonList.Count);
                }
                while (usedRandomNumbers.Contains(rand));
                usedRandomNumbers.Add(rand);

                //Add the random Button to the required Buttons
                reqList.Add(buttonList[rand]);

                //set the color, wait, set the color back
                rend = buttonList[rand].GetComponent<Renderer>();
                rend.material = right;
                yield return new WaitForSeconds(1.0f);
                rend.material = basic;
            }
            ss_ButtonsEnabled = true;
        }

        public IEnumerator ColorPopAll(Material mat)
        {
            foreach (GameObject cube in buttonList)
            {
                rend = cube.GetComponent<Renderer>();
                rend.material = mat;
            }

            yield return new WaitForSeconds(1.0f);

            ClearButtonsToWhite();
        }
    }
}
