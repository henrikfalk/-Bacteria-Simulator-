using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class BacteriaStateRunning : BacteriaState {

    private Ray ray;
    private RaycastHit hitData;

    public BacteriaStateRunning(Bacteria _bacteria) : base(_bacteria) {
        stateName = STATE.ALIVE;
    }

    public override void Enter() {

        base.Enter();
    }

    public override void Update() {

        if (DateTime.Now > bacteria.deadTime) {
            bacteria.die("Dead " + bacteria.gameObject.name);

            nextState = new BacteriaStateDead(bacteria);
            stage = EVENT.EXIT;
            return;
        }

        // If right mouse button pressed then die
        if (Input.GetMouseButtonDown(1) == true) {
            
            if (bacteria.simulationSceneManager.defaultCamera.gameObject.activeSelf == true) {
                ray = bacteria.simulationSceneManager.defaultCamera.ScreenPointToRay(Input.mousePosition);
            } else {
                ray = bacteria.simulationSceneManager.lockCamera.ScreenPointToRay(Input.mousePosition);
            }

            if (Physics.Raycast(ray, out hitData) == true) {
                

                if (hitData.collider.name.Equals(bacteria.gameObject.name) == true) {

                    bacteria.die("Dead " + bacteria.gameObject.name);

                    nextState = new BacteriaStateDead(bacteria);
                    stage = EVENT.EXIT;
                    return;
                }
            }
        }

    }

    public override void FixedUpdate() {
        move();
    }

    public override void Exit() {
        base.Exit();
    }

    public void move() {

        Deaccelerate();

        float randomDirectionX;
        float randomDirectionY;
        float randomDirectionZ;
        Vector3 forceDir;

        // Get the environments temperature which is equals to ours
        float temp = bacteria.simulationSceneManager.simulationController.GetEnvironmentTemperature(bacteria.transform.position);

        if (temp > bacteria.temperatureOptimal + bacteria.temperatureRange) {
            // It is getting to hot - move downwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, -1,randomDirectionZ);
            forceDir.Normalize();
            bacteria.bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
            return;
        }

        if (temp < bacteria.temperatureOptimal - bacteria.temperatureRange) {
            // It is getting to cold - move upwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, 1,randomDirectionZ);
            forceDir.Normalize();
            bacteria.bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
            return;
        }
            // Just move since our temperature inside the optimal range
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionY = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, randomDirectionY,randomDirectionZ);
            forceDir.Normalize();
            bacteria.bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);

    }

    // Deaccelerate if we are to fast
    private void Deaccelerate() {

        var velocity = bacteria.bacteriaRigidbody.velocity;
        if (velocity.magnitude > bacteria.maxVelocity) {
        
            //we deaccelerate
            velocity -= velocity.normalized * 0.5f;
            
            bacteria.bacteriaRigidbody.velocity = Time.deltaTime * velocity;
        }
    }

}
