using System;

namespace RaceGame
{
    /// <summary>
    /// Pitstop class die de pitstop code bevat
    /// </summary>
    class Pitstop
    {
        int PosX = Base.currentGame.PitStopPoint.x;
        int PosY = Base.currentGame.PitStopPoint.y;
        int Range = 36;
        bool PitstopPlays = false;
        int timeout = 0;

        /// <summary>
        /// Dit is de constructor van de pitstop class. Hierin wordt de checkpitstop() method toegevoegd aan de gameloop
        /// </summary>
        public Pitstop()
        {
            Base.gameTasks.Add(CheckPitStop);
        }

        /// <summary>
        /// In deze method wordt elke frame gecontroleerd of een speler in de pitstop staat. In de pitstop wordt de fuel en health bijgevuld
        /// Ook wordt gedetecteerd wanneer deze de pitstop weer verlaat zodat er 1 bij de pitstopcounter kan opgeteld worden.
        /// </summary>
        void CheckPitStop()
        {
            if (PitstopPlays && timeout > 0)
                timeout--;

            if (Base.currentGame.player1.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player1.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player1.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player1.vehicle.drawInfo.y >= PosY - Range && Base.currentGame.player1.vehicle.pitstopCounter < Base.windowHandle.TotalLaps)
            {
                Base.currentGame.player1.vehicle.inPitstop = true;
                if (timeout == 0)
                {
                    AudioFiles.RegenFuel.Play();
                    timeout = 100;
                    PitstopPlays = true;
                }
            }

            else if(Base.currentGame.player1.vehicle.inPitstop)
            {
                Base.currentGame.player1.vehicle.pitstopCounter++;
                Base.currentGame.player1.vehicle.inPitstop = false;
            }

            if (Base.currentGame.player2.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player2.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player2.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player2.vehicle.drawInfo.y >= PosY - Range && Base.currentGame.player2.vehicle.pitstopCounter < Base.windowHandle.TotalLaps)
            {
                Base.currentGame.player2.vehicle.inPitstop = true;
                if (timeout == 0)
                {
                    AudioFiles.RegenFuel.Play();
                    timeout = 100;
                    PitstopPlays = true;
                }
            }

            else if (Base.currentGame.player2.vehicle.inPitstop)
            {
                Base.currentGame.player2.vehicle.pitstopCounter++;
                Base.currentGame.player2.vehicle.inPitstop = false;
            }


            if ((Base.currentGame.player1.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player1.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player1.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player1.vehicle.drawInfo.y >= PosY - Range) && Math.Abs(0-Base.currentGame.player1.vehicle.speed) < 0.1)
            {
                Base.currentGame.player1.vehicle.inPitstop = true;
                if (Base.currentGame.player1.vehicle.fuel < Base.currentGame.player1.vehicle.fuelCapacity)
                {
                    Base.currentGame.player1.vehicle.fuel += 4;
                    if (Base.currentGame.player1.vehicle.fuel > Base.currentGame.player1.vehicle.fuelCapacity)
                    {
                        Base.currentGame.player1.vehicle.fuel = Base.currentGame.player1.vehicle.fuelCapacity;
                    }
                }

                if (Base.currentGame.player1.vehicle.health < Base.currentGame.player1.vehicle.maxHealth)
                {
                    Base.currentGame.player1.vehicle.health += 1;
                }
            }

            if ((Base.currentGame.player2.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player2.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player2.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player2.vehicle.drawInfo.y >= PosY - Range) && Math.Abs(0 - Base.currentGame.player2.vehicle.speed) < 0.1)
            {
                Base.currentGame.player2.vehicle.inPitstop = true;
                if (Base.currentGame.player2.vehicle.fuel < Base.currentGame.player2.vehicle.fuelCapacity)
                {
                    Base.currentGame.player2.vehicle.fuel += 4;
                    if (Base.currentGame.player2.vehicle.fuel > Base.currentGame.player2.vehicle.fuelCapacity)
                    {
                        Base.currentGame.player2.vehicle.fuel = Base.currentGame.player2.vehicle.fuelCapacity;
                    }
                }

                if (Base.currentGame.player2.vehicle.health < Base.currentGame.player2.vehicle.maxHealth)
                {
                    Base.currentGame.player2.vehicle.health += 1;
                }
            }
        }
    }
}