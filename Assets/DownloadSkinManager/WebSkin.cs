using UnityEngine;

[System.Serializable]
public class WebSkin
{
    [SerializeField] private string link;
    [SerializeField] private string imageLink;
    public string Link { get { return link; } }
    public string ImageLink { get { return imageLink; } }
    public WebSkin(string link, string imageLink)
    {
        this.link = link;
        this.imageLink = imageLink;
    }
}  