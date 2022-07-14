using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class UI : MonoBehaviour
{
    public UIDocument window;
    public PlayerControls player;
    public EnemyControls enemy;

    private VisualElement _pauseMenu, _endMenu, _unableShield;
    private IStyle _playerHealthBar, _enemyHealthBar;
    private Label _shieldTimer, _endText;
    private Button _shieldButton;


    private void Start()
    {
        var root = window.rootVisualElement;

        var pauseButton = root.Q<Button>("PauseButton");
        pauseButton.clickable.clicked += Pause;

        _unableShield = root.Q<VisualElement>("UnableShield");
        _unableShield.style.display = DisplayStyle.None;

        _shieldTimer = root.Q<Label>("ShieldTimer");
        _shieldTimer.style.display = DisplayStyle.None;

        _shieldButton = root.Q<Button>("ShieldButton");
        _shieldButton.clickable.clicked += (() =>
        {
            if (player.UseShield())
                StartCoroutine(ShieldCooldown());
        });

        _playerHealthBar = root.Q<VisualElement>("PlayerHealthBarFill").style;
        _enemyHealthBar = root.Q<VisualElement>("EnemyHealthBarFill").style;

        _pauseMenu = root.Q<VisualElement>("PauseMenu");
        _pauseMenu.style.display = DisplayStyle.None;

        _endMenu = root.Q<VisualElement>("EndMenu");
        _endMenu.style.display = DisplayStyle.None;

        _endText = root.Q<Label>("EndTitle");

        var endRestartButton = root.Q<Button>("EndRestartButton");
        endRestartButton.clickable.clicked += Restart;

        var pauseRestartButton = root.Q<Button>("PauseRestartButton");
        pauseRestartButton.clickable.clicked += Restart;

        var resumeButton = root.Q<Button>("ResumeButton");
        resumeButton.clickable.clicked += Resume;

        ChangePlayerHealthBar(100);
        ChangeEnemyHealthBar(100);

        enemy.healthChanged += ChangeEnemyHealthBar;
        enemy.killed += EndGame;

        player.healthChanged += ChangePlayerHealthBar;
        player.killed += EndGame;
    }

    public void ChangePlayerHealthBar(float percents)
    {
        _playerHealthBar.width = Length.Percent(percents);
    }

    public void ChangeEnemyHealthBar(float percents)
    {
        _enemyHealthBar.width = Length.Percent(percents);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        _pauseMenu.style.display = DisplayStyle.Flex;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        _pauseMenu.style.display = DisplayStyle.None;
    }

    private void EndGame(bool result)
    {
        if (result)
            _endText.text = "You Loose";
        else
            _endText.text = "You Win";

        _endMenu.style.display = DisplayStyle.Flex;
        Time.timeScale = 0f;
    }

    private IEnumerator ShieldCooldown()
    {
        _shieldButton.style.display = DisplayStyle.None;
        _shieldTimer.style.display = DisplayStyle.Flex;
        _unableShield.style.display = DisplayStyle.Flex;

        for (int i = player.shieldCooldown; i > 0; i--)
        {
            _shieldTimer.text = $"{i: 0:00}";
            yield return new WaitForSeconds(1f);
        }

        _shieldButton.style.display = DisplayStyle.Flex;
        _shieldTimer.style.display = DisplayStyle.None;
        _unableShield.style.display = DisplayStyle.None;
    }
}