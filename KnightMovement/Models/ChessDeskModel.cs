using System;
using System.Collections.Generic;
using KnightMovement.Enums;
using KnightMovement.Interfaces;

namespace KnightMovement.Models
{
    public class ChessDeskModel : IDeskBehavior
    {
        const int maxOX = 8;
        const int minOX = 1;
        const int maxOY = 8;
        const int minOY = 1;
        private readonly Dictionary<string, int> LettersCoordinates;

        public ChessDeskModel()
        {
            var deskLetters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };
            LettersCoordinates = new Dictionary<string, int>();
            for (int i = 0; i < deskLetters.Length; i++)
            {
                LettersCoordinates.Add(deskLetters[i], i + 1);
            };
        }

        public FigureModel  ChessToNumericalCoordinates(string input)
        {
            input = input.Replace(" ", "").ToLower();
            var color = (FigureColor)int.Parse(input[0].ToString());

            var firstPointValue = input[1].ToString();
            var secondNumber = int.Parse(input[2].ToString());
            return new FigureModel() { X = LettersCoordinates[firstPointValue], Y = secondNumber, Color = color };
        }

        public bool IsValidCoordinates(int x, int y)
        {
            if (x <= maxOX && x >= minOX && y <= maxOY && y >= minOY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string NumericalToChessCoordinates(int x, int y)
        {
            var chars = (char)(x - 1 + 'A');
            return $"{chars.ToString()}{y}";
        }
    }
}
