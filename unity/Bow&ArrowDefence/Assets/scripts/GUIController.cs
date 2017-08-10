using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static GUIController instance;
    public Text wave;
    public Text waveTime;
    public Text points;
    public Text lives;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    wave.text = "Wave: " + GameMaster.instance.waveNumber;
	    waveTime.text = "T: " + GameMaster.instance.timeToNextWave;
        lives.text = "Lives: " + GameMaster.instance.lives;
	    points.text = "Points: " + GameMaster.instance.points;
    }
   
}
