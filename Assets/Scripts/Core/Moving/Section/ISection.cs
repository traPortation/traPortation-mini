using System;
using UnityEngine;

namespace TraPortation.Moving
{
    public interface ISection : IDisposable
    {
        SectionStatus Status { get; }
        Position Position { get; }
        Quaternion Rotation { get; }
        void Start();
        void Move(float distance);
    }
}
