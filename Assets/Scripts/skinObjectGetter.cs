using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skillwarz.SkinManager;
using System.IO;

public class skinObjectGetter : MonoBehaviour
{
    public static skinObjectGetter main;

    private skinObject[] m_CreatedObjects;

    public skinObject[] CreatedObjects { get { return m_CreatedObjects; } }

    void Start()
    {
        m_CreatedObjects = CreateObjects();
    }

    void Awake()
    {
        main = this;
    }

    public skinObject[] CreateObjects()
    {
        // Create the folder
        Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/Skillwarz/MySkins");

        // https://www.codegrepper.com/code-examples/csharp/c%23+get+all+files+in+directory+with+extension
        string[] _swskinFiles = Directory.GetFiles(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/Skillwarz/MySkins/", "*.swskin", SearchOption.AllDirectories);
        SkinData[] _datas = new SkinData[_swskinFiles.Length];

        float _readingTimeSinceStartup = Time.realtimeSinceStartup;
        for (int i = 0; i < _swskinFiles.Length; i++)
        {
            _datas[i] = SkinDataInterpreter.GetSkinData(File.ReadAllText(_swskinFiles[i]));
        }
        float _readingTimeSinceStartupEnd = Time.realtimeSinceStartup;
        Console.Instance.SendMessage("Reading Skin Data Time: <#00ff99>" + (_readingTimeSinceStartupEnd - _readingTimeSinceStartup).ToString() + "</color>");

        skinObject[] _skinObjects = skinObjectGenerator.main.GenerateSkinObjects(_datas);

        return _skinObjects;
    }

    public void RefreshObjects()
    {
        foreach(skinObject o in m_CreatedObjects)
        {
            Destroy(o.gameObject);
        }

        m_CreatedObjects = CreateObjects();
    }
}
