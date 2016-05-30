using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {
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
            MasterController.canCrawlOnArm = true;
            isOnArm = true;
        }
    }
    void OnTriggerExit(Collider other) { }
}
