using TMPro;
using UI.Abstractions.Views;
using UnityEngine.UI;

namespace UI.MainScenes.MainMunuPanels.MenuHudButtons.MenuButtons
{
    public class MenuButton : UiView
    {
        public Button Button;
        public Image Icon;
        public TMP_Text ButtonName;

        public void OnSelect()
        {
            ButtonName.gameObject.SetActive(true);
            Button.interactable = false;
        }

        public void OnDiselect()
        {
            ButtonName.gameObject.SetActive(false);
            Button.interactable = true;
        }
    }
}
