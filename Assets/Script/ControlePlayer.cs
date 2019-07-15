using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePlayer : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody2D;
    public Transform groundCheck, bala;

    public bool isGrounded;
    public bool facingRight = true;
    public float speed;
    public float direcao;

    public bool jump = false;
    public float jumpForce;

    public int numberJumps = 0;
    public int maximaJumps = 2;

    public float velocidadeTiro;
    public GameObject tiroPrefab;

    public float delayTiro;
    private bool tiroDisparado;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Grounded"));
        playerAnimator.SetBool("isGrounded", isGrounded);

        direcao = Input.GetAxisRaw("Horizontal");

        ExecutaMovimento();

        if (Input.GetButtonDown("Jump")) //<-- &&isGrounded
        {
            jump = true;
        }

        if(Input.GetButton("Fire1") && tiroDisparado == false)
        {
            if(playerRigidbody2D.velocity.x != 0 && isGrounded)
            {
                Atirar();
            }
        }

    }

    private void FixedUpdate()
    {
        MovePlayer(direcao);

        if (jump)
        {
            JumpPlayer();
        }
    }

    void JumpPlayer()
    {
        if (isGrounded)
        {
            numberJumps = 0;
        }
        if (isGrounded || numberJumps < maximaJumps)
        {
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce));
            numberJumps++;
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        velocidadeTiro *= -1;
        transform.localScale = theScale;
    }

    void ExecutaMovimento()
        {
            playerAnimator.SetFloat("velocidadeY", playerRigidbody2D.velocity.y);
            playerAnimator.SetBool("Jump", !isGrounded);
            playerAnimator.SetBool("Run", playerRigidbody2D.velocity.x != 0f && isGrounded);
        }

    void MovePlayer(float movimentoH)
    {
        playerRigidbody2D.velocity = new Vector2(movimentoH * speed, playerRigidbody2D.velocity.y);

        if (movimentoH < 0 && facingRight || (movimentoH > 0 && !facingRight))
        {
           Flip();
        }
    }

    void Atirar()
    {
        tiroDisparado = true ;
        StartCoroutine("tempoTiro");

        GameObject temporarioTiro = Instantiate(tiroPrefab);
        temporarioTiro.transform.position = bala.position;

        if (facingRight == false)
        {
            temporarioTiro.GetComponent<SpriteRenderer>().flipX = true;
        }

        temporarioTiro.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiro, 0);
    }

    IEnumerator tempoTiro()
    {
        yield return new WaitForSeconds(delayTiro);
        tiroDisparado = false;
    }
}
