using UnityEngine;

namespace CardGame.SaveSystem
{
    [CreateAssetMenu(menuName = "SaveSystems", fileName = "MemorySaveSystem")]
    public class MemorySaveSystem : BaseSaveSystem
    {
        public override void Save(GameState gameState)
        {
            Debug.Log("Saved from memory save system");
        }

        public override GameState Load()
        {
            Debug.Log("Loaded from memory save system");
            return new GameState();
        }
    }
}