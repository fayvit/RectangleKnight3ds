using UnityEngine;
using UnityEngine.UI;

public class PainelPentagonoHexagono : PainelUmaMensagem
{
    #region inspector
    [SerializeField] private Text textoDaDescricao = null;
    [SerializeField] private Image imgDaqui = null;
    [SerializeField] private Sprite[] labelImages = null;
    #endregion

    public enum Forma
    {
        pentagono,
        hexagono,
        naoForma
    }

    protected Sprite[] LabelImages { get { return labelImages; } }

    public void ConstroiPainelDosPentagonosOuHexagonos(System.Action r,Forma f)
    {
        string[] s = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.hexagonPentagonTips).ToArray();
        if (f == Forma.hexagono)
        {
            ConstroiPainelUmaMensagem(r, s[0]);
            textoDaDescricao.text = s[1];
            ModificarSprites(GameController.g.Manager.Dados.PartesDeHexagonoObtidas);
            //imgDaqui.sprite = labelImages[];
        }
        else if (f == Forma.pentagono)
        {
            ConstroiPainelUmaMensagem(r, s[2]);
            textoDaDescricao.text = s[3];
            ModificarSprites(GameController.g.Manager.Dados.PartesDePentagonosObtidas);
        }
        else {
            ConstroiPainelUmaMensagem(r);
        }
    }

    public virtual void ModificarSprites(int quantas)
    {
        imgDaqui.sprite = labelImages[quantas];
    }
}
