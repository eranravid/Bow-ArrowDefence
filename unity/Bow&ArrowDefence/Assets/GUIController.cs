using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static GUIController instance;
    public Text points;

    private int curPoints = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start ()
	{
	    points.text = "Points: 0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePoints(int p)
    {
        curPoints += p;
        points.text = "Points: " + curPoints;
    }
}
