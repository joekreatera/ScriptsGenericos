using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{


    public GameObject asteroid1;
    public GameObject asteroid2;
    public Vector3 positionA = Vector3.zero;
    public Vector3 positionB = Vector3.zero;

    [Range(0.01f, 0.8f)]
    public float minAsteroidSpeedMultiplier = 0.05f;

    [Range(0.01f, 0.8f)]
    public float maxAsteroidSpeedMultiplier = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(positionA, 0.5f);
        Gizmos.DrawWireSphere(positionB, 0.5f);
    }

    /*
    public void OnGUI()
    {
        if (GUI.Button(new Rect(10,10,100,50), "Shoot ast ")) {
            GenerateAsteroid();
        }
    }*/

    public void GenerateAsteroid() {


        if (MainController.getInstance().IsPaused()) {
            return;
        }


            Vector3 randomPos = positionA + (positionB - positionA)*Random.value;
        Vector3 randomDir = Vector3.zero;
        randomDir = (positionB - positionA) * Random.value - (positionB - positionA)/2;
        randomDir = randomDir - randomPos;
        randomDir.Normalize();

        float asteroidForce = minAsteroidSpeedMultiplier + (maxAsteroidSpeedMultiplier - minAsteroidSpeedMultiplier) * Random.value;
        GameObject asteroid;

        if (Random.value > 0.5f)
        {
            asteroid = Instantiate(asteroid1, randomPos, Quaternion.identity);
        }
        else {
            asteroid = Instantiate(asteroid2, randomPos, Quaternion.identity);
        }

        asteroid.GetComponent<Asteroid>().setDirection(randomDir*asteroidForce);

    }

}
