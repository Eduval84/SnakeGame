using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32.SafeHandles;
using static System.Windows.Input.Key;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<GridValue, ImageSource> gridValueToImage = new()
        {
            {GridValue.Empty,Images.Empty},
            {GridValue.Snake,Images.Body},
            {GridValue.Food,Images.Food}
        };
        private readonly int rows = 15, cols = 15;
        private readonly Image[,] gridImages;
        private readonly InitalGameStatus gameState;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new InitalGameStatus(rows, cols);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        private async void MainWindow_OnKeyDown_(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Enter:
                    await GameLoop();
                    break;
                case Up: 
                    gameState.ChangeDirection(Direction.Up);
                    break;
                case Down: 
                    gameState.ChangeDirection(Direction.Down);
                    break;
                case Key.Left:
                    gameState.ChangeDirection(Direction.Left);
                    break;
                case Right:
                    gameState.ChangeDirection(Direction.Right);
                    break;
            }
        }

        private async Task GameLoop()
        {
            while (!gameState.GameOver)
            {
                await Task.Delay(100);
                gameState.Move();
                Draw();
            }
        }

        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            for (int r=0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        private void Draw()
        {
            DrawGrid();
            ScoreText.Text = $"SCORE {gameState.Score}";
        }

        private void DrawGrid()
        {
            for (int r = 0 ; r< rows; r++)
            {
                for (int c = 0; c< cols; c++)
                {
                    GridValue gridValue = gameState.Grid[r,c];
                    gridImages[r,c].Source = gridValueToImage[gridValue];
                }
            }
        }

  
    }
}
