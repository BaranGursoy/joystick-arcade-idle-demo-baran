using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _createdBloodSplatCount;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        GameActions.BloodSplatCreated += IncrementCreatedBloodSplatCount;
        GameActions.BloodSplatCleaned += DecrementCreatedBloodSplatCount;
        GameActions.GameFinished += StopTheGame;
    }

    private void OnDestroy()
    {
        GameActions.BloodSplatCreated -= IncrementCreatedBloodSplatCount;
        GameActions.BloodSplatCleaned -= DecrementCreatedBloodSplatCount;
        GameActions.GameFinished -= StopTheGame;
    }

    private void IncrementCreatedBloodSplatCount()
    {
        _createdBloodSplatCount++;
    }
    
    private void DecrementCreatedBloodSplatCount()
    {
        _createdBloodSplatCount--;

        if (_createdBloodSplatCount <= 0)
        {
            GameActions.GameFinished?.Invoke();
        }
    }

    private void StopTheGame()
    {
        Time.timeScale = 0f;
    }
    
}
