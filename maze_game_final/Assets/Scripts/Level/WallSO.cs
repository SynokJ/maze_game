namespace Level
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level/" + nameof(WallSO), fileName = nameof(WallSO))]
    public class WallSO : ScriptableObject
    {
        public float PosOffset => posOffset;

        [SerializeField] protected Wall wallPref = default;
        [SerializeField, Min(0.0f)] protected float posOffset = 0.0f;

        protected Wall tempWall = default;

        public Wall InstantiateWall(Vector2 spawnPos)
        {
            tempWall = Instantiate(wallPref);
            tempWall.transform.position = spawnPos;
            tempWall.name = ((Vector2)tempWall.transform.position).ToString();
            return tempWall;
        }
    }
}