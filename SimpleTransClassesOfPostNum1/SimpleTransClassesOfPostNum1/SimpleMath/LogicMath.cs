namespace SimpleTransClassesOfPostNum1.SimpleMath;

public static class LogicMath
{
    private static readonly int[] SinTable =
    [
        0, 0x12, 0x24, 0x36, 0x47, 0x59, 0x6B, 0x7D, 0x8F,
        0xA0, 0xB2, 0xC3, 0xD5, 0xE6, 0xF8, 0x109, 0x11A, 0x12B,
        0x13C, 0x14D, 0x15E, 0x16F, 0x180, 0x190, 0x1A0, 0x1B1,
        0x1C1, 0x1D1, 0x1E1, 0x1F0, 0x200, 0x20F, 0x21F, 0x22E,
        0x23D, 0x24B, 0x25A, 0x268, 0x276, 0x284, 0x292, 0x2A0,
        0x2AD, 0x2BA, 0x2C7, 0x2D4, 0x2E1, 0x2ED, 0x2F9, 0x305,
        0x310, 0x31C, 0x327, 0x332, 0x33C, 0x347, 0x351, 0x35B,
        0x364, 0x36E, 0x377, 0x380, 0x388, 0x390, 0x398, 0x3A0,
        0x3A7, 0x3AF, 0x3B5, 0x3BC, 0x3C2, 0x3C8, 0x3CE, 0x3D3,
        0x3D8, 0x3DD, 0x3E2, 0x3E6, 0x3EA, 0x3ED, 0x3F0, 0x3F3,
        0x3F6, 0x3F8, 0x3FA, 0x3FC, 0x3FE, 0x3FF, 0x3FF, 0x400,
        0x400
    ];

    private static readonly byte[] AtanTable =
    [
        0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 4, 5, 5, 6, 6, 7, 7,
        8, 8, 8, 9, 9, 0xA, 0xA, 0xB, 0xB, 0xB, 0xC, 0xC, 0xD,
        0xD, 0xE, 0xE, 0xE, 0xF, 0xF, 0x10, 0x10, 0x11, 0x11,
        0x11, 0x12, 0x12, 0x13, 0x13, 0x13, 0x14, 0x14, 0x15,
        0x15, 0x15, 0x16, 0x16, 0x16, 0x17, 0x17, 0x18, 0x18,
        0x18, 0x19, 0x19, 0x19, 0x1A, 0x1A, 0x1B, 0x1B, 0x1B,
        0x1C, 0x1C, 0x1C, 0x1D, 0x1D, 0x1D, 0x1E, 0x1E, 0x1E,
        0x1F, 0x1F, 0x1F, 0x20, 0x20, 0x20, 0x21, 0x21, 0x21,
        0x22, 0x22, 0x22, 0x23, 0x23, 0x23, 0x23, 0x24, 0x24,
        0x24, 0x25, 0x25, 0x25, 0x25, 0x26, 0x26, 0x26, 0x27,
        0x27, 0x27, 0x27, 0x28, 0x28, 0x28, 0x28, 0x29, 0x29,
        0x29, 0x29, 0x2A, 0x2A, 0x2A, 0x2A, 0x2B, 0x2B, 0x2B,
        0x2B, 0x2C, 0x2C, 0x2C, 0x2C, 0x2D, 0x2D, 0x2D 
    ]; 

