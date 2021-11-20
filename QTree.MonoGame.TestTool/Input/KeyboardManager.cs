using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace QTree.MonoGame.TestTool.Input
{
    public static class KeyboardManager
    {
        private static KeyboardState _previousState = new KeyboardState();
        private static KeyboardState _currentState = new KeyboardState();

        public static void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public static IDictionary<Keys, int> NumberKeys { get; } = new Dictionary<Keys, int>
            {
                { Keys.D0, 0 },
                { Keys.D1, 1 },
                { Keys.D2, 2 },
                { Keys.D3, 3 },
                { Keys.D4, 4 },
                { Keys.D5, 5 },
                { Keys.D6, 6 },
                { Keys.D7, 7 },
                { Keys.D8, 8 },
                { Keys.D9, 9 }
            };

        public static IDictionary<Keys, string> StringKeys { get; } = new Dictionary<Keys, string>
        {
            { Keys.D0, "0" },
            { Keys.D1, "1" },
            { Keys.D2, "2" },
            { Keys.D3, "3" },
            { Keys.D4, "4" },
            { Keys.D5, "5" },
            { Keys.D6, "6" },
            { Keys.D7, "7" },
            { Keys.D8, "8" },
            { Keys.D9, "9" },
            { Keys.Q, "Q" },
            { Keys.W, "W" },
            { Keys.E, "E" },
            { Keys.R, "R" },
            { Keys.T, "T" },
            { Keys.Y, "Y" },
            { Keys.U, "U" },
            { Keys.I, "I" },
            { Keys.O, "O" },
            { Keys.P, "P" },
            { Keys.A, "A" },
            { Keys.S, "S" },
            { Keys.D, "D" },
            { Keys.F, "F" },
            { Keys.G, "G" },
            { Keys.H, "H" },
            { Keys.J, "J" },
            { Keys.K, "K" },
            { Keys.L, "L" },
            { Keys.Z, "Z" },
            { Keys.X, "X" },
            { Keys.C, "C" },
            { Keys.V, "V" },
            { Keys.B, "B" },
            { Keys.N, "N" },
            { Keys.M, "M" },
            { Keys.OemComma, "," },
            { Keys.OemPeriod, "." },
            { Keys.OemMinus, "-" },
            { Keys.Space, " " },
            { Keys.Divide, "/" }
        };

        public static string GetKeyString(Keys key)
        {
            return StringKeys.ContainsKey(key) ? StringKeys[key] : string.Empty;
        }

        public static bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        public static bool IsKeysDown(params Keys[] keys)
        {
            var currentKeys = CurrentKeys();
            return keys.All(x => IsKeyDown(x));
        }

        public static bool IsKeyHeld(Keys key)
        {
            return _currentState.IsKeyDown(key) && _previousState.IsKeyDown(key);
        }

        public static bool IsKeyClicked(Keys key)
        {
            return _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
        }

        public static bool IsKeysClicked(params Keys[] keys)
        {
            return keys.All(x => _currentState.IsKeyDown(x)) && keys.Any(x => _previousState.IsKeyUp(x));
        }

        public static Keys[] CurrentKeys()
        {
            return _currentState.GetPressedKeys();
        }
    }
}
