using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponTilesWindow : MonoBehaviour
{
    [SerializeField] private GameObject[] m_tileObjects;
    [SerializeField] private WeaponTilesButton m_weaponTilesButton;
    [SerializeField] private TMP_InputField m_searchBarInputField;

    public void SetWeapon(string _WeaponName)
    {
        m_weaponTilesButton.SetWeapon(_WeaponName);
        m_weaponTilesButton.SetWindow(false);
    }

    public void UpdateResearches()
    {
        for (int i = 0; i < m_tileObjects.Length; i++)
        {
            string _name = m_tileObjects[i].transform.Find("container").Find("footer").Find("skin_name").GetComponent<TMP_Text>().text;

            if (string.IsNullOrEmpty(m_searchBarInputField.text) || string.IsNullOrWhiteSpace(m_searchBarInputField.text))
                m_tileObjects[i].SetActive(true);

            m_tileObjects[i].SetActive(_name.Contains(m_searchBarInputField.text) || _name.Contains(m_searchBarInputField.text.ToUpper()));
        }
    }
}
