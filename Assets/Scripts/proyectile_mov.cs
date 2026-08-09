using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections;
using System.Collections.Generic;


public class pryectile_mov : MonoBehaviour
{

    // Speed of the player
    public float moveSpeed;

    private Rigidbody2D body;
    private Vector3 originalScale;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get references to the Rigidbody2D and Animator components
        body = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocity = new Vector2(-moveSpeed, body.linearVelocity.y);

        ////Movement
        //float horizontalInput = Input.GetAxis("Horizontal");
        //body.linearVelocity = new Vector2(speed, body.linearVelocity.y);


        ////Set animation parameters
        //anim.SetBool("run", horizontalInput != 0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            moveSpeed *= -1;
        }
    }
}