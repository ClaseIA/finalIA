using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {
    public GameObject canasta;
    public GameObject manager;
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Canasta")
        {
            
            if (other.GetComponent<Inventario>().comida >=8)
            {
                Debug.Log("llevando canasta");
                other.GetComponent<Inventario>().comida = 0;
                canasta.SetActive(true);
                other.gameObject.SetActive(false);
            }
        }

        if (other.tag == "Cueva")
        {
            canasta.SetActive(false);
            manager.GetComponent<GameManagerScript>().comida = true;
        }
    }
}
