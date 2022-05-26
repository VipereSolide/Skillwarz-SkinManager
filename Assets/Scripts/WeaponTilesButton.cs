using System.Collections;
using System.Collections.Generic;

using FeatherLight.Pro;
using UnityEngine;
using TMPro;

public class WeaponTilesButton : MonoBehaviour
{
    [SerializeField] private TMP_Text m_currentWeaponText;
    [SerializeField] private CanvasGroup m_weaponTilesWindowCanvasGroup;
    [SerializeField] private float m_weaponTilesWindowFadeTime;

    public void SetWeapon(string _WeaponName)
    {
        m_currentWeaponText.text = _WeaponName;
    }

    public void SetWindow(bool _Value)
    {
        StartCoroutine(CanvasGroupHelper.Fade(m_weaponTilesWindowCanvasGroup, _Value, m_weaponTilesWindowFadeTime));
    }
}
