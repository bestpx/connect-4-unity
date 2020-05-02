using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private const int WinLength = 4;
    private int _width;
    private int _height;
    private List<List<int>> _gameboard;

    private void PlayAt(int player, int column)
    {
        if (column < 0 || column > _width)
        {
            return;
        }

        if (_gameboard[column].Count >= _height)
        {
            return;
        }
        
        _gameboard[column].Add(player);
    }
    
    private void InitGameBoard(int width, int height)
    {
        _gameboard = new List<List<int>>();
        for (int i = 0; i < width; i++)
        {
            _gameboard.Add(new List<int>(height));
        }
    }

    /// <summary>
    /// Check state of the game
    /// </summary>
    /// <returns>0 if no player wins</returns>
    /// <returns>playerId if playerId wins</returns>
    private int CheckState()
    {
        // check vertical
        for (int i = 0; i < _width; i++)
        {
            int count = 1;
            for (int j = 1; j < _height; j++)
            {
                if (_gameboard[i][j] == _gameboard[i][j - 1])
                {
                    count++;
                }
                else
                {
                    count = 1;
                }

                if (count >= WinLength)
                {
                    return _gameboard[i][j];
                }
            }
        }
        // check horizontal
        for (int j = 0; j < _height; j++)
        {
            for (int i = 1; i < _width; i++)
            {
                int count = 1;
                if (_gameboard[i][j] == _gameboard[i - 1][j])
                {
                    count++;
                }
                else
                {
                    count = 1;
                }

                if (count >= WinLength)
                {
                    return _gameboard[i][j];
                }
            }
        }
        // check diagonal /
        for (int i = 0; i <= _width - WinLength; i++)
        {
            for (int j = 0; j <= _height - WinLength; j++)
            {
                int count = 1;
                for (int k = 1; k < WinLength; k++)
                {
                    if (_gameboard[i + k][j + k] == _gameboard[i + k - 1][j + k - 1])
                    {
                        count++;
                    }
                }

                if (count >= WinLength)
                {
                    return _gameboard[i][j];
                }
            }
        }
        // check diagonal \
        for (int i = WinLength - 1; i <= _width; i++)
        {
            for (int j = WinLength - 1; j <= _height; j++)
            {
                int count = 1;
                for (int k = 1; k < WinLength; k++)
                {
                    if (_gameboard[i + k][j - k] == _gameboard[i + k - 1][j - k + 1])
                    {
                        count++;
                    }
                }

                if (count >= WinLength)
                {
                    return _gameboard[i][j];
                }
            }
        }
        return 0;
    }
    
}
