using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canastaSprite : MonoBehaviour {
    public Sprite[] canastas;
    private SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Inventario>().comida >= 8)
        {
            spriteRenderer.sprite = canastas[1];
        }
        else
            spriteRenderer.sprite = canastas[0];

    }
}
