using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{

    private float depthSpeed = 4000f;
    private float rotationSpeed = 40f;
    private float verticalSpeed = 40f;
    private float horizontalInput; // -5 -> 5
    private float verticalInput; // -5 -> 5
    private float depthInput; // -15 -> -4

    private Vector3 rotationCenterPosition = new Vector3(0,1,0);

    public SimulationSceneManager simulationSceneManager;

    public GameObject target;

    private Ray ray;
    private RaycastHit hitData;

    void Start() {

        transform.position = new Vector3(0, 1, -15);
    }

    void Update()
    {

        if(simulationSceneManager == null || simulationSceneManager.IsSimulationRunning() == false) return;

        target = simulationSceneManager.aquarium;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        depthInput = Input.GetAxis("Mouse ScrollWheel");

        // Return if we try to move camera below the table/aquarium
        if(transform.position.y < -4.9) {
            transform.position = new Vector3(transform.position.x, -4.8f, transform.position.z);
            return;
        }

        // Handle horisontal input
        transform.RotateAround(target.transform.position, Vector3.up, Time.unscaledDeltaTime * rotationSpeed * horizontalInput); // Time.deltaTime * 

        // Handle vertical input
        transform.Translate(0, Time.unscaledDeltaTime * verticalInput * verticalSpeed, 0); // Time.deltaTime *

        ProcessMouseInput();

        KeepCameraAround();
    }

    private void KeepCameraAround() {

        // Return if we are to far away from the camera
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance > 5.0f) {
            // Handle zoom
            transform.Translate(0, 0, Time.unscaledDeltaTime * depthSpeed * depthInput); // Time.deltaTime * 

            if (distance > 15.0f) {
                var step =  0.1f; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
        } else {
                var step =  -0.3f; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

    }

    private Boolean IsPointerOverGameObject() {

        // Get the Camera that this controller is attached to
        ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        // if we are over a GameObject then return true
        if (Physics.Raycast(ray, out hitData) == true) {
            return true;
        }

        return false;
    }

    private void ProcessMouseInput()  {

        if (IsPointerOverGameObject() == true) {
            return;
        }

        if (Input.GetMouseButton(2) == true) {
            // Right mouse button clicked

            // Rotate
            // Handle horisontal input
            transform.RotateAround(target.transform.position, Vector3.up, Time.unscaledDeltaTime * rotationSpeed * 5 * Input.GetAxis("Mouse X")); // Time.deltaTime * 

            // Handle vertical input
            transform.Translate(0, Time.unscaledDeltaTime * verticalSpeed * 5 * Input.GetAxis("Mouse Y"), 0); // Time.deltaTime * 


        }
    }
}
