using TraPortation.Traffic.Node;

namespace TraPortation.Traffic.Edge
{
    /// <summary>
    /// 辺
    /// </summary>
    /// <typeparam name="T">From</typeparam>
    /// <typeparam name="U">To</typeparam>
    public interface IEdge<out T, out U>
        where T : INode
        where U : INode
    {
        T From { get; }
        U To { get; }
        float Cost { get; }
    }

}