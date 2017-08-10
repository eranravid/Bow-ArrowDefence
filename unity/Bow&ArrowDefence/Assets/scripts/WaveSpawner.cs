using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public GameObject targetPrefab;

    private GameObject[] spawnPoints;

    public float timeBetweenWaves = 5.0f;
    private float countdown = 2.0f;

    private int waveIndex = 0;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
	    
    }

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        GameMaster.instance.timeToNextWave = countdown;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        GameMaster.instance.waveNumber = waveIndex;

        for (int i = 0; i < Mathf.Min(waveIndex,3); i++)
        {
            foreach (GameObject c in spawnPoints)
            {
                SpawnEnemy(c, targetPrefab);
            }
            
            yield return new WaitForSeconds(0.5f); 
        }
    }

    private void SpawnEnemy(GameObject sp, GameObject ep)
    {
        // create target in place
        Transform  t = sp.gameObject.GetComponent<Transform>();
        GameObject target = (GameObject)Instantiate(ep, t.transform);
        target.GetComponent<Target>().waypath = sp.GetComponent<WayPath>();
    }
}
