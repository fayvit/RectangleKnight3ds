﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargedEnemy : NonRespawnOnLoadEnemy
{

#pragma warning disable 0649
    [SerializeField] private Transform[] movePoints;
#pragma warning restore 0649

    private int moveTarget=0;

    public Vector3 PreviousMoveTarget { get; set; }
    public int MoveTarget { get{ return moveTarget; } set { moveTarget = value; } }
    public Transform[] MovePoints { get { return movePoints; } set { movePoints = value; } }

    protected void TrocaMoveTarget()
    {
        if (MovePoints.Length > 0)
            PreviousMoveTarget = MovePoints[MoveTarget].position;

        if (MovePoints.Length > MoveTarget + 1)
            MoveTarget++;
        else
            MoveTarget = 0;
    }
}
