using UnityEngine;
using System.Collections;

public class straightlinewalker : MonoBehaviour {
    Animator anim;
    private float box_front;
    private float box_back;
    private float maxPauseTimeLow;
    private float maxPauseTimeHigh;
    private float speed;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        maxPauseTimeLow = 1f;
        maxPauseTimeHigh = 3f;
        speed = 1f;
        box_front = -0.23f;
        box_back = 0f;
    }

    // Update is called once per frame
    bool walking = false;
	void Update () {
        if (walking == false && Collision.canCrawlOnArm == false) //how does this work
        {
            walking = true;
            StartCoroutine(walkOnArm());
        }
    }

    IEnumerator walkOnArm()
    {
        Vector3 originPos = transform.localPosition;


        //random points inside the bounding box
        float randomZ = Random.Range(box_front, box_back);
        Vector3 randomPos = new Vector3(originPos.x, originPos.y, randomZ);

        //Distances to calculate time needed to walk
        float distance = Vector3.Distance(randomPos, originPos);
        //Speed: lower = faster
        float max_walking_time = 3f / speed; // time it takes to walk diagonal on box
        float time_to_walk = (box_back - box_front) * max_walking_time;
        //print("randomPos:" + randomPos);

        float idleTime = Random.Range(maxPauseTimeLow, maxPauseTimeHigh);
        for (float i = 0; i < idleTime; i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("idle", 0f);
        }
        //turn
        for (float i = 0; i < .5f; i += Time.deltaTime)
        {
            yield return null;
            animateObj(anim, "walk");
            walking = true;
            Quaternion rotation = Quaternion.LookRotation(randomPos - originPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i / 2f);
            //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
        }
        //walk

        for (float i = 0; i < time_to_walk; i += Time.deltaTime)
        {
            yield return null;
            animateObj(anim, "walk");
            this.transform.localPosition = new Vector3(Mathf.Lerp(originPos.x, randomPos.x, i / time_to_walk), originPos.y, Mathf.Lerp(originPos.z, randomPos.z, i / time_to_walk));
            // this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        walking = false;
    }

    void animateObj(Animator anim, string type)
    {
        anim.speed = .6f;
        anim.CrossFade(type, 0f);
    }
}
