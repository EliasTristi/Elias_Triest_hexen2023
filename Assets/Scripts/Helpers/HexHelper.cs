using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexHelper : MonoBehaviour
{

    public static Position CubeToAxial(Vector3 cube)
    {
        var hexQ = cube.x;
        var hexR = cube.y;

        return new Position((int)hexQ, (int)hexR);
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

        return new Position((int)position.q, (int)position.r);
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

    public static List<Vector3> CubeDirectionVectors = new List<Vector3>
    {
        AxialToCube(0,1),
        AxialToCube(1,0),
        AxialToCube(1,-1),
        AxialToCube(0,-1),
        AxialToCube(-1,0),
        AxialToCube(-1,1)
    }; 
    
    public static Vector3 CubeAdd(Vector3 a, Vector3 b)
    {
        var x = a.x + b.x;
        var y = a.y + b.y;
        var z = a.z + b.z;

        return new Vector3(x, y, z);
    }

    public static Vector3 CubeScale(Vector3 cube, int factor)
    {
        return new Vector3(cube.x * factor, cube.y * factor, cube.z * factor);
    }

    public static Vector3 CubeNeighbor(Vector3 cube, int direction)
    {
        return CubeAdd(cube, CubeDirection(direction));
    }

    public static Vector3 CubeDirection(int direction)
    {
        return CubeDirectionVectors[direction];
    }

    public static List<Vector3> CubeRing(Vector3 center, int radius)
    {
        List<Vector3> results = new List<Vector3>();


        var hex = CubeAdd(center, CubeScale(CubeDirection(4), radius));
        for (int i = 0; i < 6; i++)
        {

            for (int j = 0; j < radius; j++)
            {

                results.Add(hex);
                hex = CubeNeighbor(hex, i);
            }

        }
        return results;
    }

    public static List<Position> AxialRing(Position center, int radius)
    {
        // Convert the center position from axial to cube coordinates
        Vector3 centerCube = AxialToCube(center.q, center.r);

        // Get the cube ring around the center position
        var vectors = CubeRing(centerCube, radius);

        // Convert the cube coordinates to axial coordinates and create a Position object for each one
        List<Position> positions = new List<Position>();
        foreach (Vector3 v in vectors)
        {
            Position position = CubeToAxial(v);
            if ((TileHelper.Distance >= HexHelper.AxialDistance(new Position(0, 0), position)))
                positions.Add(position);
        }

        return positions;
    }
}
