using System.Collections;
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
    private float speed = 3f;

    private PlayerAnimation playerAnim;
    private SpriteRenderer playerSprite;
    private SpriteRenderer swordArcSprite;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            playerAnim.Attack();
        }
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        IsGrounded();

        FlipPlayer(move);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
            playerAnim.Jump(true);
        }

        rigid.velocity = new Vector2(move * speed, rigid.velocity.y);
        playerAnim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        //Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null)
        {
            if(resetJump == false)
            {
                playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    void FlipPlayer(float move)
    {
        if(move > 0)
        {
            playerSprite.flipX = false;
            swordArcSprite.flipY = false;
        }
        else if(move < 0)
        {
            playerSprite.flipX = true;
            swordArcSprite.flipY = true;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }


}
