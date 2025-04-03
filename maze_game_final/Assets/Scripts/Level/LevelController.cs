namespace Level
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class LevelController : MonoBehaviour
    {
        protected const float SPAWN_STEP_POSITION = 4.0f;

        [SerializeField] protected WallSO verWallPref = default;
        [SerializeField] protected WallSO horWallPref = default;
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
            DebugLevelMap();
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
                x.InitNeighboars(tempCellNeighbords);
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

        protected virtual bool IsWallsBroder()
            => rowIterator == 0 || rowIterator == levelHeight - 1 || colIterator == 0 || colIterator == levelWidth - 1;

        protected virtual void InitSpawnWalkableArea()
        {
            data[startSpawnPoint].SetPositionState(LevelCellState.walkable);
            data[startSpawnPoint].Neighboards.ToList().ForEach(x => { x.SetPositionState(LevelCellState.walkable); });
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

        protected virtual void GenerateWall(WallSO tempWall, Vector2 posOffset)
        {
            tempHorPos = SPAWN_STEP_POSITION * colIterator;
            tempVerPos = SPAWN_STEP_POSITION * rowIterator;
            tempSpawnPos = new Vector2(tempHorPos, tempVerPos);
            tempWallPref = tempWall.InstantiateWall(tempSpawnPos + posOffset, Quaternion.identity).gameObject;
            tempWallPref.name = tempWallPref.transform.position.ToString();
        }
    }
}