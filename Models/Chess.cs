using System.Linq;

namespace BlazorChess.Models.Chess;

// https://en.wikipedia.org/wiki/Chess

// TODO: bind board to view

public class Board
{
    private static readonly string _ranks = "87654321"; // the rows of the board
    private static readonly string _files = "ABCDEFGH"; // the columns of the board

    private Board(IEnumerable<Square> squares)
    {
        Squares = squares;
    }

    public IEnumerable<Square> Squares { get; }

    // TODO: FromFEN

    public static Board Initialize()
    {
        var squares = Enumerable
            .Range(0, 8)
            .SelectMany(row => Enumerable
                .Range(0, 8)
                .Select(col => new Position { Row = row, Column = col })
                .Select(MakeSquare));

        return new Board(squares);
    }

    private static Square MakeSquare(Position position)
    {
        var piece = MakePiece(position);

        var shade = position.Row % 2 == position.Column % 2
            ? ColorType.Light
            : ColorType.Dark;

        return new Square
        {
            // Rank = int.Parse(_ranks[position.Row].ToString()),
            // File = _files[position.Column],
            //Position = position,
            Color = shade,
            Piece = piece
        };
    }

    private static Piece MakePiece(Position position)
    {
        return position switch
        {
            (0, 0 or 7) => new Piece { Type = PieceType.Rook, Character = "♜" },
            (7, 0 or 7) => new Piece { Type = PieceType.Rook, Character = "♖" },
            (0, 1 or 6) => new Piece { Type = PieceType.Knight, Character = "♞" },
            (7, 1 or 6) => new Piece { Type = PieceType.Knight, Character = "♘" },
            (0, 2 or 5) => new Piece { Type = PieceType.Bishop, Character = "♝" },
            (7, 2 or 5) => new Piece { Type = PieceType.Bishop, Character = "♗" },
            (0, 3) => new Piece { Type = PieceType.Queen, Character = "♛" },
            (7, 3) => new Piece { Type = PieceType.Queen, Character = "♕" },
            (0, 4) => new Piece { Type = PieceType.King, Character = "♚" },
            (7, 4) => new Piece { Type = PieceType.King, Character = "♔" },
            (1, _) => new Piece { Type = PieceType.Pawn, Character = "♟" },
            (6, _) => new Piece { Type = PieceType.Pawn, Character = "♙" },
            _ => new Piece { Type = PieceType.Empty, Character = "" },
        };
    }
}

public record Square
{
    // public required int Rank { get; init; }
    // public required char File { get; init; }
    // public required Position Position { get; init; }
    public required ColorType Color { get; init; }
    public Piece? Piece { get; init; }
}

public record Piece
{
    //public required Position Position { get; init; }
    //public required ColorShade Shade { get; init; }
    public required PieceType Type { get; init; }
    public required string Character { get; init; }
}



public record Position()
{
    public required int Row { get; init; }
    public required int Column { get; init; }
    public void Deconstruct(out int row, out int column) => (row, column) = (Row, Column);
}

public enum ColorType
{
    Light,
    Dark
}

public enum PieceType
{
    Empty,
    King,
    Rook,
    Bishop,
    Queen,
    Knight,
    Pawn
}

// TODO: a piece has a list of moves


// private record Move
// {
//     public required MoveType Type { get; init; }
// }

// private enum MoveType
// {
//     // todo: find better names
//     // AnyDirection,
//     // Diagonal,
//     // VerticalOrHorizontal,
//     // Jump
// }


