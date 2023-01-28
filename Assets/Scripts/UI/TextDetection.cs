using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextDetection
{
    [SerializeField] private Text _textChangeResors;

    public void ChangeStatus(string text) => _textChangeResors.text = text;
}
