using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class ss_button : MonoBehaviour
    {        
        public GameObject board;

        private ss_board boardScript;
        private Renderer rend;
        private NVRButton Button;

        public GameObject submit_controller;
        private ss_instructions instructionsScript;

        private void Start()
        {
            Button = gameObject.GetComponent<NVRButton>();
            boardScript = board.GetComponent<ss_board>();
            rend = gameObject.GetComponent<Renderer>();
            instructionsScript = submit_controller.GetComponent<ss_instructions>();
        }
        

        private void Update()
        {
            if (boardScript.Ss_ButtonsEnabled && Button.ButtonDown)
            {
                //Debug.Log("Button pressed " + Button.name);

                //color every button white and the clicked one in accent color
                boardScript.ClearButtonsToWhite();
                rend.material = boardScript.right;

                //gets the current object from the reqList
                GameObject nextRandObject = boardScript.ReqList[boardScript.Count].gameObject;
                
                //if touched button is correct button in reqList, move on
                if (gameObject.GetInstanceID() == nextRandObject.GetInstanceID())
                {
                    //if not all required buttons have been clicked yet, check for correct input
                    if (boardScript.Count < boardScript.ButtonsToClick - 1)
                    {
                        boardScript.Count++;
                    }
                    else
                    {
                        instructionsScript.GameWon();
                    }
                }
                //else Game Over
                else
                {
                    instructionsScript.GameOver("Wrong Input");
                }
            }
        }
    }
}
