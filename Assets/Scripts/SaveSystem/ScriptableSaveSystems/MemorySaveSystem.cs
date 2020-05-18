using UnityEngine;

namespace CardGame.SaveSystem
{
    [CreateAssetMenu(menuName = "SaveSystems/MemorySaveSystem ", fileName = "MemorySaveSystem")]
    public class MemorySaveSystem : BaseSaveSystem
    {
        private GameState _gameState;
        
        public override void Save(GameState gameState) => _gameState = gameState;

        public override GameState Load() => _gameState;

        public override void DeleteAll() => _gameState = null;
    }
}