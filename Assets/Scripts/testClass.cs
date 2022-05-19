using UnityEngine;

using Skillwarz.SkinManager;

public class testClass
{
    public string[] GetTexturePathFromString(string _Input)
    {
        // The value that will be output at the end.
        string[] _output = new string[7];

        // Get an array of string corresponding to the lines of the _Input.
        string[] _inputInLines = _Input.Split('\n');

        // Get all the texture paths by remove the texture names on every lines.
        for (int i = 0; i < 7; i++)
        {
            string _textureName = SkinData.IntToTextureName(i);
            string _outputPath = _inputInLines[i].Replace(_textureName + ": ", "");

            Debug.Log("The path of the texture " + i + " (" + _textureName + ") is: " + _outputPath);

            // OUTPUT:
            // ./textures/albedo.png
            // ./textures/detail.png
            // ./textures/emission.png
            // ./textures/metallic.png
            // ./textures/normal.png
            // ./textures/height.png
            // ./textures/occlusion.png
            
            _output[i] = _outputPath;
        }

        return _output;
    }
}