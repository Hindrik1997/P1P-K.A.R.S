using System.Drawing;

namespace RaceGame.Delegates
{
    /// <summary>
    /// Dit is de GameTasks delegate, hier staan alle methods die elke frame uitgevoerd moeten worden in. 
    /// Deze methods moeten voldoen aan het signatuur van deze delegate
    /// </summary>
    public delegate void GameTask();
}

namespace RaceGame.Enums
{
    /// <summary>
    /// Hier staan de mogelijke spelertypes in. 
    /// AI is nu nog niet geïmplementeerd, deze Enum is belangrijk om voor later die mogelijkheid open te houden.
    /// </summary>
    public enum PlayerType
    {
        Human,
        AI
    }

    /// <summary>
    /// Hier staan alle soort voortuigen in
    /// </summary>
    public enum VehicleType
    {
        Tank,
        Jackass,
        LAPV,
        HorsePower,
        Motorfiets,
    }
}

namespace RaceGame.Classes
{
    /// <summary>
    /// Hier wordt de DrawInfo vastgelegd. Deze wordt gebruikt om alles op het scherm te tekenen.
    /// Bevat de bitmap en de positie en de hoek voor het tekenen.
    /// Daarnaast is het, doordat het een reference type is, gemakkelijk om deze posities/hoeken bij te werken.
    /// </summary>
    public class DrawInfo
    {
        public Bitmap bitmapdata;
        public float x;
        public float y;
        public int width;
        public int height;
        public bool AutoRemove;
        public int Frames;
        public float angle;
        public PointF rotatePoint;

        /// <summary>
        /// Creëert een DrawInfo met evt. optionele parameters. Angle = 0 betekent dat hij omhoog getekend wordt.
        /// </summary>
        public DrawInfo(Bitmap bitmap, int x, int y, int width, int height, float _angle = 0, float RotateX = 0f, float RotateY = 0f, bool AutoRemove = false, int Frames = 0)
        {
            bitmapdata = bitmap;
            this.x = (int)x;
            this.y = (int)y;
            this.width = width;
            this.height = height;
            this.AutoRemove = AutoRemove;
            this.Frames = Frames;
            this.angle = _angle;
            rotatePoint = new PointF(RotateX,RotateY);
        }
    }
}