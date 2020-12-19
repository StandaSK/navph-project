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
            var mypos = transform.position;
            mypos.x += 100;
            transform.position = mypos;

            buildMenuOpen = false;
        }
    }

    public void OpenBuildMenu(Tile selectedTile)
    {
        SelectedTile = selectedTile;

        if (!buildMenuOpen)
        {
            var mypos = transform.position;
            mypos.x -= 100;
            transform.position = mypos;

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
