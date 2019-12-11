using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapPanel
{
    [SerializeField] private float vel = 0.1f;
    [SerializeField] private Image imgDoMapa = null;
    [SerializeField] private RectTransform painelDeTamanhoVariavel = null;
    [SerializeField] private ScrollRect sr = null;

    public void IniciarVisualizacaoDoMapa()
    {
        bool foi = false;
        if (imgDoMapa.sprite == null)
            foi = true;
        else if (imgDoMapa.sprite.name != "")
            foi = true;

        painelDeTamanhoVariavel.parent.parent.gameObject.SetActive(true);

        if (foi)
        {
            new MyInvokeMethod().InvokeAoFimDoQuadro(() =>
            {
                new MyInvokeMethod().InvokeAoFimDoQuadro(DesenharMapa);
            });
        }
        
    }

    void DesenharMapa()
    {
        Texture2D tex = GameController.g.MapTexture;


        imgDoMapa.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), tex.texelSize);

        imgDoMapa.rectTransform.localScale = new Vector3(tex.width / 50f, tex.height / 50f, 1);


        float xI = imgDoMapa.rectTransform.sizeDelta.x;
        float yI = imgDoMapa.rectTransform.sizeDelta.y;
        float xP = painelDeTamanhoVariavel.sizeDelta.x;
        float yP = painelDeTamanhoVariavel.sizeDelta.y;

        //Debug.Log("tamanhos: " + xI + " : " + xP + " :" + yI + " : " + yP);

        if (xI > xP || yI > yP)
        {
            painelDeTamanhoVariavel.localScale = new Vector3(tex.width / 50f, tex.height / 50f, 1);
            imgDoMapa.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Update()
    {
        float v = Mathf.Min(CommandReader.GetAxis("vertical", GlobalController.g.Control) + CommandReader.GetAxis("VDpad", GlobalController.g.Control),1);
        float h = Mathf.Min(CommandReader.GetAxis("horizontal", GlobalController.g.Control) + CommandReader.GetAxis("HDpad", GlobalController.g.Control), 1);

        sr.horizontalScrollbar.value += h*vel/painelDeTamanhoVariavel.localScale.x;
        sr.verticalScrollbar.value += v*vel / painelDeTamanhoVariavel.localScale.y;
    }

    public void OnUnpausedGame()
    {
        Debug.Log(imgDoMapa+" : "+imgDoMapa.sprite);

        if (imgDoMapa != null)
            if (imgDoMapa.sprite != null)
                if (imgDoMapa.sprite.name == "")
                {
                    MonoBehaviour.Destroy(imgDoMapa.mainTexture);
                    imgDoMapa.sprite = null;
                }
    }

    public void OnExitMapaPanel()
    {
        painelDeTamanhoVariavel.parent.parent.gameObject.SetActive(false);
    }
}


