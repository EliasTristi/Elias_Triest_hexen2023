using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board;
    private Deck _deck;
    private PieceView _player;

    [SerializeField]
    private GameObject _entity;
    [SerializeField]
    private int _entityAmount = 8;

    void Start()
    {
        _deck = FindObjectOfType<Deck>();
        _board = new Board(TileHelper.Distance);

        Spawner.EntitySpawner(_entity, _entityAmount);

        _board.PieceMoved += (s, e) => e.Piece.Move(TileHelper.TileToWorldPosition(e.toPosition));
        _board.PieceTaken += (s, e) => e.Piece.Take();
        _board.PiecePlaced += (s, e) => e.Piece.Place(TileHelper.TileToWorldPosition(e.toPosition));

        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
            _board.Place(TileHelper.WorldToTilePosition(pieceView.WorldPosition), pieceView);

        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;

        foreach(var view in pieceViews)
        {
            if (view.Player == Player.Player)
            {
                _player = view;
                break;
            }
        }
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        var cards = FindObjectsOfType<Card>();

        foreach (var card in cards)
        {
            if (card.IsPlayed)
            {
                switch (card.CardType)
                {
                    case CardType.Laser:

                        break;
                    case CardType.Teleport: 
                        _board.Move(TileHelper.WorldToTilePosition(_player.WorldPosition), e.Position);
                        break;
                    case CardType.Push:
                        
                        break;
                    case CardType.Slash:
                        
                        break;
                }
            }
        }

        _deck.DeckUpdate();
    }
}
