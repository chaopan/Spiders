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
    // Use this for initialization
    public Vector3 ToVector3(Vector position)
    {
        return new Vector3(position.x/100f , position.y/100f, position.z/100f);
    }
    void Start() {
        controller = new Controller();
   
    }

    bool done = false;

    // Update is called once per frame
    void Update () {
        Frame frame = controller.Frame();
        if (frame.Hands.Count != 0 && !done)
        {

            done = true;
            spider.transform.SetParent(armL.transform);
           // spider.transform.position = new Vector3(spider.transform.position.x, spider.transform.position.y +180, spider.transform.position.z);
            /*
            Hand hand = frame.Hands[0];
            Arm arm = hand.Arm;
            Vector3 v = ToVector3(arm.ElbowPosition);

          //  v = transform.TransformVector(v);
           
            Vector3 v1 = ToVector3(arm.WristPosition);

            */




            /*GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(.4f, .4f, .4f);
            cube.transform.position = transform.TransformVector(palmL.transform.parent.parent.parent.position) + v1;
            cube.GetComponent<Renderer>().material.color = Color.red;
           */








            //  StartCoroutine(destroy(cube, (palmL.transform.parent.parent.parent.position + v), (palmL.transform.parent.parent.parent.position + v1)));
            StartCoroutine(destroy());
            


        }
    }
    IEnumerator destroy() {
        Vector3 palm = a1.transform.localPosition;
        Vector3 elbow = a2.transform.localPosition;
        
       
       // while (true)
      // {
            for (float i = 0; i < .5f; i += Time.deltaTime)
            {
                yield return null;
                //animateObj(anim, "walk");
                //walking = true;
                Quaternion rotation = Quaternion.LookRotation(palm - elbow);
                Debug.Log(rotation.y);
                spider.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i / .5f);
                //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
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
                Quaternion rotation = Quaternion.LookRotation(elbow - palm);

                spider.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i / .5f);

                //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
            }
            for (float i = 0; i < 1f; i += Time.deltaTime)
            {
                yield return null;
                //change all transform.localPosition to transform.localPosition
                spider.transform.localPosition = Vector3.Lerp(palm, elbow, i / 1f);
                    //new Vector3(Mathf.Lerp(palm.x, elbow.x, i / 1f), Mathf.Lerp(palm.y, elbow.y, i / 1f), Mathf.Lerp(palm.z, elbow.z, i / 1f));
            }
      // }
        //yield return new WaitForSeconds(.1f);
        done = false;
    }
}
