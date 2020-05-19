namespace CardGame.SaveSystem
{
    [System.Serializable]
    public class GameState
    {
        public string PlayerName { get; set; }
        public bool Mute { get; set; }
        public float TimeRemaining { get; set; }
        public float MusicVolume { get; set; }
        public int Score { get; set; }
    }
}