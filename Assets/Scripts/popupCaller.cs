using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class popupCaller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string m_PopupName;
    [SerializeField] private string m_PopupText;
    [SerializeField] private float m_PopupAppearWait;

    private float m_Timer = 0;
    private bool m_IsHighlighted = false;
    [HideInInspector] public bool m_HasCalledPopupManager = false;

    public bool IsHighlighted { get { return m_IsHighlighted; } }

    public void OnPointerEnter(PointerEventData data) { m_IsHighlighted = true; m_Timer = 0; m_HasCalledPopupManager = false; }
    public void OnPointerExit(PointerEventData data) { m_IsHighlighted = false; popupManager.Instance.EndPopup(); m_HasCalledPopupManager = false; }

    private void Update()
    {
        if (m_Timer >= m_PopupAppearWait)
        {
            if (m_HasCalledPopupManager)
                return;
            
            popupManager.Instance.CallPopup(m_PopupName, m_PopupText, this);
            m_HasCalledPopupManager = true;
            m_Timer = 0;
        }
        else if (m_IsHighlighted)
        {
            m_Timer += Time.deltaTime;
        }
    }
}