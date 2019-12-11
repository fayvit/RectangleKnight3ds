using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DadosDoJogador : DadosDoPersonagem
{
    [SerializeField] private bool _temDash = false;
    [SerializeField] private bool _temDoubleJump = false;
    [SerializeField] private bool _temDownArrowJump = false;
    [SerializeField] private bool _temMagicAttack = false;
    [SerializeField] private bool _espadaAzul = false;
    [SerializeField] private bool _espadaVerde = false;
    [SerializeField] private bool _espadaDourada = false;
    [SerializeField] private bool _espadaVermelha = false;
    [SerializeField] private int _dinheiro = 0;
    [SerializeField] private int _hexagonosCompletos = 0;
    [SerializeField] private int _pentagonosCompletos = 0;
    [SerializeField] private int _baseMaxLife = 100;
    [SerializeField] private int _baseMaxMana = 50;
    [SerializeField] private int _addLifeBarAmount = 25;
    [SerializeField] private int _addMagicBarAmount = 10;

    private int seloDoProg = 0;
    private int seloDoAmor = 0;
    private int selodaOrdem = 0;

    private int espEmb = 2;
    private int partesHex = 0;
    private int partesPent = 0;

    private SwordColor corDeEspSel = SwordColor.grey;
    private List<ItemBase> meusItens = new List<ItemBase>();
    private DinheiroCaido dCaido = new DinheiroCaido();
    private List<Emblema> meusEmbl = new List<Emblema>();

    public int SeloDoProgresso { get { return seloDoProg; } set { seloDoProg = value; } }
    public int SeloDoAmor { get { return seloDoAmor; } set { seloDoAmor = value; } }
    public int SeloDoOrdem { get { return selodaOrdem; } set { selodaOrdem = value; } }

    public int EspacosDeEmblemas { get { return espEmb; } set { espEmb = value; } }
    public int PartesDeHexagonoObtidas { get { return partesHex; } set { partesHex = value; } }
    public int PartesDePentagonosObtidas { get { return partesPent; } set { partesPent = value; } }

    public SwordColor CorDeEspadaSelecionada { get { return corDeEspSel; } set { corDeEspSel = value; } }
    public List<ItemBase> MeusItens { get { return meusItens; } set { meusItens = value; } }
    public DinheiroCaido DinheiroCaido { get { return dCaido; } set { dCaido = value; } }
    public List<Emblema> MeusEmblemas { get { return meusEmbl; } set { meusEmbl = value; } }

    public bool TemDash { get { return _temDash; } set { _temDash = value; } }
    public bool TemDoubleJump { get { return _temDoubleJump; } set { _temDoubleJump = value; } }
    public bool TemDownArrowJump { get { return _temDownArrowJump; } set { _temDownArrowJump = value; } }
    public bool TemMagicAttack { get { return _temMagicAttack; } set { _temMagicAttack = value; } }
    public bool EspadaAzul { get { return _espadaAzul; } set { _espadaAzul = value; } }
    public bool EspadaVerde { get { return _espadaVerde; } set { _espadaVerde = value; } }
    public bool EspadaDourada { get { return _espadaDourada; } set { _espadaDourada = value; } }
    public bool EspadaVermelha { get { return _espadaVermelha; } set { _espadaVermelha = value; } }
    public int Dinheiro { get { return _dinheiro; } set { _dinheiro = value; } }
    
    public int HexagonosCompletados { get { return _hexagonosCompletos; } set { _hexagonosCompletos = value; } }
    public int PentagonosCompletados { get { return _pentagonosCompletos; } set { _pentagonosCompletos = value; } }
    public int BaseMaxLife { get { return _baseMaxLife; } set { _baseMaxLife = value; } }
    public int BaseMaxMana { get { return _baseMaxMana; } set { _baseMaxMana = value; } }
    public int AddLifeBarAmount { get { return _addLifeBarAmount; } set { _addLifeBarAmount = value; } }
    public int AddMagicBarAmount { get { return _addMagicBarAmount; } set { _addMagicBarAmount = value; } }
    
    

    public DadosDoJogador()
    {
        PontosDeVida = BaseMaxLife;
        PontosDeMana = BaseMaxMana;
    }

    public override int AtaqueBasico {
        get {
            float modificador = 1;
            float adicional = 0;

            if (GameController.g.MyKeys.VerificaAutoShift("equiped_" + NomesEmblemas.ataqueAprimorado.ToString()))
            {
                adicional = 10;
            }

            return Mathf.RoundToInt(modificador*base.AtaqueBasico+adicional);

        }
        set { base.AtaqueBasico = value; }
    }

    
    
    public UltimoCheckPoint ultimoCheckPoint = new UltimoCheckPoint()
    {
        nomesDasCenas = new NomesCenas[1] { NomesCenas.TutoScene},
        Pos = new Vector3(3, 47, 0)
    };

    public int QuantidadeNoInventarioDoItem(NomeItem indice)
    {
        for (int i = 0; i < MeusItens.Count; i++)
        {
            if (MeusItens[i].Nome == indice)
                return MeusItens[i].Quantidade;
        }

        return 0;
    }

    public int NumberedPositivistStamp(int indice)
    {
        SeloPositivista.TipoSelo s = (SeloPositivista.TipoSelo)indice;
        int retorno = 0;
        switch (s)
        {
            case SeloPositivista.TipoSelo.progresso:
                retorno = SeloDoProgresso;
                break;
            case SeloPositivista.TipoSelo.amor:
                retorno = SeloDoAmor;
                break;
            case SeloPositivista.TipoSelo.ordem:
                retorno = SeloDoOrdem;
                break;
        }

        return retorno;
    }

    public void PegouSelo(SeloPositivista.TipoSelo tipo,int quantidade = 1)
    {
        switch (tipo)
        {
            case SeloPositivista.TipoSelo.progresso:
                SeloDoProgresso += quantidade;
            break;
            case SeloPositivista.TipoSelo.amor:
                SeloDoAmor += quantidade;
            break;
            case SeloPositivista.TipoSelo.ordem:
                SeloDoOrdem += quantidade;
            break;
        }
    }

    public void SomaHexagono()
    {
        PartesDeHexagonoObtidas++;
    }

    public void SomaPentagono()
    {
        PartesDePentagonosObtidas++;
    }

    public void GetSword(SwordColor cor)
    {
        switch (cor)
        {
            case SwordColor.blue:
                EspadaAzul = true;
            break;
            case SwordColor.green:
                EspadaVerde = true;
            break;
            case SwordColor.gold:
                EspadaDourada = true;
            break;
            case SwordColor.red:
                EspadaVermelha = true;
            break;
        }
    }

    public bool SwordAvailable(SwordColor cor)
    {
        bool retorno = false;
        switch (cor)
        {
            case SwordColor.blue:
                retorno = EspadaAzul;
            break;
            case SwordColor.green:
                retorno = EspadaVerde;
            break;
            case SwordColor.gold:
                retorno = EspadaDourada;
            break;
            case SwordColor.red:
                retorno = EspadaVermelha;
            break;
        }

        return retorno;
    }

}

[System.Serializable]
public class UltimoCheckPoint
{
    private float[] pos=new float[3] {0,0,0};

    public Vector3 Pos {
        get { return new Vector3(pos[0], pos[1], pos[2]); }
        set { pos = new float[3] { value.x, value.y, value.z }; }
    }
    public NomesCenas[] nomesDasCenas;
}

[System.Serializable]
public class DinheiroCaido
{
    private float[] pos = new float[3] { 0, 0, 0 };

    public Vector3 Pos
    {
        get { return new Vector3(pos[0], pos[1], pos[2]); }
        set { pos = new float[3] { value.x, value.y, value.z }; }
    }

    public NomesCenas cenaOndeCaiu;
    public int valor = 0;
    public bool estaCaido = false;
}
