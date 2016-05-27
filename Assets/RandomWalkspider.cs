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
        Vector3 originPos = this.transform.position;
        Vector2 randomXY = Random.insideUnitCircle * rand;
        Vector3 randomPos = new Vector3(originPos.x + randomXY.x, originPos.y, originPos.z + randomXY.y);
        print("originPos:" + originPos);
        print("randomPos:" + randomPos);

        float randomTime = Random.Range(1f, 3f);
        //pause
        for (float i = 0; i < randomTime; i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("idle", 0f);
            print("inside pause");
            //TODO: make spider not do 'walk' animation
            
        }
        //turn
        for (float i = 0; i < .5f; i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("walk", 0f);
            walking = true;
            Quaternion rotation = Quaternion.LookRotation(randomPos - originPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);
            //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
        }
        //walk
        for (float i = 0; i < (.3f * rand); i += Time.deltaTime)
        {
            yield return null;
            anim.CrossFade("walk", 0f);
            //this.transform.LookAt(new Vector3(rand,this.transform.position.y,rand));
            //this.transform.LookAt(randomPos);
            //this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, randDir, transform.eulerAngles.z); 
            //this.transform.position = new Vector3(Mathf.Lerp(originPos.x, rand, i / (.3f * rand)), this.transform.position.y, Mathf.Lerp(originPos.z, rand, i / (.3f * rand)));
            this.transform.position = new Vector3(Mathf.Lerp(originPos.x, randomPos.x, i / (.3f * rand)), this.transform.position.y, Mathf.Lerp(originPos.z, randomPos.z, i / (.3f * rand)));
            // this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        walking = false;
    }
}
