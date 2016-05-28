﻿using UnityEngine;
using System.Collections;

public class RandomWalkspider : MonoBehaviour {
    Animator anim;
    // Use this for initialization
    // TODO: make the spider only walk inside the box.
    // NOTES: My idea of implementing the level progression:
    //  Lvl 1: Just display the spider
    //  Lvl 2: When player presses green button, show the 'ghost hand' asset and switch out the spider to a scripted spider that will crawl on the ghost hand once the player arm is in place
    //  Lvl 3: Same as lvl 2, but a different spider that is scripted to jump onto the hand
    //  Lvl 4: Multiple spiders.
    //bounding box local positions
    private float box_front = -9f; //x
    private float box_back = -4f;
    private float box_left = 0.5f; //z
    private float box_right = -4.5f;
    
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
    bool walking = false;

	void Update () {
        if (walking == false) {
            walking = true;

            StartCoroutine(walkRandomDirection());
            
        }
	}

    IEnumerator walkRandomDirection() {
        float rand = Random.Range(.1f,10f);
        float randDir = Random.Range(0, 360);
        //float rand
        Vector3 originPos = this.transform.localPosition;
        print("origin =" + originPos);
        //random positions
        //Vector2 randomXY = Random.insideUnitCircle * rand;
        float randomX = Random.Range(box_front, box_back);
        float randomY = Random.Range(box_right, box_left);
        Vector3 randomPos = new Vector3(randomX, originPos.y, randomY);
        //Distances to calculate time needed to walk
        float distance = Vector3.Distance(randomPos, originPos);
        float box_diagonal_distance = Mathf.Sqrt(Mathf.Pow(box_front - box_back, 2) + Mathf.Pow(box_left - box_right, 2));
        //Speed: lower = faster
        float max_walking_time = 3f; // time it takes to walk diagonal on box
        float time_to_walk = (distance / box_diagonal_distance) * max_walking_time;
        print("randomPos:" + randomPos);

        float randomTime = Random.Range(1f, 3f);
        //pause
        for (float i = 0; i < randomTime; i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("idle", 0f);
            //print("inside pause");
            
        }
        //turn
        for (float i = 0; i < .5f; i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("walk", 0f);
            walking = true;
            Quaternion rotation = Quaternion.LookRotation(randomPos - originPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i/2f);
            //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
        }
        //walk
        
        for (float i = 0; i < time_to_walk; i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("walk", 0f);
            //this.transform.LookAt(new Vector3(rand,this.transform.position.y,rand));
            //this.transform.LookAt(randomPos);
            //this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, randDir, transform.eulerAngles.z); 
            //this.transform.position = new Vector3(Mathf.Lerp(originPos.x, rand, i / (.3f * rand)), this.transform.position.y, Mathf.Lerp(originPos.z, rand, i / (.3f * rand)));
            this.transform.localPosition = new Vector3(Mathf.Lerp(originPos.x, randomPos.x, i / time_to_walk), originPos.y, Mathf.Lerp(originPos.z, randomPos.z, i / time_to_walk));
            // this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        walking = false;
    }
}
