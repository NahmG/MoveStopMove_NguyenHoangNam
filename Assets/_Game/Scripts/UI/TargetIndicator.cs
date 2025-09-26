using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : GameUnit
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text text;
    [SerializeField] Image[] images;
    [SerializeField] GameObject offScreenIndicator;
    [SerializeField] RectTransform rectTransform;

    [Header("Settings")]
    [SerializeField] float edgeOffset = 20f;
    [SerializeField] float targetOffsetY = 20f;

    private Character target;
    private Camera mainCamera;
    private RectTransform canvasRect;

    public bool OutOfSight { get; private set; }
    public Character Target => target;

    public void UpdateIndicator()
    {
        if (target == null || mainCamera == null) return;
        UpdateState();
        text.text = target.Stats.Level.Value.ToString();
    }

    public void OnInit(Character target, Camera camera, Canvas canvas, Color color)
    {
        this.target = target;
        mainCamera = camera;
        canvasRect = canvas.GetComponent<RectTransform>();

        foreach (var img in images)
        {
            img.color = color;
        }
    }

    public void OnDespawn()
    {
        target = null;
        mainCamera = null;
        canvasRect = null;
    }

    private void UpdateState()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(
            target.transform.position + 1.3f * target.transform.localScale.y * Vector3.up
        );

        if (screenPos.z < 0f) screenPos *= -1f; // behind camera

        bool inView = screenPos.x >= 0 && screenPos.x <= canvasRect.rect.width &&
                      screenPos.y >= 0 && screenPos.y <= canvasRect.rect.height &&
                      screenPos.z >= 0;

        if (inView)
        {
            OutOfSight = false;
            ToggleOffScreen(false);
        }
        else
        {
            OutOfSight = true;
            screenPos = ClampToScreenEdge(screenPos);
            ToggleOffScreen(true, screenPos);
        }

        screenPos += Vector3.up * targetOffsetY;
        screenPos.z = 0;
        rectTransform.position = screenPos;
    }

    private Vector3 ClampToScreenEdge(Vector3 pos)
    {
        pos.z = 0f;
        Vector3 center = canvasRect.rect.size / 2f;
        Vector3 dir = (pos - center).normalized;

        float x = center.x - edgeOffset;
        float y = center.y - edgeOffset;
        float slope = dir.y / dir.x;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            pos = new Vector3(Mathf.Sign(dir.x) * x, slope * Mathf.Sign(dir.x) * x, 0);
        else
            pos = new Vector3(1f / slope * Mathf.Sign(dir.y) * y, Mathf.Sign(dir.y) * y, 0);

        return pos + center;
    }

    private void ToggleOffScreen(bool state, Vector3? pos = null)
    {
        if (offScreenIndicator.activeSelf != state)
            offScreenIndicator.SetActive(state);

        if (state && pos.HasValue)
        {
            Vector3 center = canvasRect.rect.size / 2f;
            float angle = Vector3.SignedAngle(Vector3.up, pos.Value - center, Vector3.forward);
            offScreenIndicator.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}