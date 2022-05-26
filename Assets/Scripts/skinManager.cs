using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skillwarz.SkinManager;

public class skinManager : MonoBehaviour
{
    public static skinManager main;

    private skinObject[] activeSkins = new skinObject[17]
    {
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null
    };

    [SerializeField] private List<skinObject> m_inventorySkins = new List<skinObject>();

    public skinObject[] ActiveSkins { get { return this.activeSkins; } }
    public skinObject[] InventorySkins
    {
        get { return m_inventorySkins.ToArray(); }
    }

    private void Start()
    {
        UpdateActiveSkins();
        settingsSaver.main.LoadActiveSkins();
        
        foreach(skinObject _Object in skinObjectGetter.main.CreatedObjects)
        {
            m_inventorySkins.Add(_Object);
        }
    }

    private void UpdateActiveSkins()
    {
        foreach(skinObject _Object in m_inventorySkins)
        {
            if (_Object.SkinData.IsEnabled)
            {
                SetWeaponActive(_Object);
            }
        }
    }

    public void SetWeaponActive(skinObject reference)
    {
        WeaponType _referenceWT = reference.SkinData.Weapon;
        int _index = SkinData.WeaponTypeToInt(_referenceWT);

        if (activeSkins[_index] != null)
            activeSkins[_index].ToggleActivity(false);

        activeSkins[_index] = reference;
        activeSkins[_index].ToggleActivity(true);
    }

    public void SetActiveSkins(string[] _Names)
    {
        if (_Names == null)
            return;

        skinObject[] _objects = skinObjectGetter.main.CreatedObjects;

        for (int i = 0; i < _objects.Length; i++)
        {
            for (int c = 0; c < _Names.Length; c++)
            {
                if (_objects[i].transform.name == _Names[c])
                {
                    SetWeaponActive(_objects[i]);
                }
            }
        }
    }

    public void DisableWeapon(skinObject reference)
    {
        for (int i = 0; i < activeSkins.Length; i++)
        {
            skinObject skin = activeSkins[i];

            if (skin == reference)
            {
                skin.ToggleActivity(false);
                
                activeSkins[i] = m_inventorySkins[i];
                activeSkins[i].ToggleActivity(true);
                break;
            }
        }
    }

    private void Awake()
    {
        main = this;
    }
}