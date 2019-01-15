using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour {

    private Vector3 initPosition;

	// Use this for initialization
	void Start () {
        initPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = initPosition + new Vector3(10*Mathf.Sin(Time.time), 0, 0);
	}
}
