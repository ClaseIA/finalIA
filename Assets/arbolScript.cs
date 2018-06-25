using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arbolScript : MonoBehaviour {
    public bool vivo;

	// Use this for initialization
	void Start () {
        vivo = true;
	}
	
	// Update is called once per frame
	void Update () {
     
        if (GetComponent<Inventario>().madera <= 0)
        {
         vivo = false;
         gameObject.SetActive(false);
        }
		
	}

}
