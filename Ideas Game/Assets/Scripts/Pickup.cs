using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject instantiatedItem;
    public GameObject pickupParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Find the object with the tag "Hand"
            GameObject handObject = GameObject.FindGameObjectWithTag("Hand");
            if (handObject != null)
            {
                // Instantiate the item and set its parent to the "Hand" object
                GameObject obj = Instantiate(instantiatedItem, handObject.transform.position, handObject.transform.rotation);
                obj.transform.parent = handObject.transform;

                //Particle
                Instantiate(pickupParticle, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }

}
