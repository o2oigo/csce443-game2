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
        UserInterface userInterface;

        double timer;
        // in milliseconds
        const int intervalSpawnMin = 1700; //
        const int intervalSpawnMax = 2000;
        const double intervalWave = 10000; // 10 seconds
        public double IntervalWave
        {
            get { return intervalWave; } 
        }
        const double intervalLevel = 15000; 

        protected Dictionary<int, Dictionary<int, Queue<Enemy>>> LevelDictionary = new Dictionary<int, Dictionary<int, Queue<Enemy>>>();

        bool levelDelay = false; 

        private int currentWave = 0;
        public int CurrentWave
        {
        get {return currentWave;}
        }

        private int currentLevel = 1;
        public int CurrentLevel
        {
            get { return currentLevel; }
        }

        private Boolean waveFinished = true;
        public Boolean isWaveFinish
        {
            get { return waveFinished; }
        }

        private double waveTimer;
        public double WaveTimer
        {
            get { return waveTimer; }
        }

        private Boolean levelFinished = false;
        public Boolean isLevelFinish
        {
            get { return levelFinished; }
        }

        private Boolean gameFinished = false;
        public Boolean isGameFinish
        {
            get { return gameFinished; }
        }

        public int totalWave()
        {
            return LevelDictionary[currentLevel].Count();
        }

        public void InitializeLevel()
        {
            //LevelDictionary.Add(levelnumber, InitializeWave(numberofwaves));
            //NOTE: number of waves must match ReinitializeWave info (each wave must be populated with enemy)
            LevelDictionary.Add(1, InitializeWave(1));
            LevelDictionary.Add(2, InitializeWave(2));
            LevelDictionary.Add(3, InitializeWave(1));
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

        public void ReinitializeLevel(int i)
        {
            //NOTE:
            // to add enemy :  
            //       Add<ENEMYTYPE>(i, waveNum, numberofEnemies, HP)
            switch (i)
            {
                case 1:
                    AddNormalEnemy(i, 1, 100, 1);
                    //AddFastEnemy(i, 1, 10, 100);
                    //AddNormalEnemy(i, 1, 2, 100);
                    //AddFastEnemy(i, 1, 5, 100);
                    //AddNormalEnemy(i, 1, 8, 100);
                    //AddFastEnemy(i, 1, 3, 100);
                    //AddHPEnemy(i, 1, 10, 100);
                    //AddNormalEnemy(i, 1, 10, 100);
                    //AddFastEnemy(i, 1, 10, 100);
                    //AddNormalEnemy(i, 1, 2, 100);
                    //AddFastEnemy(i, 1, 5, 100);
                    //AddNormalEnemy(i, 1, 8, 100);
                    //AddFastEnemy(i, 1, 3, 100);
                    //AddHPEnemy(i, 1, 10, 100);
                    //AddNormalEnemy(i, 1, 10, 100);
                    //AddFastEnemy(i, 1, 10, 100);
                    //AddNormalEnemy(i, 1, 2, 100);
                    //AddFastEnemy(i, 1, 5, 100);
                    //AddNormalEnemy(i, 1, 8, 100);
                    //AddFastEnemy(i, 1, 3, 100);
                    //AddHPEnemy(i, 1, 10, 100);
                    //AddHPEnemy(i, 2, 10, 100);
                    //AddAttackingEnemy(i, 3, 10, 100);
                    //AddFastEnemy(i, 4, 10, 100);
                    //AddMagicOnlyEnemy(i, 5, 10, 100);
                    //AddRegenEnemy(i, 6, 10, 100);
                    //AddPhysicalOnlyEnemy(i, 7, 10, 100);

                    //AddFastEnemy(1, 6, 10, 100);
                    //AddAttackingEnemy(i, 7, 5, 100);
                    //AddAttackingEnemy(i, 8, 10, 100);
                    //AddNormalEnemy(i, 9, 4, 100);
                    //AddHPEnemy(i, 9, 4, 100);
                    //AddFastEnemy(i, 9, 4, 100);
                    //AddAttackingEnemy(i, 9, 4, 100);
                    //
                    //AddNormalEnemy(i, 10, 10, 100);
                    //AddHPEnemy(i, 10, 5, 100);
                    //AddFastEnemy(i, 10, 5, 100);
                    //AddAttackingEnemy(i, 10, 5, 100);
                    break;
                case 2:
                    AddAttackingEnemy(i, 1, 0, 50);
                    //AddNormalEnemy(i, 2, 5, 50);
                    //AddHPEnemy(i, 3, 5, 1);
                    //AddFastEnemy(i, 3, 5, 100);
                    //AddHPEnemy(i, 4,2, 1);
                    //AddFastEnemy(i, 5, 2, 1);
                    //AddFastEnemy(i, 6, 10, 100);
                    //AddAttackingEnemy(i, 7, 5, 100);
                    //AddAttackingEnemy(i, 8, 10, 100);
                    //
                    //AddNormalEnemy(i, 9, 4, 100);
                    //AddHPEnemy(i, 9, 4, 100);
                    //AddFastEnemy(i, 9, 4, 100);
                    //AddAttackingEnemy(i, 9, 4, 100);
                    //
                    //AddNormalEnemy(i, 10, 10, 100);
                    //AddHPEnemy(i, 10, 5, 100);
                    //AddFastEnemy(i, 10, 5, 100);
                    //AddAttackingEnemy(i, 10, 5, 100);
                    break;
                case 3:
                    AddAttackingEnemy(i, 1, 0, 100);
                    //AddNormalEnemy(i, 2, 10, 100);
                    //AddHPEnemy(i, 3, 5, 100);
                    //AddHPEnemy(i, 4, 10, 100);
                    //AddFastEnemy(i, 5, 5, 100);
                    //AddFastEnemy(i, 6, 10, 100);
                    //AddAttackingEnemy(i, 7, 5, 100);
                    //AddAttackingEnemy(i, 8, 10, 100);
                    //
                    //AddNormalEnemy(i, 9, 4, 100);
                    //AddHPEnemy(i, 9, 4, 100);
                    //AddFastEnemy(i, 9, 4, 100);
                    //AddAttackingEnemy(i, 9, 4, 100);
                    //
                    //AddNormalEnemy(i, 10, 10, 100);
                    //AddHPEnemy(i, 10, 5, 100);
                    //AddFastEnemy(i, 10, 5, 100);
                    //AddAttackingEnemy(i, 10, 5, 100);
                    break;
                default:
                    break;
            }
            if (userInterface.getScreen("showGameScreen") == true)
            {
                userInterface.setScreenStatus("loadingGameScreen1", false);
                userInterface.setScreenStatus("loadingGameScreen2", false);
                userInterface.setScreenStatus("loadingGameScreen3", false);
                userInterface.setScreenStatus("showLevel1Screen", false);
                userInterface.setScreenStatus("showLevel2Screen", false);
                userInterface.setScreenStatus("showLevel3Screen", false);

            }
        }

        public Wave(Game1 game, UserInterface _userInterface)
        {
            userInterface = _userInterface;
            //interval = 1000;
            this.game = game;
            InitializeLevel();
            if (userInterface.getScreen("firstRun") == false)
            {
                ReinitializeLevel(currentLevel);
            }
        }
        #endregion 

        public void nextWave()
        {
            //if (waveFinished)
            //{
                if (currentWave >= LevelDictionary[currentLevel].Count())
                {
                    //ENDLEVEL
                    levelFinished = true;
                }
                else
                {
                    currentWave++;
                    waveFinished = false;
                }
                waveTimer = 0;
            //}
        }

        public void nextLevel()
        {
            //if (waveFinished && levelFinished)
            //{
                if (currentLevel < LevelDictionary.Count())
                {
                    //    //ENDGAME
                    //    gameFinished = true;
                    //}
                    //else
                    //{
                    waveTimer = 0;
                    currentLevel++;
                    levelFinished = false;
                    currentWave = 0;
                    waveFinished = true ;
                    game.Map.NextMap(currentLevel);
                    game.CreateTrees();
                    game.CreateLamps();
                    ReinitializeLevel(currentLevel);
                    timer = 0;
                }
            //}
        }

        public void Update(GameTime gameTime)
        {
            Random random = new Random();
            int spawnTimer = random.Next(intervalSpawnMin, intervalSpawnMax);
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (currentWave == 0)
            {
                waveTimer = intervalWave - timer;
                if (timer > intervalWave)
                {
                    nextWave();
                    timer = 0;
                }
            }
            else
            {
                if (!waveFinished && timer > spawnTimer)
                {
                    if (LevelDictionary[currentLevel][currentWave].Count != 0)
                    {
                        game.Enemies.Add(LevelDictionary[currentLevel][currentWave].Dequeue());
                    }
                    if (LevelDictionary[currentLevel][currentWave].Count() == 0 && game.Enemies.Count() == 0)
                    {
                        waveFinished = true;
                    }
                    timer = 0;
                }
                if (currentWave == LevelDictionary[currentLevel].Count() && waveFinished && game.Enemies.Count() == 0 && levelDelay)
                {
                    levelFinished = true;
                    //timer = 0;
                }
                if (waveFinished && game.Enemies.Count() == 0 && !levelFinished && timer > intervalWave)
                {
                    nextWave();
                    timer = 0;
                }
                else if (waveFinished && game.Enemies.Count() == 0 && !levelFinished && timer <= intervalWave)
                {
                    if ((intervalWave - timer) < 0) waveTimer = 0;
                    else waveTimer = intervalWave - timer;
                }
                if (levelFinished && game.Enemies.Count() == 0 && !gameFinished && timer > intervalWave)
                {
                    if (currentLevel >= LevelDictionary.Count())
                    {
                        gameFinished = true;
                    }
                    //game.goNextLevel(); //delete this and have goNextLevel() called when ready to start next level
                    timer = 0;
                }
            }
        }

        public void AddNormalEnemy(int lvl, int wave, int enemyNum, float hp)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                NormalEnemy e1 = new NormalEnemy();
                e1.Initialize(game, hp);
                e1.LoadContent();
                
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddHPEnemy(int lvl, int wave, int enemyNum, float hp)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                HPEnemy e1 = new HPEnemy();
                e1.Initialize(game, hp);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddAttackingEnemy(int lvl, int wave, int enemyNum, float hp)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                AttackingEnemy e1 = new AttackingEnemy();
                e1.Initialize(game, hp);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddFastEnemy(int lvl, int wave, int enemyNum, float hp)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                FastEnemy e1 = new FastEnemy();
                e1.Initialize(game, hp);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddRegenEnemy(int lvl, int wave, int enemyNum, float hp)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                RegenEnemy e1 = new RegenEnemy();
                e1.Initialize(game, hp);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddMagicOnlyEnemy(int lvl, int wave, int enemyNum, float hp)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                MagicOnlyEnemy e1 = new MagicOnlyEnemy();
                e1.Initialize(game, hp);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void AddPhysicalOnlyEnemy(int lvl, int wave, int enemyNum, float hp)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                PhysicalOnlyEnemy e1 = new PhysicalOnlyEnemy();
                e1.Initialize(game, hp);
                e1.LoadContent();
                (LevelDictionary[lvl])[wave].Enqueue(e1);
            }
        }

        public void endTimer()
        {
            if (waveFinished)
            {
                timer = intervalWave;
            }
        }

    }
}
