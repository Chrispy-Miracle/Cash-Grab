using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollar : MonoBehaviour
{
    private GameObject counter;
    private Counter counterScript;
    private AudioSource chaChingAudioSource;

    public int dollarValue;


    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.Find("Counter");
        counterScript = counter.GetComponent<Counter>();

        chaChingAudioSource = GameObject.Find("ChaChingAudioSource").GetComponent<AudioSource>();  

    }

    void OnMouseDown()
    {
        // count, play audio and destroy dollars when clicked
        counterScript.UpdateCounter(dollarValue);
        chaChingAudioSource.PlayOneShot(chaChingAudioSource.clip);

        Destroy(gameObject);
    }
}
