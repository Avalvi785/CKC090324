using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDraggedIcon : MonoBehaviour
{
    public float maxDistance = 10f;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public float duration = 1f;

    void Start()
    {
        // Calculate the initial scale based on the initial distance from the origin (0, 0, 0)
        float initialDistance = Vector3.Distance(transform.position, Vector3.zero);
        float initialScale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(0, maxDistance, initialDistance));

        // Set the initial scale
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);

        // Tween the scale using DOTween
        transform.DOScale(maxScale, duration)
            .From(minScale)
            .SetEase(Ease.OutQuad);
    }
}
