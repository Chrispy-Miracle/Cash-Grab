using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollar : MonoBehaviour
{
    private Counter counterScript;
    private GameManager gameManagerScript;

    private AudioSource chaChingAudioSource;

    public int dollarValue;


    // Start is called before the first frame update
    void Start()
    {
        counterScript = GameObject.Find("Counter").GetComponent<Counter>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        chaChingAudioSource = GameObject.Find("ChaChingAudioSource").GetComponent<AudioSource>();  
    }

    void OnMouseDown() {   
        if (gameManagerScript.gameIsActive) {
            // count, play audio and destroy dollars when clicked
            counterScript.UpdateCounter(dollarValue);
            chaChingAudioSource.PlayOneShot(chaChingAudioSource.clip);
            Destroy(gameObject);
        }
    }
}
