using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Project_arcade
{
    static class Highscore
    {
        static string highscorePath = "Content/highscore.txt";
        static StreamWriter writer;
        static StreamReader reader;
        static List<Scores> scoreList = new List<Scores>();
        static List<Scores> highScoreList = new List<Scores>();
        static Scores[] scoreArray = new Scores[10];

        public struct Scores
        {
            public string name;
            public int score;
            public Scores(string name, int score)
            {
                this.name = name;
                this.score = score;
            }
        }

        public static Scores[] GetHighScores()
        {
            return scoreArray;
        }

        public static void CreateHighScoreFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }           
        }


        public static void SaveHighScore(string name, int score)
        {
            //CreateHighScoreFile(highscorePath);
            LoadHighScore();
            Scores newScore = new Scores();
            newScore.name = name;
            newScore.score = score;

            //for (int i = 0; i < scoreArray.GetLength(0); i++)
            //{
            //    if (scoreArray[i].score == null)
            //    {
            //        scoreArray[i] = newScore;
            //        break;
            //    }
            //}

            Array.Sort(scoreArray, (x, y) => y.score.CompareTo(x.score));

            if (newScore.score > scoreArray[scoreArray.GetLength(0)-1].score)
            {
                scoreArray[scoreArray.GetLength(0) - 1] = newScore;
            }

            Array.Sort(scoreArray, (x, y) => y.score.CompareTo(x.score));

            writer = new StreamWriter(highscorePath);

            for (int i = 0; i < scoreArray.GetLength(0); i++)
            {
                string tempString1 = "";
                string tempString2 = "";

                tempString1 = scoreArray[i].name;
                tempString2 = scoreArray[i].score.ToString();
                writer.WriteLine(tempString1);
                writer.WriteLine(tempString2);
            }
            writer.Close();
         
        }

        public static void LoadHighScore()
        {
            reader = new StreamReader(highscorePath);
            Scores newScore = new Scores();
  
            using (reader)
            {
                int i = 0;
                int j = 0;

                while (reader.ReadLine() != null)
                {                 
                    string temp = reader.ReadLine();
                    string test = File.ReadLines(highscorePath).ElementAt(i);
                    string test1 = File.ReadLines(highscorePath).ElementAt(i+1);
                    int x;
                    Int32.TryParse(test1, out x);
                    newScore.score = x;
                    newScore.name = test;

                    scoreArray[j] = newScore;
                    j++;
                    i += 2;
                }
            }           
        }
    }
}
