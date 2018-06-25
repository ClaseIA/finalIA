using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoScript : MonoBehaviour {
    public bool listoCosechar;
    public Sprite[] campito;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        listoCosechar = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<Inventario>().comida <= 0)
        {
            listoCosechar = false;
        }

        if (!listoCosechar)
        {
            spriteRenderer.sprite = campito[0];
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
        spriteRenderer.sprite = campito[1];
    }
}
