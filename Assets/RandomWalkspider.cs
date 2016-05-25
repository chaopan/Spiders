using UnityEngine;
using System.Collections;

public class RandomWalkspider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
            print("inside pause");
            //TODO: make spider not do 'walk' animation
            
        }
        //turn
        for (float i = 0; i < 2f; i += Time.deltaTime)
        {
            yield return null;
            walking = true;
            Quaternion rotation = Quaternion.LookRotation(randomPos - originPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
            //this.transform.rotation = Quaternion.Slerp(currentRotation, neededRotation, Time.deltaTime);
        }
        //walk
        for (float i = 0; i < (.3f * rand); i += Time.deltaTime)
        {
            yield return null;
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
