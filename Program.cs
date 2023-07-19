
public static class Program {
    public static readonly Random Rng = new Random();
    private static readonly System.Timers.Timer timer = new();

    private const int FPS = 10;
    private const int START_ITEM_COUNT = 50;
    public const int MAP_SIZE_X = 40;
    public const int MAP_SIZE_Y = 40;

    public static bool CollisionSystem = false;

    private static readonly Item[] _items = new Item[START_ITEM_COUNT];
    private static int _rocks;
    private static int _papers;
    private static int _scissors;

    private static readonly Dictionary<(int X, int Y), Item> _occupiedPositions = new();

    public static void Main() {
        Console.CursorVisible = false;
        Graphics.BorderVisible = true;

        InitRandom();
        CountItemTypes();

        Graphics.ExtendBuffer(MAP_SIZE_X, MAP_SIZE_Y);

        timer.Interval = 1000.0 / (double)FPS;
        timer.Elapsed += (s, e) => Tick();
        timer.Start();

        // stop the loop if the user hit a key
        Console.ReadLine();
        End();
    }

    private static void CountItemTypes() {
        for (int i = 0; i < _items.Length; i++) {
            switch (_items[i].Type) {
                case ItemType.Rock: _rocks++; break;
                case ItemType.Paper: _papers++; break;
                case ItemType.Scissor: _scissors++; break;
            }
        }
    }

    public static void Tick() {
        // Draw
        foreach (var item in _items) {
            Graphics.UpdateItem(item);
        }

        // Calculate next state
        OffsetAll();       
        

        // quit if only one team is alive
        int deadTeams = 0;
        if (_rocks == 0 ) deadTeams++;
        if (_papers == 0 ) deadTeams++;
        if (_scissors == 0 ) deadTeams++;
        if (deadTeams >= 2) End();
    }

    public static void End() {
        timer.Stop();
        timer.Dispose();
        Console.SetCursorPosition(0, MAP_SIZE_Y + Graphics.OFFSET_Y);
        Console.WriteLine("Program stopped");
        Environment.Exit(0);
    }

    public static void TryMoveItem(Item item, (int X, int Y) newPos) {
        if (_occupiedPositions.ContainsKey(newPos)) {
            // change the team of the item
            TeamChanger.ChangeTeam(item, _occupiedPositions[newPos]);
            
            // dont update the position if the CollisionSystem is enabled
            if (CollisionSystem)
                return;
        }

        item.X = newPos.X;
        item.Y = newPos.Y;

        _occupiedPositions.Remove((item.PrevX, item.PrevY));
        _occupiedPositions.Add((item.X, item.Y), item);
    }

    private static void OffsetItem(Item item) {
        int offsetX = Rng.Next(3) - 1;
        int offsetY = Rng.Next(3) - 1;
        TryMoveItem(item, (item.X + offsetX, item.Y + offsetY));
    }
    private static void OffsetAll() {
        foreach (var item in _items) {
            OffsetItem(item);
        }
    }
    private static void DropAll() {
        foreach (var item in _items) {
            TryMoveItem(item, (item.X, item.Y+1));
        }
    }

    private static void InitRandom() {
        for (int i = 0; i < _items.Length; i++) {
            (int X, int Y) newPos = (Rng.Next(MAP_SIZE_X), Rng.Next(MAP_SIZE_Y));
            while (_occupiedPositions.ContainsKey(newPos)) {
                newPos = (Rng.Next(MAP_SIZE_X), Rng.Next(MAP_SIZE_Y));
            }
            _items[i] = new Item(newPos.X, newPos.Y);

            _occupiedPositions.Add(newPos, _items[i]);
        }
    }
}
