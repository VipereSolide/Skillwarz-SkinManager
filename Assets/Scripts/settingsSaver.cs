using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using Michsky.UI.ModernUIPack;

public class settingsSaver : MonoBehaviour
{
    public static settingsSaver main;

    [SerializeField] private framerateLocker m_FramerateLocker;
    [SerializeField] private CustomDropdown m_ResolutionCustomDropdown;
    [SerializeField] private ResolutionDropdown m_ResolutionDropdown;
    [SerializeField] private CustomDropdown m_gridCellSize;
    [SerializeField] private CustomDropdown m_customCursorDropdown;
    [SerializeField] private customCursor m_customCursor;

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
        PlayerPrefs.SetInt(nameof(m_ResolutionCustomDropdown), m_ResolutionCustomDropdown.selectedItemIndex);
        PlayerPrefs.SetInt(nameof(m_gridCellSize), m_gridCellSize.selectedItemIndex);
        PlayerPrefs.SetInt(nameof(m_customCursorDropdown), m_customCursorDropdown.selectedItemIndex);

        SaveActiveSkins();
    }

    private void LoadSettings()
    {
        m_FramerateLocker.SetLockedFPS(PlayerPrefs.GetInt(nameof(m_FramerateLocker)));

        // Load resolution.
        m_ResolutionCustomDropdown.selectedItemIndex = PlayerPrefs.GetInt(nameof(m_ResolutionCustomDropdown));
        m_ResolutionDropdown.UpdateResolution();
        m_ResolutionCustomDropdown.SetupDropdown();

        // Load Cursor
        m_customCursorDropdown.selectedItemIndex = PlayerPrefs.GetInt(nameof(m_customCursorDropdown));
        m_customCursor.SetActivity((m_customCursorDropdown.selectedItemIndex == 0));
        m_customCursorDropdown.SetupDropdown();

        // Load grid size
        m_gridCellSize.selectedItemIndex = PlayerPrefs.GetInt(nameof(m_gridCellSize));
        m_gridCellSize.dropdownItems[m_gridCellSize.selectedItemIndex].OnItemSelection.Invoke();
        m_gridCellSize.SetupDropdown();
    }

    public void SaveActiveSkins()
    {
        // Save the current enabled skins.
        skinObject[] _activeSkins = skinManager.main.ActiveSkins;
        string _activeSkinsInString = "";

        foreach (skinObject _obj in _activeSkins)
        {
            if (_obj == null)
                continue;

            _activeSkinsInString += _obj.transform.name + "###";
        }

        PlayerPrefs.SetString("ActiveSkins", _activeSkinsInString); // https://stackoverflow.com/questions/15933632/passing-array-to-function-that-takes-either-params-object-or-ienumerablet
    }

    public void LoadActiveSkins()
    {
        // Loads the current enabled skins.
        string[] _activeSkinsInString = PlayerPrefs.GetString("ActiveSkins").Split(new[] { "###" }, StringSplitOptions.None);
        skinManager.main.SetActiveSkins(_activeSkinsInString);
    }
}
