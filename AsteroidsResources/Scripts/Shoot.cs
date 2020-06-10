using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float forceMultiplier = 1.5f;
    public GameObject bulletPrefab;

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

        if (Input.GetButtonDown("Fire1")) {
            Vector3 dir = this.transform.TransformDirection(Vector3.forward);
            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            Physics.IgnoreCollision(this.GetComponent<Collider>(), bullet.GetComponent<Collider>());
            Rigidbody body = bullet.GetComponent<Rigidbody>();
            body.AddForce(dir* forceMultiplier, ForceMode.Impulse);
        }
    }
}
