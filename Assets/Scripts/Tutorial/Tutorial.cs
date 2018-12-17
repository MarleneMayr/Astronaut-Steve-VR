using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    
    public AudioClip audioSource1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartTutorial ()
    {
        //Disable everything in scene

        Await();

        //Audio first Instruction
        //soundOutput.PlaySound(audioSource1);
        //enable first tutorial button


    }

    IEnumerator Await()
    {
        yield return new WaitForSeconds(2f);
    }
}
