using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Michsky.UI.ModernUIPack;

public class sliderOption : MonoBehaviour
{
    [SerializeField] private Slider m_OptionSlider;
    [SerializeField] private TMP_InputField m_ValueField;

    private float m_CurrentSliderValue;
    public float CurrentValue { get { return m_CurrentSliderValue; } }

    private void Start()
    {
        m_OptionSlider.onValueChanged.AddListener( delegate { OnSliderValueChanged(); } );
        m_ValueField.onEndEdit.AddListener( delegate { OnInputFieldValueChanged(); } );
    }

    private void OnApplicationQuit()
    {
        m_OptionSlider.onValueChanged.RemoveListener( delegate { OnSliderValueChanged(); } );
        m_ValueField.onEndEdit.RemoveListener( delegate { OnInputFieldValueChanged(); } );
    }

    public void UpdateValues()
    {
        m_OptionSlider.value = m_CurrentSliderValue;
        m_ValueField.text = (Mathf.Round(m_CurrentSliderValue * 10) / 10).ToString();
    }

    public void OnSliderValueChanged()
    {
        m_CurrentSliderValue = m_OptionSlider.value;
        UpdateValues();
    }

    public void OnInputFieldValueChanged()
    {
        float _value = 0;

        if (!string.IsNullOrEmpty(m_ValueField.text) && !string.IsNullOrWhiteSpace(m_ValueField.text) && m_ValueField.text != "-")
        {
            _value = float.Parse(m_ValueField.text);

            if (_value < m_OptionSlider.minValue)
                _value = m_OptionSlider.minValue;

            if (_value > m_OptionSlider.maxValue)
                _value = m_OptionSlider.maxValue;
        }

        m_CurrentSliderValue = _value;
        UpdateValues();
    }

    public void SetValue(float value)
    {
        m_OptionSlider.value = value;
        m_ValueField.text = value.ToString();

        m_CurrentSliderValue = value;
    }
}