using System.Windows.Forms;
using RaceGame.Classes;
using RaceGame.Delegates;
using System.Collections.Generic;
using System;

namespace RaceGame
{
    /// <summary>
    /// Dit is de basisclass and het aangrijppunt van het programma. 
    /// Hier worden de eerste basishandelingen uitgevoerd. Ook verzorgt dit gemakkelijke toegang tot de belangrijkste gameclasses
    /// </summary>
    public static class Base
    {
        public static Window windowHandle;
        public static Game currentGame;
        public static List<GameTask> gameTasks;
        public static List<DrawInfo> drawInfos;

        [STAThread]
        public static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            windowHandle = new Window();
            Application.Run(windowHandle);
        }
    }
}
