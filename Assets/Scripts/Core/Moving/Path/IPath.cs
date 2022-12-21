using UnityEngine;

namespace TraPortation.Moving
{
    public interface IPath
    {
        Position Position { get; }
        Quaternion Rotation { get; }
        SectionStatus Status { get; }
        void Move(float distance);
    }
}