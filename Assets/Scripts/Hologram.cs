using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour {
    private int currentvalue;
    public List<GameObject> markers;
    public List<GameObject> holos;
    private AudioSource source;
    private int currentState;

    public Material off;
    public Material on;

    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();
        currentState = 0;
        RedrawMarkers(currentState);
    }

    // Update is called once per frame
    void Update () {
        currentvalue = ((int)gameObject.transform.localEulerAngles.y);
        int previousState = currentState;
        
        if (currentvalue >= 15 && currentvalue <= 105)
        {
            currentState = 0;
        }
        else if (currentvalue > 105 && currentvalue <= 195)
        {
            currentState = 1;
        }
        else if (currentvalue > 195 && currentvalue <= 285)
        {
            currentState = 2;
        }
        else
        {
            currentState = 3;
        }
        
        if (previousState != currentState)
        {
            source.Play();
            ActivateHolos(currentState);
            RedrawMarkers(currentState);
        }
    }

    public void RedrawMarkers(int state)
    {
        foreach (GameObject obj in markers)
        {
            Renderer rend = obj.GetComponent<Renderer>();
            rend.material = off;
        }
        Renderer rendone = markers[state].GetComponent<Renderer>();
        rendone.material = on;
    }

    public void ActivateHolos(int state)
    {
        foreach (GameObject obj in holos)
        {
            obj.SetActive(false);
        }
        holos[state].SetActive(true);
    }
}
