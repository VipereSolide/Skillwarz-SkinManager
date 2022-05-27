using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BuildSizeOptimizer : MonoBehaviour
{
    [SerializeField] private List<Texture2D> _sceneTextures = new List<Texture2D>();

    void Start()
    {
        OptimizeSize();
    }

    private void OptimizeSize()
    {
        #region Image Optimization

        Image[] _sceneImages = GameObject.FindObjectsOfType<Image>();
        RawImage[] _sceneRawImages = GameObject.FindObjectsOfType<RawImage>();


        foreach (Image _Image in _sceneImages)
        {
            if (_Image.sprite == null)
                continue;

            _sceneTextures.Add((Texture2D)_Image.sprite.texture);
        }

        foreach (RawImage _RawImage in _sceneRawImages)
        {
            if (_RawImage.texture == null)
                continue;

            _sceneTextures.Add((Texture2D)_RawImage.texture);
        }

        #endregion
    }
}