namespace TraPortation.Traffic.Node
{
    /// <summary>
    /// インデックス付き頂点 (駅や交差点など)
    /// インデックスはBoardで管理される
    /// </summary>
    public interface IIndexedNode : INode
    {
        int Index { get; }
    }
}