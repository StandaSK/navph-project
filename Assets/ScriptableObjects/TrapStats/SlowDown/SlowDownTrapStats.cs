using UnityEngine;

[CreateAssetMenu(fileName = "SlowDownTrapStats.asset", menuName = "Trap Stats/Slow Down", order = 3)]
public class SlowDownTrapStats : TrapStats
{
    /// <summary>
    /// How many seconds will the unit that steps on this trap be slow down.
    /// </summary>
    [Tooltip("How many seconds will the unit that steps on this trap be slow down.")]
    public float slowdownTime;

    /// <summary>
    /// How much will the unit that steps on this trap be slow down.
    /// </summary>
    [Tooltip("How much will the unit that steps on this trap be slow down.")]
    public float slowdownRate;

    /// <summary>
    /// How many times can this trap affect units.
    /// </summary>
    [Tooltip("How many times can this trap affect units.")]
    public int useCount;
}
