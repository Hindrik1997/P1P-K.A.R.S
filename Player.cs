using RaceGame.Enums;
using System;

namespace RaceGame
{
    /// <summary>
    /// De player class die alle eigenschappen van de spelers managen inc. de Vehicle's en hun wapens
    /// </summary>
    public class Player
    {

        public int playerID;
        public PlayerType playerType;
        public Vehicle vehicle;
        public VehicleType vehicleType;
        public int LapCounter = 0;

        /// <summary>
        /// Constructor om een speler te creëeren
        /// </summary>
        /// <param name="i">De speler ID</param>
        /// <param name="_playerType">Het type speler, voorlopig enkel menselijk.</param>
        /// <param name="_vehicleType">Het gekozen voertuig</param>
        public Player(int i, PlayerType _playerType = PlayerType.Human, VehicleType _vehicleType = VehicleType.HorsePower)
        {
            playerID = i;
            playerType = _playerType;
            vehicleType = _vehicleType;
        }

        /// <summary>
        /// Deze method maakt een Vehicle aan en stelt de spawnpositie in.
        /// </summary>
        /// <param name="vehicleType"></param>
        /// <param name="Position"></param>
        public void CreateVehicle(VehicleType vehicleType, Point Position)
        {
            switch (vehicleType)
            {
                case VehicleType.Tank:
                    vehicle = new Tank(Position.x, Position.y, this);
                    break;
                case VehicleType.Jackass:
                    vehicle = new Jackass(Position.x, Position.y, this);
                    break;
                case VehicleType.LAPV:
                    vehicle = new LAPV(Position.x, Position.y, this);
                    break;
                case VehicleType.HorsePower:
                    vehicle = new HorsePower(Position.x, Position.y, this);
                    break;
                case VehicleType.Motorfiets:
                    vehicle = new Motorfiets(Position.x, Position.y, this);
                    break;
            }
        }



    }
}
