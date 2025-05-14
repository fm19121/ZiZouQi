using System;
using UnityEngine.UIElements;

public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        return obj is GridPosition position && x == position.x && z == position.z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public static bool operator ==(GridPosition a, GridPosition b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        {
            return false;
        }
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        if (ReferenceEquals(a, b))
        {
            return false;
        }
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        {
            return true;
        }
        return a.x != b.x || a.z != b.z;
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
    public override string ToString()
    {
        return $"({x}. {z})";
    }

    internal int Count()
    {
        throw new NotImplementedException();
    }
}