using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class sizeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 m_HighlightedScale = new Vector3(1.1f,1.1f,1.1f);
    [SerializeField] private Vector3 m_NormalScale = new Vector3(1f,1f,1f);
    [SerializeField] private float m_HighlightSpeed = 0.2f;

    private bool m_IsHighlighted = false;
    private Vector3 m_ScaleVelocity;

    public void OnPointerEnter(PointerEventData data)
    {
        m_IsHighlighted = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        m_IsHighlighted = false;
    }

    private void Update()
    {
        if (m_IsHighlighted)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, m_HighlightedScale, ref m_ScaleVelocity, m_HighlightSpeed);
        }
        else
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, m_NormalScale, ref m_ScaleVelocity, m_HighlightSpeed);
        }
    }
}
