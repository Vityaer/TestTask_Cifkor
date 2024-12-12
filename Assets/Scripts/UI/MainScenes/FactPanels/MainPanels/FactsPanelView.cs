using UI.Abstractions.Views;
using UI.MainScenes.FactPanels.FactContainerViews;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScenes.FactPanels.MainPanels
{
    public class FactsPanelView : UiView
    {
        [field: SerializeField] public ScrollRect Scroll { get; private set; }
        [field: SerializeField] public FactContainerView FactContainerPrefab { get; private set; }
        [field: SerializeField] public GameObject LoadingPanel { get; private set; }
        [field: SerializeField] public GameObject ScrollPanel { get; private set; }
    }
}
