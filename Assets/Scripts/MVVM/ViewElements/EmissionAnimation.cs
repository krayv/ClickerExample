using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmissionAnimation : MonoBehaviour
{
    [SerializeField] private float _cycleLength = 25f;
    [SerializeField] private float _pulseCycleLength = 6.5f;
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360f), _cycleLength, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).Play();
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), _pulseCycleLength).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).Play();
    }
}
