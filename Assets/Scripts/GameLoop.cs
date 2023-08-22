using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Deck _deck;
    private Board _board;
    private Engine _engine;
    private PieceView _player;
    private BoardView _boardView;

    private PieceView[] _pieces;

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

        PieceView player = null;

        foreach(var view in pieceViews)
        {
            if (view.Player == Player.Player)
            {
                player = view;
                break;
            }
        }

        _pieces = pieceViews;

        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;

        _boardView = boardView;

        _engine = new Engine(_deck, _board, player, _pieces, _boardView);
        _deck.DeckSetup(_engine);
    }

    public void Undo() => _engine.UndoAction();

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        _engine.CardLogic(e.Position);
    }
}
