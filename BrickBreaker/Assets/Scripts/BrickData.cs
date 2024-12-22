using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBrickData", menuName = "ScriptableObjects/BrickData", order = 1)]
public class BrickData : ScriptableObject
{
    [SerializeField] private Sprite[] brickSpritesField;

    public Sprite[] brickSprites { get; private set; }

    public void Initialize()
    {
        brickSprites = new Sprite[brickSpritesField.Length];
        brickSpritesField.CopyTo(brickSprites, 0);
    }
}
