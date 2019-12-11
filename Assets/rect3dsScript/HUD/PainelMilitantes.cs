using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelMilitantes : PainelPentagonoHexagono {

    [SerializeField] private Image[] miniMilitantes;

    public override void ModificarSprites(int quantas)
    {
        for (int i = 0; i < miniMilitantes.Length; i++)
        {
            if (i < quantas)
            {
                miniMilitantes[i].sprite = LabelImages[1];
            }else
                miniMilitantes[i].sprite = LabelImages[0];
        }
    }
}
