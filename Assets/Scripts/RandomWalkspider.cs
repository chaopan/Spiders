using UnityEngine;
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

    public float speed; //faster = larger
    public float maxPauseTimeLow,maxPauseTimeHigh; //longer is ok
    //bounding box local positions
    private float box_front = -9f; //x
    private float box_back = -4f;
    private float box_left = 0.5f; //z
    private float box_right = -4.5f;
    public bool armCrawl;
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
    bool walking = false;
    private bool isOnArm = false;
    void Update () {

        //if the spider isnt currently moving
        if (walking == false && (MasterController.canCrawlOnArm == false || armCrawl == false)) {
            walking = true;
            StartCoroutine(walkRandomDirection());
        }

        if(MasterController.canCrawlOnArm == true) { StopCoroutine(walkRandomDirection()); }
        if (isOnArm == false && MasterController.canCrawlOnArm)
        {
            
            isOnArm = true;
            print("in here! yay!");
            StopCoroutine(walkRandomDirection());
           StartCoroutine(lookAtAndWalkToArm());
        }
    }

    IEnumerator lookAtAndWalkToArm() {
        Vector3 origin = this.transform.position;
        print("wlaking yay!");

        for(float i = 0; i < 1f; i += Time.deltaTime) {
            yield return null;
            //animateObj(anim, "walk");
            Quaternion rotation = Quaternion.LookRotation(MasterController.elbow.transform.position - this.transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i / 1f);
            //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
        }

        for(float j = 0; j < 1f; j += Time.deltaTime) {
            yield return null;
            Vector3.Lerp(origin, MasterController.elbow.transform.position, j / 1f);
        }
        yield return new WaitForSeconds(.01f);
        MasterController.spider.transform.position = MasterController.elbow.transform.position;
        float dot = Vector3.Dot(Vector3.Normalize(MasterController.spider.transform.position), Vector3.Normalize(MasterController.elbow.transform.position));
        if(Mathf.Approximately(dot,1f)) {
            MasterController.spider.transform.SetParent(MasterController.armL.transform);
        }
        else { print(Vector3.Dot(Vector3.Normalize(MasterController.spider.transform.position),Vector3.Normalize(MasterController.elbow.transform.position))); }
        MasterController.isOnArm = true;
    }

    IEnumerator walkRandomDirection() {
        //Vector2 randomXY = Random.insideUnitCircle * rand;
        //float rand = Random.Range(.1f,10f);

        //Original Position
        Vector3 originPos = transform.localPosition;
       // print("origin =" + originPos);

        //random points inside the bounding box
        float randomX = Random.Range(box_front, box_back);
        float randomY = Random.Range(box_right, box_left);
        Vector3 randomPos = new Vector3(randomX, originPos.y, randomY);

        //Distances to calculate time needed to walk
        float distance = Vector3.Distance(randomPos, originPos);
        float box_diagonal_distance = Mathf.Sqrt(Mathf.Pow(box_front - box_back, 2) + Mathf.Pow(box_left - box_right, 2));
        //Speed: lower = faster
        float max_walking_time = 3f/speed; // time it takes to walk diagonal on box
        float time_to_walk = (distance / box_diagonal_distance) * max_walking_time;
        //print("randomPos:" + randomPos);

        float randomTime = Random.Range(maxPauseTimeLow, maxPauseTimeHigh);
        //pause
        for (float i = 0; i < randomTime; i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("idle", 0f);
            
        }
        //turn
        for (float i = 0; i < .5f; i += Time.deltaTime)
        {
            yield return null;
            animateObj(anim,"walk");
            walking = true;
            Quaternion rotation = Quaternion.LookRotation(randomPos - originPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i/2f);
            //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
        }
        //walk
        
        for (float i = 0; i < time_to_walk; i += Time.deltaTime)
        {
            yield return null;
            animateObj(anim, "walk");
            
            this.transform.localPosition = new Vector3(Mathf.Lerp(originPos.x, randomPos.x, i / time_to_walk), originPos.y, Mathf.Lerp(originPos.z, randomPos.z, i / time_to_walk));

        }
        walking = false;
    }
    void animateObj(Animator anim, string type)
    {
        anim.speed = .6f;
        anim.CrossFade(type, 0f);
    }

  
}
