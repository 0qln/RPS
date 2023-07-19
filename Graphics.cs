public static class Graphics {
    private static bool _borderVisible = false;
    private static int _borderThickness = 0;

    public static int OFFSET_X { get; } = Console.GetCursorPosition().Left;
    public static int OFFSET_Y { get; } = Console.GetCursorPosition().Top;
    public static bool BorderVisible { get => _borderVisible; set {
        _borderVisible = value;
        _borderThickness = _borderVisible ? 1 : 0;
        if (_borderVisible) RenderBorder();
        else RemoveBorder();
    }} 
    public static char BorderTop        = '═'; 
    public static char BorderTopLeft    = '╔'; 
    public static char BorderLeft       = '║'; 
    public static char BorderLeftBottom = '╚'; 
    public static char BorderBottom     = '═'; 
    public static char BorderBottomRight= '╝'; 
    public static char BorderRight      = '║'; 
    public static char BorderRightTop   = '╗';



    public static void RenderBorder() {
        // Corners
        //DrawChar(0, 0, BorderTopLeft);
        
        Console.SetCursorPosition(OFFSET_X, OFFSET_Y);
        Console.Write(BorderTopLeft);

        Console.SetCursorPosition(OFFSET_X + Program.MAP_SIZE_X * 2 + 2 + 1, OFFSET_Y);
        Console.Write(BorderRightTop);

        Console.SetCursorPosition(OFFSET_X + Program.MAP_SIZE_X * 2 + 2 + 1, OFFSET_Y + Program.MAP_SIZE_Y + 1 + 1);
        Console.Write(BorderBottomRight);

        Console.SetCursorPosition(OFFSET_X, OFFSET_Y + Program.MAP_SIZE_Y + 1 + 1);
        Console.Write(BorderLeftBottom);
        
        // Top and Bottom
        for (int i = 1; i < Program.MAP_SIZE_X * 2 + 2 + 1; i++) {
            Console.SetCursorPosition(i + OFFSET_X, OFFSET_Y);
            Console.Write(BorderTop);
            Console.SetCursorPosition(i + OFFSET_X, OFFSET_Y + Program.MAP_SIZE_Y + 1 + 1);
            Console.Write(BorderBottom);
        } 
        for (int i = 1; i < Program.MAP_SIZE_Y + 1 + 1; i++) {
            Console.SetCursorPosition(OFFSET_X, i + OFFSET_Y);
            Console.Write(BorderLeft);
            Console.SetCursorPosition(OFFSET_X + Program.MAP_SIZE_X * 2 + 2 + 1, i + OFFSET_Y);
            Console.Write(BorderRight);
        } 
    }
    public static void RemoveBorder() {

    }

    public static void ExtendBuffer(int x, int y) {
        Console.SetBufferSize(Console.BufferWidth + x, Console.BufferHeight + y);
    }

    public static void DrawChar(int x, int y, char c) {
        Console.SetCursorPosition(2*x + OFFSET_X + _borderThickness, y + OFFSET_Y + _borderThickness);
        Console.Write(new String(new char[] { c, ' ' } ));
    }

    public static char ReadChar(int x, int y) {
        Console.SetCursorPosition(2*x + OFFSET_X + _borderThickness, y + OFFSET_Y + _borderThickness);
        return Convert.ToChar(Console.Read());
    }

    public static void DrawChars((int X, int Y)[] positions, char c) {
        for (int i = 0; i < positions.Length; i++) {
            Graphics.DrawChar(positions[i].X, positions[i].Y, c);
        }
    }

    public static void MarkPositions(HashSet<(int X, int Y)> set) {
        foreach(var position in set) {
            DrawChar(position.X, position.Y, 'X');
        }
    }

    public static void MoveItem(int x, int y, int newX, int newY) {
        char c = ReadChar(x, y);
        DrawChar(x, y, ' ');
        DrawChar(newX, newY, c);
    }

    public static void UpdateItem(Item item) {
        DrawChar(item.PrevX, item.PrevY, ' ');
        DrawChar(item.X, item.Y, Map.ToChar(item.Type));
    }

    public static void Clear() {
        for (int x = 0; x < Program.MAP_SIZE_X; x++) {
            for (int y = 0; y < Program.MAP_SIZE_Y; y++) {
                DrawChar(x, y, ' ');
            }
        }
    }

    public static void DrawItems(Item[] items) {
        for (int i = 0; i < items.Length; i++) {
            char c = Map.ToChar(items[i].Type);
            DrawChar(items[i].X, items[i].Y, c);
        }
    }
}
