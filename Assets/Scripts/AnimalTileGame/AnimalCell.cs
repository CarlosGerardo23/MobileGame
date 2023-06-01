using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Behaviour2D.Puzzle;
using GameDev.Behaviour2D.Puzzle.Board;

public class AnimalCell : Cell
{
    [SerializeField] private List<AnimallCellData> cellsData;
    [SerializeField] private AnimalType _type;
    private SpriteRenderer _spriteRenderere;

    public override void Init(BoardController board, int positionX, int positionY)
    {
        base.Init(board, positionX, positionY);
        _spriteRenderere= GetComponent<SpriteRenderer>();
        SetAnimalCellType();
        SetVisualType();
    }

    private void SetAnimalCellType()
    {
        _type= cellsData[Random.Range(1,cellsData.Count)].animalType;
    }
    private void SetVisualType()
    {
        for (int i = 0; i < cellsData.Count; i++)
        {
            if (cellsData[i].animalType == _type)
            {
                _spriteRenderere.sprite = cellsData[i].sprite;
                return;
            }

        }
    }
    public enum AnimalType
    {
        EMPTY,
        ELEPHANT,
        GIRAFFE,
        HIPPO,
        MONKEY,
        PANDA,
        PARROT,
        PENGUIN,
        PIG,
        RABBIT,
        SNAKE
    }
}
