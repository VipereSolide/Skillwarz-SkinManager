using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using FeatherLight.Pro;

public class popupManager : MonoBehaviour
{
    /*
    PLAN:
    */

    public static popupManager Instance;

    [SerializeField] private CanvasGroup m_PopupCanvasGroup;
    [SerializeField] private TMP_Text m_PopupHeaderTitle;
    [SerializeField] private TMP_Text m_PopupDescription;

    [Space()]

    [SerializeField] private float m_PopupFadeTime;

    [Space()]

    [SerializeField] private float m_PopupFollowSpeed;
    [SerializeField] private bool m_IsLerped = false;

    private popupCaller m_CurrentCaller;
    private Vector3 m_LerpNextPosition;

    private void Awake()
    {
        Instance = this;
    }

    public void CallPopup(string _PopupName, string _PopupDescription, popupCaller _Caller)
    {
        FadePopup(true);

        Vector2 _halfScale = m_PopupCanvasGroup.GetComponent<RectTransform>().sizeDelta / 2;
        Vector3 _nextPosition = Input.mousePosition + new Vector3(_halfScale.x, -_halfScale.y, 0);

        MovePopup(_nextPosition);

        m_PopupHeaderTitle.text = _PopupName;
        m_PopupDescription.text = _PopupDescription;

        m_CurrentCaller = _Caller;
    }
    public void EndPopup()
    {
        m_CurrentCaller = null;

        if (m_PopupCanvasGroup.alpha > 0) FadePopup(false);
    }

    private void FadePopup(bool _Value)
    {
        StartCoroutine(CanvasGroupHelper.Fade(m_PopupCanvasGroup, _Value, m_PopupFadeTime));
    }

    private void UpdatePopupPosition(bool _IsLerped)
    {
        Vector2 _halfScale = m_PopupCanvasGroup.GetComponent<RectTransform>().sizeDelta / 2;
        Vector3 _nextPosition = Input.mousePosition + new Vector3(_halfScale.x, -_halfScale.y, 0);

        if (_IsLerped)
        {
            m_LerpNextPosition = _nextPosition;
            return;
        }

        MovePopup(_nextPosition);
    }

    private void MovePopup(Vector3 _Target)
    {
        m_PopupCanvasGroup.transform.position = _Target;
    }

    private void Update()
    {
        if (m_CurrentCaller != null)
        {
            if (m_IsLerped)
            {
                m_PopupCanvasGroup.transform.position = Vector3.Lerp(m_PopupCanvasGroup.transform.position, m_LerpNextPosition, Time.deltaTime * m_PopupFollowSpeed);
            }

            UpdatePopupPosition(m_IsLerped);

            if (!m_CurrentCaller.IsHighlighted)
            {
                m_CurrentCaller.m_HasCalledPopupManager = false;
                
                EndPopup();
                FadePopup(false);
            }
        }
    }
}
