using UnityEngine;
using UnityEngine.UI;

public class SpawnMenuItem : MonoBehaviour
{
    [SerializeField]
    private Unit unitPrefab = null;

    private SpawnMenu spawnMenu;
    private Button button;

    private void Awake()
    {
        spawnMenu = transform.GetComponentInParent<SpawnMenu>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TryToSpawnUnit);
    }

    private int GetPrice()
    {
        return unitPrefab.GetPrice();
    }

    private void TryToSpawnUnit()
    {
        if (spawnMenu.player.CanAfford(GetPrice()))
        {
            var friendlyBase = AlignmentHelpers.TryToFindPlayersBaseTile(spawnMenu.player.alignment);

            if (friendlyBase != null)
            {
                if (spawnMenu.player.TryToBuy(GetPrice()))
                {
                    /* Make sure the new unit spawns with an alignment */
                    unitPrefab.alignment = spawnMenu.player.alignment;

                    /* Spawn the new unit */
                    Instantiate(unitPrefab.gameObject, friendlyBase.transform.position, Quaternion.identity);

                    /* Reset the alignment in the prefab */
                    unitPrefab.alignment = null;
                }
                else
                {
                    Debug.LogError("Invalid game state: The player can afford the unit, but can't buy it!");
                    return;
                }
            }
        }
    }
}
