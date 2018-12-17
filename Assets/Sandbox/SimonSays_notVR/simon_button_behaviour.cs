using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simon_button_behaviour : MonoBehaviour
{
    private GameObject board;
    private Renderer rend;

    private void Start()
    {
        board = GameObject.FindWithTag("Board");
    }

    private void OnMouseDown()
    {
        //color every button white and the clicked one in accent color
        board.GetComponent<simon_buttons_test>().ClearCubes();
        rend = gameObject.GetComponent<Renderer>();
        rend.material.color = Color.yellow;

        //gets the current object from the reqList
        GameObject nextRandObject = board.GetComponent<simon_buttons_test>().reqList[board.GetComponent<simon_buttons_test>().count];


        //if touched button is correct button in reqList, move on
        if (gameObject.GetInstanceID() == nextRandObject.GetInstanceID())
        {
            //if not all required buttons have been clicked yet, check for correct input
            if (board.GetComponent<simon_buttons_test>().count < board.GetComponent<simon_buttons_test>().max-1)
            {
                board.GetComponent<simon_buttons_test>().count++;
            }
            else
            {
                board.GetComponent<simon_buttons_test>().GameWon();
            }
        }
        //else Game Over
        else
        {
            board.GetComponent<simon_buttons_test>().GameOver();
        }  
    }
}
    

