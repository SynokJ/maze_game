namespace Level
{
    using UnityEngine;

    public class LevelCell
    {
        protected const char WALKABLE_AREA_SIGN = '-';
        protected const char UNWALKABLE_AREA_SIGN = '*';
        protected const char COLLECTABLE_AREA_SIGN = 'x';

        public LevelCell RootNode => rootNode;
        public LevelCell[] Neighbors => currentNeighbors;
        public Vector2 Position => new Vector2(posX, posY);
        public LevelCellState State => state;
        public LevelCellType Type => type;

        protected int posX = 0;
        protected int posY = 0;
        protected LevelCell rootNode = null;
        protected LevelCell[] currentNeighbors = null;
        protected LevelCellType type = default;
        protected LevelCellState state = default;

        public LevelCell(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
            state = LevelCellState.unwalkable;
        }

        public virtual void SetNeighbors(LevelCell[] neighboards)
        {
            currentNeighbors = new LevelCell[neighboards.Length];
            neighboards.CopyTo(currentNeighbors, 0);
        }

        public virtual void SetPositionState(LevelCellState state)
            => this.state = state;

        public virtual void SetType(LevelCellType type)
            => this.type = type;

        public virtual bool TrySetRootNode(LevelCell rootNode)
        {
            if (this.rootNode != null || rootNode == null) return false;
            this.rootNode = rootNode;
            return true;
        }

        public override string ToString()
        {
            if (state == LevelCellState.collectables)
                return COLLECTABLE_AREA_SIGN.ToString();
            return state == LevelCellState.unwalkable ? UNWALKABLE_AREA_SIGN.ToString() : WALKABLE_AREA_SIGN.ToString();
        }
    }

    public enum LevelCellState
    {
        unwalkable = 0,
        walkable = 1,
        analyzing = 2,
        collectables = 3
    }

    public enum LevelCellType
    {
        vertical = 0,
        horizontal = 1
    }
}