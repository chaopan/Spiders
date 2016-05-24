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
        Vector3 originPos = this.transform.position;
        for (float i = 0; i < (.3f * rand); i += Time.deltaTime) {
            yield return null;
            this.transform.LookAt(new Vector3(rand,this.transform.position.y,rand));
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, randDir, transform.eulerAngles.z); 
            this.transform.position = new Vector3(Mathf.Lerp(originPos.x, rand, i / (.3f * rand)), this.transform.position.y, Mathf.Lerp(originPos.z, rand, i / (.3f * rand)));
           // this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        walking = false;
    }
}
