namespace Skillwarz.SkinManager
{
	using System;
	using UnityEngine;

	public enum EmissionType
	{
		None,
		IntensityChange,
		ColorChange
	}

	public enum WeaponType
	{
		AK12,
		AK47,
		AUG,
		DAO,
		DEAGLE,
		G22,
		K10,
		KSG,
		L96A1,
		M4A1,
		M107,
		M110,
		MG4,
		MLG140,
		P99,
		USP,
		UZI
	}

	[Serializable]
	public class SkinData
	{
		#region Declarations

		[Header("Skin Data")]

		[SerializeField]
		private string skinName = "";

		[SerializeField]
		private string skinDescription = "";

		[SerializeField]
		private Texture2D skinProfilePicture = null;

		[SerializeField]
		private bool isEnabled = false;

		[SerializeField]
		private WeaponType weapon;

		[SerializeField, Tooltip("albedo, detail, emission, metallic, normal, height, occlusion")]
		private Texture2D[] skinTextures = new Texture2D[7] {null, null, null, null, null, null, null};

		[SerializeField, Tooltip("albedo, detail, emission, metallic, normal, height, occlusion")]
		private string[] skinTexturePaths = new string[7] {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};

		[Space()]

		[SerializeField]
		private SkinDataProperties properties = new SkinDataProperties(Color.white, Color.black, 0.0f, EmissionType.None, 2.0f, 0.5f, 0.05f, 1.0f);

		public string SkinName { get { return this.skinName; } }
		public string SkinDescription { get { return this.skinDescription; } }
		public Texture2D SkinProfilePicture { get { return this.skinProfilePicture; } }
		public bool IsEnabled { get { return this.isEnabled; } }
		public WeaponType Weapon { get { return this.weapon; } }
		public Texture2D[] SkinTextures { get { return this.skinTextures; } }
		public string[] SkinTexturePaths { get { return this.skinTexturePaths; } }
		public SkinDataProperties Properties { get { return this.properties; } }

		#endregion

		public SkinData(string _skinName, string _skinDescription, Texture2D _skinProfilePicture, bool _isEnabled, Texture2D[] _skinTextures, SkinDataProperties _properties, WeaponType _weapon)
		{
			this.skinName = _skinName;
			this.skinDescription = _skinDescription;
			this.skinProfilePicture = _skinProfilePicture;
			this.isEnabled = _isEnabled;
			this.skinTextures = _skinTextures;
			this.properties = _properties;
			this.weapon = _weapon;
		}

		public void SetData(string _skinName, string _skinDescription, Texture2D _skinProfilePicture, bool _isEnabled, Texture2D[] _skinTextures, SkinDataProperties _properties, WeaponType _weapon)
		{
			this.skinName = _skinName;
			this.skinDescription = _skinDescription;
			this.skinProfilePicture = _skinProfilePicture;
			this.isEnabled = _isEnabled;
			this.skinTextures = _skinTextures;
			this.properties = _properties;
			this.weapon = _weapon;
		}

		public void SetDescription(string _skinDescription)
		{
			this.skinDescription = _skinDescription;
		}

		public SkinData()
		{
			
		}

		public void SetTexturePaths(string[] _texturePaths)
		{
			this.skinTexturePaths = _texturePaths;
		}

		public static int WeaponTypeToInt(WeaponType value)
		{
			switch(value)
			{
				case WeaponType.AK12:
					return 0;
				case WeaponType.AK47:
					return 1;
				case WeaponType.AUG:
					return 2;
				case WeaponType.DAO:
					return 3;
				case WeaponType.DEAGLE:
					return 4;
				case WeaponType.G22:
					return 5;
				case WeaponType.K10:
					return 6;
				case WeaponType.KSG:
					return 7;
				case WeaponType.L96A1:
					return 8;
				case WeaponType.M107:
					return 9;
				case WeaponType.M110:
					return 10;
				case WeaponType.M4A1:
					return 11;
				case WeaponType.MG4:
					return 12;
				case WeaponType.MLG140:
					return 13;
				case WeaponType.P99:
					return 14;
				case WeaponType.USP:
					return 15;
				case WeaponType.UZI:
					return 16;
				default:
					return 0;
			}
		}

		public static string IntToTextureName(int _int)
		{
			string _textureName = "";

			switch(_int)
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

			return _textureName;
		}

		public static int TextureNameToInt(string _textureName)
		{
			int _int = 0;

			switch(_textureName)
            {
                case "albedo":
                    _int = 0;
                    break;
				case "detail":
                    _int = 1;
                    break;
				case "emission":
                    _int = 2;
                    break;
				case "metallic":
                    _int = 3;
                    break;
				case "normal":
                    _int = 4;
                    break;
				case "height":
                    _int = 5;
                    break;
				case "occlusion":
                    _int = 6;
                    break;
				default:
                    _int = 0;
                    break;
            }

			return _int;
		}

	}

	[Serializable]
	public class SkinDataProperties
	{
		[Header("Skin Properties Data")]

		[SerializeField]
		private Color albedoColor = Color.white;

		[SerializeField]
		private Color emissionColor = Color.black;

		[SerializeField] [Range(-10.0f,10.0f)]
		private float emissionIntensity = 0.0f;

		[SerializeField]
		private EmissionType emissionType = EmissionType.None;

		[SerializeField]
		private float metallicAmount = 2.0f;

		[SerializeField]
		private float normalAmount = 0.5f;

		[SerializeField]
		private float heightAmount = 0.05f;

		[SerializeField]
		private float occlusionAmount = 1.0f;

		public Color AlbedoColor { get { return this.albedoColor; } }
		public Color EmissionColor { get { return this.emissionColor; } }
		public float EmissionIntensity { get { return this.emissionIntensity; } }
		public EmissionType EmissionType { get { return this.emissionType; } }
		public float MetallicAmount { get { return this.metallicAmount; } }
		public float NormalAmount { get { return this.normalAmount; } }
		public float HeightAmount { get { return this.heightAmount; } }
		public float OcclusionAmount { get { return this.occlusionAmount; } }

		public SkinDataProperties(Color _albedoColor, Color _emissionColor, float _emissionIntensity, EmissionType _emissionType, float _metallicAmount, float _normalAmount, float _heightAmount, float _occlusionAmount)
		{
			this.albedoColor = _albedoColor;
			this.emissionColor = _emissionColor;
			this.emissionIntensity = _emissionIntensity;
			this.emissionType = _emissionType;
			this.metallicAmount = _metallicAmount;
			this.normalAmount = _normalAmount;
			this.heightAmount = _heightAmount;
			this.occlusionAmount = _occlusionAmount;
		}

		public void SetData(Color _albedoColor, Color _emissionColor, float _emissionIntensity, EmissionType _emissionType, float _metallicAmount, float _normalAmount, float _heightAmount, float _occlusionAmount)
		{
			this.albedoColor = _albedoColor;
			this.emissionColor = _emissionColor;
			this.emissionIntensity = _emissionIntensity;
			this.emissionType = _emissionType;
			this.metallicAmount = _metallicAmount;
			this.normalAmount = _normalAmount;
			this.heightAmount = _heightAmount;
			this.occlusionAmount = _occlusionAmount;
		}

		public SkinDataProperties() {}
	}
}