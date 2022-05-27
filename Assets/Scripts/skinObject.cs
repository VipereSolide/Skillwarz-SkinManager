using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;
using Skillwarz.SkinManager;
using FeatherLight.Pro;

public class skinObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private SkinData m_skinData;
    public SkinData SkinData { get { return m_skinData; } }

    [Header("References")]

    [SerializeField]
    private TMP_Text skinObjectText = null;

    [SerializeField]
    private Image skinObjectImage = null;

    [Space()]

    [SerializeField]
    private CanvasGroup activeSkinGroup = null;

    [SerializeField]
    private CanvasGroup activeSkinGroupChecked = null;

    [Header("Values")]
    [SerializeField] private bool m_isDefaultSkin;

    [SerializeField]
    private float activeSkinToggleSpeed = 0.15f;

    [SerializeField]
    private Vector3 highlightedSize = new Vector3(1.1f, 1.1f, 1.1f);

    [SerializeField]
    private float highlightedSizeSpeed = 0.15f;

    private bool isHighlighted = false;

    private float activeSkinGroupVel = 0.0f;
    private Vector3 highlightedSizeVel = new Vector3(0.0f, 0.0f, 0.0f);

    private bool isSkinActive = false;

    public bool IsEnabled { get { return isSkinActive; } }
    public bool IsDefaultSkin { get { return m_isDefaultSkin; } }

    public void OnPointerEnter(PointerEventData data)
    {
        isHighlighted = true;

        if (transform.localScale != highlightedSize && activeSkinGroup.alpha != 1)
        {
            StartCoroutine(Fade(true));
            StartCoroutine(CanvasGroupHelper.Fade(activeSkinGroup, true, activeSkinToggleSpeed));
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        isHighlighted = false;

        if (!isSkinActive)
        {
            StartCoroutine(Fade(false));
            StartCoroutine(CanvasGroupHelper.Fade(activeSkinGroup, false, activeSkinToggleSpeed));
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (IsDefaultSkin && isSkinActive)
            return;

        if (skinManager.main.ActiveSkins[SkinData.WeaponTypeToInt(this.SkinData.Weapon)] == this)
            skinManager.main.DisableWeapon(this);
        else
            skinManager.main.SetWeaponActive(this);
    }

    public void ToggleActivity(bool value)
    {
        isSkinActive = value;
        activeSkinGroupChecked.alpha = (isSkinActive) ? 1 : 0;

        StartCoroutine(Fade(isSkinActive));
        StartCoroutine(CanvasGroupHelper.Fade(activeSkinGroup, isSkinActive, activeSkinToggleSpeed));
    }

    /*private void Update()
    {
        if (isHighlighted || isSkinActive)
        {
            activeSkinGroup.alpha = Mathf.SmoothDamp(activeSkinGroup.alpha, 1, ref activeSkinGroupVel, activeSkinToggleSpeed);
            transform.localScale = Vector3.SmoothDamp(transform.localScale, highlightedSize, ref highlightedSizeVel, highlightedSizeSpeed);
        }
        else
        {
            activeSkinGroup.alpha = Mathf.SmoothDamp(activeSkinGroup.alpha, 0, ref activeSkinGroupVel, activeSkinToggleSpeed);
            transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.one, ref highlightedSizeVel, highlightedSizeSpeed);
        }
    }*/

    private IEnumerator Fade(bool _Value)
    {
        float startTime = Time.time;
        Vector3 _oldLocalScale = transform.localScale;
        Vector3 _finalValue = (_Value) ? highlightedSize : Vector3.one;

        while (Time.time < startTime + activeSkinToggleSpeed)
        {
            transform.localScale = Vector3.Lerp(_oldLocalScale, (_Value) ? highlightedSize : Vector3.one, (Time.time - startTime) / activeSkinToggleSpeed);
            yield return null;
        }

        transform.localScale = _finalValue;
    }

    public void SetSkinData(SkinData data)
    {
        this.m_skinData = data;
    }

    public void SetTextAndImage(string _name, Texture2D _image)
    {
        skinObjectText.text = _name;
        skinObjectImage.sprite = Sprite.Create(_image, new Rect(0.0f, 0.0f, _image.width, _image.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void SetTextAndImage(string _name, Sprite _image)
    {
        skinObjectText.text = _name;
        skinObjectImage.sprite = _image;
    }
}