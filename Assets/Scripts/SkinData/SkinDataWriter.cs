namespace Skillwarz.SkinManager
{
	using System;
	using System.IO;
	using UnityEngine;

	public static class SkinDataWriter
	{
        /// <summary> Writes a .swskin file by reading a SkinData class. </summary>
        /// <param name="data"> The data of the skin you want to write. </param>
        /// <param name="fileLocation"> Where you want to write your .swskin file. Remember putting a "/" at the end of your path! </param>
        /// <param name="fileName"> The name of your .swskin file. </param>
        public static void WriteSkinData(SkinData data, string fileLocation, string fileName)
        {
            string _realFileLocation = SkinDataInterpreter.SkinDataVariableToFolder(fileLocation);
            DirectoryInfo _skinDirectory = Directory.CreateDirectory(_realFileLocation + data.SkinName);
            DirectoryInfo _skinDirectoryProfile = Directory.CreateDirectory(_realFileLocation + data.SkinName + "/" + "profile/");
            DirectoryInfo _skinDirectoryTextures = Directory.CreateDirectory(_realFileLocation + data.SkinName + "/" + "textures/");

            #region WriteTextures
            // Profile Picture
            Texture2D _profilePicture = data.SkinProfilePicture;

            if (_profilePicture == null)
                _profilePicture = new Texture2D(2048,2048);

            string _writePath = _skinDirectoryProfile.FullName + "/" + "skin_profile_picture.png";
            byte[] _profilePictureBytes = _profilePicture.EncodeToPNG();
            File.WriteAllBytes(_writePath, _profilePictureBytes);

            // Skin Textures
            for (int i = 0; i < data.SkinTextures.Length; i++)
            {
                string _textureName = "albedo";

                switch(i)
                {
                    case 0:
                        _textureName = "albedo";
                        break;
                    case 1:
                        _textureName = "detail";
                        break;
                    case 2:
                        _textureName = "emission";
                        break;
                    case 3:
                        _textureName = "metallic";
                        break;
                    case 4:
                        _textureName = "normal";
                        break;
                    case 5:
                        _textureName = "height";
                        break;
                    case 6:
                        _textureName = "occlusion";
                        break;
                    default:
                        _textureName = "albedo";
                        break;
                }

                string _fullName = _skinDirectoryTextures.FullName + "/" + _textureName + ".png";

                Texture2D _texture = data.SkinTextures[i];

                if (_texture == null)
                    continue;

                byte[] _textureBytes = _texture.EncodeToPNG();
                File.WriteAllBytes(_fullName, _textureBytes);
            }
            #endregion

            string content = GetSkinDataInText(data, fileLocation, fileName);
            File.WriteAllText(_skinDirectoryProfile.FullName + "/" + fileName + ".swskin", content);
        }

        /// <summary> Returns a string corresponding to the content of a .swskin file by reading a SkinData class. </summary>
        /// <param name="data"> The data of the skin you want to write. </param>
        /// <param name="fileLocation"> Where you want to write your .swskin file. Remember putting a "/" at the end of your path! </param>
        /// <param name="fileName"> The name of your .swskin file. </param>
        public static string GetSkinDataInText(SkinData data, string fileLocation, string fileName)
        {
            string _skinName = "skin_name: " + "\"" + data.SkinName + "\"";
            string _skinDescription = "skin_description: " + "\"" + data.SkinDescription + "\"";
            
            string _skinProfilePicture = "skin_profile_picture: " + "\"" + fileLocation + data.SkinName + "/profile/skin_profile_picture.png" + "\"";

            bool[] _areTexturesExisting = new bool[7]
            {
                (data.SkinTextures[0] != null),
                (data.SkinTextures[1] != null),
                (data.SkinTextures[2] != null),
                (data.SkinTextures[3] != null),
                (data.SkinTextures[4] != null),
                (data.SkinTextures[5] != null),
                (data.SkinTextures[6] != null)
            };

            string _skinTextures_Albedo = "albedo: " + "\"" + ((_areTexturesExisting[0]) ? "textures/albedo.png" : "null") + "\"";
            string _skinTextures_Detail = "detail: " + "\"" + ((_areTexturesExisting[1]) ? "textures/detail.png" : "null") + "\"";
            string _skinTextures_Emission = "emission: " + "\"" + ((_areTexturesExisting[2]) ? "textures/emission.png" : "null") + "\"";
            string _skinTextures_Metallic = "metallic: " + "\"" + ((_areTexturesExisting[3]) ? "textures/metallic.png" : "null") + "\"";
            string _skinTextures_Normal = "normal: " + "\"" + ((_areTexturesExisting[4]) ? "textures/normal.png" : "null") + "\"";
            string _skinTextures_Height = "height: " + "\"" + ((_areTexturesExisting[5]) ? "textures/height.png" : "null") + "\"";
            string _skinTextures_Occlusion = "occlusion: " + "\"" + ((_areTexturesExisting[6]) ? "textures/occlusion.png" : "null") + "\"";
            string _skinTextures_DefaultPath = "default_path: " + "\"" + fileLocation + data.SkinName + "\"";

            string _skinOptions_albedoColor = "albedo_color: " + "\"" + data.Properties.AlbedoColor.r + "," + data.Properties.AlbedoColor.g + "," + data.Properties.AlbedoColor.b + "," + data.Properties.AlbedoColor.a + "\"";
            string _skinOptions_emissionColor = "emission_color: " + "\"" + data.Properties.EmissionColor.r + "," + data.Properties.EmissionColor.g + "," + data.Properties.EmissionColor.b + "," + data.Properties.EmissionColor.a + "\"";
            string _skinOptions_emissionIntensity = "emission_intensity: " + "\"" + data.Properties.EmissionIntensity + "\"";
            string _skinOptions_emissionType = "emission_type: " + "\"" + data.Properties.EmissionType.ToString() + "\"";
            string _skinOptions_metallicAmount = "metallic_amount: " + "\"" + data.Properties.MetallicAmount + "\"";
            string _skinOptions_normalAmount = "normal_amount: " + "\"" + data.Properties.NormalAmount + "\"";
            string _skinOptions_heightAmount = "height_amount: " + "\"" + data.Properties.HeightAmount + "\"";
            string _skinOptions_occlusionAmount = "occlusion_amount: " + "\"" + data.Properties.HeightAmount + "\"";
        
            string _skinIsEnabled = "isEnabled: " + data.IsEnabled.ToString();
            string _skinWeapon = "weapon: " + data.Weapon.ToString();

            string content = _skinName + "\n"
            + _skinDescription + "\n"
            + _skinProfilePicture + "\n"
            + "\n"
            + "skin_textures" + "\n"
            + "{" + "\n"
            + "    " + _skinTextures_Albedo + "\n"
            + "    " + _skinTextures_Detail + "\n"
            + "    " + _skinTextures_Emission + "\n"
            + "    " + _skinTextures_Metallic + "\n"
            + "    " + _skinTextures_Normal + "\n"
            + "    " + _skinTextures_Height + "\n"
            + "    " + _skinTextures_Occlusion + "\n"
            + "    " + _skinTextures_DefaultPath + "\n"
            + "}" + "\n"
            + "skin_options" + "\n"
            + "{" + "\n"
            + "    " + _skinOptions_albedoColor + "\n"
            + "    " + _skinOptions_emissionColor + "\n"
            + "    " + _skinOptions_emissionIntensity + "\n"
            + "    " + _skinOptions_emissionType + "\n"
            + "    " + _skinOptions_metallicAmount + "\n"
            + "    " + _skinOptions_normalAmount + "\n"
            + "    " + _skinOptions_heightAmount + "\n"
            + "    " + _skinOptions_occlusionAmount + "\n"
            + "}" + "\n"
            + "\n"
            + _skinIsEnabled + "\n"
            + _skinWeapon;

            return content;
        }
	}
}