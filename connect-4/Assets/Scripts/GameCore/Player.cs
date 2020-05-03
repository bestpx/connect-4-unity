using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private readonly int _id;
    
    public Player(int id)
    {
        _id = id;
    }

    public int Id => _id;
}
