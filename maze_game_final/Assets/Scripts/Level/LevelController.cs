namespace Level
{
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelController : MonoBehaviour
    {
        protected const float SPAWN_STEP_POSITION = 4.0f;

        [SerializeField] protected WallSO verWallPref = default;
        [SerializeField] protected WallSO horWallPref = default;
        [SerializeField, Min(3)] protected int levelWidth = 3;
        [SerializeField, Min(3)] protected int levelHeight = 3;

        protected char[,] data = new char[,] { };
        protected GameObject tempWallPref = default;
        protected Vector2 tempSpawnOffset = default;
        protected Vector2 tempSpawnPos = default;
        protected float tempHorPos = 0.0f;
        protected float tempVerPos = 0.0f;
        protected int rowIterator = 0;
        protected int colIterator = 0;

        protected virtual void Start()
        {
            GenerateLevelWalls();
        }

        protected virtual void GenerateLevelWalls()
        {
            rowIterator = 0;
            colIterator = 0;

            data = new char[levelHeight, levelWidth];

            string res = default;
            for (int r = 0; r < levelHeight; ++r)
            {
                for (int c = 0; c < levelWidth; ++c)
                {
                    #region Spawn Walls
                    //tempSpawnOffset = new Vector2(horWallPref.PosOffset, 0.0f);
                    //GenerateWall(verWallPref, tempSpawnOffset);
                    //tempSpawnOffset = new Vector2(-horWallPref.PosOffset, 0.0f);
                    //GenerateWall(verWallPref, tempSpawnOffset);

                    //tempSpawnOffset = new Vector2(0.0f, verWallPref.PosOffset);
                    //GenerateWall(horWallPref, tempSpawnOffset);
                    //tempSpawnOffset = new Vector2(0.0f, -verWallPref.PosOffset);
                    //GenerateWall(horWallPref, tempSpawnOffset);

                    //tempLevelData.Add();
                    #endregion

                    if (rowIterator == 0 || rowIterator == levelHeight - 1 || colIterator == 0 || colIterator == levelWidth - 1)
                    {
                        data[rowIterator, colIterator] = 'x';
                    } else
                    {
                        data[rowIterator, colIterator] = '_';
                    }
                    res += data[rowIterator, colIterator];
                    colIterator++;
                }
                res += '\n';
                rowIterator++;
                colIterator = 0;
            }

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