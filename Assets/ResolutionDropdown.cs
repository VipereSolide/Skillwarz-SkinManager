using Michsky.UI.ModernUIPack;
using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    [SerializeField] private CustomDropdown m_ResolutionCustomDropdown;

    public void SetResolution(string _Resolution)
    {
        int _x = int.Parse(_Resolution.Split('x')[0]);
        int _y = int.Parse(_Resolution.Split('x')[1]);

        Screen.SetResolution(_x, _y, FullScreenMode.Windowed, 3);
    }

    public void UpdateResolution()
    {
        string _resolution = m_ResolutionCustomDropdown.dropdownItems[m_ResolutionCustomDropdown.selectedItemIndex].itemName;
        SetResolution(_resolution);
        
        Console.Instance.SendMessage("Set Resolution to " + _resolution);
    }
}
