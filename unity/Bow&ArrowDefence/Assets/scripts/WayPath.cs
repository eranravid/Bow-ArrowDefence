using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPath : MonoBehaviour
{

    public GameObject[] waypoints; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform getWayPoint(int idx)
    {
        return waypoints[idx].transform;
    }
}
