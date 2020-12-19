using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour
{
    public Alignment alignment = null;

    [SerializeField]
    private UnitStats stats;

    private int healthPoints;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        healthPoints = stats.healthPoints;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (alignment == null)
        {
            Debug.LogError("The unit \"" + name + "\" doesn't have an alignment!\nThe unit doesn't know where to navigate to.");
            return;
        }

        var enemyBase = AlignmentHelpers.TryToFindEnemyBaseTile(alignment);

        if (enemyBase != null)
        {
            navMeshAgent.destination = enemyBase.transform.position;
        }
        else
        {
            Debug.LogError("The unit \"" + name + "\" couldn't find an enemy base!\nThe unit doesn't know where to navigate to.");
            return;
        }
    }

    public int GetBaseDamage()
    {
        return stats.baseDamage;
    }

    public int GetPrice()
    {
        return stats.cost;
    }

    public float GetRemainingDistance()
    {
        return navMeshAgent.remainingDistance;
    }

    public void TakeDamage(int damage, Alignment takenFrom)
    {
        if (damage >= healthPoints)
        {
            // The unit is killed
            healthPoints = 0;

            var enemyPlayer = AlignmentHelpers.TryToFindPlayer(takenFrom);
            if (enemyPlayer != null)
            {
                enemyPlayer.AddCurrency(stats.loot);
            }
            else
            {
                Debug.LogError("Could not find the player that killed the unit \"" + name + "\".");
            }

            Destroy(gameObject);
        }
        else
        {
            healthPoints -= damage;
        }
    }
}
