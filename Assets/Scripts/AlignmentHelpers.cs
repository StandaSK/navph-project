using UnityEngine;

public static class AlignmentHelpers
{
    /// <summary>
    /// Tries to find an enemy <see cref="BaseTile"/> with the specified <paramref name="alignment"/>.
    /// </summary>
    /// <param name="alignment">Alignment of the unit that's looking for the enemy base</param>
    /// <returns><c><see cref="BaseTile"/></c> if it finds the enemy base tile, <c>null</c> if not.</returns>
    public static BaseTile TryToFindEnemyBaseTile(Alignment alignment)
    {
        var bases = GameObject.FindGameObjectsWithTag("Base");

        if (bases == null || bases.Length == 0)
        {
            Debug.LogError("Could not find any GameObjects with the \"Base\" Tag!\n");
            return null;
        }

        foreach (GameObject go in bases)
        {
            var baseTile = go.GetComponentInChildren<BaseTile>();

            if (baseTile == null)
            {
                Debug.LogError("GameObject \"" + go.name + "\" has a \"Base\" tag, but is missing a \"BaseTile\" component!");
                continue;
            }

            if (alignment.CanHarm(baseTile.alignment))
            {
                return baseTile;
            }
        }

        return null;
    }

    /// <summary>
    /// Tries to find a player's <see cref="BaseTile"/> with the specified <paramref name="alignment"/>.
    /// </summary>
    /// <param name="alignment">Alignment of the BaseTile we're looking for</param>
    /// <returns><c><see cref="BaseTile"/></c> if it finds the player's base tile, <c>null</c> if not.</returns>
    public static BaseTile TryToFindPlayersBaseTile(Alignment alignment)
    {
        var bases = GameObject.FindGameObjectsWithTag("Base");

        if (bases == null || bases.Length == 0)
        {
            Debug.LogError("Could not find any GameObjects with the \"Base\" Tag!\n");
            return null;
        }

        foreach (GameObject go in bases)
        {
            var baseTile = go.GetComponentInChildren<BaseTile>();

            if (baseTile == null)
            {
                Debug.LogError("GameObject \"" + go.name + "\" has a \"Base\" tag, but is missing a \"BaseTile\" component!");
                continue;
            }

            // TODO: Find a better way to check if it's the same alignment
            if (!alignment.CanHarm(baseTile.alignment))
            {
                return baseTile;
            }
        }

        return null;
    }

    /// <summary>
    /// Tries to find a player with the specified <paramref name="alignment"/>.
    /// </summary>
    /// <param name="alignment">Alignment of the player we're looking for</param>
    /// <returns><c><see cref="Player"/></c> if it finds the player, <c>null</c> if not.</returns>
    public static Player TryToFindPlayer(Alignment alignment)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");

        if (players == null || players.Length == 0)
        {
            Debug.LogError("Could not find any GameObjects with the \"Player\" Tag!");
            return null;
        }

        foreach (var go in players)
        {
            var player = go.GetComponent<Player>();

            if (player == null)
            {
                Debug.LogError("GameObject \"" + go.name + "\" has a \"Player\" tag, but is missing a \"Player\" component!");
                continue;
            }

            // TODO: Find a better way to check if it's the same alignment
            if (!alignment.CanHarm(player.alignment))
            {
                return player;
            }
        }

        return null;
    }
}
