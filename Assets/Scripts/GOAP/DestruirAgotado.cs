using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirAgotado : MonoBehaviour {
    public bool checarMadera;
    public bool checarMineral;
    Inventario recursos;

	// Use this for initialization
	void Start () {
        recursos = GetComponent<Inventario>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (checarMadera && recursos.madera <= 0||checarMineral&&recursos.mineral<=0)
        {
            Destroy(gameObject);
        }
		
	}
}
