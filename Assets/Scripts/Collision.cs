using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {
    public static bool canCrawlOnArm;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private bool isOnArm = false;
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Hand" && isOnArm == false)
        {
            print(other.tag);
            canCrawlOnArm = true;
            isOnArm = true;
            //StartCoroutine(HandContact());
        }
    }
}
