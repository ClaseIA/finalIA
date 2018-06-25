using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class almacenScript : MonoBehaviour {
    public GameObject manager;
    public GameObject canasta;
    int casitas;
   public int casitasmuertas;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

       

        if (casitasmuertas>=1)
        {
            if (GetComponent<Inventario>().madera >= 30)
            {
                GetComponent<Inventario>().madera -= 30;
                GetComponent<Inventario>().casita = 1;
                casitasmuertas -= 1;

            }

        }

       else if (!canasta.activeSelf)
        {
            if (GetComponent<Inventario>().madera >=15)
            {
                GetComponent<Inventario>().madera -= 15;
                canasta.SetActive(true);
            }

         }

      


    }
}
