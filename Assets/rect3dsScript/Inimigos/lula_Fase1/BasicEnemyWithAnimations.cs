using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyWithAnimations : EnemyBasic {

    private Animator A;
    [SerializeField] private string moveAnimationId;
    [SerializeField] private string idleAnimationId;
    [SerializeField] private AudioClip som;
    [SerializeField] private AudioClip[] damages; 

    new void Start()
    {
        A = GetComponent<Animator>();
        base.Start();
    }
    protected override void EsperandoMoveAnimation()
    {
        A.SetTrigger(idleAnimationId);
        if (som != null)
            EventAgregator.Publish(new StandardSendGameEvent(gameObject,EventKey.request3dSound, som));
    }

    protected override void MoveAnimation()
    {
        A.SetTrigger(moveAnimationId);
    }

    protected override void OnDefeated()
    {
        SomDoDanoFatal();
        base.OnDefeated();
        
    }

    
    protected void SomDoDanoFatal()
    {
        //yield return new WaitForSeconds(.2f);
            
        AudioClip a = damages[Random.Range(0, damages.Length)];

        EventAgregator.Publish(new StandardSendGameEvent(EventKey.disparaSom, a));

       // yield return base.SomDoDano();


    }
}
