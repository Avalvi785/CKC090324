using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CKC.Constants.CKCConstants;

public class SingleCircleRotator : MonoBehaviour
{
    public CircleRotateSides CircleRotateSide;
    public float Duration;
    private float rotateZAngleForCircle;

    void Start()
    {
        RotateCircles();
    }

    private void RotateCircle(GameObject objectToRotat, float rotationAngle)
    {
        objectToRotat.transform.DOLocalRotate(new Vector3(0, 0, rotationAngle), Duration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);
    }

    private void RotateCircles()
    {
        switch (CircleRotateSide)
        {
            case CircleRotateSides.Left:
                rotateZAngleForCircle = 360;
                break;
            case CircleRotateSides.Rght:
                rotateZAngleForCircle = -360;
                break;
        } 

        RotateCircle(gameObject, rotateZAngleForCircle);       
    }
}
