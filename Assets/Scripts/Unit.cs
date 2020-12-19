using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour
{
    public Alignment alignment = null;

    [SerializeField]
    private UnitStats stats;

    private BaseTile destinationBase;
    private int healthPoints;
    private NavMeshAgent navMeshAgent;
    private Collider unitCollider;

    private void Awake()
    {
        healthPoints = stats.healthPoints;
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitCollider = GetComponent<Collider>();

        if (alignment == null)
        {
            Debug.LogError("The unit \"" + name + "\" doesn't have an alignment!\nThe unit doesn't know where to navigate to.");
            return;
        }

        var enemyBase = AlignmentHelpers.TryToFindEnemyBaseTile(alignment);

        if (enemyBase != null)
        {
            destinationBase = enemyBase;
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

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter");
        ContactPoint contact = collision.contacts[0];

        var otherUnit = contact.otherCollider.gameObject.GetComponentInChildren<Unit>();

        // Ignore collisions with anything but enemy units
        if (otherUnit == null || !otherUnit.alignment.CanHarm(alignment))
        {
            return;
        }

        Debug.Log("OnCollisionEnter");
        navMeshAgent.isStopped = true;
        StartCoroutine(UnStopAgent(1f));
        contact.otherCollider.gameObject.SetActive(false);
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

    /// <summary>
    /// Unstops the agent after <paramref name="time"/> has passed.
    /// </summary>
    /// <param name="time">Time after which the agent will be unstopped.</param>
    /// <returns></returns>
    private IEnumerator UnStopAgent(float time)
    {
        yield return new WaitForSeconds(time);
        navMeshAgent.isStopped = false;
    }

}
