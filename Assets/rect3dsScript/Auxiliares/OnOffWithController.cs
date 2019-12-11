using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffWithController : MonoBehaviour
{
    [SerializeField] private bool ligadoCom = false;
    [SerializeField] private Controlador[] controladores = default(Controlador[]);

    void Start()
    {
        new MyInvokeMethod().InvokeAoFimDoQuadro(() =>
        {
            bool foi = false;

            for (int i = 0; i < controladores.Length; i++)
            {
                if (controladores[i] == GlobalController.g.Control)
                    foi = true;
            }


            if (foi)
                gameObject.SetActive(ligadoCom);
            else
                gameObject.SetActive(!ligadoCom);
        });
        
        
    }
}
