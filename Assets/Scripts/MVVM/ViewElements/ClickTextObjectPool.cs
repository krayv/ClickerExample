using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Zenject;
using System.Numerics;
using DG.Tweening;
using System;
using UniRx.Toolkit;
using UnityEngine.Pool;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEditor.Progress;
using System.Text;

public class ClickTextObjectPool : MonoBehaviour
{
    private IObjectPool<TextMeshProUGUI> _clickTextObjectPool;
    [SerializeField] private TextMeshProUGUI _clickTextPrefab;
    [SerializeField] private float _textLifeTime = 2f;
    [SerializeField] private float _textYMovement = 350f;

    private BigInteger lastClickValue = 1;

    private CookiesViewModel _cookiesViewModel;

    [Inject]
    private void Construct(CookiesViewModel cookiesViewModel)
    {
        _cookiesViewModel = cookiesViewModel;

        _clickTextObjectPool = new UnityEngine.Pool.ObjectPool<TextMeshProUGUI>(CreatePoolItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, false, 10, 40);
        _cookiesViewModel.OnClickCookie += _ =>
        {
            lastClickValue = _;
            var item = _clickTextObjectPool.Get();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(item.transform.DOMoveY(item.transform.position.y + _textYMovement, _textLifeTime));
            sequence.Join(item.DOColor(new Color(1f, 1f, 1f, 0f), _textLifeTime).SetEase(Ease.InCubic));
            sequence.OnComplete(() => _clickTextObjectPool.Release(item));
            sequence.Play();
        };
    }

    private TextMeshProUGUI CreatePoolItem()
    {
        var text = GameObject.Instantiate(_clickTextPrefab);
        text.transform.SetParent(transform);
        return text;
    }

    private void OnTakeFromPool(TextMeshProUGUI item)
    {
        item.gameObject.SetActive(true);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("+");
        stringBuilder.Append(lastClickValue.ToString());
        item.text = stringBuilder.ToString();
        item.transform.position = Input.mousePosition;
    }


    private void OnReturnToPool(TextMeshProUGUI item)
    {
        item.color = Color.white;
        item.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(TextMeshProUGUI item)
    {
        GameObject.Destroy(item.gameObject);
    }
}
