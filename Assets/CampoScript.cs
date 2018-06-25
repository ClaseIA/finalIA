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

        if (GetComponent<Inventario>().comida <= 0)
        {
            listoCosechar = false;
        }

        if (!listoCosechar)
        {
            StartCoroutine(cosechatiempo());
        }

        else
            StopAllCoroutines();

      
		
	}

    IEnumerator cosechatiempo()
    {
        
        yield return new WaitForSeconds(25);
     
        GetComponent<Inventario>().comida += 4;
        listoCosechar =true;
    }
}
