using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMoveRigidbody : TargedEnemy
{

    #region inspector
    [SerializeField] private Transform andador = null;
    [SerializeField] private MovimentacaoBasica mov = null;
    #endregion

    [SerializeField] private float INTERVALO_ENTRE_INVESTIDAS = 2.5f;
    [SerializeField] private float STANDARD_SPEED = 3;
    [SerializeField] private float DISTANCIA_DE_ACELERACAO = 7;
    [SerializeField] private float TEMPO_ESPERANDO = 0.5F;

    private float tempoDecorrido = 0;
    private float ultimaAcelerada = 0;
    private Vector3 posAnt = Vector3.zero;

    public float TempoEsperando { get { return TEMPO_ESPERANDO; } set { TEMPO_ESPERANDO = value; } }
    public float TempoDecorrido { get { return tempoDecorrido; } set { tempoDecorrido = value; } }
    public float StandardSpeed { get { return STANDARD_SPEED; } set { STANDARD_SPEED = value; } }
    public float UltimaAcelerada { get { return ultimaAcelerada; } set {ultimaAcelerada=value; } }
    public MovimentacaoBasica Mov { get { return mov; } }
    public Transform Andador { get { return andador; } }
    public Vector3 PosAnterior { get { return posAnt; } set { posAnt = value; } } 

    protected abstract void OnActionRequest();
    protected abstract void OnTargetCheck();

    protected override void Start()
    {
        mov.Iniciar(andador);
        mov.ChangeSpeed(StandardSpeed);
        UltimaAcelerada = -INTERVALO_ENTRE_INVESTIDAS;

        base.Start();
    }

    protected void UpdateMovendo()
    {
        mov.AplicadorDeMovimentos(
                    Vector3.ProjectOnPlane(MovePoints[MoveTarget].position - transform.position, Vector3.up).normalized);

        if (Mathf.Abs(MovePoints[MoveTarget].position.x - transform.position.x) < 0.1f)
        {
            TempoDecorrido = 0;
            OnTargetCheck();//estado = EstadoDaqui.esperandoMove;
            TrocaMoveTarget();
        }

        if (Vector2.Distance(GameController.g.Manager.transform.position, transform.position) < DISTANCIA_DE_ACELERACAO
            &&
            Time.time - UltimaAcelerada > INTERVALO_ENTRE_INVESTIDAS
            )
        {
            OnActionRequest();//PrepararInvestida();
        }
    }

    protected void TestadorDePosicaoBase()
    {
        if (Mathf.Abs(transform.position.x - PosAnterior.x) == 0)
        {
            Mov.Pulo();
        }

        PosAnterior = transform.position;
    }

    protected void PositionChangeWithAndador()
    {
        PositionWithAndador(andador,transform);
    }

    public static void PositionWithAndador(Transform andador, Transform transform)
    {
        transform.position = andador.position;
        Vector3 t = transform.localScale;
        transform.localScale = new Vector3(-1 * Mathf.Abs(t.x) * Mathf.Sign(andador.localScale.x), t.y, t.z);
    }
}
