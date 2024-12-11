using TMPro;
using UI.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScenes.WeatherPanels
{
    public class WeatherPanelView : UiView
    {
        [field: SerializeField] public TMP_Text WeatherText { get; private set; }
        [field: SerializeField] public Image WeatherImage { get; private set; }
    }
}
