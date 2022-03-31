using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTyper : MonoBehaviour
{
    public Text text;
    public float speed;
    public float startDelay;
    private float _timer;
    private string _target;
    private string _current;
    private int _index;

    private void Start()
    {
        _target = text.text;
        text.text = "";
        _timer = startDelay;
    }

    private void Update()
    {

        _timer -= 1 * Time.deltaTime;

        if (_timer <= 0 && _index < _target.Length)
        {
            _timer = speed;
            _current += _target[_index];
            _index++;
        }

        text.text = _current;
    }
}
