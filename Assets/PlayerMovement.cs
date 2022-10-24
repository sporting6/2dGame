
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float defaultPlayerSpeed = 2f;
    private float playerSpeed;
    public Rigidbody2D player;
    public Animator anim;
    Vector2 movement;
    public float dashDefaultTime = .5f;
    public float dashTime = 0f;
    public float dashCooldown = 0f;
    public float dashSpeed = 32f;
    private float time;
    bool dashing = false;   


    void Start()
    {
        dashTime = dashDefaultTime;
        playerSpeed = defaultPlayerSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("DashTime", dashTime);
        Movement();
    }

    void FixedUpdate()
    {
        player.MovePosition(player.position + movement * playerSpeed * Time.fixedDeltaTime);
        Dash();

    }

    void Dash()
    {
        Vector3 effectPos;
        effectPos.x = player.position.x;
        effectPos.y = player.position.y;
        effectPos.z = 0;

        dashCooldown += Time.fixedDeltaTime;
        
        if (dashTime <= 0){
            playerSpeed = 2f;
            dashing = false;
            anim.SetFloat("Dash", 0f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown >= 3)
        {
            print("Dash");
            
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Dash", 1f);

             

            dashing = true;
            playerSpeed = dashSpeed;
            player.velocity = movement;
            dashCooldown = 0;
            dashTime = dashDefaultTime;
            time = Time.time;
        }
        if(dashing)
        {
            dashTime = dashTime - Time.time + time;
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
