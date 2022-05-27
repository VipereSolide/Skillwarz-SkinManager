using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skillwarz.SkinManager;

public class rememberPosition : MonoBehaviour
{
    [SerializeField] private string m_ID;
    [SerializeField] private bool m_GenerateAutoID;

    private void Start()
    {
        LoadPosition();
    }

    private void OnApplicationQuit()
    {
        SavePosition();
    }

    private void SavePosition()
    {
        string _name = m_ID;

        if (m_GenerateAutoID)
        {
            _name = transform.name;
        }

        SetVector3(_name, transform.position);
        Console.Instance.SendMessage("Saved Position of <#00ffff>" + transform.name + "</color>: " + transform.position);
    }

    private void SetVector3(string _id, Vector3 _vec)
    {
        float _x = transform.position.x;
        float _y = transform.position.y;
        float _z = transform.position.z;

        PlayerPrefs.SetFloat(_id + "_x", _x);
        PlayerPrefs.SetFloat(_id + "_y", _y);
        PlayerPrefs.SetFloat(_id + "_z", _z);
    }

    private Vector3 GetVector3(string _id)
    {
        float _x = PlayerPrefs.GetFloat(_id + "_x");
        float _y = PlayerPrefs.GetFloat(_id + "_y");
        float _z = PlayerPrefs.GetFloat(_id + "_z");

        return new Vector3(_x,_y,_z);
    }

    private void LoadPosition()
    {
        string _name = m_ID;

        if (m_GenerateAutoID)
        {
            _name = transform.name;
        }
        
        Vector3 _pos = GetVector3(_name);

        if (_pos == Vector3.zero)
            _pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        transform.position = _pos;

        Console.Instance.SendMessage("Loaded Position of <#00ffff>" + transform.name + "</color>: " + _pos);
    }
}
