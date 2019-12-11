using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyTrigger : MonoBehaviour
{
    [SerializeField] private TrophyId tId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrophiesManager.VerifyTrophy(tId);
    }
}
