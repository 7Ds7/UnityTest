using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 0.5f;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float jumpForce = 400f;

	private bool facingRight = true;
	private Rigidbody2D rbody;
	private Animator anim;
	private bool grounded = false;
	private float groundRadios = 0.2f;


	// Use this for initialization
	void Start () {
		rbody = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}

	void Update () {
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Grounded", false);
			rbody.AddForce (new Vector2 (0, jumpForce));
		}
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadios, whatIsGround);
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("vSpeed", rbody.velocity.y);
		Debug.Log ("rbody vel y " + rbody.velocity.y);

		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs(move));
		rbody.velocity = new Vector2 (move* maxSpeed, rbody.velocity.y);
		if (move > 0 && !facingRight) {
			Flip ();
		} else if ( move < 0 && facingRight) {
			Flip ();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
