using GXPEngine;
using System;

public class MyGame : Game {
	
	public Table table;

	public MyGame() : base(960, 540, false)
	{
		table = new Table("Assets/table.png");
		AddChild(table);
        UnitTests();
    }

    void Update() {
	}

    private static void UnitTests()
    {
        //-----------LAB1:


        //Vec2 myVecFirst = new Vec2(10, 11);
        //Vec2 myVecSecond = new Vec2(5, 6);

        //Vec2 resultAddition = myVecFirst + myVecSecond;
        //Console.WriteLine("Vector addition right ok ?: " +
        // (resultAddition.x == 15 && resultAddition.y == 17 &&
        // myVecFirst.x == 10 && myVecFirst.y == 11 &&
        // myVecSecond.x == 5 && myVecSecond.y == 6));

        //Vec2 resultSubstraction = myVecFirst - myVecSecond;
        //Console.WriteLine("Vector substraction right ok ?: " +
        // (resultSubstraction.x == 5 && resultSubstraction.y == 5 &&
        // myVecFirst.x == 10 && myVecFirst.y == 11 &&
        // myVecSecond.x == 5 && myVecSecond.y == 6));

        //resultSubstraction = myVecSecond - myVecFirst;
        //Console.WriteLine("Vector substraction right ok ?: " +
        // (resultSubstraction.x == -5 && resultSubstraction.y == -5 &&
        // myVecFirst.x == 10 && myVecFirst.y == 11 &&
        // myVecSecond.x == 5 && myVecSecond.y == 6));

        //Vec2 myVec = new Vec2(2, 3);
        //Vec2 resultScaling = myVec * 3;
        //Console.WriteLine("Scalar multiplication right ok ?: " +
        // (resultScaling.x == 6 && resultScaling.y == 9 && myVec.x == 2 && myVec.y == 3));
        //resultScaling = 4 * myVec;
        //Console.WriteLine("Scalar multiplication left ok ?: " +
        // (resultScaling.x == 8 && resultScaling.y == 12 && myVec.x == 2 && myVec.y == 3));

        //Vec2 myVecForLength = new Vec2(3, 4);
        //float vecLength = myVecForLength.Magnitude();
        //Console.WriteLine("Vec2 length ok ?: " +
        // (vecLength == 5f && myVecForLength.x == 3 && myVecForLength.y == 4));

        //Vec2 myVecForLength2 = new Vec2(6, 8);
        //vecLength = myVecForLength2.Magnitude();
        //Console.WriteLine("Vec2 length ok ?: " +
        // (vecLength == 10f && myVecForLength2.x == 6 && myVecForLength2.y == 8));

        //Vec2 myVecForNormalization = new Vec2(4, 3);
        //myVecForNormalization.Normalize();
        //Console.WriteLine("Vec2 normalized ok ?: " +
        // (myVecForNormalization.x == 0.8f && myVecForNormalization.y == 0.6f
        // && myVecForNormalization.Magnitude() == 1));

        //Vec2 myVecForNormalization2 = new Vec2(6, 8);
        //myVecForNormalization2.Normalize();
        //Console.WriteLine("Vec2 normalized ok ?: " +
        // (myVecForNormalization2.x == 0.6f && myVecForNormalization2.y == 0.8f
        // && myVecForNormalization2.Magnitude() == 1));

        //Vec2 myVecForNormalied = new Vec2(4, 3);
        //Vec2 resultVecNormalized = myVecForNormalied.Normalized();
        //Console.WriteLine("Vec2 returned normalized ok ?: " +
        // (myVecForNormalied.x == 4f && myVecForNormalied.y == 3f
        // && resultVecNormalized.x == 0.8f && resultVecNormalized.y == 0.6f
        // && resultVecNormalized.Magnitude() == 1));

        //Vec2 myVecForNormalied2 = new Vec2(6, 8);
        //resultVecNormalized = myVecForNormalied2.Normalized();
        //Console.WriteLine("Vec2 returned normalized ok ?: " +
        // (myVecForNormalied2.x == 6f && myVecForNormalied2.y == 8f
        // && resultVecNormalized.x == 0.6f && resultVecNormalized.y == 0.8f
        // && resultVecNormalized.Magnitude() == 1));

        //Vec2 myVecToSetXY = new Vec2(100, 200);
        //myVecToSetXY.SetXY(10, 15);
        //Console.WriteLine("Vec2 set XY ok ?: " +
        // (myVecToSetXY.x == 10f && myVecToSetXY.y == 15f));

        //myVecToSetXY.SetXY(3, 45);
        //Console.WriteLine("Vec2 set XY ok ?: " +
        // (myVecToSetXY.x == 3f && myVecToSetXY.y == 45f));


        //-----------LAB2:


        //Console.WriteLine("Deg2Rad ok ?: " +
        // (Vec2.Deg2Rad(90) == Mathf.PI / 2));
        //Console.WriteLine("Rad2Deg ok ?: " +
        // (Vec2.Rad2Deg(Mathf.PI / 2) == 90f));

        //Vec2 unitVecFromRad = Vec2.GetUnitVectorRad(Mathf.PI / 2);
        //Console.WriteLine("GetUnitVectorRad ok ?: " +
        // (Mathf.Abs(unitVecFromRad.x - 0f) < testTolerance && unitVecFromRad.y == 1));
        //Vec2 unitVecFromDeg = Vec2.GetUnitVectorDeg(180);
        //Console.WriteLine("GetUnitVectorDeg ok ?: " +
        // (unitVecFromDeg.x == -1 && Mathf.Abs(unitVecFromDeg.y - 0f) < testTolerance));

        //Vec2 randomUnitVec = Vec2.RandomUnitVector();
        //Console.WriteLine("GetRandomUnitVec ok ?: " +
        // (randomUnitVec.Magnitude() == 1));

        //Vec2 vecGetAngleDeg = new Vec2(5, 5);
        //Console.WriteLine("GetAngleDegrees ok ?: " +
        //    (vecGetAngleDeg.GetAngleDegrees() == 45f));

        //Vec2 vecGetAngleRad = new Vec2(-5, 0);
        //Console.WriteLine("GetAngleRadians ok ?: " +
        //    (vecGetAngleRad.GetAngleRadians() == Mathf.PI));

        //Vec2 vecSetAngleDeg = new Vec2(4, 3);
        //vecSetAngleDeg.SetAngleDegrees(90);
        //Console.WriteLine("SetAngleDegrees ok ?: " +
        //    (vecSetAngleDeg.GetAngleDegrees() == 90f
        //    && vecSetAngleDeg.Magnitude() == 5f));

        //Vec2 vecSetAngleRad = new Vec2(6, 8);
        //vecSetAngleRad.SetAngleRadians(Mathf.PI / 3);
        //Console.WriteLine("SetAngleRadians ok ?: " +
        //    (vecSetAngleRad.GetAngleRadians() == Mathf.PI / 3
        //    && vecSetAngleRad.Magnitude() == 10f));

        //Vec2 vecToRotateDeg = new Vec2(5, 5);
        //vecToRotateDeg.RotateDegrees(45);
        //Console.WriteLine("RotateDegrees ok ?: " +
        //    (vecToRotateDeg.GetAngleDegrees() == 90f
        //    && vecToRotateDeg.Magnitude() == Mathf.Sqrt(50)));

        //Vec2 vecToRotateRad = new Vec2(5, 0);
        //vecToRotateRad.RotateRadians(Mathf.PI / 4);
        //Console.WriteLine("RotateRadians ok ?: " +
        //    (vecToRotateRad.GetAngleRadians() == Mathf.PI / 4
        //    && vecToRotateRad.Magnitude() == Mathf.Sqrt(25)));

        //Vec2 vecRotate = new Vec2(1f, 1f);
        //Vec2 vecRotateAround = new Vec2(2f, 2f);
        //vecRotate.RotateAroundDegrees(vecRotateAround, 45);
        //Console.WriteLine("RotateAround ok ?: " +
        //    (vecRotate.x == 2f));

        //Vec2 vecRotate = new Vec2(1f, 1f);
        //Vec2 vecRotateAround = new Vec2(2f, 2f);
        //vecRotate.RotateAroundRadians(vecRotateAround, -Mathf.PI / 4);
        //Console.WriteLine("RotateAround ok ?: " +
        //    (vecRotate.y == 2f));


        //-----------LAB4:

        //Vec2 vton = new Vec2(1, 1);
        //Vec2 vn = vton.Normal();
        //Vec2 vecOut = vton.Normalized();
        //vecOut.SetXY(-vecOut.y, vecOut.x);
        //Console.WriteLine("Normal ok ?: " +
        //    (vn.x == vecOut.x
        //    && vn.y == vecOut.y));

        //Vec2 vec1 = new Vec2(1f, 0f);
        //Vec2 vec2 = new Vec2(1f, 2f);
        //float result = Vec2.Dot(vec1, vec2);
        //Console.WriteLine("Dot ok ?: " + (result == 1f));

        //Vec2 vin = new Vec2(2, -11);
        //Vec2 unitNormal = new Vec2(0.8f, 0.6f);
        //vin.Reflect(unitNormal);
        //Console.WriteLine("Reflect ok ?: " +
        //    (vin.x == 10
        //    && vin.y == -5));

        //Vec2 vel1 = new Vec2(-3f, 0f);
        //Vec2 n = new Vec2(-0.6f, 0.8f);
        //Vec2 com = new Vec2(-1f, 0f);
        //vel1.Reflect(n, com);
        //Console.WriteLine("ReflectCOM ok ?: " +
        //    (vel1.x.ToString("n2") == "-1.56"
        //      && vel1.y.ToString("n2") == "-1.92"));


        //-----------Extra:

        //Vec2 vec1 = new Vec2(1f, 0f);
        //Vec2 vec2 = new Vec2(1f, 1f);
        //float resultDeg = Vec2.AngleBetweenVec(vec1, vec2);
        //Console.WriteLine("AngleBetweenVec ok ?: " + (resultDeg == 45f));

        //Vec2 v = new Vec2(1, 1);
        //Vec2 vecOut = v.FlippedHorizontally();
        //Console.WriteLine("FlippedHorizontally ok ?: " +
        //    (vecOut.x == -1f
        //    && vecOut.y == 1));

        //Vec2 v = new Vec2(1, 1);
        //Vec2 vecOut = v.FlippedVertically();
        //Console.WriteLine("FlippedHorizontally ok ?: " +
        //    (vecOut.x == 1f
        //    && vecOut.y == -1));

        //Vec2 v = new Vec2(1, 1);
        //v.FlipHorizontally();
        //Console.WriteLine("FlipHorizontally ok ?: " +
        //    (v.x == -1f
        //    && v.y == 1f));

        //Vec2 v = new Vec2(1, 1);
        //v.FlipVertically();
        //Console.WriteLine("FlipVertically ok ?: " +
        //    (v.x == 1f
        //    && v.y == -1f));

        //Vec2 v = new Vec2(0f, 0f);
        //bool isEqual = v == 0f;
        //Console.WriteLine("== ok ?: " + isEqual);

        //Vec2 v = new Vec2(1f, 0f);
        //bool isNotEqual = v != 0f;
        //Console.WriteLine("== ok ?: " + isNotEqual);
    }

    static void Main()
	{
		new MyGame().Start();
	}
}