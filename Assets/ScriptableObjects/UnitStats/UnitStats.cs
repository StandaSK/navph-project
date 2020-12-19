using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats.asset", menuName = "Unit Stats", order = 4)]
public class UnitStats : ScriptableObject
{
    /// <summary>
    /// Name of the unit in English.
    /// </summary>
    [Tooltip("Name of the unit in English.")]
    public string nameEn;

    /// <summary>
    /// How much it costs to spawn the unit.
    /// </summary>
    [Tooltip("How much it costs to spawn the unit.")]
    public int cost;

    /// <summary>
    /// How much the enemy earns for killing the unit.
    /// </summary>
    [Tooltip("How much the enemy earns for killing the unit.")]
    public int loot;

    /// <summary>
    /// How much damage the unit deals to the enemy base.
    /// </summary>
    [Tooltip("How much damage the unit deals to the enemy base.")]
    public int baseDamage;

    /// <summary>
    /// How much damage the unit deals in one shot.
    /// </summary>
    [Tooltip("How much damage the unit deals in one shot.")]
    public int damage;

    /// <summary>
    /// How many shots per second can the unit fire.
    /// </summary>
    [Tooltip("How many shots per second can the unit fire.")]
    public float rateOfFire;

    /// <summary>
    /// How much health the unit has.
    /// </summary>
    [Tooltip("How much health the unit has.")]
    public int healthPoints;

    /// <summary>
    /// How long it takes to train the unit.
    /// </summary>
    [Tooltip("How long it takes to train the unit.")]
    public int trainingTime;

    /// <summary>
    /// Short description of the unit in English.
    /// </summary>
    [TextArea(1, 5)]
    [Tooltip("Short description of the unit in English.")]
    public string descriptionEn;
}
