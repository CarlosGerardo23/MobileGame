using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Behaviour2D.Puzzle;
using GameDev.Behaviour2D.Puzzle.Board;
using System;
using UnityEditor;

public class AnimalCell : Cell
{
    [SerializeField] private List<AnimallCellData> cellsData;
    [SerializeField] private AnimalType _type;
    private SpriteRenderer _spriteRenderere;
    List<AnimalType> _posiblesAnimalsType = new List<AnimalType>();
    public AnimalType GetAnimalType => _type;
    public override void Init(BoardController board, int positionX, int positionY)
    {
        base.Init(board, positionX, positionY);
        _spriteRenderere = GetComponent<SpriteRenderer>();
        SetAnimalCellType();
        SetVisualType();
    }

    private void SetAnimalCellType()
    {
        _posiblesAnimalsType = GetAllAnimalsType();
        do
        {
           
            _type = _posiblesAnimalsType[UnityEngine.Random.Range(0,_posiblesAnimalsType.Count)];
        } while (CheckNeighborsMatch());

    }

    private bool CheckNeighborsMatch()
    {
        Vector2 checkNeighbor;
        Cell neighbor;

        //Left Neighbor
        checkNeighbor = CellPosition + Vector2.left;
        if (board.TryGetCellByPosition(checkNeighbor, out neighbor))
        {
            if (_type == ((AnimalCell)neighbor)._type)
            {
                _posiblesAnimalsType.Remove(_type);
                return true;
            }
        }
        //Right Neighbor
        checkNeighbor = CellPosition + Vector2.right;
        if (board.TryGetCellByPosition(checkNeighbor, out neighbor))
        {
            if (_type == ((AnimalCell)neighbor)._type)
            {
                _posiblesAnimalsType.Remove(_type);
                return true;
            }
        }
        //Up Neighbor
        checkNeighbor = CellPosition + Vector2.up;
        if (board.TryGetCellByPosition(checkNeighbor, out neighbor))
        {
            if (_type == ((AnimalCell)neighbor)._type)
            {
                _posiblesAnimalsType.Remove(_type);
                return true;
            }
        }
        //Dowm Neighbor
        checkNeighbor = CellPosition + Vector2.down;
        if (board.TryGetCellByPosition(checkNeighbor, out neighbor))
        {
            if (_type == ((AnimalCell)neighbor)._type)
            {
                _posiblesAnimalsType.Remove(_type);
                return true;
            }
        }
        return false;

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
    private List<AnimalType> GetAllAnimalsType()
    {
        List<AnimalType> result = new List<AnimalType>();
        AnimalType lastType = AnimalType.SNAKE;
        int lastIndex = Convert.ToInt32(lastType);
        for (int i = 0; i < 4; i++)
        {
            AnimalType myEnum = (AnimalType)Enum.Parse(typeof(AnimalType), i.ToString());
            if (myEnum != AnimalType.EMPTY)
            result.Add(myEnum);
        }
        return result;
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
