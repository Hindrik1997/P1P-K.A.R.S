using System.Media;

namespace RaceGame
{
    /// <summary>
    /// Class waar de audiofiles zich in bevinden.
    /// </summary>
    public static class AudioFiles
    {
        public static SoundPlayer AdvancedCrash = new SoundPlayer("advancedcrash.wav");
        public static SoundPlayer DesertEagle = new SoundPlayer("deserteagle.wav");
        public static SoundPlayer Fail = new SoundPlayer("fail.wav");
        public static SoundPlayer Gunshot1 = new SoundPlayer("gunshot.wav");
        public static SoundPlayer Gunshot2 = new SoundPlayer("gunshot2.wav");
        public static SoundPlayer Gunshot3 = new SoundPlayer("gunshot3.wav");
        public static SoundPlayer Horn1 = new SoundPlayer("horn.wav");
        public static SoundPlayer Horn2 = new SoundPlayer("horn2.wav");
        public static SoundPlayer Horn3 = new SoundPlayer("horn3.wav");
        public static SoundPlayer HorseSound = new SoundPlayer("horse.wav");
        public static SoundPlayer LAPVSound = new SoundPlayer("LAPV.wav");
        public static SoundPlayer LowFuel = new SoundPlayer("lowfuel.wav");
        public static SoundPlayer MGSound = new SoundPlayer("MG.wav");
        public static SoundPlayer Motor = new SoundPlayer("motor.wav");
        public static SoundPlayer PaardEnWagen = new SoundPlayer("Paard en wagen.wav");
        public static SoundPlayer RegenFuel = new SoundPlayer("regenfuel.wav");
        public static SoundPlayer SimpleCrash = new SoundPlayer("simplecrash.wav");
        public static SoundPlayer Tank = new SoundPlayer("Tank.wav");
        public static SoundPlayer TankShot = new SoundPlayer("tankshot.wav");
        public static SoundPlayer Win = new SoundPlayer("win.wav");
        public static SoundPlayer Winkelwagen = new SoundPlayer("Winkelwagen.wav");
        public static SoundPlayer Flame = new SoundPlayer("flame.wav");

        public static bool IsCrashing = false;

        public static void loadSounds() {
            AdvancedCrash.Load();
            DesertEagle.Load();
            Fail.Load();
            Gunshot1.Load();
            Gunshot2.Load();
            Gunshot3.Load();
            Horn1.Load();
            Horn2.Load();
            Horn3.Load();
            HorseSound.Load();
            LAPVSound.Load();
            LowFuel.Load();
            MGSound.Load();
            Motor.Load();
            PaardEnWagen.Load();
            RegenFuel.Load();
            SimpleCrash.Load();
            Tank.Load();
            TankShot.Load();
            Win.Load();
            Winkelwagen.Load();
            Flame.Load();
        }
    }
}
