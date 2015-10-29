using System;
using System.Collections.Generic;
using System.Drawing;

namespace RaceGame
{
    /// <summary>
    /// Dit is de bullet class. Hier worden alle properties en functies van alle bullets opgeslagen
    /// </summary>
    public class Bullet
    {
        public float angle;
        public float speed;
        public int timeout;
        public int damage;
        public Player player;
        public DrawInfo bulletDrawInfo;

        /// <summary>
        /// Dit is de code voor het maken van de bullet
        /// </summary>
        public Bullet(Bitmap bitmap, int x, int y, int width, int height, float _angle = 0, float RotateX = 0f, float RotateY = 0f, bool AutoRemove = false, int Frames = 0)
        {
            bulletDrawInfo = new DrawInfo(bitmap, x, y, width, height, _angle, RotateX, RotateY, AutoRemove, Frames);
            Base.drawInfos.Add(bulletDrawInfo);

        }
        /// <summary>
        /// Hierin wordt de afgeschoten kogel verplaatst in de richting. Ook wordt hier gekeken of de kogel collide met een voertuig. Deze staat in de GameTasks
        /// </summary>
        public void TrackBullet()
        {

            if (player == Base.currentGame.player1)
            {
                if (Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.topleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.topleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.topright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.topright.Y) < 20
                        || Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.backleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.backleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.backright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.backright.Y) < 20)
                {
                    Base.currentGame.player2.vehicle.drawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player2.vehicle.weaponDrawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player2.vehicle.health -= damage;
                    if (Base.currentGame.player2.vehicle.health < 0)
                    {
                        Base.currentGame.player2.vehicle.health = 0;
                    }
                    Console.WriteLine("damage gedaan: "+damage);
                    Console.WriteLine("helf remain iz: " + Base.currentGame.player2.vehicle.health);
                    timeout = 0;


                }
            }
            if (player == Base.currentGame.player2)
            {
                if (Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.topleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.topleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.topright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.topright.Y) < 20
                        || Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.backleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.backleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.backright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.backright.Y) < 20)
                {
                    Base.currentGame.player1.vehicle.drawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player2.vehicle.weaponDrawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player1.vehicle.health -= damage;
                    if (Base.currentGame.player1.vehicle.health < 0)
                    {
                        Base.currentGame.player1.vehicle.health = 0;
                    }
                    Console.WriteLine(Base.currentGame.player1.vehicle.health);
                    timeout = 0;
                }
            }

            if (bulletDrawInfo.x <= 0 || bulletDrawInfo.x >= Base.currentGame.MapsizeX || bulletDrawInfo.y <= 0 || bulletDrawInfo.y >= Base.currentGame.MapsizeY)
            {
                Base.drawInfos.Remove(bulletDrawInfo);
                Base.gameTasks.Remove(TrackBullet);
                return;
            }

            bulletDrawInfo.x += (float)(Math.Cos(bulletDrawInfo.angle % 360 * (Math.PI / 180)) * speed);
            bulletDrawInfo.y += (float)(Math.Cos((90 - bulletDrawInfo.angle) % 360 * (Math.PI / 180)) * speed);
            
            if (timeout <= 0)
            {
                    Base.drawInfos.Remove(bulletDrawInfo);
                    Base.gameTasks.Remove(TrackBullet);
                return;
            }

            timeout--;
        }
    }
/// <summary>
/// Dit is de weapons class. Hier worden alle functies en properties van alle weapons bijgehouden. Veel van deze functies zijn de te overriden
/// </summary>
    public abstract class Weapons
    {
        public string name;
        public string type;
        public string spriteName;
        public int damage;
        public int fireRate;
        public float turnSpeed;
        public string turning;
        public int timeout = 300;
        public Player player;
        public Bitmap weaponSprite;
        public Bitmap BulletSprite;
        public int weaponSizeX = 50;
        public int weaponSizeY = 100;

        public static List<Bullet> Bullets = new List<Bullet>();
        public static int i;
        public int weaponReloading;

        public Weapons(Player t)
        {
            player = t;
        }
        /// <summary>
        /// Dit is de shoot functie. Zorgt er voor dat de kogel wordt gemaakt. Deze is virtual, en voor sommige wapens overriden we hem.
        /// </summary>
        virtual public void shoot()
        {
            if (weaponReloading == 0)
            {
                Bullets.Add(new Bullet(BulletSprite, (int)player.vehicle.drawInfo.x, (int)player.vehicle.drawInfo.y, 10, 10, player.vehicle.weaponDrawInfo.angle, 0f, 0f, true, timeout));
                i = Bullets.Count - 1;
                Bullets[i].speed = 30;
                Bullets[i].timeout = timeout;
                Bullets[i].player = player;
                Bullets[i].damage = damage;
                Base.gameTasks.Add(Bullets[i].TrackBullet);
                weaponReloading = fireRate;
                Base.gameTasks.Add(weaponReload);
            }
        }
        /// <summary>
        /// Deze functie zorgt er voor dat het wapen herlaad. Zo kan de speler neit oneindig snel kogels afvuren
        /// </summary>
        public void weaponReload()
        {
            weaponReloading--;
            if (weaponReloading == 0)
            {
                Base.gameTasks.Remove(weaponReload);
            }
        }
    }
    //Deze en de volgende classes slaan de informatie op voor alle wapens
    public class TankWeapon : Weapons
    {
        public TankWeapon(Player s) : base(s)
        {
            weaponSprite = Bitmaps.Vehicles.TankWeapon;
            damage = 75;
            fireRate = 100;
            turnSpeed = 3f;
            turning = "false";
            BulletSprite = Bitmaps.Bullets.RoundBullet;
        }
    }

    public class LAPVWeapon : Weapons
    {
        public LAPVWeapon(Player s) : base(s)
        {
            weaponSprite = Bitmaps.Vehicles.LAPVWeapon;
            damage = 15;
            fireRate = 5;
            turnSpeed = 3;
            BulletSprite = Bitmaps.Bullets.RegularBullet;
            weaponSizeX = weaponSprite.Width * 2;
            weaponSizeY = weaponSprite.Height * 2;
        }
    }

    public class HorsePowerWeapon : Weapons
    {
        public HorsePowerWeapon(Player s) : base(s)
        {
            damage = 2;
            fireRate = 1;
            turnSpeed = 3;
            timeout = 4;
            weaponSprite = Bitmaps.Bullets.Vlam;
        }

        public override void shoot()
        {
            if (weaponReloading == 0) 
            {
                Bullets.Add(new Bullet(weaponSprite, (int)player.vehicle.drawInfo.x, (int)player.vehicle.drawInfo.y, 30, 90, player.vehicle.weaponDrawInfo.angle, 0f, 0f, true, timeout));
                i = Bullets.Count - 1;
                Bullets[i].speed = 30;
                Bullets[i].timeout = timeout;
                Bullets[i].player = player;
                Bullets[i].damage = damage;
                Base.gameTasks.Add(Bullets[i].TrackBullet);
                weaponReloading = fireRate;
                Base.gameTasks.Add(weaponReload);
            }
        }
    }

    public class MotorfietsWeapon : Weapons
    {
        public MotorfietsWeapon(Player s) : base(s)
        {
            weaponSprite = Bitmaps.Vehicles.MotorfietsWeapon;
            damage = 3;
            fireRate = 20;
            turnSpeed = 4;
            weaponSprite = Bitmaps.Bullets.RoundBullet;
        }
    }
}
