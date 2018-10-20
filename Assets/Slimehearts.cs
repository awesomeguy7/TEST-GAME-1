using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimehearts : MonoBehaviour {
    Vector2 Targetpos;
    Vector2 Wantedpos;
	// Use this for initialization
	void Start () {
     
        
    }
	
	// Update is called once per frame
	void Update () {
        Targetpos = GameObject.Find("Slime").transform.position;
        Wantedpos.x = Targetpos.x - 0.3f;
        Wantedpos.y = Targetpos.y + 0.2f;
        transform.position = Wantedpos;

    }

}
