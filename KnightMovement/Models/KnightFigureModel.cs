using KnightMovement.Enums;
using KnightMovement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightMovement.Models
{
    public class KnightFigureModel : FigureModel, IKnightBehavior
    {
        private List<Tuple<int, int>> KnightMoves { get; set; }
        private IDeskBehavior FigureBehavior { get; set; }

        public KnightFigureModel(IDeskBehavior figureBehavior)
        {
            this.KnightMoves = new List<Tuple<int, int>> { Tuple.Create(1, 2), Tuple.Create(2, 1), Tuple.Create(-1, 2), Tuple.Create(1, -2), Tuple.Create(-2, 1), Tuple.Create(2, -1), Tuple.Create(-2, -1), Tuple.Create(-1, -2) };
            this.FigureBehavior = figureBehavior;
        }

        private List<DeskSquareModel> GetAllKnightMoves(DeskSquareModel square)
        {
            var possiblePositions = new List<DeskSquareModel>();

            foreach (var move in KnightMoves)
            {
                var x = move.Item1;
                var y = move.Item2;

                var newX = move.Item1 + square.X;
                var newY = move.Item2 + square.Y;

                if (FigureBehavior.IsValidCoordinates(newX, newY))
                {
                    possiblePositions.Add(new DeskSquareModel() { X = newX, Y = newY });
                }
            }
            return possiblePositions;
        }

        public List<string> CaptureFigures(string knightPosition, string[] figures)
        {
            var knightCoordinates = FigureBehavior.ChessToNumericalCoordinates(knightPosition);

            var figuresList = new List<FigureModel>();
            figuresList.Add(knightCoordinates);
            foreach (var item in figures)
            {
                figuresList.Add(FigureBehavior.ChessToNumericalCoordinates(item));
            }

            var friendlyFigures = figuresList.Where(x => x.Color == FigureColor.White && (x.X != knightCoordinates.X || x.Y != knightCoordinates.Y)).ToList();
            figuresList.RemoveAll(x=>friendlyFigures.Contains(x));

            var moves = new List<DeskSquareModel>();

            var result = new List<string>();

            for (int i = 0; i < figuresList.Count-1; i++)
            {
                moves.AddRange(FindKnightsPath(figuresList[i], figuresList[i+1], friendlyFigures));
                result.AddRange(GenerateResult(FindKnightsPath(figuresList[i], figuresList[i + 1], friendlyFigures)));
            }

            return result;
        }


        private List<string> GenerateResult (List<DeskSquareModel> moves)
        {
            var result = new List<string>();


            for(int i = 0; i < moves.Count-1; i++)
            {
                var currentItem = moves[i];
                var nextItem = moves[i + 1];
                var currentMove = FigureBehavior.NumericalToChessCoordinates(currentItem.X, currentItem.Y);
                var nextMove = FigureBehavior.NumericalToChessCoordinates(nextItem.X, nextItem.Y);

                var moveResult = (nextItem.X == moves.Last().X && nextItem.Y == moves.Last().Y)  ? "x" : "-";

                result.Add($"K{currentMove}{moveResult}{nextMove}");
            };

            return result;
        }

        public List<DeskSquareModel> FindKnightsPath(FigureModel knightPosition, FigureModel figure, List<FigureModel> friendlyFigures)
        {
            var movesQueue = new Queue<List<DeskSquareModel>>();
            var visitedList = new List<DeskSquareModel>();
            visitedList.AddRange(friendlyFigures);

            var TheShortestPath = new List<DeskSquareModel>
            {
            knightPosition
            };

            movesQueue.Enqueue(TheShortestPath);

            while (movesQueue.Count != 0)
            {
                List<DeskSquareModel> currentPath = movesQueue.Dequeue();
                DeskSquareModel currentSquare = currentPath.Last();

                if ((currentSquare.X == figure.X) && (currentSquare.Y == figure.Y))
                {
                    return currentPath;
                }

                var allPossibleMoves = GetAllKnightMoves(currentSquare);
                foreach (var nearSquare in allPossibleMoves)
                {

                    if (!visitedList.Any(x => x.X == nearSquare.X && x.Y == nearSquare.Y))
                    {
                        var moves = new List<DeskSquareModel>();
                        moves.AddRange(currentPath);
                        moves.Add(nearSquare);

                        visitedList.Add(nearSquare);

                        movesQueue.Enqueue(currentPath);
                        movesQueue.Enqueue(moves);
                    }
                }

            }

            //No way in here to capture all the figures, returning empty list.
            return new List<DeskSquareModel>();
        }
    }
}
