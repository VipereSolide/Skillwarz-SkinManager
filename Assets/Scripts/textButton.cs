using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using System;
using System.Collections;
using System.Collections.Generic;

public class textButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private UnityEngine.Gradient underlineObjectColor;

    [SerializeField]
    private TMP_Text content;

    [SerializeField]
    private float underlineMoveSpeed;

    [Space()]

    [SerializeField]
    private underlineObject underlineObject;

    [SerializeField]
    private RectTransform underlinePositionObject;

    private bool isHighlighted = false;


    public void OnPointerEnter(PointerEventData data)
    {
        isHighlighted = true;
            StartCoroutine(Fade(new Color32(241,241,255,255)));
    }

    public void OnPointerExit(PointerEventData data)
    {
        isHighlighted = false;
            StartCoroutine(Fade(new Color(1,1,1,0.5f)));
    }

    public void OnPointerClick(PointerEventData data)
    {
        underlineObject.transform.SetParent(transform);
        underlineObject.StartMoving(underlinePositionObject, underlineMoveSpeed, underlineObjectColor);
    }

    private IEnumerator Fade(Color _ToColor)
    {
        float startTime = Time.time;
        Color32 _oldColor = content.color;

        while (Time.time < startTime + 0.1f)
        {
            content.color = Color32.Lerp(_oldColor, _ToColor, (Time.time - startTime) / 0.1f);
            yield return null;
        }

        content.color = _ToColor;
    }
}