    private static readonly byte[] SqrtTable =
    [
        0, 0x10, 0x16, 0x1B, 0x20, 0x23, 0x27, 0x2A, 0x2D,
        0x30, 0x32, 0x35, 0x37, 0x39, 0x3B, 0x3D, 0x40, 0x41,
        0x43, 0x45, 0x47, 0x49, 0x4B, 0x4C, 0x4E, 0x50, 0x51,
        0x53, 0x54, 0x56, 0x57, 0x59, 0x5A, 0x5B, 0x5D, 0x5E,
        0x60, 0x61, 0x62, 0x63, 0x65, 0x66, 0x67, 0x68, 0x6A,
        0x6B, 0x6C, 0x6D, 0x6E, 0x70, 0x71, 0x72, 0x73, 0x74,
        0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D,
        0x7E, 0x80, 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86,
        0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,
        0x90, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x96,
        0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9B, 0x9C, 0x9D, 0x9E,
        0x9F, 0xA0, 0xA0, 0xA1, 0xA2, 0xA3, 0xA3, 0xA4, 0xA5,
        0xA6, 0xA7, 0xA7, 0xA8, 0xA9, 0xAA, 0xAA, 0xAB, 0xAC,
        0xAD, 0xAD, 0xAE, 0xAF, 0xB0, 0xB0, 0xB1, 0xB2, 0xB2,
        0xB3, 0xB4, 0xB5, 0xB5, 0xB6, 0xB7, 0xB7, 0xB8, 0xB9,
        0xB9, 0xBA, 0xBB, 0xBB, 0xBC, 0xBD, 0xBD, 0xBE, 0xBF,
        0xC0, 0xC0, 0xC1, 0xC1, 0xC2, 0xC3, 0xC3, 0xC4, 0xC5,
        0xC5, 0xC6, 0xC7, 0xC7, 0xC8, 0xC9, 0xC9, 0xCA, 0xCB,
        0xCB, 0xCC, 0xCC, 0xCD, 0xCE, 0xCE, 0xCF, 0xD0, 0xD0,
        0xD1, 0xD1, 0xD2, 0xD3, 0xD3, 0xD4, 0xD4, 0xD5, 0xD6,
        0xD6, 0xD7, 0xD7, 0xD8, 0xD9, 0xD9, 0xDA, 0xDA, 0xDB,
        0xDB, 0xDC, 0xDD, 0xDD, 0xDE, 0xDE, 0xDF, 0xE0, 0xE0,
        0xE1, 0xE1, 0xE2, 0xE2, 0xE3, 0xE3, 0xE4, 0xE5, 0xE5,
        0xE6, 0xE6, 0xE7, 0xE7, 0xE8, 0xE8, 0xE9, 0xEA, 0xEA,
        0xEB, 0xEB, 0xEC, 0xEC, 0xED, 0xED, 0xEE, 0xEE, 0xEF,
        0xF0, 0xF0, 0xF1, 0xF1, 0xF2, 0xF2, 0xF3, 0xF3, 0xF4,
        0xF4, 0xF5, 0xF5, 0xF6, 0xF6, 0xF7, 0xF7, 0xF8, 0xF8,
        0xF9, 0xF9, 0xFA, 0xFA, 0xFB, 0xFB, 0xFC, 0xFC, 0xFD,
        0xFD, 0xFE, 0xFE, 0xFF
    ];

    private static readonly byte[] DaysInMonth =
    [
        0x1F, 0x1C, 0x1F, 0x1E,
        0x1F, 0x1E, 0x1F, 0x1F,
        0x1E, 0x1F, 0x1E, 0x1F
    ];

    // start methods.

    public static int Clamp(int clampValue, int minValue, int maxValue)
    {
        if (clampValue >= maxValue) return maxValue;
        return clampValue <= minValue ? minValue : clampValue;
    }

    public static int Pow(int a1, int a2)
    {
        if (a2 == 0)
            return 1;

        var result = 1;
        {
            bool v5;

            do
            {
                var v4 = a1 * a1;

                if ((a2 & 1) == 0)
                    a1 = 1;

                result *= a1;
                v5 = a2 >> 1 == 0;
                a2 >>= 1;
                a1 = v4;
            } while (!v5);
        }

        return result;
    }

    public static int Sign(int a1)
    {
        var v1 = a1 >> 31;
        {
            if (a1 > 0)
                v1 = 1;
        }

        return v1;
    }

    public static int Max(int valueA, int valueB)
    {
        return valueA >= valueB ? valueA : valueB;
    }

    public static int Min(int valueA, int valueB)
    {
        return valueA <= valueB ? valueA : valueB;
    }

    public static int Abs(int value)
    {
        if (value < 0) return -value;
        return value;
    }

