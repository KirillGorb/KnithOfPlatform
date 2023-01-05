using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class TextDetection
{
    [SerializeField] private Text _textChangeResors;

    public void ChangeStatus(string text) => _textChangeResors.text = text;
}
