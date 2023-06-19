using GameDev.Behaviour2D.Puzzle;
using GameDev.Behaviour2D.Puzzle.Board;
using GameDev.Behaviour2D.Puzzle.Events;
using GameDev.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AnimalCellMatchController : MonoBehaviour
{
    [SerializeField] BoardController _boardController;
    [SerializeField] BoardCellMovementController _boardMovementController;
    [SerializeField] private int _minNumMatches;
    [Header("Events Observers")]
    [SerializeField] private CellEventChannel _getMatchesObserver;
    [Header("Events")]
    [SerializeField] private UnityEvent _onMatchNotFoundSubject;

    private List<Cell> _cellsToMatch = new List<Cell>();
    private AnimalCell _initialCell;
    private List<Vector2> _cellstEliminatedList= new List<Vector2>();

    private void OnEnable()
    {
        _getMatchesObserver.Subscribe(GetCellsToMatch);
    }
    private void OnDisable()
    {
        _getMatchesObserver.Unsubscribe(GetCellsToMatch);

    }
    private void GetCellsToMatch(Cell reference)
    {
        if (_cellsToMatch.Count >= 2)
            _cellsToMatch.Clear();
        _cellsToMatch.Add(reference);
        if (_cellsToMatch.Count == 2) TryGetMatches();
    }
    private void TryGetMatches()
    {
        
        bool cellsToMatch = false;
        List<Vector2> resultCells = new List<Vector2>();
        List<Vector2> collapsedCells1 = new List<Vector2>();
        List<Vector2> collapsedCells2 = new List<Vector2>();
        List<Vector2> cellsCollapsed = new List<Vector2>();
        _cellstEliminatedList.Clear();
        if (GetMatchesByCell(_cellsToMatch[0].CellPosition, out resultCells))
        {
            cellsToMatch = true;
            for (int i = 0; i < resultCells.Count; i++)
            {
                _boardController.DestroyCellByPosition(resultCells[i]);
            }

            resultCells.Sort((a, b) => a.y.CompareTo(b.y));
            _boardMovementController.CollapseBoard(GetUniquecolumns(resultCells), out collapsedCells1, out cellsCollapsed);

        }
        _cellstEliminatedList = _cellstEliminatedList.Union(cellsCollapsed).ToList();
        resultCells= new List<Vector2>();
        if (GetMatchesByCell(_cellsToMatch[1].CellPosition, out resultCells))
        {
            cellsToMatch = true;
            for (int i = 0; i < resultCells.Count; i++)
            {
                _boardController.DestroyCellByPosition(resultCells[i]);
            }

            resultCells.Sort((a, b) => a.y.CompareTo(b.y));
            _boardMovementController.CollapseBoard(GetUniquecolumns(resultCells), out collapsedCells2,out cellsCollapsed);
        }
        _cellstEliminatedList = _cellstEliminatedList.Union(cellsCollapsed).ToList();

        if (!cellsToMatch) _onMatchNotFoundSubject?.Invoke();
        else
        {
            collapsedCells1 = collapsedCells1.Union(collapsedCells2).ToList();
            StartCoroutine(ComboPices(collapsedCells1));
        }


    }

    private IEnumerator ComboPices(List<Vector2> cellsPosition)
    {
        List<Vector2> cellsCombos = new List<Vector2>();
        List<Vector2> cellsCollapsed = new List<Vector2>();
        yield return new WaitForSeconds(1);
        foreach (var cellPosition in cellsPosition)
        {
            if (GetMatchesByCell(cellPosition, out List<Vector2> cellsMatches))
            {
                cellsCombos = cellsCombos.Union(cellsMatches).ToList();
                for (int i = 0; i < cellsMatches.Count; i++)
                {
                    _boardController.DestroyCellByPosition(cellsMatches[i]);
                }
            }
        }
        if (cellsCombos.Count > 0)
        {
            cellsCombos.Sort((a, b) => a.y.CompareTo(b.y));
            _boardMovementController.CollapseBoard(GetUniquecolumns(cellsCombos), out List<Vector2> newCellsToCollapse, out cellsCollapsed);
            _cellstEliminatedList = _cellstEliminatedList.Union(cellsCollapsed).ToList();

            yield return ComboPices(newCellsToCollapse);
        }
        else
        {
            BoardCellMovementController.EnableControlls();
            StartCoroutine(_boardController.SetUpCellsByPosition(_cellstEliminatedList));
        }
    }

    private bool GetMatchesByCell(Vector2 initialPosition, out List<Vector2> resultCells)
    {
        resultCells = new List<Vector2>();
        GetInitialCell(initialPosition, resultCells);
        if (resultCells.Count > 0)
        {
            CheckNeighbors(ref resultCells);
        }
      
        return resultCells.Count >= _minNumMatches;

    }
    private void GetInitialCell(Vector2 initialPosition, List<Vector2> resultCells)
    {
        if (_boardController.TryGetCellByPosition(initialPosition, out Cell initialCell))
        {
            resultCells.Add(initialCell.CellPosition);
            _initialCell = (AnimalCell)initialCell;
        }
    }
    private void CheckNeighbors(ref List<Vector2> resultCells)
    {
        //Width Neighbors
        List<Vector2> neighbors = new List<Vector2>();
        neighbors = GetNeighborsByDirection(Vector2.right).Union(GetNeighborsByDirection(Vector2.left)).ToList();

        if (neighbors.Count >= _minNumMatches - 1)
            resultCells = resultCells.Union(neighbors).ToList();
        neighbors.Clear();
        //Vertical Neighbors
        neighbors = GetNeighborsByDirection(Vector2.up).Union(GetNeighborsByDirection(Vector2.down)).ToList();
        if (neighbors.Count >= _minNumMatches - 1)
            resultCells = resultCells.Union(neighbors).ToList();

    }

    private List<Vector2> GetNeighborsByDirection(Vector2 direction)
    {
        List<Vector2> neighbors = new List<Vector2>();


        int length = _boardController.BoardDimension.x > _boardController.BoardDimension.y ? (int)_boardController.BoardDimension.x : (int)_boardController.BoardDimension.y;
        int xPos = (int)_initialCell.CellPosition.x;
        int yPos = (int)(_initialCell.CellPosition.y);

        Vector2 newPos = new Vector2();
        for (int i = 1; i < length; i++)
        {
            newPos.x = xPos + ((int)direction.x * i);
            newPos.y = yPos + ((int)direction.y * i);
            if (_boardController.TryGetCellByPosition(newPos, out Cell result))
            {
                if (((AnimalCell)result).GetAnimalType == _initialCell.GetAnimalType)
                    neighbors.Add(result.CellPosition);
                else break;

            }
            else break;
        }

        return neighbors;
    }

    private List<Vector2> GetUniquecolumns(List<Vector2> reference)
    {
        List<Vector2> result = new List<Vector2>();

        List<int> xValueList = new List<int>();
        foreach (var vector in reference)
        {
            if (!xValueList.Contains((int)vector.x))
            {
                result.Add(vector);
                xValueList.Add((int)vector.x);
            }
        }
        return result;
    }

}
