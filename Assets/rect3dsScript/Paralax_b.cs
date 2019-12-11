using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax_b : MonoBehaviour {

    [SerializeField] private float vel;
    [SerializeField] private GameObject paralaxContainer;
    private Transform cameraPlayer;
    private float length, startPosition;

    private Transform eParalax;
    private Transform sParalax;

    private DadosDeCena.LimitantesDaCena l;

    // Use this for initialization
    void Start () {
        Transform T = Instantiate(paralaxContainer).transform;
        T.parent = transform;
        T.localPosition = T.localPosition = new Vector3(1, paralaxContainer.transform.localPosition.y, 0);
        T.localScale = paralaxContainer.transform.localScale;

        T = Instantiate(paralaxContainer).transform;
        T.parent = transform;
        T.localPosition = T.localPosition = new Vector3(-1, paralaxContainer.transform.localPosition.y, 0);
        T.localScale = paralaxContainer.transform.localScale;

        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cameraPlayer = FindObjectOfType<Camera2D>().transform;

        eParalax = transform.Find("eParalax");
        sParalax = transform.Find("sParalax");

        l = Paralax.SetarLimits();
        Debug.Log(length);
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 paraLimitsMin = transform.position - sParalax.position;
        Vector3 paraLimitsMax = transform.position - eParalax.position;

        float medPos = (eParalax.position.y - sParalax.position.y);
        float yMin = l.yMin + paraLimitsMin.y;
        float yMax = l.yMax + paraLimitsMax.y;
        

        float temp = cameraPlayer.position.x * (1-vel);
        float dist = cameraPlayer.position.x * vel;

        transform.position = new Vector3(startPosition + dist,
            Mathf.Lerp(yMin, yMax, (cameraPlayer.position.y - yMin) / (yMax - yMin)),
             transform.position.z);

        if (temp > startPosition + 0.5f * length)
        {
            startPosition += length;
        }
        else if (temp < startPosition - 0.5f * length)
        {
            startPosition -= length;
        }

    }
}
