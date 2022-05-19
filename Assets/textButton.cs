using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class textButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private UnityEngine.Gradient underlineObjectColor;

    [SerializeField]
    private TMP_Text content;

    [SerializeField]
    private float underlineMoveSpeed;

    [Space()]

    [SerializeField]
    private underlineObject underlineObject;

    [SerializeField]
    private RectTransform underlinePositionObject;

    private bool isHighlighted = false;


    public void OnPointerEnter(PointerEventData data)
    {
        isHighlighted = true;

    }

    public void OnPointerExit(PointerEventData data)
    {
        isHighlighted = false;

    }

    public void OnPointerClick(PointerEventData data)
    {
        underlineObject.transform.SetParent(transform);
        underlineObject.StartMoving(underlinePositionObject, underlineMoveSpeed, underlineObjectColor);
    }

    private void Update()
    {
        if (isHighlighted)
        {
            content.color = Color32.Lerp(content.color, new Color32(241,241,255,255), Time.deltaTime * 15f);
        }
        else
        {
            content.color = Color.Lerp(content.color, new Color(1,1,1,0.5f), Time.deltaTime * 15f);
        }
    }
}