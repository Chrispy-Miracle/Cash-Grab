using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private Rigidbody cashRigidBody;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    [SerializeField] float fanSpeed = .000004f;


    void Start() {
        gameManagerScript =  gameManager.GetComponent<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameManagerScript.gameIsActive) {
            // cash is the only thing colliding with this "fan"
            cashRigidBody = other.gameObject.GetComponent<Rigidbody>();
            
            // apply a random mostly upward impulse and rotation upon collision to simulate fan
            cashRigidBody.AddForce(new Vector3(Random.Range(-0.1f, 0.1f), 1, Random.Range(-0.1f, 0.1f)) * fanSpeed, ForceMode.Impulse);
            cashRigidBody.AddTorque(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
        }

    }
}
