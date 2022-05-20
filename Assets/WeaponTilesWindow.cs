using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTilesWindow : MonoBehaviour
{
    [SerializeField] private WeaponTilesButton m_weaponTilesButton;

    public void SetWeapon(string _WeaponName)
    {
        m_weaponTilesButton.SetWeapon(_WeaponName);
        m_weaponTilesButton.SetWindow(false);
    }
}
