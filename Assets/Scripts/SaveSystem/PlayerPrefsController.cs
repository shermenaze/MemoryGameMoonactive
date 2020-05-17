using System;
using UnityEngine;
using UnityEngine.Events;

namespace CardGame.SaveSystem
{
    public class PlayerPrefsController : MonoBehaviour
    {
        #region engine

        private void Awake()
        {
            InvokeAwake();
        }

        private void OnEnable()
        {
            InvokeEnabled();
        }

        #endregion // engine
        
        [ContextMenu("Delete All")]
        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        
        [Serializable]
        public struct PlayerPrefsInvoke
        {
            public enum ExecutionOrder
            {
                None,
                Awake,
                Enabled
            }

            public ExecutionOrder whenToRunThis;
            public string key;

            public void Invoke(UnityEvent existsEvent, StringEvent stringEvent, IntEvent intEvent,
                FloatEvent floatEvent)
            {
                if (string.IsNullOrEmpty(key)) return;

                if (PlayerPrefs.HasKey(key)) existsEvent.Invoke();

                Debug.Log(PlayerPrefs.GetString(key));
                Debug.Log(PlayerPrefs.GetInt(key));
                Debug.Log(PlayerPrefs.GetFloat(key));
            }
        }

        #region player prefs saver

        [Serializable]
        public struct PlayerPrefsSaver
        {
            public enum ValueType
            {
                String,
                Int,
                Float
            }

            public string _key;
            public ValueType _valueType;
            public string _stringValue;
            public int _intValue;
            public float _floatValue;

            public void Save()
            {
                if (string.IsNullOrEmpty(_key)) return;

                switch (_valueType)
                {
                    case ValueType.String:
                        PlayerPrefs.SetString(_key, _stringValue);
                        return;
                    case ValueType.Int:
                        PlayerPrefs.SetInt(_key, _intValue);
                        return;
                    case ValueType.Float:
                        PlayerPrefs.SetFloat(_key, _floatValue);
                        return;
                }
            }
        }

        #endregion // player prefs saver

        #region fields

        [SerializeField] private PlayerPrefsInvoke _playerPrefsInvoker;
        [SerializeField] private PlayerPrefsSaver _playerPrefsSaver;
        [SerializeField] private UnityEvent _existsEvent;
        [SerializeField] private StringEvent _stringEvent;
        [SerializeField] private IntEvent _intEvent;
        [SerializeField] private FloatEvent _floatEvent;

        #endregion // fields

        #region invoke

        public void Invoke()
        {
            _playerPrefsInvoker.Invoke(_existsEvent, _stringEvent, _intEvent, _floatEvent);
        }

        public void InvokeNone()
        {
            if (_playerPrefsInvoker.whenToRunThis == PlayerPrefsInvoke.ExecutionOrder.None)
                _playerPrefsInvoker.Invoke(_existsEvent, _stringEvent, _intEvent, _floatEvent);
        }

        public void InvokeAwake()
        {
            if (_playerPrefsInvoker.whenToRunThis == PlayerPrefsInvoke.ExecutionOrder.Awake)
                _playerPrefsInvoker.Invoke(_existsEvent, _stringEvent, _intEvent, _floatEvent);
        }

        public void InvokeEnabled()
        {
            if (_playerPrefsInvoker.whenToRunThis == PlayerPrefsInvoke.ExecutionOrder.Enabled)
                _playerPrefsInvoker.Invoke(_existsEvent, _stringEvent, _intEvent, _floatEvent);
        }

        #endregion // invoke

        public void Save()
        {
            _playerPrefsSaver.Save();
        }
    }
}