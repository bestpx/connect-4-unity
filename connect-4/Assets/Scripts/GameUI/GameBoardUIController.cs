using System.Collections;
using System.Collections.Generic;
using GameCore;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBoardUIController : MonoBehaviour
{
    [SerializeField] private GameObject _piece1;
    [SerializeField] private GameObject _piece2;
    [SerializeField] private GameObject _inputBlocker;
    [SerializeField] private TextMeshProUGUI _gameStateText;
    [SerializeField] private TextMeshProUGUI _currentTurnText;

    private GameController _gameController;
    
    void Awake()
    {
        _gameController = new GameController();
        _gameController.Initialize();
        _inputBlocker.SetActive(false);
        _gameStateText.text = "";
        _currentTurnText.text = "Player1's turn";
    }

    public void OnColumnClicked(ColumnView column, int columnId)
    {
        var currentPlayer = _gameController.CurrentPlayer;
        if (_gameController.TryPlayAtColumn(columnId) == -1)
        {
            return;
        }
        // current player changed after playing turn
        var nextPlayer = _gameController.CurrentPlayer;
        // update UI
        var prefab = _gameController.CurrentPlayer.Id == 1 ? _piece1 : _piece2;
        var go = Instantiate(prefab);
        column.AddPiece(go);
        // check game state
        var state = _gameController.GetGameState();
        if (state == -1)
        {
            Debug.Log("Ties");
            _inputBlocker.SetActive(true);
            _gameStateText.text = "Tie!";
        }
        else if (state != 0)
        {
            Debug.Log($"Player {state} wins");
            _inputBlocker.SetActive(true);
            _gameStateText.text = $"Player{state} Won!";
        }
        
        // update UI post play turn
        _currentTurnText.text = $"Player{nextPlayer.Id}'s turn";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
