using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private BoardSo _boardData;
}

[CreateAssetMenu(menuName = "Boards", fileName = "Board")]
public class BoardSo : ScriptableObject
{
    [SerializeField] private Transform _layout;
}
