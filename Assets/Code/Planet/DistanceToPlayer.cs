﻿using UnityEngine;
using System.Collections;

public class DistanceToPlayer : MonoBehaviour {

	public GameObject player;
	private float playerMass = 0;
	public bool orbit = false;
	public float force =0 ;
	public float distance = 0;
	private float G = 6.676f * Mathf.Pow (10, -11);
	private float planetMass = 0;
	private Vector3 position;
	private float radius = 40;
	private float escapeDistance = 100;
	private bool rotate = false;
	private bool shoot = false;


	private float delay = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		position = this.transform.position;
		playerMass = player.rigidbody2D.mass;
		planetMass = 1000*Mathf.Pow (10, 10);



	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z + 1);

		Vector3 result = position - player.transform.position;
		distance = Mathf.Abs (Mathf.Sqrt(result.x * result.x + result.y * result.y));



		if (distance > radius && escapeDistance > distance ) {
						force = G * (playerMass*planetMass) /(distance*distance);
						player.gameObject.rigidbody2D.AddForce (result * force);
		} else {

			if (distance < radius && !player.GetComponent<PlayerToggle>().inOrbit && !shoot) {
				player.gameObject.rigidbody2D.velocity = new Vector3(0,0,0);
				orbit = true;
				player.GetComponent<PlayerToggle>().inOrbit = true;
		
			}
		}



		if (orbit && !rotate && !shoot) {
			player.transform.parent = this.gameObject.transform;
			player.transform.eulerAngles = new Vector3(0,0,player.transform.eulerAngles.z  -90);
			rotate = true;
		}



		if (Input.GetMouseButton(0) && orbit) {
			orbit = false;
			player.GetComponent<PlayerToggle>().inOrbit = false;
			player.transform.parent = null;
			rotate = false;

			Vector2 shotDirection = new Vector2(player.transform.right.x,player.transform.up.y);
			Debug.Log(shotDirection);

			player.rigidbody2D.AddForce(new Vector2(player.transform.right.x,player.transform.right.y) *10000000);
			shoot = true;
		}

		//Debug.DrawLine (player.transform.position, player.transform.forward);
		if (shoot) {
			delay+=Time.deltaTime;

			if (delay > 2) {
				shoot = false;
				delay = 0;
			}
		}


	}
}