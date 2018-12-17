using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundboard : MonoBehaviour {

    AudioSource audiosource;

    // Use this for initialization
    void Start () {
        audiosource = gameObject.GetComponent<AudioSource>();
	}
	
	public void PlaySound (AudioClip Sound)
    {
        audiosource.clip = Sound;
        audiosource.Play();
    }
}
