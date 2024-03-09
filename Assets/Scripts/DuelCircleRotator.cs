using DG.Tweening;
using UnityEngine;
using static CKC.Constants.CKCConstants;

public class DuelCircleRotator : MonoBehaviour
{
    public GameObject OuterCircle;
    public GameObject InnerCircle;
    public CircleRotateSides RotateSideForOuterCircle;
    public CircleRotateSides RotateSideForInnerCircle;
    public float Duration;
    private float rotateZAngleForOuterCircle;
    private float rotateZAngleForInnerrCircle;
    

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
        switch (RotateSideForOuterCircle)
        {
            case CircleRotateSides.Left:
                rotateZAngleForOuterCircle = 360;
                break;
            case CircleRotateSides.Rght:
                rotateZAngleForOuterCircle = -360;
                break;
        }

        switch (RotateSideForInnerCircle)
        {
            case CircleRotateSides.Left:
                rotateZAngleForInnerrCircle = 360;
                break;
            case CircleRotateSides.Rght:
                rotateZAngleForInnerrCircle = -360;
                break;
        }

        RotateCircle(OuterCircle, rotateZAngleForOuterCircle);
        RotateCircle(InnerCircle, rotateZAngleForInnerrCircle);
    }
}
