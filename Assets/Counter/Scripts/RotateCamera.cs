using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    private float verticalInput;
    private float horizontalInput;

    public float rotateSpeed;

    private bool canLookUp = true;
    private bool canLookDown = true;
    private bool canLookRight = true;
    private bool canLookLeft = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get user input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // values to allow but constrain view angles
        canLookUp = transform.eulerAngles.x > 10.0f;
        canLookDown = transform.eulerAngles.x < 30.0f;
        canLookRight = transform.eulerAngles.y < 280f;
        canLookLeft = transform.eulerAngles.y > 260f;

        // look up/down   
        if (canLookUp && verticalInput > 0) {
            UpdateCameraVertical();
        } else if (canLookDown  && verticalInput < 0) {
            UpdateCameraVertical();
        }
        // look right/left
        if (canLookRight && horizontalInput > 0) {
            UpdateCameraHorizontal();
        } else if (canLookLeft  && horizontalInput < 0) {
            UpdateCameraHorizontal();
        }

    }

    void UpdateCameraVertical() {
        transform.Rotate(Vector3.right * -verticalInput * Time.deltaTime * rotateSpeed);
    }

    void UpdateCameraHorizontal() {
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * rotateSpeed);
    }
}
