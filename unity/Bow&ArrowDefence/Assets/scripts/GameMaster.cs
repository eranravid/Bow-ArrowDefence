using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public GameObject targetPrefab;

    public GameObject baseGateObj;

    public int waveNumber = 0;
    public float timeToNextWave = 0.0f;
    public int lives = 10;
    public int points = 0; 

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start () {
	   
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextWaveStarted()
    {
        waveNumber++;
    }

    public void HitGate(int d, GameObject go)
    {
        lives -= d;
        go.GetComponentInChildren<Animation>().Play("orcattack"); ; // destroy the target
    }

    public void HitTarget(GameObject go)
    {
        go.gameObject.transform.parent.GetComponent<Target>().TargetHit();
        points++;        
        
    }

    
}
