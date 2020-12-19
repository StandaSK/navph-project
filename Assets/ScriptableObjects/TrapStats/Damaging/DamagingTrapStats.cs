using UnityEngine;

[CreateAssetMenu(fileName = "DamagindTrapStats.asset", menuName = "Trap Stats/Damaging", order = 3)]
public class DamagingTrapStats : TrapStats
{
    /// <summary>
    /// How much damage does this trap deal to the unit that steps on it.
    /// </summary>
    [Tooltip("")]
    public int damageDealt;

    /// <summary>
    /// How many times can this trap affect units.
    /// </summary>
    [Tooltip("How many times can this trap affect units.")]
    public int useCount;
}
