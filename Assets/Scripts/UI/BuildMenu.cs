using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public Player player;

    [HideInInspector]
    public Tile SelectedTile { get; private set; } = null;

    private bool buildMenuOpen = false;

    public void CloseBuildMenu()
    {
        if (buildMenuOpen)
        {
            var mypos = transform.localPosition;
            mypos.x += 100;
            transform.localPosition = mypos;

            buildMenuOpen = false;
        }
    }

    public void OpenBuildMenu(Tile selectedTile)
    {
        SelectedTile = selectedTile;

        if (!buildMenuOpen)
        {
            var mypos = transform.localPosition;
            mypos.x -= 100;
            transform.localPosition = mypos;

            buildMenuOpen = true;
        }
    }

    public void ToggleBuildMenu(Tile selectedTile)
    {
        if (buildMenuOpen)
        {
            CloseBuildMenu();
        }
        else
        {
            OpenBuildMenu(selectedTile);
        }
    }
}
