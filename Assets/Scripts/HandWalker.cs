using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class HandWalker : MonoBehaviour {
    public GameObject palmL, armL;
    public GameObject palmR,armR;
    public GameObject a1, a2;
    public GameObject spider;

    Vector3 palmPosition, elbowPosition;
    // Use this for initialization
    public Vector3 ToVector3(Vector position)
    {
        return new Vector3(position.x/100f , position.y/100f, position.z/100f);
    }
    void Awake() {
       MasterController.controller = new Controller();
       
       palmPosition = a1.transform.localPosition; 
       elbowPosition = a2.transform.localPosition;
        MasterController.armL = armL;
        MasterController.armR = armR;
        MasterController.elbow = a2;
        MasterController.palm = a1;
        MasterController.spider = spider;
    }

    bool done = false;

    // Update is called once per frame
    void Update () {
        Frame frame = MasterController.controller.Frame();
        if(frame.Hands.Count != 0 && !done && MasterController.isOnArm == true)
        {

            done = true;
           // MasterController.isOnArm = ;
          StartCoroutine(destroy());
            


        }
    }

    IEnumerator destroy() {
        
            for (float i = 0; i < .5f; i += Time.deltaTime)
            {
                yield return null;
                //animateObj(anim, "walk");
                //walking = true;
                Quaternion rotation = Quaternion.LookRotation(palmPosition - elbowPosition, Vector3.down);
                //Debug.Log(rotation.y);
                spider.transform.localRotation = Quaternion.Lerp(spider.transform.localRotation, rotation, i / .5f);
      
            }
            for (float i = 0; i < 1f; i += Time.deltaTime)
            {
                yield return null;

                spider.transform.localPosition = Vector3.Lerp(elbowPosition, palmPosition, i / 1f);
                   // new Vector3(Mathf.Lerp(elbow.x, palm.x, i / 1f), Mathf.Lerp(elbow.y, palm.y, i / 1f), Mathf.Lerp(elbow.z, palm.z, i / 1f));
            }

            //works smoothly
            for (float i = 0; i < .5f; i += Time.deltaTime)
            {
                yield return null;
                Quaternion rotation = Quaternion.LookRotation(elbowPosition-palmPosition, Vector3.down);

                spider.transform.localRotation = Quaternion.Lerp(spider.transform.localRotation, rotation, i / .5f);

                //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
            }
            for (float i = 0; i < 1f; i += Time.deltaTime)
            {
                yield return null;
                spider.transform.localPosition = Vector3.Lerp(palmPosition, elbowPosition, i / 1f);
               }

        done = false;
    }
}
