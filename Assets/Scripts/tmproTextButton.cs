using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class tmproTextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private FontStyles m_FontStyles;
    [SerializeField] private FontStyles m_NormalFontStyles;

    private TMP_Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        m_Text.fontStyle = m_FontStyles;
    }

    public void OnPointerExit(PointerEventData data)
    {
        m_Text.fontStyle = m_NormalFontStyles;
    }
}
