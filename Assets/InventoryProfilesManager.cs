using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skillwarz.SkinManager.ProfileManagement
{
    public class InventoryProfilesManager : MonoBehaviour
    {
        public void SaveSelectedSkins(string _ProfileName)
        {
            string _savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/SkinManager/";

            // Creates the folder if it does not exist.
            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }

            string _fileName = _savePath + _ProfileName + ".swskinprofile";
            string _content = "";

            foreach (skinObject _Object in skinManager.main.ActiveSkins)
            {
                string _output = (_Object == null) ? "|null" : _Object.SkinData.SkinName;
                _content += _output + "\n";
            }

            File.WriteAllText(_fileName, _content);
        }

        public Profile LoadSelectedSkins(string _ProfileName)
        {
            string _savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/SkinManager/";
            string _fileName = _savePath + _ProfileName + ".swskinprofile";

            if (!File.Exists(_fileName))
            {
                Debug.LogError("The given path does not exist.");
                return null;
            }

            string _content = File.ReadAllText(_fileName);
            string[] _profileSelectedElements = new string[16]
            {
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null",
                "|null"
            };

            for (int i = 0; i < _content.Split('\n').Length; i++)
            {
                string _Line = _content.Split('\n')[i];

                if (string.IsNullOrEmpty(_Line) || string.IsNullOrWhiteSpace(_Line))
                {
                    continue;
                }

                _profileSelectedElements[i] = _Line;
            }

            Profile _output = new Profile(_profileSelectedElements);
            return _output;
        }
    }

    [Serializable]
    public class Profile
    {
        [SerializeField]
        private string[] m_selectedElements = new string[16]
        {
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null",
            "|null"
        };

        public string[] SelectedElements
        {
            get { return m_selectedElements; }
        }

        public Profile(string[] _SelectedElements)
        {
            this.m_selectedElements = _SelectedElements;
        }

    }
}