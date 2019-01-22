
namespace SubwayMap
{
    interface IMenu : IOptions
    {
        void ShowMenu();
        bool DetectKey();
        void ExecuteSelection(string selection, SubwayMap<char> map);
        string SelectMenu();
    }
}
