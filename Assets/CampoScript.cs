using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoScript : MonoBehaviour {
    public bool listoCosechar;
	// Use this for initialization
	void Start () {
        listoCosechar = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!listoCosechar)
        {
            StartCoroutine(cosechatiempo());
        }
		
	}

    IEnumerator cosechatiempo()
    {
        yield return new WaitForSeconds(15);
        listoCosechar=true;
    }
}
