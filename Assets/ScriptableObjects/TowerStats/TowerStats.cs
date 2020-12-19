using UnityEngine;

/// <summary>
/// Scriptable object that defines tower characteristics.
/// </summary>
[CreateAssetMenu(fileName = "TowerStats.asset", menuName = "Tower Stats", order = 2)]
public class TowerStats : ScriptableObject
{
    /// <summary>
    /// Name of the tower in English.
    /// </summary>
    [Tooltip("Name of the tower in English.")]
    public string nameEn;

    /// <summary>
    /// How much it costs to build the tower.
    /// </summary>
    [Tooltip("How much it costs to build the tower.")]
    public int cost;

    /// <summary>
    /// How much money you will earn by selling the tower.
    /// </summary>
    [Tooltip("How much money you will earn by selling the tower.")]
    public int sellPrice;

    /// <summary>
    /// How much damage the tower deals in one shot.
    /// </summary>
    [Tooltip("How much damage the tower deals in one shot.")]
    public int damage;

    /// <summary>
    /// Range of the tower's attacks.
    /// </summary>
    [Tooltip("Range of the tower's attacks.")]
    public float range;

    /// <summary>
    /// How many seconds between shots.
    /// </summary>
    [Tooltip("How many seconds between shots.")]
    public float rateOfFire;

    /// <summary>
    /// Short description of the tower in English.
    /// </summary>
    [TextArea(1, 5)]
    [Tooltip("Short description of the tower in English.")]
    public string descriptionEn;
}
