using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
  public GameObject buttonRed;
   public GameObject buttonGreen;
    public bool green;
    bool currentlyPressing = false;
	// Use this for initialization
	void Start () {
        StartCoroutine(ButtonPressing());
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void OnTriggerExit(Collider other) {
        if(currentlyPressing == false) {
            StartCoroutine(ButtonPressing());
        }
    }
        IEnumerator ButtonPressing() {
        currentlyPressing = true;
        GameObject currButton = ((green == true) ? (buttonGreen) : (buttonRed));
        for(float i = 0; i < 1.0f; i += Time.deltaTime) {
            currButton.transform.position = new Vector3 (currButton.transform.position.x,Mathf.Lerp(11.0f, 10.21f, i / 1.0f), currButton.transform.position.z);
            yield return null;
        }
        for(float i = 0; i < 1.0f; i += Time.deltaTime) {
            currButton.transform.position = new Vector3(currButton.transform.position.x, Mathf.Lerp(10.21f, 11.0f, i / 1.0f), currButton.transform.position.z);
            yield return null;
        }
        currentlyPressing = false;
        yield return null;
    }
}
