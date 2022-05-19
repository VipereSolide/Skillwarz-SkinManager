using UnityEngine;

public class websiteUrlButton : MonoBehaviour
{
    [SerializeField] private string m_URL;

    public void Open()
    {
        Application.OpenURL(m_URL);
    }
}
