using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private GhostManager ghostManager;
    [SerializeField] private HumanManager humanManager;
    [SerializeField] private endScreenScript endGameScreen;

    private void Awake()
    {
        humanManager.onAllHumansDead += endGame;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }

    private void endGame()
    {
        endGameScreen.revealScreen(timer);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
