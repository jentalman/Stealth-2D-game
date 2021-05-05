using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsLisiner : MonoBehaviour
{
    [SerializeField] private NoiseDetector _noiseDetectedEvent;
    [SerializeField] private EscapeTheLevel _escapeEvent;
    [SerializeField] private UIManager _screens;
    [SerializeField] private List<EnemyTrigger> _detectedEvents = new List<EnemyTrigger>();
    [SerializeField] private List<EnemyCollision> _gameOverEvents = new List<EnemyCollision>();
    [SerializeField] private List<Enemy> _states = new List<Enemy>();

    private void Start()
    {
        _escapeEvent.EscapeEvent += _screens.OpenWinScreen;

        foreach (var state in _states)
        {
            _noiseDetectedEvent.PlayerDetectedEvent += state.SetStateChase;
        }

        foreach (var gameOver in _gameOverEvents)
        {
            gameOver.GameOverEvent += _screens.OpenLoseScreen;
        }

        foreach (var detected in _detectedEvents)
        {
            foreach (var state in _states)
            {
                detected.PlayerDetectedEvent += state.SetStateChase;
            }
        }
    }

    private void OnDisable()
    {
        _escapeEvent.EscapeEvent -= _screens.OpenWinScreen;

        foreach (var state in _states)
        {
            _noiseDetectedEvent.PlayerDetectedEvent -= state.SetStateChase;
        }

        foreach (var gameOver in _gameOverEvents)
        {
            gameOver.GameOverEvent -= _screens.OpenLoseScreen;
        }

        foreach (var detected in _detectedEvents)
        {
            foreach (var state in _states)
            {
                detected.PlayerDetectedEvent += state.SetStateChase;
            }
        }
    }

    public void GetCollisionEvents(EnemyCollision gameOver)
    {
        _gameOverEvents.Add(gameOver);
    }

    public void GetTriggersEvents(EnemyTrigger detected)
    {
        _detectedEvents.Add(detected);
    }

    public void GetStateEvents(Enemy state)
    {
        _states.Add(state);
    }
}
