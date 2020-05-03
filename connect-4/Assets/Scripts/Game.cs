using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private const int WinLength = 4;
    private readonly int _width;
    private readonly int _height;
    /// <summary>
    /// 2x2 array. Outer array index is x. Inner array index is y. 0,0 is bottom left of the board
    /// 0 means no piece, otherwise the value is player id
    /// </summary>
    private List<List<int>> _gameboard;

    public Game(int width, int height)
    {
        _width = width;
        _height = height;
        InitGameBoard(width, height);
    }

    public bool CanPlayAtColumn(int column)
    {
        return NextAtColumn(column) < _height;
    }

    public void PlayAt(int player, int column)
    {
        if (column < 0 || column > _width)
        {
            return;
        }

        if (!CanPlayAtColumn(column))
        {
            return;
        }

        _gameboard[column][NextAtColumn(column)] = player;
    }

    /// <summary>
    /// Find the next available spot at column
    /// </summary>
    /// <param name="column">index of column</param>
    /// <returns>next available row index, returns height if column is full</returns>
    private int NextAtColumn(int column)
    {
        int height = 0;
        for (height = 0; height < _height; height++)
        {
            if (_gameboard[column][height] == 0)
            {
                break;
            }
        }

        return height;
    }
    
    private void InitGameBoard(int width, int height)
    {
        _gameboard = new List<List<int>>();
        for (int i = 0; i < width; i++)
        {
            _gameboard.Add(new List<int>(height));
            for (int j = 0; j < height; j++)
            {
                _gameboard[i].Add(0);
            }
        }
    }

    /// <summary>
    /// Check state of the game
    /// </summary>
    /// <returns>0 if no player wins</returns>
    /// <returns>playerId if playerId wins</returns>
    public int CheckState()
    {
        // check vertical
        for (int i = 0; i < _width; i++)
        {
            int count = 1;
            for (int j = 1; j < _height; j++)
            {
                if (_gameboard[i][j] != 0 && _gameboard[i][j] == _gameboard[i][j - 1])
                {
                    count++;
                    if (count >= WinLength)
                    {
                        return _gameboard[i][j];
                    }
                }
                else
                {
                    count = 1;
                }
            }
        }
        // check horizontal
        for (int j = 0; j < _height; j++)
        {
            int count = 1;
            for (int i = 1; i < _width; i++)
            {
                if (_gameboard[i][j] != 0 && _gameboard[i][j] == _gameboard[i - 1][j])
                {
                    count++;
                    if (count >= WinLength)
                    {
                        return _gameboard[i][j];
                    }
                }
                else
                {
                    count = 1;
                }
            }
        }
        // check diagonal /, i,j is bottom left position of a diagonal line, k is position on the diagonal line
        for (int i = 0; i <= _width - WinLength; i++)
        {
            for (int j = 0; j <= _height - WinLength; j++)
            {
                int count = 1;
                for (int k = 1; k < WinLength; k++)
                {
                    if (_gameboard[i + k][j + k] != 0 && 
                        _gameboard[i + k][j + k] == _gameboard[i + k - 1][j + k - 1])
                    {
                        count++;
                        if (count >= WinLength)
                        {
                            return _gameboard[i][j];
                        }
                    }
                    else
                    {
                        count = 1;
                    }
                }
            }
        }
        // check diagonal \, i,j is bottom right
        for (int i = WinLength - 1; i < _width; i++)
        {
            for (int j = 0; j < _height - WinLength + 1; j++)
            {
                int count = 1;
                for (int k = 1; k < WinLength; k++)
                {
                    if (_gameboard[i - k][j + k] != 0 &&
                        _gameboard[i - k][j + k] == _gameboard[i - k + 1][j + k - 1])
                    {
                        count++;
                        if (count >= WinLength)
                        {
                            return _gameboard[i][j];
                        }
                    }
                    else
                    {
                        count = 1;
                    }
                }
            }
        }
        return 0;
    }
    
}
