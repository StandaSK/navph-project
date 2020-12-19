using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CurrencyUI : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private Text uiText;

    private void Awake()
    {
        uiText = GetComponent<Text>();
        player.currencyChanged.AddListener(UpdateUiText);
    }

    private void UpdateUiText()
    {
        uiText.text = "Currency: " + player.GetCurrency();
    }
}
