using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Alignment alignment;

    [HideInInspector]
    public bool isUpgraded = false;

    [SerializeField]
    private TowerStats stats;

    private void Awake()
    {
        InvokeRepeating(nameof(TryFire), 0f, stats.rateOfFire);
    }

    public int GetPrice()
    {
        return stats.cost;
    }

    /// <summary>
    /// Sell this tower. Gives the money from selling it to the player, destroys this GameObject.
    /// </summary>
    public void Sell()
    {
        var player = AlignmentHelpers.TryToFindPlayer(alignment);

        if (player != null)
        {
            player.AddCurrency(stats.sellPrice);
            Destroy(gameObject);
        }
    }

    private void TryFire()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stats.range);
        List<Unit> enemyUnits = new List<Unit>();

        foreach (Collider coll in hitColliders)
        {
            var unit = coll.gameObject.GetComponent<Unit>();

            if (unit == null || !alignment.CanHarm(unit.alignment))
            {
                continue;
            }

            enemyUnits.Add(unit);
        }

        // If there are no enemy units in range, do nothing
        if (enemyUnits.Count == 0)
        {
            return;
        }

        // Fire at the enemy unit closest to its destination (our base)
        enemyUnits.OrderBy(o => o.GetRemainingDistance()).ToList()[0].TakeDamage(stats.damage, alignment);
    }

    /// <summary>
    /// Upgrade this tower.
    /// </summary>
    public void Upgrade()
    {
        // TODO: Implement tower upgrade system.
        isUpgraded = true;
    }
}
