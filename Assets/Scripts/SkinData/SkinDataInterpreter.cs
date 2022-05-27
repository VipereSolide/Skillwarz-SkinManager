namespace Skillwarz.SkinManager
{
    using System;
    using System.IO;

    using UnityEngine;

    public static class SkinDataInterpreter
    {
        /// <summary> Takes a string corresponding to the content of a .swskin file. It will return a SkinData class by reading the string. </summary>
        /// <param name="_Value"> The content of the .swskin </param>
		public static SkinData GetSkinData(string _Value, bool _LoadTextures = false)
        {
            float _firstTime = Time.realtimeSinceStartup;
            string[] _valueFileLines = _Value.Split('\n');

            string _outputSkinName = _valueFileLines[0].Replace("skin_name: ", "").Replace("\"", "");
            string _outputSkinDescription = _valueFileLines[1].Replace("skin_description: ", "").Replace("\"", "");

            #region StringReferences
            string _outputSkinProfilePicturePath = _valueFileLines[2].Replace("skin_profile_picture: ", "").Replace("\"", "");
            _outputSkinProfilePicturePath = SkinDataVariableToFolder(_outputSkinProfilePicturePath);

            string[] _outputSkinTexturePaths = new string[7]
            {
                _valueFileLines[6].Replace("albedo: ","").Replace("\"",""),
                _valueFileLines[7].Replace("detail: ","").Replace("\"",""),
                _valueFileLines[8].Replace("emission: ","").Replace("\"",""),
                _valueFileLines[9].Replace("metallic: ","").Replace("\"",""),
                _valueFileLines[10].Replace("normal: ","").Replace("\"",""),
                _valueFileLines[11].Replace("height: ","").Replace("\"",""),
                _valueFileLines[12].Replace("occlusion: ","").Replace("\"","")
            };

            string _outputDefaultPath = _valueFileLines[13].Replace("default_path: ", "").Replace("\"", "").Replace("    ", "");
            _outputDefaultPath = SkinDataVariableToFolder(_outputDefaultPath);

            string _outputAlbedoColor = _valueFileLines[17].Replace("albedo_color: ", "").Replace("\"", "");
            string _outputEmissionColor = _valueFileLines[18].Replace("emission_color: ", "").Replace("\"", "");
            string _outputEmissionIntensity = _valueFileLines[19].Replace("emission_intensity: ", "").Replace("\"", "");
            string _outputEmissionType = _valueFileLines[20].Replace("emission_type: ", "").Replace("\"", "");
            string _outputMetallicAmount = _valueFileLines[21].Replace("metallic_amount: ", "").Replace("\"", "");
            string _outputNormalAmount = _valueFileLines[22].Replace("normal_amount: ", "").Replace("\"", "");
            string _outputHeightAmount = _valueFileLines[23].Replace("height_amount: ", "").Replace("\"", "");
            string _outputOcclusionAmount = _valueFileLines[24].Replace("occlusion_amount: ", "").Replace("\"", "");

            string _outputIsEnabled = _valueFileLines[27].Replace("isEnabled: ", "").Replace("\"", "");
            string _outputWeapon = _valueFileLines[28].Replace("weapon: ", "").Replace("\"", "");
            #endregion
            #region References

            Texture2D _skinProfilePicture = new Texture2D(2, 2);
            if (File.Exists(_outputSkinProfilePicturePath))
            {
                _skinProfilePicture.LoadImage(File.ReadAllBytes(_outputSkinProfilePicturePath), false);
                _skinProfilePicture.Apply();
            }

            Texture2D[] _skinTextures = new Texture2D[7]
            {
                null,
                null,
                null,
                null,
                null,
                null,
                null
            };

            string[] _skinTexturePaths = new string[7]
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
            };

            /*

            EDIT: Loading all the textures at every application start isn't the best approach. As we have the path to the textures, we can only
            load them when we want to move them. We don't necessarily need to load them. We can only use the links and load them when we want.
            I will still put an option to load all the textures too if we would need something like that in the future.

            */

            if (_LoadTextures)
            {
                for (int i = 0; i < _outputSkinTexturePaths.Length; i++)
                {
                    Texture2D _texture = new Texture2D(2, 2);
                    string _path = SkinDataVariableToFolder((_outputDefaultPath + "/" + _outputSkinTexturePaths[i]).Replace("    ", ""));

                    if (!File.Exists(_path))
                        continue;

                    _skinTexturePaths[i] = _path;

                    _texture.LoadImage(File.ReadAllBytes(_path), false);
                    _texture.Apply();

                    _skinTextures[i] = _texture;
                }
            }

            for (int i = 0; i < _outputSkinTexturePaths.Length; i++)
            {
                string _path = (_outputDefaultPath + "/" + _outputSkinTexturePaths[i]).Replace("    ", "");
                _skinTexturePaths[i] = _path;
            }


            Color _albedoColor = Color.white;
            //Debug.Log(_outputAlbedoColor.Split(',')[0]);
            _albedoColor.r = float.Parse(_outputAlbedoColor.Split(',')[0]);
            _albedoColor.g = float.Parse(_outputAlbedoColor.Split(',')[1]);
            _albedoColor.b = float.Parse(_outputAlbedoColor.Split(',')[2]);
            _albedoColor.a = float.Parse(_outputAlbedoColor.Split(',')[3]);

            Color _emissionColor = Color.white;
            _emissionColor.r = float.Parse(_outputEmissionColor.Split(',')[0]);
            _emissionColor.g = float.Parse(_outputEmissionColor.Split(',')[1]);
            _emissionColor.b = float.Parse(_outputEmissionColor.Split(',')[2]);
            _emissionColor.a = float.Parse(_outputEmissionColor.Split(',')[3]);

            float _emissionIntensity = float.Parse(_outputEmissionIntensity);
            EmissionType _emissionType = (EmissionType)Enum.Parse(typeof(EmissionType), _outputEmissionType);
            float _metallicAmount = float.Parse(_outputMetallicAmount);
            float _normalOutput = float.Parse(_outputNormalAmount);
            float _heightAmount = float.Parse(_outputHeightAmount);
            float _occlusionAmount = float.Parse(_outputOcclusionAmount);

            bool _isEnabled = bool.Parse(_outputIsEnabled);
            WeaponType _weapon = (WeaponType)Enum.Parse(typeof(WeaponType), _outputWeapon);
            #endregion

            SkinDataProperties _properties = new SkinDataProperties(_albedoColor, _emissionColor, _emissionIntensity, _emissionType, _metallicAmount, _normalOutput, _heightAmount, _occlusionAmount);
            SkinData _data = new SkinData(_outputSkinName, _outputSkinDescription, _skinProfilePicture, _isEnabled, _skinTextures, _properties, _weapon);
            _data.SetTexturePaths(_skinTexturePaths);

            float _endTime = Time.realtimeSinceStartup;
            Skillwarz.SkinManager.Console.Instance.SendMessage("GetSkinData: <#00ff99>" + (_endTime - _firstTime).ToString() + "</color>");
            return _data;
        }

        /// <summary>
        /// Returns a string representing a folder path using a swskin variable (e.g. ###MyDocuments###).
        /// </summary>
        /// <param name="_Content"> The swskin variable. </param>
        public static string SkinDataVariableToFolder(string _Content)
        {
            string _output = _Content;

            string[] _specialFoldersInString = Enum.GetNames(typeof(System.Environment.SpecialFolder));

            foreach (string _value in _specialFoldersInString)
            {
                if (!_output.Contains(_value))
                    continue;

                string _valuePath = System.Environment.GetFolderPath(
                    (Environment.SpecialFolder)System.Enum.Parse(
                        typeof(Environment.SpecialFolder),
                        _value
                    )
                );

                _output = _output.Replace("###" + _value + "###", _valuePath);
            }

            return _output;
        }
    }
}