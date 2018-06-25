using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosqueScript : MonoBehaviour {
    public GameObject[] arbolitos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < arbolitos.Length; i++)
        {
            if (!arbolitos[i].activeSelf)
            {
                StartCoroutine(CreceTiempo(arbolitos[i]));
            }
        }
		
	}

    IEnumerator CreceTiempo(GameObject arbolin)
    {
        yield return new WaitForSeconds(15);
        arbolin.SetActive(true);
        arbolin.GetComponent<arbolScript>().vivo = true;
        arbolin.GetComponent<Inventario>().madera = 25;

    }
}
