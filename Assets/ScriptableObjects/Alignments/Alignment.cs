/*
 * Taken from Unity Tower Defense Template
 * https://assetstore.unity.com/packages/essentials/tutorial-projects/tower-defense-template-107692
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object that defines which "team" it belongs to and which other alignments it can harm.
/// </summary>
[CreateAssetMenu(fileName = "Alignment.asset", menuName = "Alignment", order = 1)]
public class Alignment : ScriptableObject
{
    /// <summary>
    /// A collection of other alignment objects that we can harm
    /// </summary>
    public List<Alignment> opponents;

    /// <summary>
    /// Gets whether the given alignment is in our known list of opponents
    /// </summary>
    public bool CanHarm(Alignment other)
    {
        if (other == null)
        {
            return true;
        }

        return opponents.Contains(other);
    }
}
