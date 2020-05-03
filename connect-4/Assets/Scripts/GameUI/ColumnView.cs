using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColumnView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int ColumnIndex;

    [SerializeField] private GameBoardUIController _gameBoardUiController;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Column {ColumnIndex} Clicked!");
        _gameBoardUiController.OnColumnClicked(this, ColumnIndex);
    }

    public void AddPiece(GameObject pieceGo)
    {
        pieceGo.transform.SetParent(transform);
        pieceGo.transform.SetSiblingIndex(0);
    }
}
