using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class customCursor : MonoBehaviour
{

    [SerializeField]
    private GameObject m_cursorContainer;

    [SerializeField]
    private bool isActive = false;

    [SerializeField]
    private customCursorObject[] cursorSprites;

    public bool activeSelf { get { return isActive; } }

    private CanvasGroup m_CanvasGroup;
    public static customCursor instance;

    private int currentCursor = 0;

    void Awake()
    {
        instance = this;

        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        SetCursor(0);
        UpdateCursorState();
    }

    void Update()
    {
        Cursor.visible = !isActive;

        if (isActive)
        {
            transform.position = Input.mousePosition;
        }
    }

    /// <summary>
    /// Changes the cursor's icon corresponding to an index in the cursorSprites list.
    /// </summary>
    /// <param name="index"> The icon's index. </param>
    public void SetCursor(int index)
    {
        int i = index;

        if (i > cursorSprites.Length - 1)
            i = cursorSprites.Length - 1;

        foreach (customCursorObject _cursor in cursorSprites)
        {
            _cursor.SetActive((_cursor == cursorSprites[i]));
        }

        currentCursor = i;
    }

    public void ToggleActivity()
    {
        isActive = !isActive;

        UpdateCursorState();
    }

    public void SetActivity(bool _Value)
    {
        isActive = _Value;

        UpdateCursorState();
    }

    public void UpdateCursorState()
    {
        foreach (customCursorObject _cursor in cursorSprites)
        {
            if (isActive)
                _cursor.SetActive((_cursor == cursorSprites[currentCursor]));
            else
                _cursor.SetActive(false);
        }
    }
}
