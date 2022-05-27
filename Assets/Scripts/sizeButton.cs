using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class sizeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 m_HighlightedScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private Vector3 m_NormalScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float m_HighlightSpeed = 0.2f;

    private bool m_IsHighlighted = false;
    private Vector3 m_ScaleVelocity;

    public void OnPointerEnter(PointerEventData data)
    {
        m_IsHighlighted = true;
        StartCoroutine(Fade(true));
    }

    public void OnPointerExit(PointerEventData data)
    {
        m_IsHighlighted = false;
        StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool _Value)
    {
        float startTime = Time.time;
        Vector3 _oldScale = transform.localScale;
        Vector3 _finalValue = (_Value) ? m_HighlightedScale : m_NormalScale;

        while (Time.time < startTime + m_HighlightSpeed)
        {
            transform.localScale = Vector3.Lerp(_oldScale, _finalValue, (Time.time - startTime) / m_HighlightSpeed);
            yield return null;
        }

        transform.localScale = _finalValue;
    }
}
