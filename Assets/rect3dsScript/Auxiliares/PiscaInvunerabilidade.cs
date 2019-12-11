using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PiscaInvunerabilidade
{
#pragma warning disable 0649
    [SerializeField] private SpriteRenderer[] meuSprite;
#pragma warning restore 0649
    private float tempoDecorrido = 0;
    private float tempoDeinvunerabilidadeAlvo = 0;
    private int numeroDePiscadelas = 5;
    private int vezesPiscadas = 0;

    private const float TEMPO_PADRAO = .75f;

    public bool Invuneravel = false;


    // Start is called before the first frame update
    public void Start(float tempoDeInvunerabilidade = 0,int numeroDePiscadelas = 5)
    {
        this.numeroDePiscadelas = numeroDePiscadelas;
        if (tempoDeInvunerabilidade == 0)
            tempoDeinvunerabilidadeAlvo = TEMPO_PADRAO;
        else
            tempoDeinvunerabilidadeAlvo = tempoDeInvunerabilidade;

        tempoDecorrido = 0;
        Invuneravel = true;
        vezesPiscadas = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Invuneravel)
        {
            tempoDecorrido += Time.deltaTime;

            if (vezesPiscadas % 2 == 0)
            {
                for(int i=0;i<meuSprite.Length;i++)
                meuSprite[i].color = Color.Lerp(Color.white, Color.gray, tempoDecorrido * numeroDePiscadelas / tempoDeinvunerabilidadeAlvo - vezesPiscadas);

                
            }
            else
            {
                for (int i = 0; i < meuSprite.Length; i++)
                    meuSprite[i].color = Color.Lerp(Color.gray, Color.white, tempoDecorrido * numeroDePiscadelas / tempoDeinvunerabilidadeAlvo - vezesPiscadas);
            }

            if (tempoDecorrido * numeroDePiscadelas / tempoDeinvunerabilidadeAlvo - vezesPiscadas > 1)
                vezesPiscadas++;

            if (tempoDecorrido >= tempoDeinvunerabilidadeAlvo)
            {
                Invuneravel = false;
                for (int i = 0; i < meuSprite.Length; i++)
                    meuSprite[i].color = Color.white;
            }
        }
    }
}
