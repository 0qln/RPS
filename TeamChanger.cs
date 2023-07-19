public static class TeamChanger {
    private static Dictionary<(ItemType, ItemType), (ItemType, ItemType)> _map = new() {
         {(ItemType.Rock, ItemType.Rock), (ItemType.Rock, ItemType.Rock)},
         {(ItemType.Rock, ItemType.Paper), (ItemType.Paper, ItemType.Paper)},
         {(ItemType.Rock, ItemType.Scissor), (ItemType.Rock, ItemType.Rock)},

         {(ItemType.Paper, ItemType.Rock), (ItemType.Paper, ItemType.Paper)},
         {(ItemType.Paper, ItemType.Paper), (ItemType.Paper, ItemType.Paper)},
         {(ItemType.Paper, ItemType.Scissor), (ItemType.Scissor, ItemType.Scissor)},

         {(ItemType.Scissor, ItemType.Rock), (ItemType.Rock, ItemType.Rock)},
         {(ItemType.Scissor, ItemType.Paper), (ItemType.Scissor, ItemType.Scissor)},
         {(ItemType.Scissor, ItemType.Scissor), (ItemType.Scissor, ItemType.Scissor)}
    };

    public static void ChangeTeam(Item item1, Item item2) {
        item1.Type = _map[(item1.Type, item2.Type)].Item1;
        item2.Type = _map[(item1.Type, item2.Type)].Item2;
    } 
}
