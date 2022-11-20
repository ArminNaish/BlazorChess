using System.Linq;

namespace BlazorChess.Model;

// https://en.wikipedia.org/wiki/Chess

public class Board
{
    private Board(IEnumerable<Square> squares, IEnumerable<Piece> pieces)
    {
        Squares = squares;
        Pieces = pieces;
    }

    public IEnumerable<Square> Squares { get; }

    public IEnumerable<Piece> Pieces { get; }

    // TODO: FromFEN

    private static Board Initialize()
    {
        var squares = Enumerable
            .Range(1, 8)
            .SelectMany(row => Enumerable
                .Range(1, 8)
                .Select(MakePosition)
                .Select(MakeSquare));

        var pieces = Enumerable
            .Range(1, 8)
            .SelectMany(row => Enumerable
                .Range(1, 8)
                .Select(MakePosition)
                .Select(MakePiece));

        return new Board(squares, pieces);
    }

    private static Position MakePosition(int row, int column) => new Position(row, column);

    private static Piece MakePiece(Position position)
    {
        return new Piece
        {
            Position = position,
            Shade = position.Row % 2 == position.Column % 2 ? ColorShade.Dark : ColorShade.Light,
            Type = position switch
            {
                (0 or 7, 0) => PieceType.Rook,
                (0 or 7, 1) => PieceType.Knight,
                (0 or 7, 2) => PieceType.Bishop,
                (0 or 7, 3) => PieceType.Queen,
                (0 or 7, 4) => PieceType.King,
                (0 or 7, 5) => PieceType.Bishop,
                (0 or 7, 6) => PieceType.Knight,
                (0 or 7, 7) => PieceType.Rook,
                (1 or 6, _) => PieceType.Pawn,
                _ => throw new NotSupportedException($"Position is not supported: {position}")
            }
        };
    }

    private static Square MakeSquare(Position position)
    {
        var files = "ABCDEFGH"; // cols
        var ranks = "87654321"; // rows
        return new Square
        {
            Rank = ranks[position.Row],
            File = files[position.Column],
            Position = position,
            Shade = position.Row % 2 == position.Column % 2 ? ColorShade.Light : ColorShade.Dark,
        };
    }

}

public record Square
{
    public required int Rank { get; init; }
    public required char File { get; init; }
    public required Position Position { get; init; }
    public required ColorShade Shade { get; init; }
}

public record Piece
{
    public required Position Position { get; init; }
    public required ColorShade Shade { get; init; }
    public required PieceType Type { get; init; }
}

public record Position(int Row, int Column);

public enum ColorShade
{
    Light,
    Dark
}

public enum PieceType
{
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


