using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class settingsSaver : MonoBehaviour
{
    public static settingsSaver main;

    [SerializeField] private framerateLocker m_FramerateLocker;
    [SerializeField] private TMP_InputField m_SkillwarzPathField;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        LoadSettings();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt(nameof(m_FramerateLocker), m_FramerateLocker.LockedFPS);
        PlayerPrefs.SetString(nameof(m_SkillwarzPathField), m_SkillwarzPathField.text);

        SaveActiveSkins();
    }

    private void LoadSettings()
    {
        m_FramerateLocker.SetLockedFPS(PlayerPrefs.GetInt(nameof(m_FramerateLocker)));
        m_SkillwarzPathField.text = PlayerPrefs.GetString(nameof(m_SkillwarzPathField));
    }

    public void SaveActiveSkins()
    {
        // Save the current enabled skins.
        skinObject[] _activeSkins = skinManager.main.ActiveSkins;
        string _activeSkinsInString = "";

        foreach(skinObject _obj in _activeSkins)
        {
            if(_obj == null)
                continue;
            
            _activeSkinsInString += _obj.transform.name + "###";
        }

        PlayerPrefs.SetString("ActiveSkins", _activeSkinsInString); // https://stackoverflow.com/questions/15933632/passing-array-to-function-that-takes-either-params-object-or-ienumerablet
    }

    public void LoadActiveSkins()
    {
        // Loads the current enabled skins.
        string[] _activeSkinsInString = PlayerPrefs.GetString("ActiveSkins").Split(new []{"###"}, StringSplitOptions.None);
        skinManager.main.SetActiveSkins(_activeSkinsInString);
    }
}
