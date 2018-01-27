﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void DeadEventHandler();

public class PlayerController : Character
{
    public event DeadEventHandler Dead;
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private LayerMask whatIsGround;

    public float jumpForce;
    private bool immortal = false;
    [SerializeField]
    private float immortalTime;
    private SpriteRenderer sprR;
    //private bool attacking;
    //private bool isGrounded;
    //private bool jumping;
    //private bool jumpAttacking;
    [SerializeField]
    private bool airControl;
    public float groundRadius;

    [SerializeField]
    private List<string> whatIsPointPickup = new List<string>();

    //[SerializeField]
    private Vector2 startPosition;

    public Rigidbody2D MyRb2d
    {
        get; set;
    }

    public bool Jumping
    {
        get; set;
    }
    public bool OnGround
    {
        get; set;
    }
    public bool Running
    {
        get; set;
    }
    public bool AirKicking
    {
        get; set;
    }
    private float[] AirKickSpeeds = new float[5];
    private int AirKickStage;
    [SerializeField]
    private bool CanAirKick;
    [SerializeField]
    private bool Jumped;
    [SerializeField]
    private bool IsAirKicking;

    public override bool IsDead
    {
        get
        {
            if (health <= 0)
            {
                OnDead();
            }
            return health <= 0;
        }
    }


    // Use this for initialization
    public override void Start()
    {
        base.Start();
        startPosition = transform.position;
        sprR = GetComponent<SpriteRenderer>();
        MyRb2d = GetComponent<Rigidbody2D>();

        whatIsPointPickup.Add("HW");

        CanAirKick = false;
        Jumped = false;
        IsAirKicking = false;
        AirKickStage = 0;
        AirKickSpeeds[0] = speed;
        AirKickSpeeds[1] = speed * 1.25f;
        AirKickSpeeds[2] = speed * 1.5f;
        AirKickSpeeds[3] = speed * 1.75f;
        AirKickSpeeds[4] = speed * 2.0f;
    }

    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                Death();
            }
            HandleInput();
        }
    }
    
    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float hor = Input.GetAxis("Horizontal");
            OnGround = IsGrounded();
            if (OnGround && MyRb2d.velocity.y < 0)
            {
                Jumped = false;
                CanAirKick = true;
            }
            HandleMovement(hor);
            Flip(hor);
            //HandleAttacks();
            HandleLayers();

            //ResetValues();
        }
        else if (IsDead)
        {
            MyRb2d.velocity = new Vector2(0, MyRb2d.velocity.y);
        }
    }

    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    private void HandleMovement(float h)
    {
        if (MyRb2d.velocity.y < 0)
        {
            MyAnimator.SetBool("fall", true);
        }
        if (OnGround || airControl)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                h *= 2;
                MyAnimator.SetBool("run", true);
                MyAnimator.SetTrigger("startRun");
            }
            //if (Input.GetKeyUp(KeyCode.LeftShift))
            else
            {
                MyAnimator.SetBool("run", false);
                MyAnimator.ResetTrigger("startRun");
            }
            MyRb2d.velocity = new Vector2(h * speed, MyRb2d.velocity.y);
        }
        if (Jumping && MyRb2d.velocity.y == 0)
        {
            MyRb2d.AddForce(new Vector2(0, jumpForce));            
        }
        MyAnimator.SetFloat("walkSpeed", Mathf.Abs(h));
        /*
        if (isGrounded && jumping)
        {
            isGrounded = false;
            rb2d.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("jump");
        }
        */
    }

    /*private void HandleAttacks()
    {
        if (attacking && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack");
        }
        if (jumpAttacking && !isGrounded && !this.animator.GetCurrentAnimatorStateInfo(1).IsTag("JumpAttack"))
        {
            animator.SetBool("jumpAttack", true);
        }
        if (!jumpAttacking && !this.animator.GetCurrentAnimatorStateInfo(1).IsTag("JumpAttack"))
        {
            animator.SetBool("jumpAttack", false);
        }
    }*/

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (OnGround)
            {
                MyAnimator.SetTrigger("jump");
                Jumped = true;
            }
            //else if (!OnGround && !Jumping && Running /*&& MyRb2d.velocity.y < 0*/)
            else if (!OnGround && !Jumping && !Jumped && Running && MyRb2d.velocity.y < 0)
            {
                if (CanAirKick)
                {
                    Debug.Log("AIR KICK!");
                    MyAnimator.SetTrigger("airKick");
                    AirKick();
                    CanAirKick = false;
                    IsAirKicking = true;
                }
            }
            //jumping = true;
        }
        if (IsAirKicking && Mathf.Abs(Input.GetAxis("Horizontal")) < 1)
        {
            ResetAirKick();
        }
        if (Input.GetKey(KeyCode.X))
        {
            MyAnimator.SetTrigger("attack");
            //FireJuice(0);
            //attacking = true;
            //jumpAttacking = true;
        }
    }

    private void Flip(float h)
    {
        if (h > 0 && !facingRight || h < 0 && facingRight)
        {
            /*facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;*/
            if (IsAirKicking)
            {
                ResetAirKick();
            }
            ChangeDirection();
        }

    }

    /*private void ResetValues()
    {
        attacking = false;
        jumping = false;
        jumpAttacking = false;
    }*/

    private bool IsGrounded()
    {
        if (MyRb2d.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i=0; i<colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        //animator.ResetTrigger("jump");
                        //animator.SetBool("fall", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool IsGrounded2()
    {
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void FireJuice(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            base.FireJuice(value);
            /*if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(JuicePrefab, JuicePos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                tmp.GetComponent<JuiceBullets>().Initialize(Vector2.right, false);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(JuicePrefab, JuicePos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                tmp.GetComponent<JuiceBullets>().Initialize(Vector2.left, false);
            }*/
        }
        /*else if (!OnGround && value == 1)
        {
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(JuicePrefab, JuicePos.position, Quaternion.Euler(new Vector3(0, 0, -45)));
                tmp.GetComponent<JuiceBullets>().Initialize(Vector2.right, true);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(JuicePrefab, JuicePos.position, Quaternion.Euler(new Vector3(0, 0, 225)));
                tmp.GetComponent<JuiceBullets>().Initialize(Vector2.left, true);
            }
        }*/
    }

    private void AirKick()
    {
        MyRb2d.velocity = new Vector2(MyRb2d.velocity.x, 5);
        if (AirKickStage < 4)
        {
            AirKickStage += 1;
            speed = AirKickSpeeds[AirKickStage];
        }
    }

    private void ResetAirKick()
    {
        IsAirKicking = false;
        CanAirKick = false;
        AirKickStage = 0;
        speed = AirKickSpeeds[0];
    }

    private IEnumerator IndicateImmortality()
    {
        while (immortal)
        {
            sprR.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprR.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            health -= 10;
            ResetAirKick();
            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortality());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }
        }
    }

    public override void Death()
    {
        MyRb2d.velocity = Vector2.zero;
        ResetAirKick();
        MyAnimator.SetTrigger("idle");
        health = startHealth;
        GameManager.Instance.Lives--;
        if (GameManager.Instance.Lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            transform.position = startPosition;
        }
    }

    //private void OnCollisionEnter2D(Collision2D other)
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        /*
        //if (other.gameObject.tag == "HW")
        if (whatIsPointPickup.Contains(other.gameObject.tag))
        {
            GameManager.Instance.Score += 100;
            Destroy(other.gameObject);
        }
        */
        if (other.gameObject.tag == "Fruit")
        {
            if (health < 20)
            {
                health += 10;
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }

    //Exit fruit to make it opague again
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
