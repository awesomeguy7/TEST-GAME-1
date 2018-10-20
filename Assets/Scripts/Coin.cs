using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public Vector3 Targetpos;
    public int Targethearts;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Targetpos = GameObject.Find("Slime").transform.position;
        Targethearts = GameObject.Find("Slime").GetComponent<Slime>().hearts;
        Debug.Log(Targethearts);
        if(Targethearts <= 0)
        {
            Instantiate(this, Targetpos, Quaternion.identity);
        }
    }
}
