using UnityEngine;

public abstract class TrapStats : ScriptableObject
{
    /// <summary>
    /// Name of the trap in English.
    /// </summary>
    [Tooltip("Name of the trap in English.")]
    public string nameEn;

    /// <summary>
    /// How much it costs to build the trap.
    /// </summary>
    [Tooltip("How much it costs to build the trap.")]
    public int cost;

    /// <summary>
    /// Short description of the trap in English.
    /// </summary>
    [TextArea(1, 5)]
    [Tooltip("Short description of the trap in English.")]
    public string descriptionEn;
}
