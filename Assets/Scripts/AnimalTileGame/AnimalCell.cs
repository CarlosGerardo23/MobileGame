using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Behaviour2D.Puzzle;
using GameDev.Behaviour2D.Puzzle.Board;
using System;
using DG.Tweening;
using UnityEditor;

public class AnimalCell : Cell
{
    [SerializeField] private List<AnimallCellData> cellsData;
    [SerializeField] private AnimalType _type;

    [Header("Animation variables")]
    [SerializeField] private float _creationMovementTime;
    [SerializeField] private float _maxScaleDestroyed;
    [SerializeField] private float _maxScaleTime;
    [SerializeField] private float _timeToDetroy;
    [SerializeField] private bool _useAllPosibleCells;
    [SerializeField] private int _numberOfCells;

    private SpriteRenderer _spriteRenderere;
    List<AnimalType> _posiblesAnimalsType = new List<AnimalType>();
    public AnimalType GetAnimalType => _type;
    public override void Init(BoardController board, int positionX, int positionY)
    {
        transform.localPosition = new Vector3(positionX, positionY + 1, 0);
        base.Init(board, positionX, positionY);
        _spriteRenderere = GetComponent<SpriteRenderer>();
        SetAnimalCellType();
        SetVisualType();
        transform.DOMove(new Vector3(positionX, positionY, 0), _creationMovementTime);
    }

    private void SetAnimalCellType()
    {
        _posiblesAnimalsType = GetAllAnimalsType();
        do
        {

            _type = _posiblesAnimalsType[UnityEngine.Random.Range(0, _posiblesAnimalsType.Count)];
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
        lastIndex = _useAllPosibleCells ? lastIndex : (_numberOfCells <= lastIndex ? _numberOfCells : lastIndex);
        for (int i = 0; i < lastIndex; i++)
        {
            AnimalType myEnum = (AnimalType)Enum.Parse(typeof(AnimalType), i.ToString());
            if (myEnum != AnimalType.EMPTY)
                result.Add(myEnum);
        }
        return result;
    }
    public override void DestroyCell()
    {
        transform.localScale = Vector3.one * _maxScaleDestroyed;
        transform.DOScale(Vector3.one * _maxScaleDestroyed, _maxScaleTime).onComplete = () =>
            {
                transform.DOScale(Vector3.zero, _timeToDetroy).onComplete = () =>
                {

                    base.DestroyCell();
                };
            };
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
