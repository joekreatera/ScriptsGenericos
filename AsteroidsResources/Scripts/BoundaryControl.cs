using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Lost() {
        MainController.getInstance().Lost();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Vector3 newPos = other.gameObject.transform.position;

        if (other.gameObject.tag.Equals("asteroid"))
        {
            Lost();
        }

        if (other.gameObject.tag.Equals("verticalWalls")) {
           newPos.z *= -0.85f;
           newPos.x = this.transform.position.x;
           this.transform.position = newPos; 
        }

        if (other.gameObject.tag.Equals("horizontalWalls"))
        {
            newPos.x *= -0.9f;
            newPos.z = this.transform.position.z;
            this.transform.position = newPos;
        }
    }

}
