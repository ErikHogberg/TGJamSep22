using TMPro;
using UnityEngine;

public class SetTextHelper : MonoBehaviour
{
    public TMP_Text Text;

    public string Prefix;
    public string Suffix;

    public string NumberFormat = "0.00";

    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
    }

    public void SetText(float number)
    {
        if (!Text)
        {
            Debug.LogWarning("No text for text setter");
            return;
        }

        Text.SetText($"{Prefix}{number.ToString(NumberFormat)}{Suffix}");

    }

}
