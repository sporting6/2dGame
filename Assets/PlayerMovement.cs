
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 2f;
    public Rigidbody2D player;
    public Animator anim;
    Vector2 movement;
    Vector2 dash;
    public float dashx = 3;
    public float dashy = 3;
    public float dashCooldown = 0;


    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        Movement();

        dash.x = dashx * movement.x;
        dash.y = dashy * movement.y;
    }

    void FixedUpdate()
    {
        player.MovePosition(player.position + movement * playerSpeed * Time.fixedDeltaTime);
        Dash();

    }

    void Dash()
    {
        dashCooldown += Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown >= 3)
        {
            print("Dash");
            player.MovePosition(player.position + dash * .2f);
            dashCooldown = 0;
        }
    }

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
}
