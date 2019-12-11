using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UmaOpcaoDeUpdates : UmaOpcao
{
    #region inspector
    [SerializeField] private Image mini = null;
    [SerializeField] private Text txt = null;
    #endregion 


    public Image Mini { get { return mini; } private set { mini = value; } }

    public void SetarOpcao(string texto,Sprite img,System.Action<int> acao)
    {
        mini.sprite = img;
        txt.text = texto;
        Acao += acao;
    }


}