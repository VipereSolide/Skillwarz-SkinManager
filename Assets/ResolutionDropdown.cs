using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    public void SetResolution(string _Resolution)
    {
        int _x = int.Parse(_Resolution.Split('x')[0]);
        int _y = int.Parse(_Resolution.Split('x')[1]);

        Screen.SetResolution(_x, _y, FullScreenMode.Windowed, 3);
    }
}
