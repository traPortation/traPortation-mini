using System;

namespace TraPortation.Moving
{
    public interface ISection : IDisposable
    {
        SectionStatus Status { get; }
        Position Position { get; }
        void Start();
        void Move(float delta);
    }
}
