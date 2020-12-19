using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class Tile : MonoBehaviour
{
    [Tooltip("Whether anything can be built on this tile.")]
    public bool Buildable = true;
    [Tooltip("Whether anything is already built on this tile.")]
    public bool Occupied = false;

    [SerializeField]
    private MenuController menuController = null;

    private Color originalColor;
    private Color highlightedColor;

    protected virtual void Awake()
    {
        originalColor = GetComponent<Renderer>().material.color;
        highlightedColor = Color.Lerp(originalColor, Color.white, 0.7f);
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = highlightedColor;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }

    protected virtual void OnMouseUpAsButton()
    {
        if (menuController != null)
        {
            menuController.OpenMenu(this);
        }
        else
        {
            Debug.LogError("Reference to " + nameof(menuController) + " in " + gameObject.transform.parent.name + "." + name + " is empty. Please add this reference.");
        }
    }
}
