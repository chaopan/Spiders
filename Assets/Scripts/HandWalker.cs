using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class HandWalker : MonoBehaviour {
    public GameObject palmL, armL;
    public GameObject palmR,armR;
    public GameObject a1, a2;

    public GameObject spider;
    Controller controller;

    Vector3 palm, elbow;
    // Use this for initialization
    public Vector3 ToVector3(Vector position)
    {
        return new Vector3(position.x/100f , position.y/100f, position.z/100f);
    }
    void Start() {
        controller = new Controller();
        spider.transform.SetParent(armL.transform);
       palm = a1.transform.localPosition; //+ new Vector3(0,.5f,0);
       elbow = a2.transform.localPosition;// + new Vector3(0, .5f, 0);
    }

    bool done = false;

    // Update is called once per frame
    void Update () {
        Frame frame = controller.Frame();
        if (frame.Hands.Count != 0 && !done)
        {

            done = true;
            
          StartCoroutine(destroy());
            


        }
    }

    IEnumerator destroy() {
        
            for (float i = 0; i < .5f; i += Time.deltaTime)
            {
                yield return null;
                //animateObj(anim, "walk");
                //walking = true;
                Quaternion rotation = Quaternion.LookRotation(palm - elbow, Vector3.down);
                Debug.Log(rotation.y);
                spider.transform.localRotation = Quaternion.Lerp(spider.transform.localRotation, rotation, i / .5f);
      
            }
            for (float i = 0; i < 1f; i += Time.deltaTime)
            {
                yield return null;

                spider.transform.localPosition = Vector3.Lerp(elbow, palm, i / 1f);
                   // new Vector3(Mathf.Lerp(elbow.x, palm.x, i / 1f), Mathf.Lerp(elbow.y, palm.y, i / 1f), Mathf.Lerp(elbow.z, palm.z, i / 1f));
            }


            //works smoothly
            for (float i = 0; i < .5f; i += Time.deltaTime)
            {
                yield return null;
                Quaternion rotation = Quaternion.LookRotation(elbow-palm, Vector3.down);

                spider.transform.localRotation = Quaternion.Lerp(spider.transform.localRotation, rotation, i / .5f);

                //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
            }
            for (float i = 0; i < 1f; i += Time.deltaTime)
            {
                yield return null;
                spider.transform.localPosition = Vector3.Lerp(palm, elbow, i / 1f);
               }

        done = false;
    }
}
