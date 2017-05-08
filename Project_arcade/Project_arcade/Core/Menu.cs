using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Project_arcade
{
    class Menu
    {
        /* TODO Fixxa så att när man väljer en bokstav, currentchar bokstaven flyttas korrekt efter storleken på bokstaven*/

        private bool charSwitch, singlePlayer, pvp, gameOn, readySwitch, optionsSwitch, colorSwitch, colorSwitch2, inOptions, inMainMenu, inCredits, settingNames;
        private GamePlay gamePlay;
        private Vector2 startGamePos, optionsPos, creditsPos, namePos, animationPos, animationPos2;
        private Color altColor, mainStartColor, mainOptionsColor, mainCreditsColor, logoColor;
        private int count, charX, charY, recCount, charCount, creditOffset;
        private float spriteTimer, spriteInterval, logoFlare, logoRotation;
        private string pvpstring, singleplayerstring, name;
        private Highscore.Scores[] scoreArray = new Highscore.Scores[10];
        private char currentChar;
        private char[] charArray;
        private List<char> nameList;
        private char[,] charSelectionArray;
        private Rectangle nameSelector1, animationSrcRec, animationSrcRec2;


        private enum MenuChoice { Btn_1, Btn_2, Btn_3 }
        private MenuChoice currentMenuChoice;
        private List<MenuChoice> menuList;

        private enum CharChoice { Right, Left, Up, Down, Red, Blue, None }
        private CharChoice currentCharChoice = CharChoice.Red;
       
        public Menu()
        {
            singlePlayer = false;
            pvp = true;
            readySwitch = false;
            settingNames = false;
            optionsSwitch = false;
            inCredits = false;
            inOptions = false;
            inMainMenu = true;
            colorSwitch = true;
            colorSwitch2 = true;
            charSwitch = false;
            startGamePos = new Vector2(500, 500);
            optionsPos = new Vector2(500, 600);
            creditsPos = new Vector2(500, 700);
            namePos = new Vector2(500,300);
            logoFlare = 1f;
            animationPos = new Vector2(1000, 500);
            animationSrcRec = new Rectangle(0, 25, 50, 50);
           // animationPos2 = new Vector2(300, 500);
            animationPos2 = new Vector2(1150, 500);
            animationSrcRec2 = new Rectangle(0, 25, 50, 50);
            nameList = new List<char>();
            charArray = new char[30];
            charSelectionArray = new char[10,3];
            mainStartColor = Color.White;
            mainOptionsColor = Color.White;
            mainCreditsColor = Color.White;
            altColor = Color.Red;
            logoRotation = 0;
            logoColor = new Color(255, 255, 255, 100);
            
            spriteTimer = 0;
            charCount = 0;
            spriteInterval = 100;
            count = 0;
            recCount = 0;
            charX = 0; charY = 0;
            pvpstring = "PVP ON";
            singleplayerstring = "SINGLE PLAYER OFF";
            menuList = new List<MenuChoice>();
            menuList.Add(MenuChoice.Btn_1);
            menuList.Add(MenuChoice.Btn_2);
            menuList.Add(MenuChoice.Btn_3);
            currentMenuChoice = MenuChoice.Btn_1;
            FillKeyboard();
            nameSelector1 = new Rectangle(500, 533, 13, 10);          
            Highscore.LoadHighScore();      
        }

        private void DrawHighScore(SpriteBatch spriteBatch)
        {
            scoreArray = Highscore.GetHighScores();

            for (int i = 0; i < scoreArray.GetLength(0)-1; i++)
            {
                var tempScore = scoreArray[i];
                string name = (i + 1 +".") + "  " + tempScore.name;
                string score = "" + tempScore.score;
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), name, new Vector2(1350, 400 + i * 50), Color.Goldenrod);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), score, new Vector2(1550, 400 + i * 50), Color.Goldenrod);
            }
            spriteBatch.DrawString(FontManager.GetFont("menu_font"), "HIGHSCORE", new Vector2(1440, 350 ), Color.Goldenrod);

        }

        private void DrawMenu(SpriteBatch spriteBatch)
        {
            if (inMainMenu)
            {
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "START GAME", startGamePos, mainStartColor);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "OPTIONS", optionsPos, mainOptionsColor);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "CREDITS", creditsPos, mainCreditsColor);

            }
            if (inOptions)
            {
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), pvpstring, startGamePos, mainStartColor);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), singleplayerstring, optionsPos, mainOptionsColor);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "BACK", creditsPos, mainCreditsColor);
                
            }
            if (inCredits)
            {
                readySwitch = false;
                creditOffset += 1;
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "BACK", creditsPos, mainCreditsColor);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Game Programming: Christofer Malmberg", new Vector2(600, 0 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Game Design: Christofer Malmberg", new Vector2(600, -200 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Game artist: Christofer Malmberg", new Vector2(600, -400 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Sound Artist: Christofer Malmberg", new Vector2(600, -600 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Animator: Christofer Malmberg", new Vector2(600, -800 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Lead Artist: Christofer Malmberg", new Vector2(600, -1000 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Level Editor: Christofer Malmberg", new Vector2(600, -1200 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Creative Director: Christofer Malmberg", new Vector2(600, -1400 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Producer: Christofer Malmberg", new Vector2(600, -1600 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Assistant Producer: Christofer Malmberg", new Vector2(600, -1800 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Executive Producer: Christofer Malmberg", new Vector2(600, -2000 + creditOffset), Color.White);
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Thanks for playing!", new Vector2(600, -2200 + creditOffset), Color.White);

                if (creditOffset >= 3280)
                    creditOffset = 0;
                
            }          
        }

        private void DrawNameUI(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontManager.GetFont("menu_font"), "Name: ", new Vector2(400, 300), Color.White);
            spriteBatch.DrawString(FontManager.GetFont("menu_font"), currentChar.ToString(), namePos, Color.White);
            if (name != null)
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), name, new Vector2(500, 300), Color.White);
            spriteBatch.Draw(TextureManager.GetTexture("rock_default"), nameSelector1, new Rectangle(0,50,10,10), Color.Red);
        }

        private void SetArray()
        {

            int y = 0;
            int x = 0;
            int j = 0;
            int k = 0;

            for (int i = 0; i < charArray.GetLength(0); i++)
            {
                x = i;

                if (i >= 10)
                {
                    y = 1;
                    x = j;
                    j++;

                }
                if (i >= 20)
                {
                    y = 2;
                    x = k;
                    k++;
                }  
           
                charSelectionArray[x, y] = charArray[i];
            }
       
           // currentChar = charSelectionArray[charX, charY];
        }

        private void UpdateNameUI()
        {

            if (charSwitch)
            {
                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Right))
                {
                    if (nameSelector1.X <= 1000 && charX <= 8)
                    {
                        nameSelector1.X += 50;
                        charX += 1;
                        currentCharChoice = CharChoice.Right;
                        charSwitch = false;
                    }
                }

                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Left))
                {
                    if (nameSelector1.X >= 0 && charX >= 1 )
                    {
                        charX -= 1;
                        nameSelector1.X -= 50;
                        currentCharChoice = CharChoice.Left;
                        charSwitch = false;
                    }
                }

                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Up))
                {
                    if (nameSelector1.Y >= 0 && charY >= 1)
                    {
                        charY -= 1;
                        nameSelector1.Y -= 50;
                        currentCharChoice = CharChoice.Up;
                        charSwitch = false;

                    }
                }
                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Down))
                {
                    if (nameSelector1.Y <= 700 && charY <= 1)
                    {
                        charY += 1;
                        nameSelector1.Y += 50;
                        currentCharChoice = CharChoice.Down;
                        charSwitch = false;
                    }
                }

                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Red))
                {
                    if (charCount <=13)
                    {
                        currentCharChoice = CharChoice.Red;
                        nameList.Add(currentChar);
                        namePos.X += 17;
                        charSwitch = false;
                        charCount += 1;
                        name = "";
                        for (int i = 0; i < nameList.Count; i++)
                            name += nameList.ElementAt(i).ToString();
                    }               
                }

                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Blue))
                {
                    if (nameList.Count > 0)
                    {
                        currentCharChoice = CharChoice.Blue;
                        nameList.Remove(nameList.ElementAt(nameList.Count - 1));
                        name = "";
                        charCount -= 1;
                        for (int i = 0; i < nameList.Count; i++)
                            name += nameList.ElementAt(i).ToString();
                        namePos.X -= 17;
                        charSwitch = false;
                    } 
                }
                if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Start))
                {
                    gamePlay = new GamePlay(pvp, singlePlayer, name);
                    gameOn = true;   
                }
            }

            switch (currentCharChoice)
            {
                case CharChoice.Right:
                    if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Right))
                        charSwitch = true;
                    break;
                case CharChoice.Left:
                    if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Left))
                        charSwitch = true;
                    break;
                case CharChoice.Up:
                    if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Up))
                        charSwitch = true;
                    break;
                case CharChoice.Down:
                    if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Down))
                        charSwitch = true;
                    break;
                case CharChoice.Red:
                    if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Red))
                        charSwitch = true;
                    break;
                case CharChoice.Blue:
                    if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Blue))
                        charSwitch = true;
                    break;
                case CharChoice.None:                 
                    break;
                default:
                    break;
            }
        }

        private void FillKeyboard()
        {
            charArray[0] = 'A'; charArray[1] = 'B'; charArray[2] = 'C'; charArray[3] = 'D'; charArray[4] = 'E'; charArray[5] = 'F';
            charArray[6] = 'G'; charArray[7] = 'H'; charArray[8] = 'I'; charArray[9] = 'J'; charArray[10] = 'K'; charArray[11] = 'L';
            charArray[12] = 'M'; charArray[13] = 'N'; charArray[14] = 'O'; charArray[15] = 'P'; charArray[16] = 'Q'; charArray[17] = 'R';
            charArray[18] = 'S'; charArray[19] = 'T'; charArray[20] = 'U'; charArray[21] = 'V'; charArray[22] = 'W'; charArray[23] = 'X';
            charArray[24] = 'Y'; charArray[25] = 'Z'; charArray[26] = ','; charArray[27] = '.'; charArray[28] = '-'; charArray[29] = '&';
                // charArray[30] = ' ';
        }

        private void DrawKeyboard(SpriteBatch spriteBatch)
        {
            int offsetX = 0;
            int offsetY = 0;
            int j = 0;
            int k = 0;
            for (int i = 0; i < charArray.GetLength(0); i++)
            {
                var temp = charArray[i].ToString();
         
                offsetX = i * 50;

                if (i >= 10 && i <= 20)
                {
                    offsetY = 50;
                    offsetX = j * 50;
                    j++;
                }
                if (i >= 20)
                {
                    offsetY = 100;
                    offsetX = k * 50;
                    k++;
                }
                spriteBatch.DrawString(FontManager.GetFont("menu_font"), temp, new Vector2(offsetX + 500, offsetY +500), Color.White);   
            }
        }

        private void HandleInput(GameTime gameTime)
        {
            if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Down) && InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Up))
                readySwitch = true;

            if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Down) && readySwitch)
            {
                count++;
                if (count >= menuList.Count )
                    count = 0;

                altColor.R = (byte)255;
                colorSwitch = true;
                currentMenuChoice = menuList[count];
                readySwitch = false;

              
            }
            if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Up) && readySwitch)
            {
                count--;
                if (count < 0)
                    count = menuList.Count-1;

                altColor.R = (byte)255;
                colorSwitch = true;
                currentMenuChoice = menuList[count];
                readySwitch = false;
            }

            if (InputHandler.IsButtonUp(PlayerIndex.One, PlayerInput.Red))
                optionsSwitch = true;



            #region MenuButtons
            if (InputHandler.IsButtonDown(PlayerIndex.One, PlayerInput.Red) && optionsSwitch)
            {
                if (inMainMenu)
                {
                    optionsSwitch = true;
                    switch (currentMenuChoice)
                    {
                        case MenuChoice.Btn_1:
                            settingNames = true;
                            SetArray();
                            break;
                        case MenuChoice.Btn_2:
                            optionsSwitch = false;
                            inOptions = true;
                            inMainMenu = false;
                            break;
                        case MenuChoice.Btn_3:
                            optionsSwitch = false;
                            inCredits = true;
                            inMainMenu = false;
                            break;
                        default:
                            break;
                    }
                }
                if (inOptions)
                {         
                    switch (currentMenuChoice)
                    {
                        case MenuChoice.Btn_1:
                            if (pvp && optionsSwitch)
                            {
                                pvp = false;
                                pvpstring = "PVP OFF";
                                optionsSwitch = false;
                            }
                            if (!pvp && optionsSwitch)
                            {
                                pvp = true;
                                pvpstring = "PVP ON";
                                optionsSwitch = false;
                            }
                         
                            break;

                        case MenuChoice.Btn_2:
                            if (singlePlayer && optionsSwitch)
                            {
                                singlePlayer = false;
                                singleplayerstring = "SINGLE PLAYER OFF";
                                optionsSwitch = false;

                            }
                            if (!singlePlayer && optionsSwitch)
        
                            {
                                singlePlayer = true;
                                singleplayerstring = "SINGLE PLAYER ON";
                                optionsSwitch = false;
                            }
                          
                            break;
                        case MenuChoice.Btn_3:
                            Back();
                            break;
                        default:
                            break;
                    }
                    
                }
                if (inCredits && optionsSwitch)
                {
                    inMainMenu = true;
                    inOptions = false;
                    inCredits = false;
                    optionsSwitch = false;
                    creditOffset = 0;
                }
            #endregion 

            }
        }

        private void Back()
        {
            inMainMenu = true;
            inCredits = false;
            inOptions = false;
            optionsSwitch = false;
            creditOffset = 0;
        }

        private void HandleMenuUI()
        {
            if (currentMenuChoice == MenuChoice.Btn_1)
                mainStartColor = altColor;
            else
                mainStartColor = Color.White;

            if (currentMenuChoice == MenuChoice.Btn_2)
                mainOptionsColor = altColor;
            else
                mainOptionsColor = Color.White;

            if (currentMenuChoice == MenuChoice.Btn_3)
                mainCreditsColor = altColor;
            else
                mainCreditsColor = Color.White;
        }

        private void HandleFontFlare(GameTime gameTime)
        {
            if (colorSwitch)
            {
                altColor.R -= (byte)(1 * (gameTime.ElapsedGameTime.Milliseconds / 4));
                if (altColor.R <= 100)
                    colorSwitch = false;
            }
            if (!colorSwitch)
            {
               altColor.R += (byte)(1 * (gameTime.ElapsedGameTime.Milliseconds / 4));
                if (altColor.R >= 250)
                    colorSwitch = true;
            }          
        }

        private void HandleLogoFlare(GameTime gameTime)
        {
            if (colorSwitch2)
            {
                logoFlare -= 0.0001f *gameTime.ElapsedGameTime.Milliseconds;
                if (logoFlare <= 0.2f)
                    colorSwitch2 = false;

            }
            if (!colorSwitch2)
            {
                logoFlare += 0.0001f * gameTime.ElapsedGameTime.Milliseconds;
                if (logoFlare >= 1.0f)
                    colorSwitch2 = true;
            }
        }

        private void HandleAnimation(GameTime gameTime)
        {

            spriteTimer -= gameTime.ElapsedGameTime.Milliseconds;

            if (spriteTimer <= 0)
            {
                animationSrcRec.Y = 165;
                spriteTimer = spriteInterval;
                animationSrcRec.X = (recCount % 4) * 50 + 25;
                animationSrcRec2.Y = 95;
                spriteTimer = spriteInterval;
                animationSrcRec2.X = (recCount % 4) * 50 + 25;
                recCount++;
            }             
        }

        public void Update(GameTime gameTime)
        {
            HandleFontFlare(gameTime);
            HandleLogoFlare(gameTime);
            HandleAnimation(gameTime);
            logoRotation += 0.005f;
     
            if (gameOn)
            {
                 gamePlay.Update(gameTime);
                 if (gamePlay.Retry)
                     gamePlay = new GamePlay(pvp, singlePlayer, name);

                 if (gamePlay.Exit)
                 {
                     gameOn = false;
                     settingNames = false;
                     charCount = 0;
                     name = "";
                     nameList = new List<char>();
                     namePos = new Vector2(500, 300);
                     readySwitch = false;
                 }
            }


            if (!gameOn)// && !settingNames)
            {
                HandleInput(gameTime);
                HandleMenuUI();
            }
            if (settingNames)
            {
                UpdateNameUI();
                currentChar = charSelectionArray[charX, charY];
            }                  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (inCredits)
            {

            }
            if (!gameOn)
            {
                spriteBatch.Draw(TextureManager.GetTexture("bg_1"), new Vector2(0, 0), Color.White);
                spriteBatch.Draw(TextureManager.GetTexture("sd_spritesheet"), animationPos, animationSrcRec, Color.White, 0, new Vector2(0, 0), 3f, SpriteEffects.None, 1f);
                spriteBatch.Draw(TextureManager.GetTexture("sd_spritesheet"), animationPos2, animationSrcRec2, Color.OrangeRed, 0, new Vector2(0, 0), 3f, SpriteEffects.None, 1f);
                spriteBatch.Draw(TextureManager.GetTexture("Logo_1"), new Vector2(200,200), new Rectangle(0,0,250,250), Color.White * logoFlare, logoRotation, new Vector2(125,125), 5f, SpriteEffects.None, 1f);
                DrawHighScore(spriteBatch);
          
            }


            if (settingNames)
            {
                DrawKeyboard(spriteBatch);
                DrawNameUI(spriteBatch);
            }

            if (gameOn)
            {
                gamePlay.Draw(spriteBatch);
                settingNames = false;
            }

            else if (!settingNames)
                DrawMenu(spriteBatch);



        }
    }
}
