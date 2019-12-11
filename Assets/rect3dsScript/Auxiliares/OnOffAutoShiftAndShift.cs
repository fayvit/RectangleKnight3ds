using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffAutoShiftAndShift : MonoBehaviour
{
    #region inspector
    [SerializeField] private string autoShiftID = "";
    [SerializeField] private KeyShift shift = default(KeyShift);
    [SerializeField] private bool autoShiftDisableWith = false;
    [SerializeField] private bool shiftDisableWith = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        KeyVar mykeys = GameController.g.MyKeys;

        if (mykeys.VerificaAutoShift(autoShiftID) == autoShiftDisableWith 
            && 
            mykeys.VerificaAutoShift(shift) == shiftDisableWith)
            Destroy(gameObject);
    }

    
}
