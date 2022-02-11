using System;

public interface ISection: IDisposable
{
    SectionStatus Status { get; }
    Position Position { get; }
    void Start();
    void Move(float delta);
}
