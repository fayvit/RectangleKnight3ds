using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MenuPentagonosHexagonos
{
    #region inspector
    [SerializeField] private Image partesDeHexagonoObtidas = null;
    [SerializeField] private Image partesDePentagonosObtidas = null;
    [SerializeField] private Text numHexagonosCompletados =  null;
    [SerializeField] private Text numPentagonosCompletados = null;
    [SerializeField] private Text totalDeHp = null;
    [SerializeField] private Text totalDeMp = null;
    [SerializeField] private Sprite[] hexagonoSprites = null;
    [SerializeField] private Sprite[] pentagonoSprites = null;
    #endregion

    public void IniciarHud()
    {
        partesDeHexagonoObtidas.transform.parent.gameObject.SetActive(true);

        DadosDoJogador dj = GameController.g.Manager.Dados;
        partesDeHexagonoObtidas.sprite = hexagonoSprites[dj.PartesDeHexagonoObtidas];
        partesDePentagonosObtidas.sprite = pentagonoSprites[dj.PartesDePentagonosObtidas];
        numHexagonosCompletados.text = dj.HexagonosCompletados.ToString();
        numPentagonosCompletados.text = dj.PentagonosCompletados.ToString();
        totalDeHp.text = dj.PontosDeVida + " / " + dj.MaxVida;
        totalDeMp.text = dj.PontosDeMana + " / " + dj.MaxMana;
    }

    public void FinalizarHud()
    {
        partesDeHexagonoObtidas.transform.parent.gameObject.SetActive(false);
    }
}