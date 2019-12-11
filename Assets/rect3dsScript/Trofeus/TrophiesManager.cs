using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophiesManager
{
    public static void VerifyTrophy(TrophyId id)
    {
#if UNITY_WEBGL
        GameJoltTrophyDictionary.VerifyTrophy(id);
#endif
#if UNITY_ANDROID
        GooglePlayTrophyDictionary.VerifyTrophy(id);
#endif

    }
}

public enum TrophyId
{
    coloqueEmblemaNaEspada,
    coleteUmFragmentoDeHexagono,
    coleteUmFragmentoDePentagono,
    coleteUmLosango,
    completeUmHexagono,
    completeUmPentagono,
    abraUmCofre,
    derroteGrandeCirculo,
    derroteMagoSetaSombria,
    encontreFinalNaGarganta,
    encontreFinalNoAquifero,
    todosOsTrofeus
}