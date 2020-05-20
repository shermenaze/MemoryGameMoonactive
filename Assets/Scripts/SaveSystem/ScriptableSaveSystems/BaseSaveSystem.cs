using UnityEngine;

namespace CardGame.SaveSystem
{
    public abstract class BaseSaveSystem : ScriptableObject, ISaveSystem
    {
        public abstract void Save(GameState gameState);

        public abstract GameState Load();
        
        public abstract void DeleteAll();
    }
}