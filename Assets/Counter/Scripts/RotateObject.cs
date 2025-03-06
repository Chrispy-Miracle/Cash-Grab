using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed;
    private GameManager gameManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript =  GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.gameIsActive || gameManagerScript.countdownActive) {
            transform.Rotate(Vector3.up * rotateSpeed);
        }
    }
}
