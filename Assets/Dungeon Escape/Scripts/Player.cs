﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] //Allow to change the variable value in the inspector
    private float jumpForce = 5.0f;
    [SerializeField]
    private LayerMask ground; //Can use bit shift or this options as a variable
    private bool resetJump = false;
    private float speed = 2.5f;

    private PlayerAnimation playerAnim;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        FlipPlayer(move);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
        }

        rigid.velocity = new Vector2(move * speed, rigid.velocity.y);
        playerAnim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, 1 << 8);
        if(hitInfo.collider != null)
        {
            if(resetJump == false)
                return true;
        }

        return false;
    }

    void FlipPlayer(float move)
    {
        if(move > 0)
        {
            playerSprite.flipX = false;
        }
        else if(move < 0)
        {
            playerSprite.flipX = true;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }


}
