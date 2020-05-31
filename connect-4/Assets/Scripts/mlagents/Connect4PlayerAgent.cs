using System.Collections;
using System.Collections.Generic;
using GameCore;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Connect4PlayerAgent : Agent
{
    [SerializeField] private Connect4Academy _academy;
    [SerializeField] private int _playerId;
    private GameController _gameController;
    
    public float timeBetweenDecisionsAtInference;
    float m_TimeSinceDecision;

    public override void OnEpisodeBegin()
    {
        Debug.Log($"{gameObject.name}: OnEpisodeBegin");
        base.OnEpisodeBegin();

        _gameController = _academy.GameController;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        // observe board state
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                int value = _gameController.GetValueAt(i, j);
                sensor.AddOneHotObservation(observation: value, range: 3);
            }
        }
    }

    public override void CollectDiscreteActionMasks(DiscreteActionMasker actionMasker)
    {
        base.CollectDiscreteActionMasks(actionMasker);
        var list = new List<int>();
        for (int i = 0; i < 7; i++)
        {
            if (!_gameController.CanPlayAtColumn(_playerId, i))
            {
                list.Add(i);
            }
        }
        actionMasker.SetMask(0, list);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        base.OnActionReceived(vectorAction);
        int action = Mathf.FloorToInt(vectorAction[0]);
        int result = _gameController.TryPlayAtColumn(action);
        Debug.Log($"{_playerId} tries to play at column {action}");
        if (result == -1)
        {
            Debug.LogError($"{_playerId} failed to play at column {action}, something is wrong...");
        }

        int gamestate = _gameController.GetGameState();
        if (gamestate == _playerId)
        {
            Debug.Log($"player {_playerId} won");
            SetReward(1);
            EndEpisode();
        }
        else if (gamestate == -1)
        {
            Debug.Log($"tie");
            SetReward(0.1f);
            EndEpisode();
        }
        else if (gamestate != 0)
        {
            Debug.Log($"player {_playerId} lost");
            SetReward(-1);
            EndEpisode();
        }
    }
    
    public void FixedUpdate()
    {
        WaitTimeInference();
    }

    private void WaitTimeInference()
    {
        if (m_TimeSinceDecision >= timeBetweenDecisionsAtInference
            && _gameController.CurrentPlayer.Id == _playerId)
        {
            m_TimeSinceDecision = 0f;
            RequestDecision();
        }
        else
        {
            m_TimeSinceDecision += Time.fixedDeltaTime;
        }
    }
}
