using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public EventHandler PlayClicked;

    public void Play() => OnPlayClicked(EventArgs.Empty);

    private void OnPlayClicked(EventArgs e)
    {
        var handler = PlayClicked;
        handler?.Invoke(this, e);
    }
}
