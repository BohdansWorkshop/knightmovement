using KnightMovement.Models;
using System;

namespace KnightMovement.Interfaces
{
    public interface IDeskBehavior
    {
        bool IsValidCoordinates(int x, int y);
        string NumericalToChessCoordinates(int x, int y);
        FigureModel ChessToNumericalCoordinates(string x);
    }
}
