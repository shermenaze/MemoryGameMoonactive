using System;
using UnityEngine;

namespace CardGame.SaveSystem
{
    [CreateAssetMenu(menuName = "SaveSystems/PlayerPrefSaveSystem", fileName = "PlayerPrefSaveSystem")]
    public class PlayerPrefsSaveSystem : BaseSaveSystem
    {
        #region String Consts

        private const string MUSIC_VOLUME_PREF = "Music_Volume";
        private const string PLAYER_NAME_PREF = "Player_Name";
        private const string SCORE_PREF = "Score";
        private const string TIME_REMAINING_PREF = "Time_Remaining";
        private const string MUTE_PREF = "Mute";

        #endregion

        #region Pref Setters

        private void SetPref(string key, float value) => PlayerPrefs.SetFloat(key, value);

        private void SetPref(string key, string value) => PlayerPrefs.SetString(key, value);

        private void SetPref(string key, int value) => PlayerPrefs.SetInt(key, value);

        private void SetPref(string key, bool value) => PlayerPrefs.SetInt(key, Convert.ToInt32(value));

        private bool GetBoolPref(string key, bool defaultValue = true)
        { return Convert.ToBoolean(PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue))); }
        
        #endregion

        #region Set Unity Params

        private void SetMute(bool isMute) => SetPref(MUTE_PREF, isMute);

        private void SetTimeRemaining(float time) => SetPref(TIME_REMAINING_PREF, time);

        private void SetMusicVolume(float musicVolume) => SetPref(MUSIC_VOLUME_PREF, musicVolume);

        private void SetPlayerName(string playerName) => SetPref(PLAYER_NAME_PREF, playerName);
        
        private void SetScore(int value) => SetPref(SCORE_PREF, value);
        
        #endregion
        
        public override void Save(GameState gameState) 
        {
            SetTimeRemaining(gameState.TimeRemaining);
            SetMusicVolume(gameState.MusicVolume);
            SetPlayerName(gameState.PlayerName);
            SetMute(gameState.Mute);
            SetScore(gameState.Score);
        }

        public override GameState Load()
        {
            if(!PlayerPrefs.HasKey(PLAYER_NAME_PREF)) return null;

            var gameState = new GameState
            {
                TimeRemaining = PlayerPrefs.GetFloat(TIME_REMAINING_PREF),
                Mute = GetBoolPref(MUTE_PREF),
                MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF),
                PlayerName = PlayerPrefs.GetString(PLAYER_NAME_PREF),
                Score = PlayerPrefs.GetInt(SCORE_PREF),
            };

            return gameState;
        }

        public override void DeleteAll()
        {
            if(!PlayerPrefs.HasKey(MUTE_PREF)) return;

            PlayerPrefs.DeleteKey(TIME_REMAINING_PREF);
            PlayerPrefs.DeleteKey(MUTE_PREF);
            PlayerPrefs.DeleteKey(MUSIC_VOLUME_PREF);
            PlayerPrefs.DeleteKey(PLAYER_NAME_PREF);
            PlayerPrefs.DeleteKey(SCORE_PREF);
        }
    }
}