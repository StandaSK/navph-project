using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private TrapStats stats;

    public int GetPrice()
    {
        return stats.cost;
    }
}
