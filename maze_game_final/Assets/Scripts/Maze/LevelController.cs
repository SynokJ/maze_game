namespace Level
{
    using System;
    using System.Linq;
    using UnityEngine;
    using System.Collections.Generic;

    public class LevelController : MonoBehaviour
    {
        protected const float SPAWN_STEP_POSITION = 4.0f;

        public event Action<List<LevelCell>> OnLevelPathGenerated = delegate { };
        public int LevelWidth => levelWidth;
        public int LevelHeight => levelHeight;
        public List<LevelCell> LevelCells => data;

        [SerializeField, Min(3)] protected int levelWidth = 3;
        [SerializeField, Min(3)] protected int levelHeight = 3;

        protected List<LevelCell> data = new List<LevelCell>();
        protected LevelCell[] tempCellNeighbords = null;
        protected LevelCell[] tempNbs = null;
        protected GameObject tempWallPref = default;
        protected Vector2 tempSpawnOffset = default;
        protected Vector2 tempSpawnPos = default;
        protected LevelCell tempCell = null;
        protected float tempHorPos = 0.0f;
        protected float tempVerPos = 0.0f;
        protected int startSpawnPoint = 0;
        protected int rowIterator = 0;
        protected int colIterator = 0;

        protected virtual void Awake()
        {
            int halfOfHeight = Mathf.RoundToInt((levelHeight - 1) * 0.5f);
            int halfOfWidth = Mathf.RoundToInt((levelWidth - 1) * 0.5f);
            startSpawnPoint = halfOfHeight * levelWidth + halfOfWidth;
        }

        protected virtual void Start()
            => GenerateLevel();

        protected virtual void GenerateLevel()
        {
            InitDefaultLevelMap();
            InitMapPath();
            InitLevelBorder();
            InitCellType();

            OnLevelPathGenerated(data);
        }

        protected virtual void InitDefaultLevelMap()
        {
            data.Clear();
            for (int r = 0; r < levelHeight; ++r)
            {
                for (int c = 0; c < levelWidth; ++c)
                {
                    tempCell = new LevelCell(c, r);
                    tempCell.SetPositionState(LevelCellState.unwalkable);
                    data.Add(tempCell);
                }
            }

            data.ForEach(InitNeighborsToCell);
        }

        protected virtual void InitNeighborsToCell(LevelCell currentCell)
        {
            tempCellNeighbords = new LevelCell[4];
            SetAvailableNeighbors(Mathf.RoundToInt(currentCell.Position.y), Mathf.RoundToInt(currentCell.Position.x));
            currentCell.SetNeighbors(tempCellNeighbords);
        }

        protected virtual void SetAvailableNeighbors(int r, int c)
        {
            InitCellNeighbors(0, r - 1, c);
            InitCellNeighbors(1, r, c + 1);
            InitCellNeighbors(2, r + 1, c);
            InitCellNeighbors(3, r, c - 1);

            tempCellNeighbords = tempCellNeighbords.Where(x => x != null).ToArray();
        }

        protected virtual void InitCellNeighbors(int n, int r, int c)
        {
            if (TryGetNeighboar(out tempCell, r, c))
                tempCellNeighbords[n] = tempCell;
        }

        protected virtual bool TryGetNeighboar(out LevelCell resultCell, int r, int c)
        {
            resultCell = null;
            if (IsUnavailableCell(r, c)) return false;
            resultCell = data[r * levelWidth + c];
            return true;
        }

        protected virtual void InitMapPath()
            => GeneratePath(data[startSpawnPoint]);

        protected virtual LevelCell GeneratePath(LevelCell startCell)
        {
            if (IsEdgeCell(startCell))
            {
                return startCell;
            }

            startCell.SetPositionState(LevelCellState.walkable);
            tempNbs = RandomizeArray(startCell.Neighbors);
            foreach (LevelCell tempCell in tempNbs)
            {
                if (tempCell.State != LevelCellState.unwalkable) continue;
                tempCell.TrySetRootNode(startCell);
                tempNbs.ToList().ForEach(x =>
                {
                    if (tempCell.Position.x != x.Position.x && tempCell.Position.y != x.Position.y)
                    {
                        x.SetPositionState(LevelCellState.analyzing);
                    }
                });
                GeneratePath(tempCell);
            }
            return tempNbs.Last();
        }

        protected virtual void InitCellType()
        {
            foreach (LevelCell cell in data)
            {
                tempCellNeighbords = cell.Neighbors;
                bool isVertical = false;
                foreach (var tempNbs in tempCellNeighbords)
                {
                    if (tempNbs.State == LevelCellState.unwalkable) continue;

                    if (tempNbs.Position.y == cell.Position.y)
                    {
                        isVertical = tempNbs.Position.x != cell.Position.x;
                    }
                }

                cell.SetType(isVertical ? LevelCellType.vertical : LevelCellType.horizontal);
            }
        }

        protected virtual void InitLevelBorder()
        {
            List<LevelCell> borderCells = data.Where(x => IsEdgeCell(x)).ToList();
            borderCells.ForEach(cell => { cell.SetPositionState(LevelCellState.unwalkable); });
        }

        protected virtual bool IsUnavailableCell(int r, int c)
            => r < 0 || r > levelHeight - 1 || c < 0 || c > levelWidth - 1;

        protected virtual bool IsEdgeCell(LevelCell startCell)
        {
            bool ver = startCell.Position.x <= 0 || startCell.Position.x >= levelWidth - 1;
            bool hor = startCell.Position.y <= 0 || startCell.Position.y >= levelHeight - 1;
            return ver || hor;
        }

        protected virtual LevelCell[] RandomizeArray(LevelCell[] data)
        {
            LevelCell[] result = new LevelCell[data.Length];
            data.CopyTo(result, 0);

            int size = data.Length;
            for (int i = 0; i < size; ++i)
            {
                int index = UnityEngine.Random.Range(0, data.Length);
                LevelCell temp = result[i];
                result[i] = result[index];
                result[index] = temp;
            }

            return result;
        }

        protected virtual void DebugLevelMap()
        {
            string res = default;
            data.ForEach(x =>
            {
                res += x + " ";
                if (x.Position.x == levelWidth - 1)
                {
                    res += '\n';
                }
            });
            Debug.Log(res);
        }
    }
}