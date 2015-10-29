﻿using System.Drawing;

namespace RaceGame
{
    /// <summary>
    /// Deze class bevat statische classes met bitmaps erin.
    /// Zo hoeven we maar 1 malig een bitmap in te laden en in het geheugen te laden.
    /// Dit scheelt een hele hoop geheugen.
    /// </summary>
    public static class Bitmaps
    {
        public static class Vehicles
        {
            public static Bitmap TankBody = new Bitmap("tankbody.png");
            public static Bitmap TankWeapon = new Bitmap("loop.png");

            public static Bitmap Jackass = new Bitmap("cart.png");

            public static Bitmap LAPVBody = new Bitmap("lapv.png");
            public static Bitmap LAPVWeapon = new Bitmap("lapv turret.png");

            public static Bitmap HorsePowerBody = new Bitmap("horse.png");

            public static Bitmap MotorfietsBody = new Bitmap("motor.png");
            public static Bitmap MotorfietsWeapon = new Bitmap("hoofdenwapenmotor.png");
            public static Bitmap Transparent = new Bitmap("transparent.png");

        }

        public static class Roads
        {

            public static Bitmap HorizontalStraight = new Bitmap("hortrack.png");
            public static Bitmap VerticalStraight = new Bitmap("vertrack.png");

            public static Bitmap HorizontalPitstop = new Bitmap("PitHori.png");
            public static Bitmap VerticalPitstop = new Bitmap("PitVert.png");

            public static Bitmap LeftTop = new Bitmap("ulcorner.png");
            public static Bitmap LeftBottom = new Bitmap("dlcorner.png");

            public static Bitmap RightTop = new Bitmap("urcorner.png");
            public static Bitmap RightBottom = new Bitmap("drcorner.png");

        }

        public static class Other
        {
            public static Bitmap Wrench = new Bitmap("Wrench.png");

            public static Bitmap OneGreenTwoGreenUp = new Bitmap("OneGreenTwoGreenUp.png");
            public static Bitmap OneGreenTwoGreenRight = new Bitmap("OneGreenTwoGreenRight.png");
            public static Bitmap OneGreenTwoGreenDown = new Bitmap("OneGreenTwoGreenDown.png");
            public static Bitmap OneGreenTwoGreenLeft = new Bitmap("OneGreenTwoGreenLeft.png");

            public static Bitmap OneGreenTwoBlueUp = new Bitmap("OneGreenTwoBlueUp.png");
            public static Bitmap OneGreenTwoBlueRight = new Bitmap("OneGreenTwoBlueRight.png");
            public static Bitmap OneGreenTwoBlueDown = new Bitmap("OneGreenTwoBlueDown.png");
            public static Bitmap OneGreenTwoBlueLeft = new Bitmap("OneGreenTwoBlueLeft.png");

            public static Bitmap OneBlueTwoBlueUp = new Bitmap("OneBlueTwoBlueUp.png");
            public static Bitmap OneBlueTwoBlueRight = new Bitmap("OneBlueTwoBlueRight.png");
            public static Bitmap OneBlueTwoBlueDown = new Bitmap("OneBlueTwoBlueDown.png");
            public static Bitmap OneBlueTwoBlueLeft = new Bitmap("OneBlueTwoBlueLeft.png");

            public static Bitmap OneBlueTwoGreenUp = new Bitmap("OneBlueTwoGreenUp.png");
            public static Bitmap OneBlueTwoGreenRight = new Bitmap("OneBlueTwoGreenRight.png");
            public static Bitmap OneBlueTwoGreenDown = new Bitmap("OneBlueTwoGreenDown.png");
            public static Bitmap OneBlueTwoGreenLeft = new Bitmap("OneBlueTwoGreenLeft.png");

            public static Bitmap HorizontalFinish = new Bitmap("finish.png");
            public static Bitmap VerticalFinish = new Bitmap("finish2.png");

            public static Bitmap Redpointer = new Bitmap("Redpointer.png");
            public static Bitmap Bluepointer = new Bitmap("Bluepointer.png");

            public static Bitmap p1Win = new Bitmap("FINISH CUP P1.png");
            public static Bitmap p2Win = new Bitmap("FINISH CUP P2.png");
        }

        public static class Obstacles
        {
            public static Bitmap Tree = new Bitmap("TreeTex.png");
            public static Bitmap Stone = new Bitmap("stone.png");
            public static Bitmap Pylon = new Bitmap("pylon.png");
        }

        public class Bullets
        {
            public static Bitmap RoundBullet = new Bitmap("kannonbal.png");
            public static Bitmap Vlam = new Bitmap("vlam.png");
            public static Bitmap RegularBullet = new Bitmap("regbullet.png");
        }
    }
}
