
namespace SubwayMap
{
    interface IMenu
    {
        void ShowMenu();
        bool DetectKey();
        void ExecuteSelection(string selection, SubwayMap<char> map);
        string SelectMenu();
    }
}
