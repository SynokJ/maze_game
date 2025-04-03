namespace Level
{
    using UnityEngine;

    public class LevelCell
    {
        protected const char WALKABLE_AREA_SIGN = '-';
        protected const char UNWALKABLE_AREA_SIGN = '*';

        public LevelCell[] Neighboards => currentNeighboards;
        public Vector2 Position => new Vector2(posX, posY);
        public LevelCellState State => state;

        protected int posX = 0;
        protected int posY = 0;
        protected LevelCell rootNode = null;
        protected LevelCell[] currentNeighboards = null;
        protected LevelCellState state = default;

        public LevelCell(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
            state = LevelCellState.unwalkable;
        }

        public virtual void InitNeighboars(LevelCell[] neighboards)
        {
            currentNeighboards = new LevelCell[neighboards.Length];
            neighboards.CopyTo(currentNeighboards, 0);
        }

        public virtual void SetPositionState(LevelCellState state)
            => this.state = state;

        public virtual bool TrySetRootNode(LevelCell rootNode)
        {
            if (this.rootNode != null || rootNode == null) return false;
            this.rootNode = rootNode;
            return true;
        }

        public override string ToString()
            => state == LevelCellState.unwalkable ? UNWALKABLE_AREA_SIGN.ToString() : WALKABLE_AREA_SIGN.ToString();
    }

    public enum LevelCellState
    { 
        unwalkable = 0,
        walkable = 1
    } 
}