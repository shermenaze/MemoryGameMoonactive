using UnityEngine;
using UnityEngine.Events;


public class PlayerPrefsController : MonoBehaviour
{
    #region player prefs invoke

    [System.Serializable]
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

        public void Invoke(UnityEvent existsEvent, StringEvent stringEvent, IntEvent intEvent, FloatEvent floatEvent)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (PlayerPrefs.HasKey(key))
            {
                existsEvent.Invoke();
            }

            Debug.Log(PlayerPrefs.GetString(key));
            Debug.Log(PlayerPrefs.GetInt(key));
            Debug.Log(PlayerPrefs.GetFloat(key));
        }
    }

    #endregion // player prefs invoke

    #region player prefs saver

    [System.Serializable]
    public struct PlayerPrefsSaver
    {
        public enum ValueType
        {
            String,
            Int,
            Float
        }

        public string key;
        public ValueType valueType;
        public string stringValue;
        public int intValue;
        public float floatValue;

        public void Save()
        {
            if (string.IsNullOrEmpty(key)) return;

            switch (valueType)
            {
                case ValueType.String:
                    PlayerPrefs.SetString(key, stringValue);
                    return;
                case ValueType.Int:
                    PlayerPrefs.SetInt(key, intValue);
                    return;
                case ValueType.Float:
                    PlayerPrefs.SetFloat(key, floatValue);
                    return;
            }
        }
    }

    #endregion // player prefs saver

    #region fields

    [SerializeField] private PlayerPrefsInvoke playerPrefsInvoker;
    [SerializeField] private PlayerPrefsSaver playerPrefsSaver;
    [SerializeField] private UnityEvent existsEvent;
    [SerializeField] private StringEvent stringEvent;
    [SerializeField] private IntEvent intEvent;
    [SerializeField] private FloatEvent floatEvent;

    #endregion // fields

    #region invoke

    public void Invoke()
    {
        playerPrefsInvoker.Invoke(existsEvent, stringEvent, intEvent, floatEvent);
    }

    public void InvokeNone()
    {
        if (playerPrefsInvoker.whenToRunThis == PlayerPrefsInvoke.ExecutionOrder.None)
            playerPrefsInvoker.Invoke(existsEvent, stringEvent, intEvent, floatEvent);
    }

    public void InvokeAwake()
    {
        if (playerPrefsInvoker.whenToRunThis == PlayerPrefsInvoke.ExecutionOrder.Awake)
            playerPrefsInvoker.Invoke(existsEvent, stringEvent, intEvent, floatEvent);
    }

    public void InvokeEnabled()
    {
        if (playerPrefsInvoker.whenToRunThis == PlayerPrefsInvoke.ExecutionOrder.Enabled)
            playerPrefsInvoker.Invoke(existsEvent, stringEvent, intEvent, floatEvent);
    }

    #endregion // invoke

    public void Save()
    {
        playerPrefsSaver.Save();
    }

    [ContextMenu("Delete All")]
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

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
}