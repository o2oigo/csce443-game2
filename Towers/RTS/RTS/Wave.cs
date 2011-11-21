using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS
{
    public class Wave
    {
        Game1 game;
        Random rand = new Random();
        float timer, interval;
        protected Dictionary<int, Queue<Enemy>> WaveDictionary = new Dictionary<int, Queue<Enemy>>();

        private int currentWave = 1;
        public int CurrentWave
        {
        get {return currentWave;}
        }

        private Boolean waveFinished = false;
        public Boolean isWaveFinish
        {
            get { return waveFinished; }
        }


        private Boolean levelFinished = false;
        public Boolean isLevelFinish
        {
            get { return levelFinished; }
        }

        #region Hard coded enemy in each level
        public Wave(Game1 game)
        {
            interval = 1000;
            this.game = game;
            for (int i = 1; i <= 6; i++)
            {
                Queue<Enemy> enemyQueue = new Queue<Enemy>();
                if (i == 1)
                {
                    NormalEnemy normal1 = new NormalEnemy();
                    normal1.Initialize(game);
                    normal1.LoadContent();
                    NormalEnemy normal2 = new NormalEnemy();
                    normal2.Initialize(game);
                    normal2.LoadContent();
                    AttackingEnemy normal4 = new AttackingEnemy();
                    normal4.Initialize(game);
                    normal4.LoadContent();

                    enemyQueue.Enqueue(normal1);
                    enemyQueue.Enqueue(normal2);
                    enemyQueue.Enqueue(normal4);

                }
                else if (i == 2)
                {
                    NormalEnemy normal1 = new NormalEnemy();
                    normal1.Initialize(game);
                    normal1.LoadContent();
                    NormalEnemy normal2 = new NormalEnemy();
                    normal2.Initialize(game);
                    normal2.LoadContent();
                    NormalEnemy normal3 = new NormalEnemy();
                    normal3.Initialize(game);
                    normal3.LoadContent();
                    NormalEnemy normal4 = new NormalEnemy();
                    normal4.Initialize(game);
                    normal4.LoadContent();
                    enemyQueue.Enqueue(normal1);
                    enemyQueue.Enqueue(normal2);
                    enemyQueue.Enqueue(normal3);
                    enemyQueue.Enqueue(normal4);
                }
                else if (i == 3)
                {
                    HPEnemy normal1 = new HPEnemy();
                    normal1.Initialize(game);
                    normal1.LoadContent();
                    HPEnemy normal2 = new HPEnemy();
                    normal2.Initialize(game);
                    normal2.LoadContent();
                    AttackingEnemy normal3 = new AttackingEnemy();
                    normal3.Initialize(game);
                    normal3.LoadContent();
                    AttackingEnemy normal4 = new AttackingEnemy();
                    normal4.Initialize(game);
                    normal4.LoadContent();
                    enemyQueue.Enqueue(normal1);
                    enemyQueue.Enqueue(normal2);
                    enemyQueue.Enqueue(normal3);
                    enemyQueue.Enqueue(normal4);
                }
                else if (i == 4)
                {
                    HPEnemy normal1 = new HPEnemy();
                    normal1.Initialize(game);
                    normal1.LoadContent();
                    HPEnemy normal2 = new HPEnemy();
                    normal2.Initialize(game);
                    normal2.LoadContent();
                    HPEnemy normal3 = new HPEnemy();
                    normal3.Initialize(game);
                    normal3.LoadContent();
                    HPEnemy normal4 = new HPEnemy();
                    normal4.Initialize(game);
                    normal4.LoadContent();
                    enemyQueue.Enqueue(normal1);
                    enemyQueue.Enqueue(normal2);
                    enemyQueue.Enqueue(normal3);
                    enemyQueue.Enqueue(normal4);
                }
                else if (i == 5)
                {
                    FastEnemy normal1 = new FastEnemy();
                    normal1.Initialize(game);
                    normal1.LoadContent();
                    FastEnemy normal2 = new FastEnemy();
                    normal2.Initialize(game);
                    normal2.LoadContent();
                    FastEnemy normal3 = new FastEnemy();
                    normal3.Initialize(game);
                    normal3.LoadContent();
                    FastEnemy normal4 = new FastEnemy();
                    normal4.Initialize(game);
                    normal4.LoadContent();
                    FastEnemy normal5 = new FastEnemy();
                    normal5.Initialize(game);
                    normal5.LoadContent();
                    FastEnemy normal6 = new FastEnemy();
                    normal6.Initialize(game);
                    normal6.LoadContent();
                    enemyQueue.Enqueue(normal1);
                    enemyQueue.Enqueue(normal2);
                    enemyQueue.Enqueue(normal3);
                    enemyQueue.Enqueue(normal4);
                    enemyQueue.Enqueue(normal5);
                    enemyQueue.Enqueue(normal6);
                }
                else if (i == 6)
                {
                    NormalEnemy normal1 = new NormalEnemy();
                    normal1.Initialize(game);
                    normal1.LoadContent();
                    NormalEnemy normal2 = new NormalEnemy();
                    normal2.Initialize(game);
                    normal2.LoadContent();
                    NormalEnemy normal3 = new NormalEnemy();
                    normal3.Initialize(game);
                    normal3.LoadContent();
                    NormalEnemy normal4 = new NormalEnemy();
                    normal4.Initialize(game);
                    normal4.LoadContent();
                    HPEnemy normal5 = new HPEnemy();
                    normal5.Initialize(game);
                    normal5.LoadContent();
                    HPEnemy normal6 = new HPEnemy();
                    normal6.Initialize(game);
                    normal6.LoadContent();
                    HPEnemy normal7 = new HPEnemy();
                    normal5.Initialize(game);
                    normal5.LoadContent();
                    HPEnemy normal8 = new HPEnemy();
                    normal6.Initialize(game);
                    normal6.LoadContent();
                    HPEnemy normal9 = new HPEnemy();
                    normal5.Initialize(game);
                    normal5.LoadContent();
                    FastEnemy normal10 = new FastEnemy();
                    normal6.Initialize(game);
                    normal6.LoadContent();
                    FastEnemy normal11 = new FastEnemy();
                    normal5.Initialize(game);
                    normal5.LoadContent();
                    FastEnemy normal12 = new FastEnemy();
                    normal6.Initialize(game);
                    normal6.LoadContent();
                    enemyQueue.Enqueue(normal1);
                    enemyQueue.Enqueue(normal2);
                    enemyQueue.Enqueue(normal3);
                    enemyQueue.Enqueue(normal4);
                    enemyQueue.Enqueue(normal5);
                    enemyQueue.Enqueue(normal6);
                }

                WaveDictionary.Add(i, enemyQueue);
            }
        }
        #endregion 

        public void nextWave()
        {
            if (currentWave >= 6)
            {
                //ENDLEVEL
                levelFinished = true;
            }
            else
            {
                currentWave++;
                waveFinished = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!waveFinished && timer > interval)
            {
                game.Enemies.Add(WaveDictionary[currentWave].Dequeue());
                if (WaveDictionary[currentWave].Count() == 0) waveFinished = true;
                timer = 0f;
            }
            if (currentWave == 6 && waveFinished && game.Enemies.Count() == 0)
            {
                levelFinished = true;
            }
            if (waveFinished && game.Enemies.Count() == 0 && !levelFinished && timer > 9000)
            {
                nextWave();
                timer = 0f;
            }
            
        }
    }
}
