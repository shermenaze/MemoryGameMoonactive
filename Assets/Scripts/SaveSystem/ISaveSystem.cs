namespace CardGame.SaveSystem
{
    public interface ISaveSystem
    {
        void Save(GameState gameState);
        GameState Load();
    }
}