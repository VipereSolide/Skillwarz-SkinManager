using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.IO;
using System.Text.RegularExpressions;

namespace VS.SkinDesigner.WebManagement
{
    public class WebSkinGalleryManager : MonoBehaviour
    {
        // Skillwarz Download Page: https://skillwarz.com/files/
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawner;
        [SerializeField] private List<WebSkin> skins = new List<WebSkin>();

        const string SKIN_REGEX = "<a\\s+(?:[^>]*?\\s+)?href='+([^']*)'(?:[^>]*?\\s+)?data-background-src='+([^']*)'";
        Regex skinRegex = new Regex(SKIN_REGEX, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private void Start()
        {
            StartCoroutine(LoadSkins());
        }

        private IEnumerator LoadSkins()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get("https://skillwarz.com/files/category/2-weapon-skins/");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("[WebRequest] An error occurred while retrieving the information.");
                yield return false;
            }

            using (MemoryStream stream = new MemoryStream(webRequest.downloadHandler.data))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (reader.Peek() >= 0)
                {
                    MatchCollection matches = skinRegex.Matches(reader.ReadLine());
                    if (matches.Count > 0 && matches[0].Groups.Count == 3) {
                    {
                        skins.Add(new WebSkin((string)matches[0].Groups[1].Value, (string)matches[0].Groups[2].Value));
                    }
                }
            }

            StartCoroutine(CreateInstance());
        }

        IEnumerator CreateInstance()
        {
            foreach (WebSkin skin in skins)
            {
                if(skin.Link != null && skin.ImageLink !=null) {
                    using UnityWebRequest www = UnityWebRequestTexture.GetTexture(skin.ImageLink);
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError(www.error);
                    }
                    else
                    {
                        GameObject newPrefab = Instantiate(prefab, spawner);
                        WebSkinObject obj = newPrefab.GetComponent<WebSkinObject>();

                        Texture2D texture = new Texture2D(2,2);
                        texture = DownloadHandlerTexture.GetContent(www);
                        obj.SetData(texture, skin.Link);
                    }
                }
            }
        }      
    }
}
}