using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{

    private float speed = 10f; //  meter pr. second
    private float depthSpeed = 50f; //  meter pr. second
    private float horizontalInput; // -5 -> 5
    private float verticalInput; // -5 -> 5
    private float depthInput; // -15 -> -4

    void Start() {
        transform.position = new Vector3(0, 1, -15);
    }

    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        depthInput = Input.GetAxis("Mouse ScrollWheel");

        if(transform.position.x < -5) {
            transform.position = new Vector3(-5,transform.position.y, transform.position.z);
        }
        if(transform.position.x > 5) {
            transform.position = new Vector3(5,transform.position.y, transform.position.z);
        }

        if(transform.position.y < -5) {
            transform.position = new Vector3(transform.position.x, -5, transform.position.z);
        }
        if(transform.position.y > 5) {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }

        if(transform.position.z < -15) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -15);
        }
        if(transform.position.z > -4) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -4);
        }

        if(transform.position.x >= -5 && transform.position.x <= 5) {
            transform.Translate(horizontalInput * Time.deltaTime * speed, 0, 0);
        }
        if(transform.position.y >= -5 && transform.position.y <= 5) {
            transform.Translate(0, verticalInput * Time.deltaTime * speed, 0);
        }
        if(transform.position.z >= -15 && transform.position.z <= -4) {
            transform.Translate(0, 0, depthInput * Time.deltaTime * depthSpeed);
        }
    }
}
