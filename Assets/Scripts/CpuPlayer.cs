using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CpuPlayer : MonoBehaviour
{
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

        nextActionDecided = true;
        StartCoroutine(SpawnUnit());

        // TODO: Make some other actions as the CPU player
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
}
