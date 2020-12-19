using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Alignment alignment;

    [SerializeField]
    [Tooltip("The player's starting currency")]
    private int startingCurrency = 50;
    [SerializeField]
    [Tooltip("How much currency the player gets each bonus period.")]
    private int periodicCurrencyBonus = 5;
    [SerializeField]
    [Tooltip("How long until bonus currency starts appearing (in seconds).")]
    private float currencyBonusInitialTimeout = 2f;
    [SerializeField]
    [Tooltip("Time between currency bonus additions (in seconds).")]
    private float currencyBonusPeriod = 2f;

    /// <summary>
    /// Occurs when currency changed.
    /// </summary>
    public UnityEvent currencyChanged;

    private int currency;

    private void Awake()
    {
        currency = startingCurrency;
        currencyChanged.Invoke();
        InvokeRepeating(nameof(AddCurrencyPeriodically), currencyBonusInitialTimeout, currencyBonusPeriod);
    }

    private void Start()
    {
        /* This is called to ensure a correct value in the UI when switching scenes from the main menu */
        currencyChanged.Invoke();
    }

    public void AddCurrency(int ammount)
    {
        currency += ammount;
        currencyChanged.Invoke();
    }

    private void AddCurrencyPeriodically()
    {
        currency += periodicCurrencyBonus;
        currencyChanged.Invoke();
    }

    /// <summary>
    /// Checks whether the player can afford to buy something for the specified <paramref name="price"/>.
    /// </summary>
    /// <param name="price">How much the thing costs</param>
    /// <returns><c>true</c> if the player can afford it, <c>false</c> if not.</returns>
    public bool CanAfford(int price)
    {
        return price <= currency;
    }

    public int GetCurrency()
    {
        return currency;
    }

    /// <summary>
    /// Method for trying to buy something for the specified <paramref name="price"/>.
    /// </summary>
    /// <param name="price">How much the thing costs</param>
    /// <returns><c>true</c> if the purchase was successful, <c>false</c> if the player has insufficient funds.</returns>
    public bool TryToBuy(int price)
    {
        if (price <= currency)
        {
            currency -= price;
            currencyChanged.Invoke();
            return true;
        }

        return false;
    }
}
