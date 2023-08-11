using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexHelper : MonoBehaviour
{

    public static Vector2 CubeToAxial(Vector3 cube)
    {
        var hexQ = cube.x;
        var hexR = cube.y;

        return new Vector2(hexQ, hexR);
    }

    public static Vector3 AxialToCube(float q, float r)
    {
        var cubeQ = q;
        var cubeR = r;
        var cubeS = -q - r;

        return new Vector3(cubeQ, cubeR, cubeS);
    }

    public static Vector3 CubeRound(Vector3 cube)
    {
        var cubeQ = Mathf.Round(cube.x);
        var cubeR = Mathf.Round(cube.y);
        var cubeS = Mathf.Round(cube.z);

        var Qdiff = Mathf.Abs(cubeQ - cube.x);
        var Rdiff = Mathf.Abs(cubeR - cube.y);
        var Sdiff = Mathf.Abs(cubeS - cube.z);

        if (Qdiff > Rdiff && Qdiff > Sdiff)
            cubeQ = -cubeR - cubeS;
        else if (Rdiff > Sdiff)
            cubeR = -cubeQ - cubeS;
        else
            cubeS = -cubeQ - cubeR;

        return new Vector3(cubeQ, cubeR, cubeS);
    }

    public static Position AxialRound(float q, float r)
    {
        var position = CubeToAxial(CubeRound(AxialToCube(q, r)));

        return new Position((int)position.x, (int)position.y);
    }

    public static int AxialDistance(Position A, Position B)
    {
        return (Mathf.Abs(A.q - B.q) + Mathf.Abs(A.q + A.r - B.q - B.r) + Mathf.Abs(A.r - B.r)) / 2;
    }

    public static Position AxialAdd(Position A, Position B)
    {
        return new Position(A.q + B.q, A.r + B.r);
    }

    public static Position AxialSubstract(Position A, Position B)
    {
        return new Position(A.q - B.q, A.r - B.r);
    }
}
