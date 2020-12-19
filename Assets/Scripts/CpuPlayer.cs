using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CpuPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private List<Trap> traps;
    [SerializeField]
    private List<Tower> groundTowers;
    [SerializeField]
    private List<Tower> waterTowers;
    [SerializeField]
    private List<Unit> units;
    [SerializeField]
    private float initialDecisionBreak = 1f;
    [SerializeField]
    private float timeBetweenDecisions = 1f;

    private bool nextActionDecided = false;
    private BaseTile baseTile;
    private Player cpuPlayer;

    private void Awake()
    {
        cpuPlayer = GetComponent<Player>();
        baseTile = AlignmentHelpers.TryToFindPlayersBaseTile(cpuPlayer.alignment);
        InvokeRepeating(nameof(DecideAction), initialDecisionBreak, timeBetweenDecisions);
    }

    private void DecideAction()
    {
        if (nextActionDecided)
        {
            return;
        }

        var rand = Random.Range(0, 3);

        if (rand == 0)
        {
            nextActionDecided = true;
            StartCoroutine(SpawnUnit());
        }
        else if (rand == 1)
        {
            nextActionDecided = true;
            StartCoroutine(TryToBuildWaterTower());
        }
        else if (rand == 2)
        {
            nextActionDecided = true;
            StartCoroutine(TryToBuildGroundTower());
        }

        // TODO: After implementing the trap system, make the CPU player build traps as well
    }

    private List<FieldTile> GetEmptyFieldTiles()
    {
        var fieldTiles = map.GetComponentsInChildren<FieldTile>();
        var emptyFieldTiles = new List<FieldTile>();

        foreach (var tile in fieldTiles)
        {
            if (!tile.Occupied)
            {
                emptyFieldTiles.Add(tile);
            }
        }

        return emptyFieldTiles;
    }

    private List<WaterTile> GetEmptyWaterTiles()
    {
        var waterTiles = map.GetComponentsInChildren<WaterTile>();
        var emptyWaterTiles = new List<WaterTile>();

        foreach (var tile in waterTiles)
        {
            if (!tile.Occupied)
            {
                emptyWaterTiles.Add(tile);
            }
        }

        return emptyWaterTiles;
    }

    private IEnumerator SpawnUnit()
    {
        bool unitSpawned = false;
        var selectedUnit = units[Random.Range(0, units.Count)];

        while (!unitSpawned)
        {
            if (cpuPlayer.CanAfford(selectedUnit.GetPrice()))
            {
                if (cpuPlayer.TryToBuy(selectedUnit.GetPrice()))
                {
                    /* Make sure the new unit spawns with an alignment */
                    selectedUnit.alignment = cpuPlayer.alignment;

                    /* Spawn the new unit */
                    Instantiate(selectedUnit.gameObject, baseTile.transform.position, Quaternion.identity);

                    /* Reset the alignment in the prefab */
                    selectedUnit.alignment = null;
                }
                else
                {
                    Debug.LogError("Invalid game state: The CPU player can afford the unit, but can't buy it!");
                }

                unitSpawned = true;
                nextActionDecided = false;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator TryToBuildGroundTower()
    {
        bool towerBuilt = false;
        var selectedGroundTower = groundTowers[Random.Range(0, groundTowers.Count)];
        var emptyFieldTiles = GetEmptyFieldTiles();

        if (emptyFieldTiles.Count == 0)
        {
            nextActionDecided = false;
            yield break;
        }

        while (!towerBuilt)
        {
            emptyFieldTiles = GetEmptyFieldTiles();

            if (emptyFieldTiles.Count == 0)
            {
                nextActionDecided = false;
                yield break;
            }

            if (cpuPlayer.CanAfford(selectedGroundTower.GetPrice()))
            {
                if (cpuPlayer.TryToBuy(selectedGroundTower.GetPrice()))
                {
                    var selectedEmptyFieldTile = emptyFieldTiles[Random.Range(0, emptyFieldTiles.Count)];

                    selectedEmptyFieldTile.Occupied = true;
                    var go = Instantiate(selectedGroundTower.gameObject, selectedEmptyFieldTile.transform.parent);

                    /* Set alignment of the instantiated Tower */
                    var tower = go.GetComponent<Tower>();
                    if (tower != null)
                    {
                        tower.alignment = cpuPlayer.alignment;
                    }
                }
                else
                {
                    Debug.LogError("Invalid game state: The CPU player can afford the unit, but can't buy it!");
                }

                towerBuilt = true;
                nextActionDecided = false;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator TryToBuildWaterTower()
    {
        bool towerBuilt = false;
        var selectedWaterTower = waterTowers[Random.Range(0, waterTowers.Count)];
        var emptyWaterTiles = GetEmptyWaterTiles();

        if (emptyWaterTiles.Count == 0)
        {
            nextActionDecided = false;
            yield break;
        }

        while (!towerBuilt)
        {
            emptyWaterTiles = GetEmptyWaterTiles();

            if (emptyWaterTiles.Count == 0)
            {
                nextActionDecided = false;
                yield break;
            }

            if (cpuPlayer.CanAfford(selectedWaterTower.GetPrice()))
            {
                if (cpuPlayer.TryToBuy(selectedWaterTower.GetPrice()))
                {
                    var selectedEmptyWaterTile = emptyWaterTiles[Random.Range(0, emptyWaterTiles.Count)];

                    selectedEmptyWaterTile.Occupied = true;
                    var go = Instantiate(selectedWaterTower.gameObject, selectedEmptyWaterTile.transform.parent);

                    /* Set alignment of the instantiated Tower */
                    var tower = go.GetComponent<Tower>();
                    if (tower != null)
                    {
                        tower.alignment = cpuPlayer.alignment;
                    }
                }
                else
                {
                    Debug.LogError("Invalid game state: The CPU player can afford the unit, but can't buy it!");
                }

                towerBuilt = true;
                nextActionDecided = false;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
