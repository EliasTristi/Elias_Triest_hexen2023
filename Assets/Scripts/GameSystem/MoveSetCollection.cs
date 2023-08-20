using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetCollection : MonoBehaviour
{
    public static List<List<Position>> GetValidTilesForMeteor(Position position, Board board)
    {
        List<List<Position>> positions = new List<List<Position>>();

        positions.Add(new MoveSetHelper(board, position).Collect(0, 0, 1).CollectValidPositions());
        positions.Add(new MoveSetHelper(board, position).RightUp(1).CollectValidPositions());
        positions.Add(new MoveSetHelper(board, position).Right(1).CollectValidPositions());
        positions.Add(new MoveSetHelper(board, position).RightDown(1).CollectValidPositions());
        positions.Add(new MoveSetHelper(board, position).LeftUp(1).CollectValidPositions());
        positions.Add(new MoveSetHelper(board, position).Left(1).CollectValidPositions());
        positions.Add(new MoveSetHelper(board, position).LeftDown(1).CollectValidPositions());

        //positions.Add(position);
        //positions.Add(HexHelper.AxialAdd(position, new Position(1, 0))); // right
        //positions.Add(HexHelper.AxialAdd(position, new Position(0, 1))); // right up
        //positions.Add(HexHelper.AxialAdd(position, new Position(1, -1))); // right down
        //positions.Add(HexHelper.AxialAdd(position, new Position(-1, 0))); // left
        //positions.Add(HexHelper.AxialAdd(position, new Position(-1, 1))); // left up
        //positions.Add(HexHelper.AxialAdd(position, new Position(0, -1))); // left down

        return positions;
    }

    public static List<List<Position>> GetAllTiles(Board board)
    {
        List<List<Position>> positions = new List<List<Position>>();

        positions.Add(new MoveSetHelper(board, new Position(-3, 0)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, new Position(-3, 1)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, new Position(-3, 2)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, new Position(-3, 3)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, new Position(-2, 3)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, new Position(-1, 3)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, new Position(0, 3)).RightUp().CollectValidPositions());

        return positions;
    }

    public static List<List<Position>> GetValidTilesForShoot(PieceView player, Board board)
    {
        List<List<Position>> positions = new List<List<Position>>();

        positions.Add(new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition)).Right().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition)).RightDown().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition)).LeftUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition)).Left().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition)).LeftDown().CollectValidPositions());

        return positions;
    }

    public static List<List<Position>> GetValidTilesForCone(PieceView player, Board board)
    {
        List<List<Position>> positions = new List<List<Position>>();

        positions.Add(GetTileConeRightUp(player, board));
        positions.Add(GetTileConeRight(player, board));
        positions.Add(GetTileConeRightDown(player, board));
        positions.Add(GetTileConeLeftUp(player, board));
        positions.Add(GetTileConeLeft(player, board));
        positions.Add(GetTileConeLeftDown(player, board));

        return positions;
    }


    private static List<Position> GetTileConeRightUp(PieceView player, Board board)
    {
        return new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition))
            .RightUp(1)
            .LeftUp(1)
            .Right(1)
            .CollectValidPositions();
    }

    private static List<Position> GetTileConeRight(PieceView player, Board board)
    {
        return new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition))
            .RightUp(1)
            .LeftUp(1)
            .Right(1)
            .CollectValidPositions();
    }
    private static List<Position> GetTileConeRightDown(PieceView player, Board board)
    {
        return new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition))
            .RightDown(1)
            .Right(1)
            .LeftDown(1)
            .CollectValidPositions();
    }
    private static List<Position> GetTileConeLeftUp(PieceView player, Board board)
    {
        return new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition))
            .LeftUp(1)
            .RightUp(1)
            .Left(1)
            .CollectValidPositions();
    }

    private static List<Position> GetTileConeLeft(PieceView player, Board board)
    {
        return new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition))
            .Left(1)
            .LeftUp(1)
            .LeftDown(1)
            .CollectValidPositions();
    }
    private static List<Position> GetTileConeLeftDown(PieceView player, Board board)
    {
        return new MoveSetHelper(board, TileHelper.WorldToTilePosition(player.WorldPosition))
            .LeftDown(1)
            .Left(1)
            .RightDown(1)
            .CollectValidPositions();
    }
}
