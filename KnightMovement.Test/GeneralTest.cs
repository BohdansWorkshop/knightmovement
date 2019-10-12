using KnightMovement.Enums;
using KnightMovement.Interfaces;
using KnightMovement.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private KnightFigureModel KnightFigureModel;
        private ChessDeskModel DeskBehavior;

        [SetUp]
        public void Setup()
        {
            this.DeskBehavior = new ChessDeskModel();
            this.KnightFigureModel = new KnightFigureModel(DeskBehavior);
        }

        [Description("Testing primitive knight move which affects 3 desk squares")]
        [Test]
        public void PrimitiveMove1()
        {
            var firstMoveConfiguration = KnightFigureModel.FindKnightsPath(new FigureModel() { X = 1, Y = 1 }, new FigureModel() { X = 3, Y = 1 }, new List<FigureModel>());
            var firstTotalMoves = 3;
            Assert.AreEqual(firstMoveConfiguration.Count, firstTotalMoves);
        }

        [Description("Testing primitive knight move which affects 5 desk squares")]
        [Test]
        public void PrimitiveMove2()
        {
            var secondMoveConfiguration = KnightFigureModel.FindKnightsPath(new FigureModel() { X = 2, Y = 5 }, new FigureModel() { X = 7, Y = 6 }, new List<FigureModel>());
            var secondTotalMoves = 5;
            Assert.AreEqual(secondMoveConfiguration.Count, secondTotalMoves);
        }

        [Description("Testing coordinates interpreter from chess marking to numerical")]
        [Test]
        public void CoordinatesInterpreter1()
        {
            var firstCoordinate = "0 a6";

            var firstFigure = DeskBehavior.ChessToNumericalCoordinates(firstCoordinate);

            Assert.AreEqual(firstFigure.X, 1);
            Assert.AreEqual(firstFigure.Y, 6);
            Assert.AreEqual(firstFigure.Color, FigureColor.Black);
        }

        [Description("Testing coordinates interpreter from chess marking to numerical")]
        [Test]
        public void CoordinatesInterpreter2()
        {
            var secondCoordinate = "1 c5";

            var secondFigure = DeskBehavior.ChessToNumericalCoordinates(secondCoordinate);

            Assert.AreEqual(secondFigure.X, 3);
            Assert.AreEqual(secondFigure.Y, 5);
            Assert.AreEqual(secondFigure.Color, FigureColor.White);
        }

        [Description("Testing coordinates interpreter from chess marking to numerical")]
        [Test]
        public void CoordinatesInterpreter3()
        {
            var thirdCoordinate = "1 h8";

            var thirdFigure = DeskBehavior.ChessToNumericalCoordinates(thirdCoordinate);

            Assert.AreEqual(thirdFigure.X, 8);
            Assert.AreEqual(thirdFigure.Y, 8);
            Assert.AreEqual(thirdFigure.Color, FigureColor.White);
        }

        [Description("Testing coordinates interpreter from numberical to chess marking")]
        [Test]
        public void CoordinatesInterpreter4()
        {
            var chessCoordinates = DeskBehavior.NumericalToChessCoordinates(2, 3);
            Assert.AreEqual(chessCoordinates, "B3");
        }

        [Description("Testing coordinates interpreter from numberical to chess marking")]
        [Test]
        public void CoordinatesInterpreter5()
        {
            var chessCoordinates = DeskBehavior.NumericalToChessCoordinates(8, 5);
            Assert.AreEqual(chessCoordinates, "H5");
        }
    }
}