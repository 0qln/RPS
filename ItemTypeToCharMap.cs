public static class Map {
    private static Dictionary<ItemType, char> _map = new() {
        { ItemType.Rock, 'r' },
        { ItemType.Paper, 'p'},
        { ItemType.Scissor, 's'}
    };

    public static char ToChar(ItemType type) {
        return _map[type];
    }
}
