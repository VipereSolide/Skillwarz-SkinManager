using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using FeatherLight.Pro;

public class colorPickerObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color m_CurrentColor;
    [SerializeField] private Image m_ColorImage;
    [SerializeField] private Slider m_ColorAlphaSlider;
    [SerializeField] private AdvancedColorPicker m_ColorPicker;

    public Color CurrentColor { get { return m_CurrentColor; } }

    private void Start()
    {
        m_ColorImage.color = m_CurrentColor;
    }

    public void SetColor(Color _color)
    {
        m_CurrentColor = _color;

        UpdateColor();
    }

    public void UpdateColor()
    {
        m_ColorImage.color = m_CurrentColor;

        Color32 _currentColorIn32 = m_CurrentColor;
        m_ColorAlphaSlider.value = (int)_currentColorIn32.a;
    }

    public void OnPointerClick(PointerEventData data)
    {
        m_ColorPicker.m_ActiveAsker = this;
        m_ColorPicker.SetStartColor(m_CurrentColor);
        m_ColorPicker.SetActive(true, this);
    }

}
