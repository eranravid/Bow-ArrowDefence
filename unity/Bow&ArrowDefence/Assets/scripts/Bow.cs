using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject arrowStartPoint;
    public GameObject bowString;
    public GameObject currentArrow;

    public float arrowBackSpeed = 2.5f;
    public float arrowBaseSpeed = 12f;
    public float arrowReloadTime = 1.5f;

    private Vector3 bowStringOriginalPosition;
    private bool mouseClicked = false;
    private float maxArrowBackDist = 5f;

	// Use this for initialization
	void Start ()
	{
	    currentArrow = (GameObject)Instantiate(arrowPrefab, arrowStartPoint.transform,false);
	    bowStringOriginalPosition = bowString.transform.localPosition;

	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && currentArrow != null)
        {
            mouseClicked = true;
            float dist = Time.deltaTime / arrowBackSpeed  * maxArrowBackDist;
            if (bowString.transform.localPosition.x <= maxArrowBackDist)
            {
                bowString.transform.localPosition = bowString.transform.localPosition + new Vector3(dist, 0, 0);
                currentArrow.transform.localPosition = currentArrow.transform.localPosition + new Vector3(0, 0, -dist);
            }
        }

        if (mouseClicked && !Input.GetMouseButton(0))
        {
            mouseClicked = false;
            FireArrow();
            bowString.transform.localPosition = bowStringOriginalPosition;
            StartCoroutine(initiateNewArrow());
        }

    }

    IEnumerator initiateNewArrow()
    {
        yield return new WaitForSeconds(arrowReloadTime);
        currentArrow = (GameObject)Instantiate(arrowPrefab, arrowStartPoint.transform, false);
    }

    void FireArrow()
    {
        currentArrow.transform.parent = null;
        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * arrowBaseSpeed * bowString.transform.localPosition.x;
        r.useGravity = true;

        currentArrow.GetComponent<Collider>().isTrigger = false;

        currentArrow = null;
    }

}
