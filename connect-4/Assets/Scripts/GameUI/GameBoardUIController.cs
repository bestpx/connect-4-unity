using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBoardUIController : MonoBehaviour
{
    [SerializeField] private GameObject _piece1;
    [SerializeField] private GameObject _piece2;
    [SerializeField] private GameObject _inputBlocker;
    [SerializeField] private TextMeshProUGUI _gameStateText;
    
    private Game _game;
    private Player _player1;
    private Player _player2;
    private Player _currentPlayer;

    void Awake()
    {
        _game = new Game(7,6);
        _player1 = new Player(1);
        _player2 = new Player(2);
        _currentPlayer = _player1;
        _inputBlocker.SetActive(false);
        _gameStateText.text = "";
    }

    public void OnColumnClicked(ColumnView column, int columnId)
    {
        if (!_game.CanPlayAtColumn(columnId))
        {
            return;
        }
        _game.PlayAt(_currentPlayer.Id, columnId);
        
        // update UI
        var prefab = _currentPlayer == _player1 ? _piece1 : _piece2;
        var go = Instantiate(prefab);
        column.AddPiece(go);
        
        // check game state
        var state = _game.CheckState();
        if (state != 0)
        {
            Debug.Log($"Player {_currentPlayer.Id} wins");
            _inputBlocker.SetActive(true);
            _gameStateText.text = $"Player{_currentPlayer.Id} Won!";
        }
        else
        {
            _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
