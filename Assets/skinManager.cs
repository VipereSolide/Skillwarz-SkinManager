using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skillwarz.SkinManager;

public class skinManager : MonoBehaviour
{
    public static skinManager main;

    private skinObject[] activeSkins = new skinObject[16]
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
        null
    };

    public skinObject[] ActiveSkins { get { return this.activeSkins; } }

    private void Start()
    {
        settingsSaver.main.LoadActiveSkins();
    }

    public void SetWeaponActive(skinObject reference)
    {
        WeaponType _referenceWT = reference.SkinData.Weapon;
        int _index = SkinData.WeaponTypeToInt(_referenceWT);

        if(activeSkins[_index] != null)
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
            for(int c = 0; c < _Names.Length; c++)
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
        for(int i = 0; i < activeSkins.Length; i++)
        {
            skinObject skin = activeSkins[i];

            if (skin == reference)
            {
                skin.ToggleActivity(false);
                activeSkins[i] = null;
                break;
            }
        }
    }

    private void Awake()
    {
        main = this;
    }
}