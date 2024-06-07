using System.Numerics;
using SimpleTransClassesOfPostNum1.SimpleDataStream;
using SimpleTransClassesOfPostNum1.SimpleDataStream.Checksum;

namespace SimpleTransClassesOfPostNum1.SimpleMath.SectorOfVector;

public class LogicVector3(int x, int y, int z)
{
    private int _x = x;
    private int _y = y;
    private int _z = z;

    public LogicVector3(Vector3 vector3) : this((int)vector3.X, (int)vector3.Y, (int)vector3.Z)
    {
        // pass.
    }

    public LogicVector3() : this(0, 0, 0)
    {
        // pass.
    }

    public void Set(LogicVector3 vector3)
    {
        _x = vector3._x;
        _y = vector3._y;
        _z = vector3._z;
    }

    public void Set(LogicVector2 vector2)
    {
        _x = vector2.GetX();
        _y = vector2.GetY();
        _z = 0;
    }

    public void Set(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public void Add(LogicVector3 vector3)
    {
        _x += vector3._x;
        _y += vector3._y;
        _z += vector3._z;
    }

    public void Add(LogicVector2 vector2)
    {
        _x += vector2.GetX();
        _y += vector2.GetY();
        _z += 0;
    }

    public void Add(int x, int y, int z)
    {
        _x += x;
        _y += y;
        _z += z;
    }

    public void Subtract(LogicVector3 vector3)
    {
        _x -= vector3._x;
        _y -= vector3._y;
        _z -= vector3._z;
    }

    public void Subtract(LogicVector2 vector2)
    {
        _x -= vector2.GetX();
        _y -= vector2.GetY();
        _z -= 0;
    }

    public void Subtract(int x, int y, int z)
    {
        _x -= x;
        _y -= y;
        _z -= z;
    }

    public void Multiply(LogicVector3 vector3)
    {
        _x *= vector3._x;
        _y *= vector3._y;
        _z *= vector3._z;
    }

    public void Multiply(LogicVector2 vector2)
    {
        _x *= vector2.GetX();
        _y *= vector2.GetY();
        _z *= 1;
    }

    public void Multiply(int x, int y, int z)
    {
        _x *= x;
        _y *= y;
        _z *= z;
    }

    public void Divide(LogicVector3 vector3)
    {
        _x /= vector3._z;
        _y /= vector3._y;
        _z /= vector3._z;
    }

    public void Divide(LogicVector2 vector2)
    {
        _x /= vector2.GetX();
        _y /= vector2.GetY();
        _z /= 1;
    }

    public void Divide(int x, int y, int z)
    {
        _x /= x;
        _y /= y;
        _z /= z;
    }

    public int Dot(LogicVector3 vector3)
    {
        return _x * vector3._x + _y * vector3._y;
    }

    public int Dot(LogicVector2 vector2)
    {
        return _x * vector2.GetX() + _y * vector2.GetY();
    }

    public int Dot(int x, int y)
    {
        return _x * x + _y * y;
    }

    public int Normalize(int value)
    {
        if (LogicMath.Abs(GetLength()) == 0) return GetLength();
        {
            _x = _x * value / GetLength();
            _y = _y * value / GetLength();
            _z = _z * value / GetLength();
        }

        return GetLength();
    }

    public int NormalizeX(int value)
    {
        if (LogicMath.Abs(GetLength()) == 0) return GetLength();
        {
            _x = _x * value / GetLength();
        }

        return GetLength();
    }

    public int NormalizeY(int value)
    {
        if (LogicMath.Abs(GetLength()) == 0) return GetLength();
        {
            _y = _y * value / GetLength();
        }

        return GetLength();
    }

    public int NormalizeZ(int value)
    {
        if (LogicMath.Abs(GetLength()) == 0) return GetLength();
        {
            _z = _z * value / GetLength();
        }

        return GetLength();
    }

    public int GetDistance(LogicVector3 vector3)
    {
        var x = _x - vector3._x;
        var distance = 0x7FFFFFFF;

        if ((uint)(x + 46340) > 92680) return LogicMath.Sqrt(distance);
        var y = _y - vector3._y;

        if ((uint)(y + 46340) > 92680) return LogicMath.Sqrt(distance);
        var distanceX = x * x;
        var distanceY = y * y;

        if ((uint)distanceY < (distanceX ^ 0x7FFFFFFFu)) distance = distanceX + distanceY;

        return LogicMath.Sqrt(distance);
    }

    public int GetDistanceSquared(LogicVector3 vector3)
    {
        var x = _x - vector3._x;
        var distance = 0x7FFFFFFF;

        if ((uint)(x + 46340) > 92680) return distance;
        var y = _y - vector3._y;

        if ((uint)(y + 46340) > 92680) return distance;
        var distanceX = x * x;
        var distanceY = y * y;

        if ((uint)distanceY < (distanceX ^ 0x7FFFFFFFu)) distance = distanceX + distanceY;

        return distance;
    }

    public int GetDistanceSquaredTo(int x, int y)
    {
        var distance = 0x7FFFFFFF;

        x -= _x;

        if ((uint)(x + 46340) > 92680) return distance;
        y -= _y;

        if ((uint)(y + 46340) > 92680) return distance;
        var distanceX = x * x;
        var distanceY = y * y;

        if ((uint)distanceY < (distanceX ^ 0x7FFFFFFFu)) distance = distanceX + distanceY;

        return distance;
    }

    public int GetLength()
    {
        var length = 0x7FFFFFFF;

        if ((uint)(46340 - _x) > 92680) return LogicMath.Sqrt(length);
        if ((uint)(46340 - _y) > 92680) return LogicMath.Sqrt(length);

        var lengthX = _x * _x;
        var lengthY = _y * _y;

        if ((uint)lengthY < (lengthX ^ 0x7FFFFFFFu)) length = lengthX + lengthY;

        return LogicMath.Sqrt(length);
    }

    public int GetLengthSquared()
    {
        var length = 0x7FFFFFFF;

        if ((uint)(46340 - _x) > 92680) return length;
        if ((uint)(46340 - _y) > 92680) return length;

        var lengthX = _x * _x;
        var lengthY = _y * _y;

        if ((uint)lengthY < (lengthX ^ 0x7FFFFFFFu)) length = lengthX + lengthY;

        return length;
    }

    public bool IsInArea(int minX, int minY, int maxX, int maxY)
    {
        if (_x >= minX && _y >= minY)
            return _x < minX + maxX && _y < maxY + minY;
        return false;
    }

    public bool IsEqual(LogicVector3 vector3)
    {
        return _x == vector3._x && _y == vector3._y && _z == vector3._z;
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }

    public int GetZ()
    {
        return _z;
    }

    public void Decode(ByteStream byteStream)
    {
        _x = byteStream.ReadInt();
        _y = byteStream.ReadInt();
        _z = byteStream.ReadInt();
    }

    public void Encode(ChecksumEncoder checksumEncoder)
    {
        checksumEncoder.WriteInt(_x);
        checksumEncoder.WriteInt(_y);
        checksumEncoder.WriteInt(_z);
    }

    public void Encode(ByteStream byteStream)
    {
        byteStream.WriteInt(_x);
        byteStream.WriteInt(_y);
        byteStream.WriteInt(_z);
    }

    public LogicVector3 Clone()
    {
        return new LogicVector3(_x, _y, _z);
    }

    public void Destruct()
    {
        _x = 0;
        _y = 0;
        _z = 0;
    }

    public bool Equals(LogicVector3 b)
    {
        return GetX() == b.GetX() && GetY() == b.GetY() && GetZ() == b.GetZ();
    }
    
    public static bool operator ==(LogicVector3 a, LogicVector3 b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(LogicVector3 a, LogicVector3 b)
    {
        return !a.Equals(b);
    }
    
    public override string ToString()
    {
        return $"LogicVector3({_x}, {_y}, {_z})";
    }
}