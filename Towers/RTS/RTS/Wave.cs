﻿using System;
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
        //protected Dictionary<int, Queue<Enemy>> WaveDictionary = new Dictionary<int, Queue<Enemy>>();
        protected Dictionary<int, Dictionary<int, Queue<Enemy>>> LevelDictionary = new Dictionary<int, Dictionary<int, Queue<Enemy>>>();

        private int currentWave = 1;
        public int CurrentWave
        {
        get {return currentWave;}
        }

        private int currentLevel = 1;
        public int CurrentLevel
        {
            get { return currentLevel; }
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

        public void InitializeLevel()
        {
            LevelDictionary.Add(1, InitializeWave(10));
            LevelDictionary.Add(2, InitializeWave(10));
            LevelDictionary.Add(3, InitializeWave(10));
        }

        public Dictionary<int, Queue<Enemy>> InitializeWave(int waveNum)
        {
            Dictionary<int, Queue<Enemy>> WaveDictionary = new Dictionary<int, Queue<Enemy>>();
            for (int i = 1; i <= waveNum; i++)
            {
                WaveDictionary.Add(i, new Queue<Enemy>());
            }
            return WaveDictionary;
        }   

        #region Hard coded enemy in each level
        public Wave(Game1 game)
        {
            interval = 1000;
            this.game = game;
            InitializeLevel();

            //level1//
            AddNormalEnemy(1, 1, 5);
            AddNormalEnemy(1, 2, 10);
            AddHPEnemy(1, 3, 5);
            AddHPEnemy(1, 4, 10);
            AddFastEnemy(1, 5, 5);
            AddFastEnemy(1, 6, 10);
            AddAttackingEnemy(1, 7, 5);
            AddAttackingEnemy(1, 8, 10);

            AddNormalEnemy(1, 9, 4);
            AddHPEnemy(1, 9, 4);
            AddFastEnemy(1, 9, 4);
            AddAttackingEnemy(1, 9, 4);

            AddNormalEnemy(1, 10, 10);
            AddHPEnemy(1, 10, 5);
            AddFastEnemy(1, 10, 5);
            AddAttackingEnemy(1, 10, 5);

            //level2//
            AddNormalEnemy(2, 1, 5);
            AddNormalEnemy(2, 2, 10);
            AddHPEnemy(2, 3, 5);
            AddHPEnemy(2, 4, 10);
            AddFastEnemy(2, 5, 5);
            AddFastEnemy(2, 6, 10);
            AddAttackingEnemy(2, 7, 5);
            AddAttackingEnemy(2, 8, 10);

            AddNormalEnemy(2, 9, 4);
            AddHPEnemy(2, 9, 4);
            AddFastEnemy(2, 9, 4);
            AddAttackingEnemy(2, 9, 4);

            AddNormalEnemy(2, 10, 10);
            AddHPEnemy(2, 10, 5);
            AddFastEnemy(2, 10, 5);
            AddAttackingEnemy(2, 10, 5);

            //level3//
            AddNormalEnemy(3, 1, 5);
            AddNormalEnemy(3, 2, 10);
            AddHPEnemy(3, 3, 5);
            AddHPEnemy(3, 4, 10);
            AddFastEnemy(3, 5, 5);
            AddFastEnemy(3, 6, 10);
            AddAttackingEnemy(3, 7, 5);
            AddAttackingEnemy(3, 8, 10);

            AddNormalEnemy(3, 9, 4);
            AddHPEnemy(3, 9, 4);
            AddFastEnemy(3, 9, 4);
            AddAttackingEnemy(3, 9, 4);

            AddNormalEnemy(3, 10, 10);
            AddHPEnemy(3, 10, 5);
            AddFastEnemy(3, 10, 5);
            AddAttackingEnemy(3, 10, 5);
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
                //game.Enemies.Add(WaveDictionary[currentWave].Dequeue());
                game.Enemies.Add(LevelDictionary[currentLevel][currentWave].Dequeue());
                if (LevelDictionary[currentLevel][currentWave].Count() == 0) waveFinished = true;
                timer = 0f;
            }
            if (currentWave == 10 && waveFinished && game.Enemies.Count() == 0)
            {
                levelFinished = true;
            }
            if (waveFinished && game.Enemies.Count() == 0 && !levelFinished && timer > 9000)
            {
                nextWave();
                timer = 0f;
            }
        }

        public void AddNormalEnemy(int lvl, int wave, int enemyNum)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                NormalEnemy e1 = new NormalEnemy();
                e1.Initialize(game);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddHPEnemy(int lvl, int wave, int enemyNum)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                HPEnemy e1 = new HPEnemy();
                e1.Initialize(game);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddAttackingEnemy(int lvl, int wave, int enemyNum)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                AttackingEnemy e1 = new AttackingEnemy();
                e1.Initialize(game);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddFastEnemy(int lvl, int wave, int enemyNum)
        {
            FastEnemy e1 = new FastEnemy();
            e1.Initialize(game);
            e1.LoadContent();
            (LevelDictionary[lvl])[wave].Enqueue(e1);
        }
    }
}
