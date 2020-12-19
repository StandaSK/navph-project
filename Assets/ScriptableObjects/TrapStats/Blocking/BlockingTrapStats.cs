using UnityEngine;

[CreateAssetMenu(fileName = "BlockingTrapStats.asset", menuName = "Trap Stats/Blocking", order = 3)]
public class BlockingTrapStats : TrapStats
{
    /// <summary>
    /// How much health the trap has.
    /// </summary>
    [Tooltip("How much health the trap has.")]
    public int healthPoints;
}
