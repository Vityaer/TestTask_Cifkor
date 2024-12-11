using System.Collections.Generic;

namespace Models.MainMenuButtons
{
    public interface IMenuButtonsDataSo
    {
        Dictionary<string, MenuButtonData> ButtonDatas { get; }
    }
}
