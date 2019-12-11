﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovimentacaoBasica
{
    [SerializeField] private float m_MaxSpeed = 10f;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_AirControl = true;
#pragma warning disable 0649
    [SerializeField] private JumpManager myJump;
    [SerializeField] private bool jumpType;
    [SerializeField] private AudioClip somDoPulo;
#pragma warning restore 0649
    private Transform transform;
    private Transform m_GroundCheck;
    private Transform m_CeilingCheck;

    private bool m_Grounded;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_Jump;
    private bool m_FacingRight = true;
    private bool aplicandoForca;

    const float k_GroundedRadius = .2f;
    const float k_CeilingRadius = .01f;

    private bool estaNoChao;
    private bool estavaNoChao;
    private bool retornoDonoChao;

    public Vector3 Velocity
    {
        get { return m_Rigidbody2D.velocity;}
    }

    public bool NoChao
    {
        get {
            // bool noChao = false;
            estaNoChao = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius,256);

            for (int i = 0; i < colliders.Length; i++)
            {
                if(transform.gameObject.name=="teste")
                    Debug.Log(colliders[i].name);

                if (colliders[i].gameObject != transform.gameObject)
                {
                    //noChao = true;
                    retornoDonoChao = true;
                    estaNoChao = true;
                    estavaNoChao = true;
                }
            }

            if (!estaNoChao && estavaNoChao)
            {
                estavaNoChao = false;
                retornoDonoChao = true;
                GameController.g.StartCoroutine(PuloFantasma());
            }
            return retornoDonoChao;
        }
    }

    IEnumerator PuloFantasma()
    {
        yield return new WaitForSeconds(0.1f);
        if (!estaNoChao && !estavaNoChao)
            retornoDonoChao = false;
    }

    public void Iniciar(Transform T)
    {
        transform = T;
        m_Rigidbody2D = T.GetComponent<Rigidbody2D>();
        m_GroundCheck = transform.Find("checkGround");
        m_CeilingCheck = transform.Find("topo");
        myJump.IniciarCampos(m_Rigidbody2D);
    }

    public void AplicadorDeMovimentos(Vector3 V,bool m_jump = false,bool temPuloDuplo = false)
    {
        if (!aplicandoForca)
        {
            m_Grounded = false;
            // m_Jump = m_jump;

            if (NoChao)
                m_Grounded = true;


            Move(V.x, m_Jump || m_jump, temPuloDuplo);

            m_Jump = false;
        }
    }

    public void Move(float move, bool jump,bool temPuloDuplo)
    {

        if (m_Grounded || m_AirControl)
        {
            
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

            
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }

            if (m_Grounded && jump)
            {
                m_Grounded = false;

                if (!jumpType)
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                else
                    myJump.IniciaAplicaPulo(m_Rigidbody2D.transform.position.y);

                EventAgregator.Publish(new StandardSendGameEvent(EventKey.disparaSom, somDoPulo));
                EventAgregator.Publish(new StandardSendGameEvent(EventKey.animaIniciaPulo,m_Rigidbody2D.transform));
            }
            else if (!m_Grounded && jump && myJump.PodePuloDuplo && temPuloDuplo)
            {
                myJump.IniciaAplicaPuloDuplo(m_Rigidbody2D.transform.position.y);
            }

            if (myJump.EstouPulando)
            {
                myJump.VerificaPulo();
            }
            
            if(m_Grounded)
                myJump.NaoEstouPulando();
        }
    }

    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void JumpForce()
    {
        if (!jumpType)
            m_Rigidbody2D.AddForce(new Vector2(0f, 1.5f * m_JumpForce));
        else
            myJump.IniciaAplicaPulo(m_Rigidbody2D.transform.position.y);
    }

    public void ApplyForce(Vector2 f,float tempoAplicando = 0.25f)
    {
        aplicandoForca = true;

        m_Rigidbody2D.AddForce(f);
        GameController.g.StartCoroutine(RetornaAplicandoForca(tempoAplicando));
    }

    IEnumerator RetornaAplicandoForca(float t)
    {
        yield return new WaitForSeconds(t);
        aplicandoForca = false;
    }

    public void ChangeSpeed(float newSpeed)
    {
        m_MaxSpeed = newSpeed;
    }

    public void GravityScaled(float val)
    {
        m_Rigidbody2D.AddForce(new Vector2(0, -val));
    }

    public void ChangeGravityScale(float val)
    {
        m_Rigidbody2D.gravityScale = val;
    }

    public void Pulo()
    {
        m_Jump = true;
        
    }
}
