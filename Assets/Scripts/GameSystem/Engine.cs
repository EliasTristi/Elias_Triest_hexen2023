using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private Deck _deck;
    private Board _board;
    private PieceView _player;
    private PieceView[] _enemies;
    private BoardView _boardView;

    private List<Position> _selectedPositions;
    public List<Position> SelectedPositions => _selectedPositions;

    public Engine(Deck deck, Board board, PieceView player, PieceView[] enemies, BoardView boardView)
    {
        _deck = deck;
        _board = board;
        _player = player;
        _enemies = enemies;
        _boardView = boardView;
    }

    public void CardLogic(Position position)
    {
        var cards = _deck.GetComponentsInChildren<Card>();

        foreach (var card in cards)
        {
            if (card.IsPlayed)
            {
                if (card.CardType == CardType.Teleport)
                    card.IsPlayed = _board.Move(TileHelper.WorldToTilePosition(_player.WorldPosition), position);
                else if (card.CardType == CardType.Meteor)
                {
                    var positionGroups = MoveSetCollection.GetValidTilesForMeteor(position, _board);
                    foreach (var validPositions in positionGroups)
                    {
                        foreach (var validPosition in validPositions)
                        {
                            _board.Take(validPosition);
                        }
                    }

                }
                else if (card.CardType == CardType.Laser)
                    foreach (var selectedPos in _selectedPositions)
                        _board.Take(selectedPos);
                else if (card.CardType == CardType.Slash)
                    foreach (var selectedPos in _selectedPositions)
                        _board.Take(selectedPos);
                else if (card.CardType == CardType.Push)
                    foreach (var selectedPos in _selectedPositions)
                    {
                        var offset = HexHelper.AxialSubstract(selectedPos, TileHelper.WorldToTilePosition(_player.WorldPosition));
                        var moveTo = HexHelper.AxialAdd(selectedPos, offset);

                        if (_board.IsValid(moveTo))
                            _board.Move(selectedPos, moveTo);
                        else
                            _board.Take(selectedPos);
                    }
                else if (!_selectedPositions.Contains(position))
                {
                    card.IsPlayed = false;
                    return;
                }
            }
        }
        _deck.DeckUpdate();
    }

    internal void SetActivePositions(List<Position> positions)
    {
        _boardView.SetActivePositions = positions;
    }

    internal List<Position> GetValidPositions(CardType cardType)
    {
        List<Position> positions = new List<Position>();

        if (cardType == CardType.Teleport)
        {
            foreach (var position in _boardView.ActivePositions)
            {
                bool IsPositionFree = true;

                foreach (var piece in _enemies)
                {
                    var piecePosition = TileHelper.WorldToTilePosition(piece.WorldPosition);

                    if (position.q == piecePosition.q && position.r == piecePosition.r /*&& piece.gameObject.activeSelf*/)
                    {
                        IsPositionFree = false;
                        break;
                    }
                }

                if (IsPositionFree)
                    positions.Add(position);
            }

            return positions;
        }

        return null;
    }

    internal List<List<Position>> GetValidPositionGroups(CardType cardType)
    {
        if (cardType == CardType.Laser)
            return MoveSetCollection.GetValidTilesForShoot(_player, _board);
        else if (cardType == CardType.Slash || cardType == CardType.Push)
            return MoveSetCollection.GetValidTilesForCone(_player, _board);
        else if (cardType == CardType.Meteor)
            return MoveSetCollection.GetAllTiles(_board);

        return null;
    }

    public void SetHighlights(Position position, CardType type, List<Position> validPositions, List<List<Position>> validPositionGroups = null)
    {
        switch (type)
        {
            case CardType.Teleport:
                if (validPositions.Contains(position))
                {
                    List<Position> positions = new List<Position>();

                    positions.Add(position);
                    SetActivePositions(positions);
                }
                break;
            case CardType.Laser:
            case CardType.Slash:
            case CardType.Push:
                if (!validPositions.Contains(position))
                    SetActivePositions(validPositions);
                else
                {
                    foreach (List<Position> positions in validPositionGroups)
                    {
                        if (positions.Count == 0)
                            continue;

                        if ((positions.Contains(position) && CardType.Laser == type)
                            || (positions[0] == position && CardType.Slash == type)
                            || (positions[0] == position && CardType.Push == type))
                        {
                            SetActivePositions(positions);
                            _selectedPositions = positions;
                            break;
                        }
                    }
                }
                break;
            case CardType.Meteor:
                var positionGroup = MoveSetCollection.GetValidTilesForMeteor(position, _board);
                var positionList = new List<Position>();
                
                foreach (var posList in positionGroup)
                {
                    positionList.AddRange(posList);
                }

                positionList.Add(position);
                SetActivePositions(positionList);
                break;
            default:
                _selectedPositions = new List<Position>();
                break;
        }
    }
}
