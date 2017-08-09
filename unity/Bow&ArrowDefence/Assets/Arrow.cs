using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float destroyTime = 25.0f;
    private bool isHit = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isHit == false)
		    transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
	}

    void OnCollisionEnter(Collision collision)
    {
        isHit = true;
        Rigidbody r = gameObject.GetComponent<Rigidbody>();
        r.useGravity = false;
        r.isKinematic = true;
        Collider c = gameObject.GetComponent<Collider>();
        c.isTrigger = true;

        if (collision.gameObject.tag == "Targets")
        {
            GUIController.instance.UpdatePoints(1);
        }

        // destroy
        Destroy(gameObject, destroyTime);
    }

}
