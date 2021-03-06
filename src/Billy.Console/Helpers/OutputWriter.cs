﻿using System;

namespace Billy.Console.Helpers
{
    /// <summary>
    /// Defines the output way.
    /// </summary>
    internal class OutputWriter
    {
        private static readonly object LockObject = new object();

        public static void Print(string? message)
            => PrintInternal(message, ConsoleColor.DarkYellow);

        public static void PrintError(string? message)
            => PrintInternal(message, ConsoleColor.DarkRed);

        private static void PrintInternal(string? message, ConsoleColor color)
        {
            lock (LockObject)
            {
                var oldColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = color;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = oldColor;
            }
        }
    }
}