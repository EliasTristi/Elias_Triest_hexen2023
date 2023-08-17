using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetCollection : MonoBehaviour
{
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
