using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudeCenaLocal : MonoBehaviour
{
    [SerializeField] private NomesCenas[] cenasAlvo = null;
    [SerializeField] private RestritorDeCamLimits restritor = null;

    void OnFadeOutComplete()
    {
        StaticMudeCena.OnFadeOutComplete(cenasAlvo, cenasAlvo[0], transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (UnicidadeDoPlayer.Verifique(collision))
            {
                GlobalController.g.FadeV.IniciarFadeOutComAction(OnFadeOutComplete, 0.5f);

                restritor.VerifiqueLimitantesParaMudeCena(cenasAlvo[0]);
            }

        }
    }
}
