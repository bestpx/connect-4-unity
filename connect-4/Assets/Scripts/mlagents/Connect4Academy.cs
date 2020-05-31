using System;
using System.Collections;
using System.Collections.Generic;
using GameCore;
using Unity.MLAgents;
using UnityEngine;

public class Connect4Academy : MonoBehaviour
{
    private GameController _gameController;
    private void Awake()
    {
        Academy.Instance.OnEnvironmentReset += OnEnvironmentReset;
    }

    private void OnEnvironmentReset()
    {
        Debug.Log("OnEnvironmentReset");
        _gameController = new GameController();
        _gameController.Initialize();
    }
    
    public GameController GameController
    {
        get { return _gameController; }
    }
}
