﻿using Microsoft.Xna.Framework;

namespace QTree.MonoGame.Common.Interfaces
{
    public interface IQuadTreeObject<T>
    {
        QuadId Id { get; }
        Rectangle Bounds { get; }
        T Object { get; }
    }
}
