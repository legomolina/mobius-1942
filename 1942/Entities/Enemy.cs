using static SDL2.SDL;
using Engine.Components;
using Engine.Core;
using Engine.Core.Math;
using System.Xml.Serialization;

namespace _1942.Entities
{
    internal class Enemy : Ship
    {
        private const string BULLET_TEXTURE_FILENAME = "Assets/Textures/bullet.png";
        private const string BULLET_SOUND_FILENAME = "Assets/Effects/bullet_shot.wav";
        private const string ENEMY_TEXTURE_FILENAME = "Assets/Textures/enemy1.png";
        private const int SHOT_WAIT_TIME = 500; // milliseconds

        private readonly Player player;

        private uint lastTimeStartShot = SDL_GetTicks();
        private uint elapsedTimeFromLastShot = 0;
        private int[] currentSegment = new int[2];
        private bool isMoving = false;

        public Enemy(GraphicsManager graphics, Player player) : base(graphics)
        {
            this.player = player;
            Speed = 0.1f;

            InitializeWaypoints();

            XmlSerializer serializer = new XmlSerializer(typeof(Enemies.Enemy));
            StreamReader reader = new StreamReader("Assets/Enemies/enemy_1.xml");
            var enemy = (Enemies.Enemy)serializer.Deserialize(reader);
        }

        private Point GetCirclePoints(int cX, int cY, int cR, int deg)
        {
            var x = cX + (cR * Math.Cos(deg * Math.PI / 180));
            var y = cY + (cR * Math.Sin(deg * Math.PI / 180));
            return new Point((float)x, (float)y);
        }

        private void InitializeWaypoints()
        {
            //Waypoints = new List<Waypoint>()
            //{
            //    new Waypoint(new Point(-50, 50)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 270)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 290)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 310)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 330)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 350)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 360)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 10)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 30)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 50)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 70)),
            //    new Waypoint(GetCirclePoints(graphics.WindowWidth - 200, 150, 100, 90)),
            //    new Waypoint(new Point(-50, 250)),
            //};
        }

        public override void LoadContent(AssetManager assetManager)
        {
            bulletTexture = assetManager.LoadTexture(BULLET_TEXTURE_FILENAME);
            shipTexture = assetManager.LoadTexture(ENEMY_TEXTURE_FILENAME);
            shootSound = assetManager.LoadSoundEffect(BULLET_SOUND_FILENAME);
            shootSound.SetVolume(15);

            shipSprite = new AnimatedSprite(shipTexture, 4, AnimationDirections.HORIZONTAL)
            {
                AnimationFPS = ANIMATION_FPS,
            };

            Width = ((AnimatedSprite)shipSprite).FrameWidth;
            Height = ((AnimatedSprite)shipSprite).FrameHeight;
        }

        protected override void Shoot()
        {
            if (player.Center.Y > Position.Y)
            {
                Vector2 direction = Vector2.FromPoints(Center, player.Center);
                float rotation = (float)(Math.Atan2(player.Center.X - Position.X, player.Center.Y - Position.Y) * (180 / Math.PI));

                Bullet bullet = new Bullet(bulletTexture, Center, direction)
                {
                    Rotation = -rotation,
                    Speed = 0.5f
                };

                shootSound!.Play(1, 1);
                bullets.Add(bullet);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            elapsedTimeFromLastShot = SDL_GetTicks() - lastTimeStartShot;

            if (elapsedTimeFromLastShot >= SHOT_WAIT_TIME)
            {
                Shoot();
                lastTimeStartShot = SDL_GetTicks();
            }

            if (isMoving)
            {
                //Vector2 moveDirection = CurrentSegment[0].GetDirection(CurrentSegment[1]);
                //float distanceToNextWaypoint = (Position.ToVector() - CurrentSegment[1].Position.ToVector()).Magnitude();
                //Position = ((moveDirection / moveDirection.Magnitude()) * Speed * gameTime.DeltaTime + Position.ToVector()).ToPoint();
                //Rotation = new Vector2(0, 1).AngleBetween(moveDirection / moveDirection.Magnitude()) * -1;


                //if (distanceToNextWaypoint < 20)
                //{
                //    if (currentSegment[1] < Waypoints.Count - 1)
                //    {
                //        currentSegment = new int[2] { currentSegment[1], currentSegment[1] + 1 };
                //    }
                //    else
                //    {
                //        InitPath();
                //    }
                //}
            }
        }

        public override void Render()
        {
            base.Render();

            //for (int i = 0; i < Waypoints.Count - 1; i++)
            //{
            //    DebugManager.RenderLine(Waypoints[i].Position, Waypoints[i + 1].Position);
            //}
        }

        public void InitPath()
        {
            currentSegment = new int[2] { 0, 1 };
            isMoving = true;

            //Position = new Point(Waypoints[0].Position.X - Width / 2, Waypoints[1].Position.Y - Height / 2);
        }

        public void StopPath()
        {
            isMoving = false;
        }
    }
}
