using UnityEngine;

/// <summary>
/// Deals with opening and closing the correct menus.
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField]
    private BuildMenu fieldBuildMenu = null;
    [SerializeField]
    private BuildMenu roadBuildMenu = null;
    [SerializeField]
    private TowerMenu towerMenu = null;
    [SerializeField]
    private BuildMenu waterBuildMenu = null;
    [SerializeField]
    private Player player;

    private Tile selectedTile;

    #region Close Menu Helpers
    private void CloseAllBuildMenus()
    {
        CloseFieldBuildMenu();
        CloseRoadBuildMenu();
        CloseWaterBuildMenu();
    }

    private void CloseAllMenus()
    {
        CloseAllBuildMenus();

        if (towerMenu != null)
        {
            towerMenu.CloseTowerMenu();
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to tower menu!");
        }
    }

    private void CloseFieldBuildMenu()
    {
        if (fieldBuildMenu != null)
        {
            fieldBuildMenu.CloseBuildMenu();
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to field build menu!");
        }
    }

    private void CloseRoadBuildMenu()
    {
        if (roadBuildMenu != null)
        {
            roadBuildMenu.CloseBuildMenu();
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to road build menu!");
        }
    }

    private void CloseWaterBuildMenu()
    {
        if (waterBuildMenu != null)
        {
            waterBuildMenu.CloseBuildMenu();
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to water build menu!");
        }
    }
    #endregion

    public void OpenMenu(Tile tile)
    {
        selectedTile = tile;

        if (tile.Buildable)
        {
            if (tile.Occupied)
            {
                if (tile is FieldTile || tile is WaterTile)
                {
                    var tower = tile.transform.parent.GetComponentInChildren<Tower>();

                    if (tower == null)
                    {
                        Debug.Log("Unknown building occupies tile \"" + tile.transform.parent.name + "\". Closing all menus.");
                        CloseAllMenus();
                    }

                    // TODO: Find a better way to check if it's the same alignment
                    if (!tower.alignment.CanHarm(player.alignment))
                    {
                        OpenTowerMenu();
                    }
                    else
                    {
                        CloseAllMenus();
                    }
                }
                else if (tile is RoadTile)
                {
                    // TODO: Should we have a trap menu?
                    CloseAllMenus();
                }
                else
                {
                    Debug.LogError("Unknown buildable, occupied tile type. Closing all menus.");
                    CloseAllMenus();
                }
            }
            else
            {
                OpenBuildMenu();
            }
        }
        else
        {
            /* Non-buildable tiles, close all menus */
            CloseAllMenus();
        }
    }

    /// <summary>
    /// Opens a build menu corresponding to the <see cref="selectedTile"/>.
    /// </summary>
    private void OpenBuildMenu()
    {
        /* Close the tower menu */
        if (towerMenu != null)
        {
            towerMenu.CloseTowerMenu();
        }

        if (selectedTile is FieldTile)
        {
            /* Open the field build menu, close the others */
            OpenFieldBuildMenu();
            CloseRoadBuildMenu();
            CloseWaterBuildMenu();
        }
        else if (selectedTile is RoadTile)
        {
            /* Open the road build menu, close the others */
            CloseFieldBuildMenu();
            OpenRoadBuildMenu();
            CloseWaterBuildMenu();
        }
        else if (selectedTile is WaterTile)
        {
            /* Open the water build menu, close the others */
            CloseFieldBuildMenu();
            CloseRoadBuildMenu();
            OpenWaterBuildMenu();
        }
        else
        {
            Debug.LogError("Unknown tile type or a non-buildable tile: " + selectedTile.name);
        }
    }

    #region Open Menu Helpers
    /// <summary>
    /// Opens a build menu for ground-based towers.
    /// </summary>
    private void OpenFieldBuildMenu()
    {
        if (fieldBuildMenu != null)
        {
            fieldBuildMenu.OpenBuildMenu(selectedTile);
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to field build menu!");
        }
    }

    /// <summary>
    /// Opens a build menu for traps.
    /// </summary>
    private void OpenRoadBuildMenu()
    {
        if (roadBuildMenu != null)
        {
            roadBuildMenu.OpenBuildMenu(selectedTile);
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to road build menu!");
        }
    }

    /// <summary>
    /// Opens a build menu for water-based towers.
    /// </summary>
    private void OpenWaterBuildMenu()
    {
        if (waterBuildMenu != null)
        {
            waterBuildMenu.OpenBuildMenu(selectedTile);
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to water build menu!");
        }
    }

    /// <summary>
    /// Opens a tower menu, through which towers are upgraded or sold.
    /// </summary>
    private void OpenTowerMenu()
    {
        CloseAllBuildMenus();

        if (towerMenu != null)
        {
            towerMenu.OpenTowerMenu(selectedTile);
        }
        else
        {
            Debug.LogError("Menu controller is missing a reference to tower menu!");
        }
    }
    #endregion
}
