public class Item {
    private int _x = 0;
    private int _y = 0;

    public ItemType Type;
    public int X { get => _x; set {
        PrevX = _x;
        _x = (value <= Program.MAP_SIZE_X && value >= 0) 
            ? value 
            : _x; 
    }}
    public int Y { get => _y; set {
        PrevY = _y;
        _y = (value <= Program.MAP_SIZE_Y && value >= 0) 
            ? value 
            : _y; 
    }}
    public int PrevX { get; private set; }
    public int PrevY { get; private set; }

    public Item(int x, int y) {
        X = x;
        Y = y;
        Type = (ItemType) Program.Rng.Next(3);
    }
}
