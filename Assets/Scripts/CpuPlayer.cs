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
        // TODO: Make some actions as the CPU player
    }
}