    public static int GetAngle(int x, int y)
    {
        switch (x)
        {
            case 0 when y == 0:
                return 0;
            case > 0 when y >= 0:
            {
                if (y >= x) return 90 - AtanTable[(x << 7) / y];

                return AtanTable[(y << 7) / x];
            }
        }

        var num = Abs(x);
        if (x <= 0 && y > 0)
        {
            if (num < y) return 90 + AtanTable[(num << 7) / y];

            return 180 - AtanTable[(y << 7) / num];
        }

        var num2 = Abs(y);
        if (x < 0 && y <= 0)
        {
            if (num2 < num) return 180 + AtanTable[(num2 << 7) / num];
            if (num2 == 0) return 0;

            return 270 - AtanTable[(num << 7) / num2];
        }

        if (num < num2) return 270 + AtanTable[(num << 7) / num2];
        return num == 0 ? 0 : NormalizeAngle360(360 - AtanTable[(num2 << 7) / num]);
    }

    public static int GetAngleBetween(int a1, int a2)
    {
        var result = (a1 - a2) % 360 + ((a1 - a2) % 360 < 0 ? 0x168 : 0);
        {
            if (result > 179) result -= 360;
            if (result < 0) result = -result;
        }

        return result;
    }

    public static int NormalizeAngle180(int angle)
    {
        var result = angle % 360 + (angle % 360 < 0 ? 0x168 : 0);
        {
            if (result > 179)
                result -= 360;
        }

        return result;
    }

    public static int NormalizeAngle360(int angle)
    {
        return angle % 360 + (angle % 360 < 0 ? 0x168 : 0);
    }

    public static int Sin(int a1)
    {
        var v1 = a1 % 360 + (a1 % 360 < 0 ? 0x168 : 0);

        int result;
        {
            if (v1 > 179)
            {
                var v3 = v1 - 180;
                {
                    if (v1 - 180 > 90)
                        v3 = 360 - v1;
                }

                result = -SinTable[v3];
            }
            else
            {
                if (v1 > 90)
                    v1 = 180 - v1;

                result = SinTable[v1];
            }
        }

        return result;
    }

    public static int Sin(int a1, int a2)
    {
        var v2 = a1 % 360 + (a1 % 360 < 0 ? 0x168 : 0);
        int v3;

        if (v2 > 179)
        {
            var v4 = v2 - 180;
            {
                if (v2 - 180 > 90)
                    v4 = 360 - v2;
            }

            v3 = -SinTable[v4];
        }
        else
        {
            if (v2 > 90)
                v2 = 180 - v2;

            v3 = SinTable[v2];
        }

        return v3 * a2 / 1024;
    }

    public static int Cos(int a1)
    {
        var v1 = (a1 + 90) % 360 + ((a1 + 90) % 360 < 0 ? 360 : 0);

        int result;
        {
            if (v1 > 179)
            {
                var v3 = v1 - 180;
                {
                    if (v1 - 180 > 90)
                        v3 = 360 - v1;
                }

                result = -SinTable[v3];
            }
            else
            {
                if (v1 > 90)
                    v1 = 180 - v1;

                result = SinTable[v1];
            }
        }

        return result;
    }

    public static int Cos(int a1, int a2)
    {
        var v2 = (a1 + 90) % 360 + ((a1 + 90) % 360 < 0 ? 360 : 0);

        switch (v2)
        {
            case > 179:
            {
                var v4 = v2 - 180;
                {
                    if (v2 - 180 > 90)
                        v4 = 360 - v2;
                }

                return -SinTable[v4] * a2 / 1024;
            }
            case > 90:
                v2 = 180 - v2;
                break;
        }

        return SinTable[v2] * a2 / 1024;
    }

    public static int GetRotatedX(int a1, int a2, int a3)
    {
        var v3 = (a3 + 90) % 360 + ((a3 + 90) % 360 < 0 ? 360 : 0);
        int v4;
        int v8;

        if (v3 > 179)
        {
            var v5 = v3 - 180;
            {
                if (v3 - 180 > 90)
                    v5 = 360 - v3;
            }

            v4 = -SinTable[v5];
        }
        else
        {
            if (v3 > 90)
                v3 = 180 - v3;

            v4 = SinTable[v3];
        }

        var v6 = v4 * a1;
        var v7 = a3 % 360 + (a3 % 360 < 0 ? 360 : 0);

        if (v7 > 179)
        {
            var v9 = v7 - 180;
            {
                if (v7 - 180 > 90)
                    v9 = 360 - v7;
            }

            v8 = -SinTable[v9];
        }
        else
        {
            if (v7 > 90)
                v7 = 180 - v7;

            v8 = SinTable[v7];
        }

        return (v6 - v8 * a2) / 1024;
    }

