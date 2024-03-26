using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private RectTransform _rectTransform;

    public Item CurrentItem { get; private set; }

    private void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void ShowTooltip(Item item, RectTransform itemRectTransform, Vector2 offset)
    {
        gameObject.SetActive(true);
        mainText.text = item.Description;
        nameText.text = item.Name;

        _rectTransform.SetAsLastSibling();
        _rectTransform.position = itemRectTransform.position + (Vector3)offset;
        CurrentItem = item;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        CurrentItem = null;
    }
}
