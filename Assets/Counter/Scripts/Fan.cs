using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private Rigidbody dollarRigidBody;
    private GameManager gameManagerScript;
    [SerializeField] float fanSpeed = .000004f;


    void Start() {
        gameManagerScript =  GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameManagerScript.gameIsActive) {
            // dollars are the only thing colliding with this "fan"
            dollarRigidBody = other.gameObject.GetComponent<Rigidbody>();
            
            // apply a random mostly upward impulse and rotation upon collision to simulate fan
            dollarRigidBody.AddForce(new Vector3(Random.Range(-0.1f, 0.1f), 1, Random.Range(-0.1f, 0.1f)) * fanSpeed, ForceMode.Impulse);
            dollarRigidBody.AddTorque(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
        }
    }
}
