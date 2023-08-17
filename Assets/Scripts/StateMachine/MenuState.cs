using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : State
{
    private MenuButton _menuButton;

    public override void OnEnter()
    {
        base.OnEnter();

        var scene = SceneManager.LoadSceneAsync(States.Menu, LoadSceneMode.Additive);
        scene.completed += InitializeScene;
    }

    private void InitializeScene(AsyncOperation operation)
    {
        _menuButton = FindObjectOfType<MenuButton>();
        if (_menuButton != null)
            _menuButton.PlayClicked += OnPlayClicked;
    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        StateMachine.ChangeTo(States.Game);
    }

    public override void OnExit()
    {
        base.OnExit();

        if (_menuButton != null)
            _menuButton.PlayClicked -= OnPlayClicked;

        SceneManager.UnloadSceneAsync(States.Menu);
    }
}
