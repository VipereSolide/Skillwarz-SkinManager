using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using TMPro;

namespace Skillwarz.SkinManager.ProfileManagement
{
    public class InventoryProfilesManager : MonoBehaviour
    {
        [SerializeField] private CustomDropdown m_profilesDropdown;
        [SerializeField] private Transform m_profilesDropdownContainer;
        [SerializeField] private Sprite m_uiMask;


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
                string _output = (_Object == null || _Object.IsDefaultSkin) ? "|null" : _Object.SkinData.SkinName;
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
            string[] _profileSelectedElements = new string[17]
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

            Profile _output = new Profile(_profileSelectedElements, _ProfileName);
            return _output;
        }

        public Profile[] LoadProfiles()
        {
            string[] _profiles = Directory.GetFiles(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/SkinManager/", "*.swskinprofile", SearchOption.AllDirectories);
            List<Profile> _output = new List<Profile>();

            for (int i = 0; i < _profiles.Length; i++)
            {
                _output.Add(LoadSelectedSkins(Path.GetFileNameWithoutExtension(_profiles[i])));
            }

            return _output.ToArray();
        }

        public void CreateProfileDropdownItems()
        {
            Profile[] _loadedProfiles = LoadProfiles();

            foreach (Profile _Profile in _loadedProfiles)
            {
                m_profilesDropdown.CreateNewItem(_Profile.ProfileName, m_uiMask);

                m_profilesDropdown.dropdownItems[m_profilesDropdown.dropdownItems.Count - 1].OnItemSelection.AddListener(() =>
                {
                    skinManager.main.SetActiveSkins(_Profile.SelectedElements);
                });
            }
        }

        public void CreateProfileWithInputField(GameObject _InputField)
        {
            SaveSelectedSkins(_InputField.GetComponent<TMP_InputField>().text);

            foreach(Transform _T in m_profilesDropdownContainer)
                Destroy(_T.gameObject);

            m_profilesDropdown.dropdownItems.Clear();
            m_profilesDropdown.SetupDropdown();

            CreateProfileDropdownItems();

            Console.Instance.SendMessage("Created new profile <#00ff99>" + _InputField.GetComponent<TMP_InputField>().text + "</color>.");
        }

        private void Start()
        {
            CreateProfileDropdownItems();
        }
    }

    [Serializable]
    public class Profile
    {
        [SerializeField]
        private string m_profileName;

        [SerializeField]
        private string[] m_selectedElements = new string[17]
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
            "|null",
            "|null"
        };

        public string[] SelectedElements
        {
            get { return m_selectedElements; }
        }

        public string ProfileName
        {
            get { return m_profileName; }
        }

        public Profile(string[] _SelectedElements, string _ProfileName)
        {
            this.m_selectedElements = _SelectedElements;
            this.m_profileName = _ProfileName;
        }

    }
}