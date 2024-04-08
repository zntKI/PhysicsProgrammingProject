using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public struct Vec2
    {
        public float x;
        public float y;

        public Vec2(float pX = 0, float pY = 0)
        {
            x = pX;
            y = pY;
        }

        public float Magnitude()
            => Mathf.Sqrt(x * x + y * y);

        public void Normalize()
        {
            var length = Magnitude();
            if (length > 0)
            {
                x /= length;
                y /= length;
            }
        }

        public Vec2 Normalized()
        {
            Vec2 result = new Vec2(x, y);
            result.Normalize();
            return result;
        }

        public Vec2 Normal()
        => new Vec2(-y, x).Normalized();

        public void SetXY(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetXY(Vec2 vec2)
            => this = vec2;

        public float GetAngleRadians()
            => Mathf.Atan2(y, x);

        public float GetAngleDegrees()
            => Rad2Deg(Mathf.Atan2(y, x));

        public void SetAngleDegrees(float deg)
            => this = GetUnitVectorDeg(deg) * Magnitude();

        public void SetAngleRadians(float rad)
            => this = GetUnitVectorRad(rad) * Magnitude();

        public void RotateDegrees(float deg)
            => Rotate(false, deg);

        public void RotateRadians(float rad)
            => Rotate(true, rad);

        private void Rotate(bool isRad, float angle)
        {
            float sin = Mathf.Sin(isRad ? angle : Deg2Rad(angle));
            float cos = Mathf.Cos(isRad ? angle : Deg2Rad(angle));
            SetXY(x * cos - y * sin, x * sin + y * cos);
        }

        public void RotateAroundDegrees(Vec2 point, float deg)
            => RotateAround(point, false, deg);

        public void RotateAroundRadians(Vec2 point, float rad)
            => RotateAround(point, true, rad);

        private void RotateAround(Vec2 point, bool isRad, float angle)
        {
            Vec2 vecToRotate = this - point;
            vecToRotate.Rotate(isRad, angle);
            vecToRotate += point;
            this = vecToRotate;
        }

        public void Reflect(Vec2 unitNormal, float C = 1f)
        => this = this - (1 + C) * Dot(this, unitNormal) * unitNormal;

        public void Reflect(Vec2 unitNormal, Vec2 COMvec2, float C = 1f)
        => this = this - (1 + C) * Dot(this - COMvec2, unitNormal) * unitNormal;

        public static Vec2 operator +(Vec2 left, Vec2 right)
            => new Vec2(left.x + right.x, left.y + right.y);

        public static Vec2 operator -(Vec2 left, Vec2 right)
            => new Vec2(left.x - right.x, left.y - right.y);

        public static Vec2 operator *(float scalar, Vec2 vec2)
            => new Vec2(scalar * vec2.x, scalar * vec2.y);

        public static Vec2 operator *(Vec2 vec2, float scalar)
            => new Vec2(scalar * vec2.x, scalar * vec2.y);

        public override string ToString()
        {
            return String.Format("({0},{1})", x, y);
        }

        public static float Dot(Vec2 firstVec2, Vec2 secondVec2)
        => (firstVec2.x * secondVec2.x) + (firstVec2.y * secondVec2.y);

        public static float Deg2Rad(float deg)
            => deg * Mathf.PI / 180;
        public static float Rad2Deg(float rad)
            => rad * 180 / Mathf.PI;

        public static Vec2 GetUnitVectorRad(float rad)
            => new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));

        public static Vec2 GetUnitVectorDeg(float deg)
            => GetUnitVectorRad(Deg2Rad(deg));

        public static Vec2 RandomUnitVector()
            => GetUnitVectorDeg(Utils.Random(0f, 360f));
    }
}
