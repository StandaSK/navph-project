using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildMenuItem : MonoBehaviour
{
    [SerializeField]
    private GameObject buildItemPrefab = null;

    private BuildMenu buildMenu;
    private Button button;

    private void Awake()
    {
        buildMenu = transform.GetComponentInParent<BuildMenu>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TryToBuildItem);
    }

    private int GetPrice()
    {
        var tower = buildItemPrefab.GetComponent<Tower>();

        if (tower == null)
        {
            var trap = buildItemPrefab.GetComponent<Trap>();

            if (trap == null)
            {
                Debug.LogError("Unknown build item. Assuming maximum possible cost.");
                return int.MaxValue;
            }

            return trap.GetPrice();
        }

        return tower.GetPrice();
    }

    private void TryToBuildItem()
    {
        if (buildMenu.player.CanAfford(GetPrice()))
        {
            var tile = buildMenu.SelectedTile;
            var tileParent = tile.transform.parent;

            if (!tile.Buildable || tile.Occupied)
            {
                Debug.LogError("The selected tile is occupied or not buildable!");
                return;
            }

            if (buildMenu.player.TryToBuy(GetPrice()))
            {
                buildMenu.CloseBuildMenu();
                tile.Occupied = true;
                var go = Instantiate(buildItemPrefab, tileParent);

                /* If the instantiated GO is a Tower, set its alignment */
                var tower = go.GetComponent<Tower>();
                if (tower != null)
                {
                    tower.alignment = buildMenu.player.alignment;
                }
            }
            else
            {
                Debug.LogError("Invalid game state: The player can afford the item, but can't buy it!");
                return;
            }
        }
    }
}
