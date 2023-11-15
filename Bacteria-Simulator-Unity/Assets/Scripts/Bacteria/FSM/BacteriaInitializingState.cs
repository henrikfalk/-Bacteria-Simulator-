using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaStateInitializing : BacteriaState {

    public BacteriaStateInitializing(Bacteria _bacteria) : base(_bacteria) {
        stateName = STATE.INIT;
    }

    public override void Enter() {

        base.Enter();
    }

    public override void UpdateFixed() {

        // We are in the air
        move();

        // If we enter the water then disable gravity and change to BacteriaRunningState
        if (bacteria.transform.position.y < 4.9) {

            // Make random rotation when hitting water and remove gravity
            bacteria.transform.Rotate(UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f));
            bacteria.bacteriaRigidbody.useGravity = false;
//            bacteria.bacteriaRigidbody.AddForce(Vector3.zero, ForceMode.VelocityChange);

            nextState = new BacteriaStateRunning(bacteria);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit() {
        base.Exit();
    }

    public void move() {

        Deaccelerate();

        float randomDirectionX;
        float randomDirectionZ;
        Vector3 forceDir;

        // Get the environments temperature which is equals to ours
        float temp = bacteria.simulationSceneManager.simulationController.GetEnvironmentTemperature(bacteria.transform.position);

        // Not optimized HFALK
        if ( bacteria.transform.position.y > 4.9) {
            // we are not in the water so go downwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, -1,randomDirectionZ);
            forceDir.Normalize();
            bacteria.bacteriaRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
            return;
        }

    }

    // Deaccelerate if we are to fast
    private void Deaccelerate() {

        var velocity = bacteria.bacteriaRigidbody.velocity;
        if (velocity.magnitude > bacteria.maxVelocity) {
        
            //we deaccelerate
            velocity -= velocity.normalized * 0.5f;
            
            bacteria.bacteriaRigidbody.velocity = velocity;
        }
    }
}
