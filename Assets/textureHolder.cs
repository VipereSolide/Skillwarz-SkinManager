using UnityEngine.UI;
using UnityEngine;

using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

using FeatherLight.Pro;
using TMPro;
using SFB;

public class textureHolder : MonoBehaviour
{
    [SerializeField] private RawImage m_CurrentTextureImage;

    [Space()]

    [SerializeField] private CanvasGroup m_FetchByUrlWindow;
    [SerializeField] private float m_FetchByUrlWindowAlphaTime;
    [SerializeField] private RawImage m_FetchByUrlImage;

    [Space()]

    [SerializeField] private Environment.SpecialFolder m_DiskFetchFolder;
    [SerializeField] private bool m_RememberLastPathLocation;

    private Texture2D m_HeldTexture;
    public Texture2D Texture { get { return m_HeldTexture; } }

    private string m_LastFileLocation = "";

    private void Start()
    {
        LoadDiskLocation();
    }

    private void OnApplicationQuit()
    {
        SaveDiskLocation();
    }

    public void SetFetchByUrlWindowActive(bool value)
    {
        StartCoroutine(CanvasGroupHelper.Fade(m_FetchByUrlWindow, value, m_FetchByUrlWindowAlphaTime));
    }

    private void SaveDiskLocation()
    {
        PlayerPrefs.SetString(nameof(m_LastFileLocation), m_LastFileLocation);
    }

    private void LoadDiskLocation()
    {
        string _location = PlayerPrefs.GetString(nameof(m_LastFileLocation));

        if (_location != null)
            m_LastFileLocation = _location;
        else
            m_LastFileLocation = Environment.GetFolderPath(m_DiskFetchFolder);
    }

    public void TrashTexture()
    {
        UpdateCurrentTexture(null);
    }

    public void FetchTextureOnDisk()
    {
        string[] _paths = SFB.StandaloneFileBrowser.OpenFilePanel("Select a texture...", m_LastFileLocation, "png", false);
        string _currentPath = _paths[0];

        m_LastFileLocation = System.IO.Directory.GetDirectoryRoot(_currentPath);

        byte[] _textureInBytes = File.ReadAllBytes(_currentPath);
        Texture2D _texture = new Texture2D(2,2);

        _texture.LoadImage(_textureInBytes);
        _texture.Apply();

        UpdateCurrentTexture(_texture);
    }

    public void FetchTextureByURL()
    {
        Texture2D _texture = (Texture2D)m_FetchByUrlImage.texture;
        
        UpdateCurrentTexture(_texture);
    }

    private void UpdateCurrentTexture(Texture2D _newTexture)
    {
        m_HeldTexture = _newTexture;
        m_CurrentTextureImage.texture = m_HeldTexture;
    }

    public void UpdateCurrentTexture()
    {
        m_CurrentTextureImage.texture = m_HeldTexture;
    }
}