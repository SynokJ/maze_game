using Level;
using UnityEngine;

[RequireComponent(typeof(Wall))]
public class WallView : MonoBehaviour
{
    [SerializeField] protected Collider2D wallCollider = default;
    [SerializeField] protected Renderer wallRenderer = default;
    
    protected Wall wall = default;

    protected virtual void Awake()
    {
        wall = GetComponent<Wall>();
    }

    public virtual void ShowWall()
        => SetVisibilityByStats(true);

    public virtual void HideWall()
        => SetVisibilityByStats(false);

    protected virtual void SetVisibilityByStats(bool status)
    {
        wallCollider.enabled = status;
        wallRenderer.enabled = status;
    }
}
