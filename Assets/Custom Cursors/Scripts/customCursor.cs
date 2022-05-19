using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class customCursor : MonoBehaviour
{

    [SerializeField]
    private bool isActive = true;

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

        foreach(customCursorObject _cursor in cursorSprites)
        {
            _cursor.SetActive( (_cursor == cursorSprites[i]) );
        }

        currentCursor = i;
    }

    /// <summary>
    /// Actives or disable the cursor (= isNormalCursor || isCustomCursor).
    /// </summary>
    public void SetActive(bool value)
    {
        isActive = value;

        if (m_CanvasGroup != null)
            m_CanvasGroup.alpha = (value) ? 1 : 0;
    }
}
