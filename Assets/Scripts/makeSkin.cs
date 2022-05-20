using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;

using Michsky.UI.ModernUIPack;
using Skillwarz.SkinManager;
using FeatherLight.Pro;
using TMPro;
using SFB;

using UnityEngine.UI;
using UnityEngine;

public class makeSkin : MonoBehaviour
{
    [Header("Step 1")]
    [SerializeField] private TMP_InputField m_SkinNameInputField;
    [SerializeField] private TMP_InputField m_SkinDescriptionInputField;
    [SerializeField] private TMP_Text m_SkinWeaponDropDown;

    [Header("Step 2")]

    [SerializeField] private textureHolder m_SkinProfilePictureHolder;
    [SerializeField] private textureHolder[] m_SkinTexturesHolders;

    [Header("Step 3")]
    [SerializeField] private colorPickerObject m_AlbedoColorPicker;
    [SerializeField] private colorPickerObject m_EmissionColorPicker;
    [SerializeField] private sliderOption m_EmissionIntensitySliderOption;
    [SerializeField] private CustomDropdown m_EmissionTypeDropdown;
    [SerializeField] private sliderOption m_HeightIntensitySliderOption;
    [SerializeField] private sliderOption m_MetallicIntensitySliderOption;
    [SerializeField] private sliderOption m_NormalIntensitySliderOption;
    [SerializeField] private sliderOption m_OcclusionIntensitySliderOption;

    [Header("Step 4")]
    
    [SerializeField] private TMP_Text m_PreviewText;

    private SkinData m_CurrentSkinData = null;

    private SkinData GetSkinData()
    {
        string _skinName = m_SkinNameInputField.text;
        string _skinDescription = m_SkinDescriptionInputField.text;
        Texture2D _skinProfilePicture = m_SkinProfilePictureHolder.Texture;
        
        Texture2D[] _skinTextures = new Texture2D[7]
        {
            m_SkinTexturesHolders[0].Texture,
            m_SkinTexturesHolders[1].Texture,
            m_SkinTexturesHolders[2].Texture,
            m_SkinTexturesHolders[3].Texture,
            m_SkinTexturesHolders[4].Texture,
            m_SkinTexturesHolders[5].Texture,
            m_SkinTexturesHolders[6].Texture
        };

        bool _IsEnabled = true;
        WeaponType _skinWeaponType = (WeaponType)System.Enum.Parse(typeof(WeaponType), m_SkinWeaponDropDown.text);

        Color _skinAlbedoColor = m_AlbedoColorPicker.CurrentColor;
        Color _skinEmissionColor = m_EmissionColorPicker.CurrentColor;
        float _skinEmissionIntensity = m_EmissionIntensitySliderOption.CurrentValue;
        EmissionType _skinEmissionType = (EmissionType)System.Enum.Parse(typeof(EmissionType), m_EmissionTypeDropdown.selectedText.text.Replace(" ",""));
        float _skinMetallicIntensity = m_MetallicIntensitySliderOption.CurrentValue;
        float _skinNormalIntensity = m_NormalIntensitySliderOption.CurrentValue;
        float _skinHeightIntensity = m_HeightIntensitySliderOption.CurrentValue;
        float _skinOcclusionIntensity = m_OcclusionIntensitySliderOption.CurrentValue;

        SkinDataProperties _skinProperties = new SkinDataProperties(_skinAlbedoColor, _skinEmissionColor, _skinEmissionIntensity, _skinEmissionType, _skinMetallicIntensity, _skinNormalIntensity, _skinHeightIntensity, _skinOcclusionIntensity);
        SkinData _data = new SkinData(_skinName, _skinDescription, _skinProfilePicture, _IsEnabled, _skinTextures, _skinProperties, _skinWeaponType);

        return _data;
    }

    private string GetSkinDataInText()
    {
        m_CurrentSkinData = GetSkinData();

        string _path = "###MyDocuments###/Skillwarz/MySkins/";
        string _fileName = m_SkinNameInputField.text + "_profile";
        string _skinDataInString = SkinDataWriter.GetSkinDataInText(m_CurrentSkinData, _path, _fileName);

        // Fixing the Description Multiline Error
        string _fixedDescription = ReplaceBreakCharacters(_skinDataInString).Replace("\n","\\n");
        m_CurrentSkinData.SetDescription(_fixedDescription);

        // Refresh the skin data in string
        _skinDataInString = SkinDataWriter.GetSkinDataInText(m_CurrentSkinData, _path, _fileName);

        return _skinDataInString;
    }

    private string ReplaceBreakCharacters(string _content)
    {
        char[] _chars = _content.ToCharArray();

        int _charIndex = 0;

        int _startChar = 0;
        int _endChar = 0;

        for (int i = 0; i < _chars.Length; i++)
        {
            if (_charIndex == 1)
            {
                _startChar = i;
            }
            if (_charIndex == 4)
            {
                _endChar = i;
                break;
            }

            if (_chars[i] == '"' && _charIndex < 5)
            {
                _charIndex++;
            }
        }

        string _word = "";

        for (int c = 0; c < _chars.Length; c++)
        {
            if (c >= _startChar && c <= _endChar)
            {
                _word += _chars[c].ToString();
            }
        }

        _word = _word.Split('"')[2];

        return _word;
    }

    public void SetPreviewText()
    {
        m_PreviewText.text = GetSkinDataInText();
    }

    public void CreateSkinData()
    {
        if (m_CurrentSkinData == null)
            m_CurrentSkinData = GetSkinData();

        if (m_CurrentSkinData == null)
            return;

        string _ppath = "###MyDocuments###/Skillwarz/MySkins/";
        string _fileName = m_SkinNameInputField.text + "_profile";
        Debug.Log(_ppath);
        SkinDataWriter.WriteSkinData(m_CurrentSkinData, _ppath, _fileName);
    }
}
