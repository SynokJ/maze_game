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

        [SerializeField, Min(3)] protected int levelWidth = 3;
        [SerializeField, Min(3)] protected int levelHeight = 3;

        protected List<LevelCell> data = new List<LevelCell>();
        protected LevelCell[] tempCellNeighbords = null;
        protected GameObject tempWallPref = default;
        protected Vector2 tempSpawnOffset = default;
        protected Vector2 tempSpawnPos = default;
        protected LevelCell tempCell = null;
        protected float tempHorPos = 0.0f;
        protected float tempVerPos = 0.0f;
        protected int startSpawnPoint = 0;
        protected int rowIterator = 0;
        protected int colIterator = 0;

        private void Awake()
        {
            int halfOfHeight = Mathf.RoundToInt((levelHeight - 1) * 0.5f);
            int halfOfWidth = Mathf.RoundToInt((levelWidth - 1) * 0.5f);
            startSpawnPoint = halfOfHeight * levelWidth + halfOfWidth;
        }

        protected virtual void Start()
        {
            InitDefaultLevelMap();
            InitSpawnWalkableArea();
            InitMapPath();
            InitWalkState();

            OnLevelPathGenerated(data);
        }

        private void InitWalkState()
        {
            data.ForEach(cell =>
            {
                tempCellNeighbords = cell.Neighboards;
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
            });
        }

        protected virtual void InitDefaultLevelMap()
        {
            data.Clear();
            for (int r = 0; r < levelHeight; ++r)
            {
                for (int c = 0; c < levelWidth; ++c)
                {
                    data.Add(new LevelCell(c, r));
                }
            }

            data.ForEach(x =>
            {
                tempCellNeighbords = new LevelCell[4];
                InitNeighboard(Mathf.RoundToInt(x.Position.y), Mathf.RoundToInt(x.Position.x));
                x.SetNeighboars(tempCellNeighbords);
            });
        }

        protected virtual void InitNeighboard(int r, int c)
        {
            if (TryGetNeighboar(out tempCell, r - 1, c))
                tempCellNeighbords[0] = tempCell;

            if (TryGetNeighboar(out tempCell, r, c + 1))
                tempCellNeighbords[1] = tempCell;

            if (TryGetNeighboar(out tempCell, r + 1, c))
                tempCellNeighbords[2] = tempCell;

            if (TryGetNeighboar(out tempCell, r, c - 1))
                tempCellNeighbords[3] = tempCell;

            tempCellNeighbords = tempCellNeighbords.Where(x => x != null).ToArray();
        }

        protected virtual bool TryGetNeighboar(out LevelCell resultCell, int r, int c)
        {
            resultCell = null;
            if (r < 0 || r > levelHeight - 1 || c < 0 || c > levelWidth - 1) return false;
            resultCell = data[r * levelWidth + c];
            return true;
        }

        protected virtual void InitSpawnWalkableArea()
        {
            data[startSpawnPoint].SetPositionState(LevelCellState.walkable);
            data[startSpawnPoint].Neighboards.ToList().ForEach(x => { x.SetPositionState(LevelCellState.walkable); });
        }

        protected virtual void InitMapPath()
        {
            tempCellNeighbords = new LevelCell[data[startSpawnPoint].Neighboards.Length];
            data[startSpawnPoint].Neighboards.CopyTo(tempCellNeighbords, 0);

            foreach (LevelCell tempCell in tempCellNeighbords)
            {
                GeneratePath(tempCell);
            }
        }

        protected virtual LevelCell GeneratePath(LevelCell startCell)
        {
            if (IsEdgeCell(startCell))
            {
                return startCell;
            }

            startCell.SetPositionState(LevelCellState.walkable);
            LevelCell[] tempNbs = RandomizeArray(startCell.Neighboards);
            foreach (LevelCell tempCell in tempNbs)
            {
                if (tempCell.State == LevelCellState.walkable) continue;
                tempCell.TrySetRootNode(startCell);
                GeneratePath(tempCell);
            }
            return tempNbs.Last();
        }

        protected virtual bool IsEdgeCell(LevelCell startCell)
        {
            bool ver = startCell.Position.x <= 0 || startCell.Position.x >= levelWidth - 1;
            bool hor = startCell.Position.y <= 0 || startCell.Position.y >= levelHeight - 1;
            return ver || hor;
        }

        protected virtual LevelCell[] RandomizeArray(LevelCell[] data)
        {
            UnityEngine.Random.InitState(DateTime.Now.Second);
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