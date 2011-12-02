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
        const double intervalSpawn = 2000;
        const double intervalWave = 10000;
        const double intervalLevel = 9000;

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

        private Boolean gameFinished = false;
        public Boolean isGameFinish
        {
            get { return gameFinished; }
        }

        public void InitializeLevel()
        {
            LevelDictionary.Add(1, InitializeWave(1));
            LevelDictionary.Add(2, InitializeWave(1));
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

        //level, wave, normal,hp,
        public void AddEnemy()
        {
        }

        public void ReinitializeLevel(int i)
        {
            switch (i)
            {
                case 1:
                    AddNormalEnemy(i, 1, 5, 50);
                    //AddNormalEnemy(i, 2, 10, 100);
                    //AddHPEnemy(i, 3, 5, 100);
                    //AddHPEnemy(i, 4, 10, 100);
                    //AddFastEnemy(i, 5, 5, 100);
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
                    AddAttackingEnemy(i, 1, 5, 100);
                    //AddNormalEnemy(i, 2, 10, 100);
                    //AddHPEnemy(i, 3, 5, 100);
                    //AddFastEnemy(i, 3, 5, 100);
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
                case 3:
                    AddAttackingEnemy(i, 1, 5, 100);
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
        }

        public void nextLevel()
        {
            if (currentLevel < LevelDictionary.Count())
            {
            //    //ENDGAME
            //    gameFinished = true;
            //}
            //else
            //{
                currentLevel++;
                levelFinished = false;
                currentWave = 1;
                waveFinished = false;
                game.Map.NextMap(currentLevel);
                game.CreateTrees();
                ReinitializeLevel(currentLevel);
            }
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!waveFinished && timer > intervalSpawn)
            {
                //game.Enemies.Add(WaveDictionary[currentWave].Dequeue());
                game.Enemies.Add(LevelDictionary[currentLevel][currentWave].Dequeue());
                if (LevelDictionary[currentLevel][currentWave].Count() == 0) waveFinished = true;
                timer = 0;
            }
            if (currentWave == LevelDictionary[currentLevel].Count() && waveFinished && game.Enemies.Count() == 0)
            {
                levelFinished = true;
            }
            if (waveFinished && game.Enemies.Count() == 0 && !levelFinished && timer > intervalWave)
            {
                nextWave();
                timer = 0;
            }
            if (levelFinished && game.Enemies.Count() == 0 && !gameFinished && timer > intervalWave)
            {
                if (currentLevel >= LevelDictionary.Count())
                {
                    gameFinished = true;
                }
                //nextLevel();  //delete this and have nextLevel() called when ready to start next level
                //game.goNextLevel();
                timer = 0;
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
            FastEnemy e1 = new FastEnemy();
            e1.Initialize(game, hp);
            e1.LoadContent();
            (LevelDictionary[lvl])[wave].Enqueue(e1);
        }
    }
}
