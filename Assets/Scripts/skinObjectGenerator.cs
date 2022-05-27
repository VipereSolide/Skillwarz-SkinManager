using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skillwarz.SkinManager;

public class skinObjectGenerator : MonoBehaviour
{
    public static skinObjectGenerator main;

    [Header("References")]

    [SerializeField]
    private Transform instantiateContainer;

    [SerializeField]
    private skinObject skinDataObject;

    private void Awake()
    {
        main = this;
    }

    /// <summary> Instantiates skin objects for an array of SkinData. </summary>
    /// <param name="skinDatas"> The SkinData array used to create all the skin objects. </param>
    public skinObject[] GenerateSkinObjects(SkinData[] skinDatas)
    {
        skinObject[] _output = new skinObject[skinDatas.Length];

        for(int i = 0; i < skinDatas.Length; i++)
        {
            SkinData data = skinDatas[i];

            skinObject _skinObject = Instantiate(skinDataObject, instantiateContainer);
            _skinObject.SetTextAndImage(data.SkinName, data.SkinProfilePicture);
            _skinObject.SetSkinData(data);
            _skinObject.transform.name = data.SkinName;

            _output[i] = _skinObject;
        }

        return _output;
    }
}
