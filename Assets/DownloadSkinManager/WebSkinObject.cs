using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WebSkinObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RawImage texture = null;
    [SerializeField] private string link = "";

    public RawImage Texture { get { return texture; } }
    public string Link { get { return link; } }

    public void SetData(Texture2D tex, string link)
    {
        texture.texture = tex;

        float _crossProduct = (float)tex.width * (300.0f / (float)tex.height);
        texture.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
            _crossProduct,
            300
        );

        this.link = link;
    }

    public void OnPointerClick(PointerEventData data)
    {
        Application.OpenURL(link);
    }
}
