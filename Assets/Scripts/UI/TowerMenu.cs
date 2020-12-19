using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public Player player;

    [SerializeField]
    private Button sellButton = null;
    [SerializeField]
    private Button upgradeButton = null;

    private bool towerMenuOpen = false;
    private Tile selectedTile;
    private Tower selectedTower;

    private void Awake()
    {
        if (sellButton == null)
        {
            Debug.Log("The tower menu is missing a reference to the sell button!\nAttempting a fix.");
            upgradeButton = transform.Find("SellButton").GetComponent<Button>();
        }

        if (upgradeButton == null)
        {
            Debug.Log("The tower menu is missing a reference to the upgrade button!\nAttempting a fix.");
            upgradeButton = transform.Find("UpgradeButton").GetComponent<Button>();
        }

        sellButton.onClick.AddListener(SellTower);
        upgradeButton.onClick.AddListener(UpgradeTower);
    }

    public void CloseTowerMenu()
    {
        if (towerMenuOpen)
        {
            var mypos = transform.position;
            mypos.y -= 50;
            transform.position = mypos;

            towerMenuOpen = false;
        }
    }

    public void OpenTowerMenu(Tile selectedTile)
    {
        this.selectedTile = selectedTile;
        selectedTower = selectedTile.transform.parent.GetComponentInChildren<Tower>();

        if (selectedTower == null)
        {
            Debug.LogError("The tile \"" + selectedTile.transform.parent.name + "\" does not have a Tower built on it!");
            CloseTowerMenu();
            return;
        }

        upgradeButton.interactable = !selectedTower.isUpgraded;

        if (!towerMenuOpen)
        {
            var mypos = transform.position;
            mypos.y += 50;
            transform.position = mypos;

            towerMenuOpen = true;
        }
    }

    public void SellTower()
    {
        CloseTowerMenu();
        selectedTower.Sell();
        selectedTower = null;
        selectedTile.Occupied = false;
    }

    public void ToggleTowerMenu(Tile selectedTile)
    {
        if (towerMenuOpen)
        {
            CloseTowerMenu();
        }
        else
        {
            OpenTowerMenu(selectedTile);
        }
    }

    public void UpgradeTower()
    {
        // TODO: After implementing the tower upgrade system, check if the player can afford the upgrade price
        selectedTower.Upgrade();
        upgradeButton.interactable = false;
    }
}
