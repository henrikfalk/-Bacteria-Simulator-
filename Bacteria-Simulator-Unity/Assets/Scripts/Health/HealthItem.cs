using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{

    public FishTankSceneManager fishTankSceneManager;
    public EnvironmentManager environment;

    protected Rigidbody healthItemRigidbody;

    public float maxVelocity;

    public float energy; // not used yet

    protected void Start()
    {
        healthItemRigidbody = GetComponent<Rigidbody>();

        GameObject obj1 = GameObject.Find("FishTankSceneManager");
        fishTankSceneManager = obj1.GetComponent<FishTankSceneManager>();

        GameObject obj2 = GameObject.Find("EnvironmentManager");
        environment = obj2.GetComponent<EnvironmentManager>();
        
    }

    void Update()
    {
        move();
    }

    void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Air") == true) {

            // Make random rotation when hitting water
            transform.Rotate(UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f), UnityEngine.Random.Range(-90f, 90f));
            healthItemRigidbody.useGravity = false;

        }
    }

    // Deaccelerate if we are to fast
    private void Deaccelerate() {

        var velocity = healthItemRigidbody.velocity;
        if (velocity.magnitude > maxVelocity) {
        
            //we deaccelerate
            velocity -= velocity.normalized * 0.5f;
            
            healthItemRigidbody.velocity = velocity;
        }
    }

    protected virtual void move() {

        Deaccelerate();

        float randomDirectionX;
        float randomDirectionY;
        float randomDirectionZ;
        Vector3 forceDir;

        if (transform.position.y > 4.95) {
            // we are not in the water so go downwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, -1,randomDirectionZ);
            forceDir.Normalize();
            healthItemRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);

        }

        if (transform.position.y < -4.95) {
            // It is getting to cold - move upwards
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, 1,randomDirectionZ);
            forceDir.Normalize();
            healthItemRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
            return;
        }

        // Just move since our temperature inside the optimal range
            randomDirectionX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionY = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomDirectionZ = UnityEngine.Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirectionX, randomDirectionY,randomDirectionZ);
            forceDir.Normalize();
            healthItemRigidbody.AddForce(forceDir * 0.09f, ForceMode.Impulse);
    }

}