    public static int GetRotatedY(int a1, int a2, int a3)
    {
        var v3 = a3 % 360 + (a3 % 360 < 0 ? 360 : 0);
        int v4;
        int v8;

        if (v3 > 179)
        {
            var v5 = v3 - 180;
            {
                if (v3 - 180 > 90)
                    v5 = 360 - v3;
            }

            v4 = -SinTable[v5];
        }
        else
        {
            if (v3 > 90)
                v3 = 180 - v3;

            v4 = SinTable[v3];
        }

        var v6 = v4 * a1;
        var v7 = (a3 + 90) % 360 + ((a3 + 90) % 360 < 0 ? 360 : 0);

        if (v7 > 179)
        {
            var v9 = v7 - 180;
            {
                if (v7 - 180 > 90)
                    v9 = 360 - v7;
            }

            v8 = -SinTable[v9];
        }
        else
        {
            if (v7 > 90)
                v7 = 180 - v7;

            v8 = SinTable[v7];
        }

        return (v6 + v8 * a2) / 1024;
    }

    public static int Sqrt(int value)
    {
        int result;
        {
            if (value < 0x10000)
            {
                if (value < 256)
                {
                    result = -1;
                    if (value >= 0)
                        return SqrtTable[value] >> 4;
                }
                else
                {
                    var v7 = value switch
                    {
                        < 4096 and < 1024 => SqrtTable[value & 0xFFFFFFFC] >> 3,
                        < 4096 => SqrtTable[value >> 4] >> 2,
                        < 0x4000 => SqrtTable[value >> 6] >> 1,
                        _ => SqrtTable[value >> 8]
                    };

                    result = v7 + 1;

                    if (result * result > value)
                        return v7;
                }
            }
            else
            {
                int v5;
                {
                    if (value < 0x1000000)
                    {
                        var v6 = value switch
                        {
                            < 0x100000 and < 0x40000 => 2 * SqrtTable[value >> 0xA],
                            < 0x100000 => 4 * SqrtTable[value >> 0xC],
                            < 0x400000 => 8 * SqrtTable[value >> 0xE],
                            _ => 16 * SqrtTable[value >> 0x10]
                        };

                        v5 = value / v6 + (v6 | 1);
                    }
                    else
                    {
                        int v4;
                        switch (value)
                        {
                            case < 0x10000000 and < 0x4000000:
                                v4 = 32 * SqrtTable[value >> 0x12];
                                break;
                            case < 0x10000000:
                                v4 = SqrtTable[value >> 0x14] << 6;
                                break;
                            case < 0x40000000:
                                v4 = SqrtTable[value >> 0x16] << 7;
                                break;
                            default:
                            {
                                result = 0xFFFF;
                                if (value == 0x7FFFFFFF)
                                    return result;
                                v4 = SqrtTable[value >> 0x18] << 8;
                                break;
                            }
                        }

                        v5 = value / (((v4 | 1) + value / v4) >> 1) + (((v4 | 1) + value / v4) >> 1) + 1;
                    }
                }

                return (v5 >> 1) - ((v5 >> 1) * (v5 >> 1) > value ? 1 : 0);
            }
        }

        return result;
    }

    public static uint SqrtApproximate(int a1, int a2)
    {
        if (a1 < 0)
            a1 = -a1;
        if (a2 < 0)
            a2 = -a2;

        var v2 = a1 < a2;
        var v3 = a2;

        if (a1 > a2)
            a2 = a1;
        if (v2)
            v3 = a1;

        return (uint)(a2 + ((uint)(53 * v3) >> 7));
    }

    public static int GetDaysInMonth(int a1, int a2)
    {
        int result;
        {
            if (a1 == 1 && a2 != 0)
                result = 29;
            else
                result = DaysInMonth[a1];
        }

        return result;
    }

    // end methods;
}