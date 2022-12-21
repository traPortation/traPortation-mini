namespace TraPortation.Moving
{
    public interface IPath
    {
        Position Position { get; }
        SectionStatus Status { get; }
        void Move(float distance);
    }
}