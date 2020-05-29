using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthSeeker : MonoBehaviour
{

    public enum STATE { FORWARD, TURNING , DEATH, WON }
    Texture2D back;
    Texture2D back2;

    float originalY;
    public STATE state;
    CharacterController controller;
    Rigidbody body;
    public float speed = 0.05f;
    float angles = 0;
    float targetAngles = 0;
    float t = 0;
    public float maxTurningTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.AddComponent<CharacterController>();
        body = this.gameObject.AddComponent<Rigidbody>();
        body.isKinematic = true;
        controller.height = .5f;

        originalY = this.transform.position.y;

        back = new Texture2D(200, 200);

        Color[] colors = new Color[back.width * back.height];
        for (int i = 0; i < back.width * back.height; i++)
            colors[i] = Color.black;

        back.SetPixels(colors);
        back.Apply();


        back2 = new Texture2D(200, 200);

        Color[] colors2 = new Color[back2.width * back2.height];
        for (int i = 0; i < back2.width * back2.height; i++)
            colors2[i] = Color.red;

        back2.SetPixels(colors2);
        back2.Apply();
    }



    public void OnGUI()
    {
        if (state == STATE.WON) {
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), back);
        GUI.Label(new Rect(Screen.width/2-50, Screen.height / 2-50,200,200) ,"GANASTE!" );
        }
        if (state == STATE.DEATH)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), back2);
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 200), "PERDISTE!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (state == STATE.WON) {
            //controller.Move(new Vector3(0,0,0));
            return;
        }


        if (state == STATE.TURNING) {
            t += Time.deltaTime;
            float thisT = t / maxTurningTime;

            Vector3 rot = new Vector3(0,Mathf.Lerp(angles, targetAngles , thisT),0);

            if (thisT >= 1) {
                rot.y = targetAngles;
                state = STATE.FORWARD;
                t = 0;
            }
            this.transform.eulerAngles = rot;
           
        }

        if (state == STATE.FORWARD) {

            Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
            Vector2 bottom = Vector3.down;
            float s = speed;
            if (Physics.Raycast(this.transform.position - fwd.normalized * 0.5f, bottom, 0.6f))
            {
               
                if (Mathf.Abs(originalY - this.transform.position.y) > 2)
                {
                    state = STATE.WON;
                }
                body.isKinematic = true;
            }
            else
            {
               
                t += Time.deltaTime;
                if (t > 5)
                {
                    Debug.Log(t);
                    state = STATE.DEATH;
                }

                body.isKinematic = false;
                s = 0;
                return;
                
            }
            if (s != 0)
            controller.Move(fwd * s);

          
            Ray r = new Ray(this.transform.position, fwd);
            if (Physics.Raycast(r, 1))
            {
               
                angles = this.transform.eulerAngles.y;
                t = 0;
                if (Random.value > 0.5)
                {
                    targetAngles = angles + 90;
                }
                else {
                    targetAngles = angles - 90;
                }
                state = STATE.TURNING;
            }
          

            
        }
        
    }
}
