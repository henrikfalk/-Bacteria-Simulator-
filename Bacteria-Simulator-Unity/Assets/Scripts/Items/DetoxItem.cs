using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetoxItem : BaseItem
{

    protected override void ItemStart() {

//        Debug.Log("DetoxItem::ItemStart");
        StartCoroutine(WaitForDissolve());
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
*/

    private IEnumerator WaitForDissolve() {

        // Wait a bit before dissolving
        float waitPeriod = UnityEngine.Random.Range(10f, 20f);
        yield return new WaitForSeconds(waitPeriod);

        // Create dying particlesystem effect
        Instantiate(simulationSceneManager.itemDetoxParticleSystem, transform.position, transform.rotation);

        simulationSceneManager.simulationController.RemoveToxicity(25);

        Destroy(gameObject);

    }
}
