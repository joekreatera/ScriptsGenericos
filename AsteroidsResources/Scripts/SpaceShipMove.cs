using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMove : MonoBehaviour
{

    Vector3 headingSpeed = Vector3.zero;
    [Range(0.01f,10f)]
    public float turnMultiplier;

    [Range(0.5f, 1f)]
    public float unweightMultiplier;

    [Range(0.001f, 0.5f)]
    public float speedMultiplier = 0.005f;

    public GameObject rocket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (MainController.getInstance().IsPaused())
        {
            return;
        }

        Vector3 rot = this.transform.eulerAngles;
        rot.y += Input.GetAxis("Horizontal")* turnMultiplier;
        this.transform.eulerAngles = rot;

        if (Input.GetAxis("Vertical") > 0)
        {
            rocket.SetActive(true);
            Vector3 dir = this.transform.TransformDirection(Vector3.forward);
            headingSpeed += dir * speedMultiplier * Mathf.Max(0, Input.GetAxis("Vertical"));

        }
        else {
            rocket.SetActive(false);
        }

        headingSpeed *= unweightMultiplier; // drag on 70 %
        
        this.transform.position = this.transform.position + headingSpeed;
    }
}
