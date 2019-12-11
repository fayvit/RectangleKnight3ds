using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraFlip : MonoBehaviour {

    private float x1;
    private float x2;

	// Use this for initialization
	void Start () {
        x1 = transform.position.x;
        x2 = x1;

        Invoke("MyFlip", 0.5f);
	}

    void MyFlip()
    {
        if (transform)
        {
            x2 = x1;
            x1 = transform.position.x;
            FlipDirection.Flip(transform, x1 - x2);
            Invoke("MyFlip", 0.5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
