using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casitaScript : MonoBehaviour {
    public bool destruida;
    public Sprite[] casitas;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        // destruida = false;
        spriteRenderer = GetComponent<SpriteRenderer>();


    }
	
	// Update is called once per frame
	void Update () {

        if (destruida)
        {
             spriteRenderer.sprite= casitas[1];
        }
        else
            spriteRenderer.sprite = casitas[0];

    }
}
