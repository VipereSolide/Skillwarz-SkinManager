using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using FeatherLight.Pro;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class fetchByUrlWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_UrlField;
    [SerializeField] private RawImage m_TextureHolder;

    [Space()]

    [SerializeField] private CanvasGroup m_WarningPopup;
    [SerializeField] private TMP_Text m_WarningPopupContent;
    [SerializeField] private float m_WarningPopupFadeTime;
    [SerializeField] private float m_WarningPopupLive;

    private Texture2D m_HeldTexture;
    public Texture2D Texture { get { return m_HeldTexture; } }

    private void FetchTexture()
    {
        StartCoroutine(GetTextureByUrl(m_UrlField.text));
    }

    private IEnumerator GetTextureByUrl(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            string _error = "The given URL could communicate with the server but couldn't find the given image.";

            this.URLError(_error);
            Debug.LogError(_error);
            yield return false;
        }
        else if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            string _error = "The given URL could not connect to the server. Please provide a valid URL.";

            this.URLError(_error);
            Debug.LogError(_error);
            yield return false;
        }
        else if (webRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            string _error = "The processing of the data fetched by the URL are invalid.";
            
            this.URLError(_error);
            Debug.LogError(_error);
            yield return false;
        }

        byte[] _textureInBytes = webRequest.downloadHandler.data;
        Texture2D _texture = new Texture2D(2,2);

        _texture.LoadImage(_textureInBytes);
        _texture.Apply();

        SetTexture(_texture);
    }

    private void SetTexture(Texture2D _tex)
    {
        m_HeldTexture = _tex;
        m_TextureHolder.texture = _tex;
    }

    private void URLError(string error)
    {
        m_WarningPopupContent.text = error;
        CanvasGroupHelper.Fade(m_WarningPopup, true, m_WarningPopupFadeTime);

        Invoke(nameof(FadePopupOff), m_WarningPopupLive);
    }

    private void FadePopupOff()
    {
        CanvasGroupHelper.Fade(m_WarningPopup, false, m_WarningPopupFadeTime);
    }

    private void Start()
    {
        m_UrlField.onEndEdit.AddListener( delegate { FetchTexture(); } );
    }

    private void OnApplicationQuit()
    {
        m_UrlField.onEndEdit.RemoveListener( delegate { FetchTexture(); } );
    }
}