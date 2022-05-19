using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using FeatherLight.Pro;

public class AdvancedColorPicker : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_Background;
    [SerializeField] private float m_BackgroundFadeTime = 0.2f;

    [Space()]

    [SerializeField] private ColorPicker m_ColorPicker;
    [SerializeField] private TMP_InputField m_ColorHandler_Red;
    [SerializeField] private TMP_InputField m_ColorHandler_Green;
    [SerializeField] private TMP_InputField m_ColorHandler_Blue;
    [SerializeField] private TMP_InputField m_ColorHandler_Alpha;
    
    [Space()]

    [SerializeField] private Image m_ColorImage;
    [SerializeField] private Slider m_AlphaSlider;

    private Color m_Color;
    private Color m_StartColor;
    
    [HideInInspector] public colorPickerObject m_ActiveAsker;

    public void SetActive(bool value, colorPickerObject asker)
    {
        if (value)
        {
            // Updates the Asker variable.
            m_ActiveAsker = asker;
        }
        {
            // Sets the color of the Asker.
            m_ActiveAsker.SetColor(m_Color);
        }

        StartCoroutine(CanvasGroupHelper.Fade(m_Background, value, m_BackgroundFadeTime));        
    }

    public void DisableAndSendColor()
    {
        SetActive(false, null);
    }

    // Disables without sending it's color.
    public void Disable()
    {
        StartCoroutine(CanvasGroupHelper.Fade(m_Background, false, m_BackgroundFadeTime));
        m_ActiveAsker = null;
    }

    // Changes the color before it's appearing.
    public void SetStartColor(Color _color)
    {
        m_StartColor = _color;
        m_Color = m_StartColor;
        UpdateColor();
    }

    // When changing the color in the ColorPicker.
    private void OnColorChange(Color _color)
    {
        this.m_Color = new Color(_color.r, _color.g, _color.b, 1);
        UpdateColor();
    }

    public void UpdateColorFromInputField()
    {
        byte _red = GetByteFromText(m_ColorHandler_Red.text);
        byte _green = GetByteFromText(m_ColorHandler_Green.text);
        byte _blue = GetByteFromText(m_ColorHandler_Blue.text);
        byte _alpha = GetByteFromText(m_ColorHandler_Alpha.text);

        m_Color = new Color32(_red,_green,_blue,_alpha);
        
        UpdateColor();
    }

    private byte GetByteFromText(string _value)
    {
        byte _output = (byte)0;

        if (!string.IsNullOrEmpty(_value) && !string.IsNullOrEmpty(_value))
        {
            int _outputInt = int.Parse(_value);

            if ( _outputInt > 255 )
            {
                _output = (byte)255;
            }
            else if ( _outputInt >= 0 )
            {
                _output = (byte)_outputInt;
            }
        }

        return _output;
    }

    // Updates all the color fields.
    private void UpdateColor()
    {
        Color32 _col = m_Color;

        m_ColorHandler_Red.text = _col.r.ToString();
        m_ColorHandler_Green.text = _col.g.ToString();
        m_ColorHandler_Blue.text = _col.b.ToString();
        m_ColorHandler_Alpha.text = _col.a.ToString();

        m_AlphaSlider.value = int.Parse(m_ColorHandler_Alpha.text);
        m_ColorImage.color = m_Color;
    }

    private void Start()
    {
        m_ColorPicker.onColorChanged += OnColorChange;
    }

    private void OnApplicationQuit()
    {
        m_ColorPicker.onColorChanged -= OnColorChange;
    }
}
