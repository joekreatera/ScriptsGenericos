using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public enum STATE {
        INITIALIZE,
        PLAYING,
        WON,
        LOST,
        PAUSED
    }

    public STATE state;

    public AsteroidSpawner left;
    public AsteroidSpawner right;
    public AsteroidSpawner top;
    public AsteroidSpawner bottom;

    private float nextShoot = 0.0f;

    private static MainController instance;

    public float level1Timing=2;
    public float level2Timing=1;
    private float levelTiming;
    int score = 0;

    public GameObject finalScreen;
    // Start is called before the first frame update
    void Start()
    {
        levelTiming = level1Timing;
    }

    public bool IsPaused()
    {
        return state != STATE.PLAYING;
    }

    public static MainController getInstance()
    {
        return MainController.instance;
    }
    public void Awake() {
        MainController.instance = this;
    }

    public void OnGUI()
    {
        if( GUI.Button(new Rect(Screen.width-100, Screen.height-50,100,50), "PAUSE")){
            if (state == STATE.PAUSED)
            {
                Time.timeScale = 1;
                state = STATE.PLAYING;
            }
            else {
                if (state == STATE.PLAYING) {
                    Time.timeScale = 0;
                    state = STATE.PAUSED;
                }
            }
        }
    }

    void ShootAsteroid() {

        nextShoot += Time.deltaTime;

        if (nextShoot > levelTiming) {

            nextShoot = 0.0f;
            float rnd = Random.value;

            if (rnd > 0.75) {
                top.GenerateAsteroid();
            }
            else if (rnd > 0.50)
            {
                bottom.GenerateAsteroid();
            }
            else if (rnd > 0.25)
            {
                left.GenerateAsteroid();
            }
            else
            {
                right.GenerateAsteroid();
            }
        }
    }

    public void Lost() {
        state = STATE.LOST;
    }

    public void AddScore() {
        Debug.Log("Add Score");
        score++;
        if (score >= 8) {
            levelTiming = level2Timing;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == STATE.PLAYING) {
            ShootAsteroid();
        }

        if (state == STATE.LOST) {
            finalScreen.SetActiveRecursively(true);
        }
    }
}
