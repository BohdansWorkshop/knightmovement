using KnightMovement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnightMovement.Interfaces
{
    interface IKnightBehavior
    {
        List<string> CaptureFigures(string knightPosition, string[] figures);
        List<DeskSquareModel> FindKnightsPath(FigureModel knightPosition, FigureModel figure, List<FigureModel> friendlyFigures);
    }
}
