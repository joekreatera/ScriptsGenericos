using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum STATE
    {
        INITIALIZED,
        ON_GAME,
        CRASHED
    };
    public STATE state = STATE.INITIALIZED;
    public GameObject player;

    public GameObject miniAsteroid;

    private Vector3 dir = Vector3.zero;
    // Start is called before the first frame update

    public void setDirection(Vector3 d) {
        dir = d;
    }

    void Start()
    {
        //dir *= 0.01f;
        Vector3 r = this.transform.GetChild(0).transform.eulerAngles;
        r.y = Random.value * 360;
        this.transform.GetChild(0).transform.eulerAngles = r;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.name + " // " + other.gameObject.tag);
        if (other == player) {
            // 
            other.SendMessage("Lost");
        }
        

        if ( other.gameObject.tag.CompareTo("bullet") == 0 ) { // is bullet

            Vector3 entranceVector = other.GetComponent<Rigidbody>().velocity.normalized;

            Vector3 exit1 = Vector3.Cross(entranceVector, Vector3.up);
            Debug.DrawRay(this.transform.position, exit1);

            Destroy(other.gameObject);

            if (miniAsteroid != null)
            {
                InstantiateMiniAsteroids(exit1 + dir.normalized);
                InstantiateMiniAsteroids(-exit1 + dir.normalized);

            }
            else {
                MainController.getInstance().AddScore();
            }
            state = STATE.CRASHED;
            Destroy(this.gameObject);

        }
        if (other.gameObject.tag.Equals("verticalWalls") || other.gameObject.tag.Equals("sideWalls") ) {
            if (state == STATE.ON_GAME)
            {
                Destroy(this.gameObject);
            }
            if (state == STATE.INITIALIZED) {
                state = STATE.ON_GAME;
            }

        }

    }

    private void InstantiateMiniAsteroids(Vector3 dir)
    {
        GameObject ma = Instantiate(miniAsteroid, this.transform.position, Quaternion.identity);
        ma.GetComponent<Rigidbody>().AddForce(dir * 5, ForceMode.Impulse);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), ma.GetComponent<Collider>());
       
    }

    void ChangeChildRotation() {

        Vector3 r = this.transform.GetChild(0).transform.eulerAngles;
        r.y +=  dir.magnitude*2;
        this.transform.GetChild(0).transform.eulerAngles = r;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (MainController.getInstance().IsPaused())
        {
            return;
        }
        Vector3 pos = this.transform.position;
        pos = pos + dir;
        this.transform.position = pos;

        ChangeChildRotation();

        if ((player.transform.position - pos).magnitude > 30) {
            state = STATE.CRASHED;
            Destroy(this.gameObject);
        }
    }
}
