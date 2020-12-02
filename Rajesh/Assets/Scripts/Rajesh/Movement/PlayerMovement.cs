using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //komponenty
    Rigidbody2D rb;
    Animator animator;
    BoxCollider2D collider;

    //ruch
    public float startSpeed;
    public float speed;
    public float moveInput = 0;
    public bool run;

    //boost
    public float boostTime;
    public float startBoostTime;
    public float speedMultiplier;

    //ślizg
    public float startSlideTime;
    public float slideTime;
    public bool sliding;
    bool blockSlide;

    //skok
    public bool isGrounded;
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGrounded;
    public int startExtraJumps;
    int extraJumps;
    public bool jumped = false;
    public bool landing;
    public bool takingof;
    public bool landingBuffer;
    public bool landingAnim;
    float timeToJump;
    public float startTimeToJump;
    public bool blockSpeed;
    public bool startJump;
    public bool engagingJump;
    public bool smoothEnd;

    private void Start()
    {
        //deifiniowanie startowej prędkości i czasu ślizgu
        speed = startSpeed;
        slideTime = startSlideTime;
        timeToJump = 0;

        //definiowanie komponentów
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

        //uzupełnianie dodatkowych skoków
        extraJumps = startExtraJumps;
    }

    void Update()
    {
        landingBuffer = landing;

        if(sliding && smoothEnd)
        {
            jumped = false;
        }

        if (blockSpeed && sliding)
        {
            blockSpeed = !blockSpeed;
        }
        else if (blockSpeed)
        {
            speed = startSpeed * 1.5f;
        }

        //dodatkowa prędkośc po skoku
        if (boostTime > 0)
        {
            if (boostTime / startBoostTime < 0.5)
            {
                speed = startSpeed + (((startSpeed * speedMultiplier) - startSpeed) * (boostTime / startBoostTime)*2);
            }
            else
            {
                speed = startSpeed * speedMultiplier;
            }
            boostTime -= Time.deltaTime;
        }
        else
        {
            speed = startSpeed;
        }

        //szczytywanie input z klawiatury
        if (!jumped && !sliding && !landingAnim && !startJump)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                moveInput = 0;
            }
            else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                moveInput = 0;
            }
            else if (moveInput == 0)
            {
                moveInput = Input.GetAxisRaw("Horizontal");
            }
            else if (((moveInput < 1 && moveInput != 0) || (moveInput > -1 && moveInput != 0)) && (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1))
            {
                moveInput = Input.GetAxisRaw("Horizontal");
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                moveInput = 0;
            }
            else
            {
                moveInput = Input.GetAxis("Horizontal");
            }
        }
        
        if(!(run && moveInput != 0))
        {
            moveInput = Input.GetAxis("Horizontal");
        }
        //odbijanie postaci
        if(transform.localScale.x < 0 && moveInput > 0)
        {
            Flip();
        }
        else if((transform.localScale.x > 0 && moveInput < 0))
        {
            Flip();
        }

        //uzupełnianie pozostałych skoków
        if (isGrounded)
        {
            extraJumps = startExtraJumps;
        }

        //skok
        if(Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded && !jumped && timeToJump <= 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetTrigger("TakeOf");
            jumped = true;
            if (sliding)
            {
                speed = startSpeed * 1.8f;
            }
            timeToJump = startTimeToJump;
        }

        //przyśpieszanie skoku
        if (!isGrounded && jumped)
        {
            speed = startSpeed * 1.5f;
        }

        if(landing && jumped)
        {
            speed = startSpeed * speedMultiplier;
        }

        if(isGrounded && landingAnim)
        {
            jumped = false;
        }

        //ustawianie odpowiedniej animacji
        if ((moveInput == 1 || moveInput == -1) && isGrounded && (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1))
        {
            animator.SetBool("isRunning", true);
            run = true;
            animator.speed = Mathf.Abs(moveInput) * (speed / startSpeed) *1.5f;
        }
        else if ((moveInput < 1 && moveInput > -1 && moveInput != 0) && (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1) && isGrounded)
        {
            animator.SetBool("isRunning", true);
            run = true;
            animator.speed = Mathf.Abs(moveInput) * (speed / startSpeed) * 1.5f;
        }
        else if((moveInput < 1 && moveInput > -1  && moveInput != 0) && (Input.GetAxis("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0) && isGrounded)
        {
            animator.SetBool("isRunning", true);
            run = true;
            animator.speed = Mathf.Abs(moveInput) * (speed / startSpeed) * 1.5f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            run = false;
            boostTime = 0;
            animator.speed = 1;
        }

        if(jumped && landing && !takingof && !(jumped && !landing))
        {
            moveInput = Input.GetAxis("Horizontal");
        }

        //sprawdzanie warunków do ślizgu
        if (!isGrounded && Input.GetKey(KeyCode.DownArrow) && !blockSlide)
        {
            if (slideTime > 0)
            {
                if (isGrounded) { speed = startSpeed * 1.8f; }
                moveInput = NormalizeValue(transform.localScale.x);
                animator.SetBool("isSliding", true);
                sliding = true;
                if (isGrounded) { slideTime -= Time.deltaTime; }
            }
            else
            {
                animator.SetBool("isSliding", false);
                speed = startSpeed;
                sliding = false;
                blockSlide = true;
            }
        }
        else if (sliding && Input.GetKey(KeyCode.DownArrow) && !blockSlide)
        {
            if (slideTime > 0)
            {
                if (isGrounded)
                {
                    speed = startSpeed * 1.8f;
                }
                moveInput = NormalizeValue(transform.localScale.x);
                animator.SetBool("isSliding", true);
                sliding = true;
                if (isGrounded) { slideTime -= Time.deltaTime; }
            }
            else
            {
                animator.SetBool("isSliding", false);
                speed = startSpeed;
                sliding = false;
                blockSlide = true;
            }
        }
        else if (landing && jumped && Input.GetKey(KeyCode.DownArrow) && !blockSlide)
        {
            jumped = false;
            if (slideTime > 0)
            {
                if (isGrounded)
                {
                    speed = startSpeed * 1.8f;
                }
                moveInput = NormalizeValue(transform.localScale.x);
                animator.SetBool("isSliding", true);
                sliding = true;
                if (isGrounded) { slideTime -= Time.deltaTime; }
            }
            else
            {
                animator.SetBool("isSliding", false);
                speed = startSpeed;
                sliding = false;
                blockSlide = true;
            }
        }
        else if (landingBuffer == false && landing && jumped && Input.GetKey(KeyCode.DownArrow) && !blockSlide)
        {
            jumped = false;
            if (slideTime > 0)
            {
                if (isGrounded)
                {
                    speed = startSpeed * 1.8f;
                }
                moveInput = NormalizeValue(transform.localScale.x);
                animator.SetBool("isSliding", true);
                sliding = true;
                if (isGrounded) { slideTime -= Time.deltaTime; }
            }
            else
            {
                animator.SetBool("isSliding", false);
                speed = startSpeed;
                sliding = false;
                blockSlide = true;
            }
        }
        else if(isGrounded && Input.GetKey(KeyCode.DownArrow) && !blockSlide)
        {
            jumped = false;
            if (slideTime > 0)
            {
                if (isGrounded)
                {
                    speed = startSpeed * 1.8f;
                }
                moveInput = NormalizeValue(transform.localScale.x);
                animator.SetBool("isSliding", true);
                sliding = true;
                if (isGrounded) { slideTime -= Time.deltaTime; }
            }
            else
            {
                animator.SetBool("isSliding", false);
                speed = startSpeed;
                sliding = false;
                blockSlide = true;
            }
        }
        else if (landing && Input.GetKey(KeyCode.DownArrow) && !blockSlide)
        {
            jumped = false;
            if (slideTime > 0)
            {
                if (isGrounded)
                {
                    speed = startSpeed * 1.8f;
                }
                moveInput = NormalizeValue(transform.localScale.x);
                animator.SetBool("isSliding", true);
                sliding = true;
                if (isGrounded) { slideTime -= Time.deltaTime; }
            }
            else
            {
                animator.SetBool("isSliding", false);
                speed = startSpeed;
                sliding = false;
                blockSlide = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            blockSlide = false;
            animator.SetBool("isSliding", false);
            speed = startSpeed;
            sliding = false;
        }
        else
        {
            animator.SetBool("isSliding", false);
            sliding = false;
            if (slideTime < startSlideTime && !startJump && !sliding && !takingof) { slideTime += Time.deltaTime; }
        }

        if(timeToJump > 0) { timeToJump -= Time.deltaTime; }
        if (blockSpeed && sliding)
        {
            blockSpeed = !blockSpeed;
        }
        else if (blockSpeed)
        {
            speed = startSpeed * 1.5f;
        }

        if(isGrounded && jumped && !sliding && !landingAnim && !startJump && !takingof)
        {
            jumped = false;
            blockSpeed = false;
        }

        if (isGrounded && !jumped && blockSpeed && !sliding && !landingAnim)
        {
            blockSpeed = false;
        }

        if (engagingJump || landingAnim)
        {
            moveInput = NormalizeValue(transform.localScale.x);
            speed = 1.5f * startSpeed;
        }

        if (smoothEnd)
        {
            moveInput = NormalizeValue(transform.localScale.x);
            speed = 1.5f * startSpeed;
        }

        if(isGrounded && !startJump && !landingAnim && !takingof && smoothEnd)
        {
            smoothEnd = false;
        }

    }
    private void FixedUpdate()
    {
        //zapisanie wartości isGrounded do buffora
        bool groundBuffer = isGrounded;

        //sprawdzanie czy gracz dotyka ziemi
        if (!takingof) { isGrounded = Physics2D.OverlapCircle(groundCheck.position, collider.size.x * 0.80f, whatIsGrounded); }

        //zakończenie animacji skoku
        if(isGrounded == true && groundBuffer == false)
        {
            animator.SetBool("isLanding", true);
            landing = true;
        }
        else if (groundBuffer)
        {
            animator.SetBool("isLanding", true);
            landing = true;
        }
        else
        {
            animator.SetBool("isLanding", false);
            landing = false;
        }

        //zmiana x velocity w zależności od inputu z klawiatury
        if (!blockSpeed) { rb.velocity = new Vector2(rb.velocity.x + moveInput * speed * Time.fixedDeltaTime, rb.velocity.y); }
        else { rb.velocity = new Vector2(rb.velocity.x + NormalizeValue(transform.localScale.x) * speed * Time.fixedDeltaTime, rb.velocity.y); }
    }

    private void OnDrawGizmos()
    {
        //rysowanie gizmo ground
        try { Gizmos.DrawWireSphere(groundCheck.position, collider.size.x * 0.95f); }
        catch { };
    }

    void Flip()
    {
        //odbijanie w poziomie
        Vector3 scaler = transform.localScale;
        scaler.x = -scaler.x;
        transform.localScale = scaler;
    }

    //funkcja określająca czy zmienna jest dodatnia (1), czy ujemna (-1)
    float NormalizeValue(float about)
    {
        if(about > 0)
        {
            return 1;
        }
        else if(about < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
