using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class simon_buttons_test : MonoBehaviour {

    public List<GameObject> buttonList; //List of all involved Buttons on the Console
    public List<GameObject> reqList; //required List, the User has to follow
    public Text UI_Text;
    public int count;
    public int rand;
    public int max = 4; //4 buttons have to be clicked

    private Renderer rend;
    private List<int> usedRandomNumbers;

    void Start () {
        reqList = new List<GameObject>();
        usedRandomNumbers = new List<int>();
        count = 0;
        ClearCubes();

        //Function for showing the klick pattern
        StartCoroutine(ColorShow());
    }

    //resets each button's color
    public void ClearCubes ()
    {
        foreach (GameObject cube in buttonList)
        {
            rend = cube.GetComponent<Renderer>();
            rend.material.color = Color.white;
        }
    }

    IEnumerator ColorShow()
    {
        for (int i = 0; i < max; i++)
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
            rend.material.color = Color.red;
            yield return new WaitForSeconds(1);
            rend.material.color = Color.white;
        }
    }

    public void GameOver ()
    {
        ClearCubes();
        UI_Text.text = "GAME OVER";
        rand = -1;
    }

    public void GameWon()
    {
        UI_Text.text = "Congratulations! GAME WON";
        rand = -1;
    }
}
