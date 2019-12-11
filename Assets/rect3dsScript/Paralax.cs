using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {

    [SerializeField]GameObject[] paralaxObjects;
    [SerializeField] DadosDeCena.LimitantesDaCena limitantes;

    Transform[][] paralaxLimits;
    Transform camera;

	// Use this for initialization
	void Start () {
        limitantes = SetarLimits();
        SetarParalaxLimits();
        camera = FindObjectOfType<Camera2D>().transform;
	}

    void SetarParalaxLimits()
    {
        paralaxLimits = new Transform[paralaxObjects.Length][];
        for (int i = 0; i < paralaxObjects.Length; i++)
        {
            Transform T1 = paralaxObjects[i].transform.Find("startParalaxLimits");
            Transform T2 = paralaxObjects[i].transform.Find("endParalaxLimits");

            if (T1 != null && T2 != null)
            {
                paralaxLimits[i] = new Transform[2] { T1, T2 };
            }
        }
    }

    public static DadosDeCena.LimitantesDaCena SetarLimits()
    {
        DadosDeCena.LimitantesDaCena limitantes = new DadosDeCena.LimitantesDaCena();
        GameObject G = GameObject.Find("startBlock");
        if (G != null)
        {
            limitantes.xMin = G.transform.position.x;
            limitantes.yMin = G.transform.position.y;
        }

        G = GameObject.Find("endBlock");
        if (G != null)
        {
            limitantes.xMax = G.transform.position.x;
            limitantes.yMax = G.transform.position.y;
        }

        return limitantes;
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < paralaxObjects.Length; i++)
        {
            if (paralaxLimits[i] != null)
            {

                Vector3 paraLimitsMin = paralaxObjects[i].transform.position - paralaxLimits[i][0].position;
                Vector3 paraLimitsMax = paralaxObjects[i].transform.position - paralaxLimits[i][1].position;

                Vector3 medPos = (paralaxLimits[i][1].position - paralaxLimits[i][0].position);
                float xMin = limitantes.xMin + paraLimitsMin.x;
                float xMax = limitantes.xMax + paraLimitsMax.x;
                float yMin = limitantes.yMin + paraLimitsMin.y;
                float yMax = limitantes.yMax + paraLimitsMax.y;



                paralaxObjects[i]. transform.position = new Vector3(
                    Mathf.Lerp(xMin, xMax, (camera.position.x - xMin) / (xMax - xMin)),
                    Mathf.Lerp(yMin, yMax, (camera.position.y - yMin) / (yMax - yMin)),
                    0);


            }
        }
	}
}
