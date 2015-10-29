using System;

namespace RaceGame
{

    /// <summary>
    /// Dit is de progressbars class. Deze bevat de methods die de balken updaten die onderaan de user interface komen te staan
    /// </summary>
    public static class Progressbars
    {
        public static int TotalLaps;

        /// <summary>
        /// Hier worden de minima en maxima van de progressbars ingesteld
        /// </summary>
        public static void Initialize()
        {
            Base.windowHandle.Player1Fuel.Maximum = Convert.ToInt32(Base.currentGame.player1.vehicle.fuelCapacity);
            Base.windowHandle.Player2Fuel.Maximum = Convert.ToInt32(Base.currentGame.player2.vehicle.fuelCapacity);
            Base.windowHandle.Player1Health.Maximum = Convert.ToInt32(Base.currentGame.player1.vehicle.maxHealth);
            Base.windowHandle.Player2Health.Maximum = Convert.ToInt32(Base.currentGame.player2.vehicle.maxHealth);
            Base.windowHandle.Player1Speed.Maximum = Convert.ToInt32(Base.currentGame.player1.vehicle.maxSpeed * 100);
            Base.windowHandle.Player2Speed.Maximum = Convert.ToInt32(Base.currentGame.player2.vehicle.maxSpeed * 100);
        }

        /// <summary>
        /// Deze functie update elke frame de waardes van de progressbars. Hij staat dan dus ook in de Gametasks
        /// </summary>
        public static void Check()
        {
            Base.windowHandle.Player1Fuel.Value = Convert.ToInt32(Base.currentGame.player1.vehicle.fuel);
            Base.windowHandle.Player2Fuel.Value = Convert.ToInt32(Base.currentGame.player2.vehicle.fuel);
            Base.windowHandle.Player1Health.Value = Base.currentGame.player1.vehicle.health;
            Base.windowHandle.Player2Health.Value = Base.currentGame.player2.vehicle.health;


            if (Base.currentGame.player1.vehicle.speed > Base.currentGame.player1.vehicle.maxSpeed)
            {
                Base.windowHandle.Player1Speed.Value = Convert.ToInt32(Base.currentGame.player1.vehicle.maxSpeed * 100);
            }
            else
            {
                Base.windowHandle.Player1Speed.Value = Convert.ToInt32(Math.Abs(Base.currentGame.player1.vehicle.speed * 100));
            }
            if (Base.currentGame.player2.vehicle.speed > Base.currentGame.player2.vehicle.maxSpeed)
            {
                Base.windowHandle.Player2Speed.Value = Convert.ToInt32(Base.currentGame.player2.vehicle.maxSpeed * 100);
            }
            else
            {
                Base.windowHandle.Player2Speed.Value = Convert.ToInt32(Math.Abs(Base.currentGame.player2.vehicle.speed * 100));
            }

            Base.windowHandle.Player1PitCount.Text = Base.currentGame.player1.vehicle.pitstopCounter + "/" + TotalLaps;
            Base.windowHandle.Player2PitCount.Text = Base.currentGame.player2.vehicle.pitstopCounter + "/" + TotalLaps;

            Base.windowHandle.Player1LapCount.Text = Base.currentGame.player1.LapCounter + "/" + TotalLaps;
            Base.windowHandle.Player2LapCount.Text = Base.currentGame.player2.LapCounter + "/" + TotalLaps;
        }
    }  
}
