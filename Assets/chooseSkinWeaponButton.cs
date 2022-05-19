using UnityEngine;
using FeatherLight.Pro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class chooseSkinWeaponButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CanvasGroup m_HoverCanvasGroup;
    [SerializeField] private Transform m_ProfilePictureTransform;

    [SerializeField] private float m_HoverProfilePictureSize = 1.25f;
    [SerializeField] private float m_HoverTransformSize = 1.1f;
    [SerializeField] private float m_HoverAlphaTime = 0.15f;
    [SerializeField] private float m_HoverSizeSpeed = 10f;

    private bool isHover = false;

    public void OnPointerEnter(PointerEventData data) { HoverIn(); }
    public void OnPointerExit(PointerEventData data) { HoverOut(); }

    private void HoverIn()
    {
        isHover = true;
        
        StartCoroutine(CanvasGroupHelper.Fade(m_HoverCanvasGroup, isHover, m_HoverAlphaTime));
    }

    private void HoverOut()
    {
        isHover = false;

        StartCoroutine(CanvasGroupHelper.Fade(m_HoverCanvasGroup, isHover, m_HoverAlphaTime));
    }

    private void Update()
    {
        if (isHover)
        {
            m_ProfilePictureTransform.localScale = Vector3.Lerp(m_ProfilePictureTransform.localScale, Vector3.one * m_HoverProfilePictureSize, Time.deltaTime * m_HoverSizeSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * m_HoverTransformSize, Time.deltaTime * m_HoverSizeSpeed);
        }
        else
        {
            m_ProfilePictureTransform.localScale = Vector3.Lerp(m_ProfilePictureTransform.localScale, Vector3.one, Time.deltaTime * m_HoverSizeSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * m_HoverSizeSpeed);
        }
    }
}
