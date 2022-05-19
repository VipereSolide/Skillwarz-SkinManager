using System.IO;
using System;
using System.Diagnostics;

using UnityEngine;

using TMPro;
using FeatherLight.Pro;
using Skillwarz.SkinManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_WarningDeleteFilesWindow;
    [SerializeField] private TMP_InputField m_SkillwarzPathField;
    [SerializeField] private string m_SkillwarzPath = "";

    private bool m_IsFirstTimeLaunching = false;

    private void Start()
    {
        // Updates the m_SkillwarzPath string based on the m_SkillwarzPathField input field.
        UpdateSkillwarzPath();

        // Attaches a listener to the input field so it updates the m_SkillwarzPath everytime we edit it.
        m_SkillwarzPathField.onEndEdit.AddListener( delegate { UpdateSkillwarzPath(); } );

        m_IsFirstTimeLaunching = ( PlayerPrefs.GetInt("m_IsFirstTimeLaunching") == 0 ) ? true : false;
    }

    private void OnApplicationQuit()
    {
        // Unattaches the listener attached above.
        m_SkillwarzPathField.onEndEdit.RemoveListener( delegate { UpdateSkillwarzPath(); } );

        PlayerPrefs.SetInt("m_IsFirstTimeLaunching", (!m_IsFirstTimeLaunching) ? 1 : 0);
    }

    public void UpdateSkillwarzPath()
    {
        m_SkillwarzPath = m_SkillwarzPathField.text;
    }

    // Public so it can be called by the Unity UI action system.
    public void LaunchSkillwarz()
    {
        if (m_IsFirstTimeLaunching)
        {
            CanvasGroupHelper.SetActive(m_WarningDeleteFilesWindow, true);

            m_IsFirstTimeLaunching = false;
            return;
        }

        // Moves all the skin files in the good folder.
        MoveFilesToWeaponSkins();
    }

    public void ResetFirstTimeLaunching()
    {
        m_IsFirstTimeLaunching = true;
    }

    private void MoveFilesToWeaponSkins()
    {
        // For every skinObjects present in the "Inventory" category.
        skinObject[] _activeSkins = skinManager.main.ActiveSkins;

        // Where the skin texture files should go.
        string _directoryName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/Skillwarz/WeaponSkins/";

        // If the directory doesn't exist, create it
        if (!Directory.Exists(_directoryName))
        {
            Directory.CreateDirectory(_directoryName);
        }

        // Clears everything in the WeaponSkins directory, so there's no old skin and name conflicts.
        string[] _filesInDir = Directory.GetFiles(_directoryName);
                
        foreach(string _file in _filesInDir)
        {
            File.Delete(_file);
        }

        // For every skinObjects in the Inventory category...
        foreach(skinObject _object in _activeSkins)
        {
            // If the object doesn't exist, go to the next object...
            if (_object == null)
                continue;

            // Class that contains all the data relative to the skins.
            SkinData _data = _object.SkinData;

            // If the object has no data attached to it, go to the next object...
            if (_data == null)
                continue;

            // For every textures path the skin has (7 as for now).
            for(int _textureIndex = 0; _textureIndex < _data.SkinTexturePaths.Length; _textureIndex++)
            {
                // If the current path doesn't exist, go to the next one...
                if (_data.SkinTexturePaths[_textureIndex] == null)
                    continue;

                string _textureName = SkinData.IntToTextureName(_textureIndex); // Turns an int (from 0 to 7) to a texture name like Albedo, Detail, Emissive and so on.
                string _textureSuffix = _textureName.ToCharArray()[0].ToString(); // The first letter of the name. So A for albedo, D for detail, E for emissive and so on.
                string _weapon = _data.Weapon.ToString(); // The weapon used by the skin (every weapons in the game, so AK47, USP45 and so on).
                string _totalName = _weapon + ((_textureSuffix == "a") ? "" : ("_" + _textureSuffix)).ToUpper(); // The full name of the weapon. It will be something like "AK47_E" or "AK47".
                string _texturePath = _data.SkinTexturePaths[_textureIndex]; // The full path of the texture.

                // If the texture was null, continue.
                if (IsTexturePathNull(_texturePath))
                    continue;

                // Puts all the textures in the WeaponSkins folder.
                File.Copy(_data.SkinTexturePaths[_textureIndex], _directoryName + _totalName + ".png");
            }
        }
    }

    // Returns true if the skin texture path name is "null" and false in any other case.
    private bool IsTexturePathNull(string _TexturePath)
    {
        string[] _pathParts = _TexturePath.Split('/');
        return (_pathParts[_pathParts.Length - 1] == "null");
    }
}