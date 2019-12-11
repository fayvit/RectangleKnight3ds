using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AnimadorDoPersonagem
{
    private Animator animador;

    public AnimadorDoPersonagem(Transform T)
    {
        animador = T.GetComponent<Animator>();
    }

    public void FinalizaPulo()
    {
        animador.SetBool("emPulo", false);
    }

    public void AnimaIniciaPulo()
    {
        animador.SetBool("emPulo", true);
        animador.Play("subindo");
    }

    public void EfetuarAnimacao(float vel, bool estaNoAr)
    {
        animador.SetFloat("velocidade", vel);
        animador.SetBool("noAr", estaNoAr);
    }

    public void AnimaAtaqueNormal(float f)
    {
        if(f==0)
            animador.Play("ataqueNormal");
        else
            animador.Play("atkAndando");
    }

    public void AnimaAtaqueNormalForaDoChao()
    {
        animador.Play("atkNormalAr");
    }

    public void AnimaAtaqueAlto()
    {
        animador.Play("atkAlto");
    }


    public void AnimaAtaqueAltoForaDoChao()
    {
        animador.Play("atkAltoAr");
    }
    

    public void AnimaAtaqueBaixo()
    {
        animador.Play("atkBaixo");
    }

}
