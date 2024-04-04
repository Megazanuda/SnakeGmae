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
using System.Windows.Threading;
using System.Timers;
namespace WpfApp6
{

    public partial class MainWindow : Window
    {
        //private Table_score table_Score = new Table_score();
        private readonly List<Point> _bonusPoints = new List<Point>();
        private readonly List<Point> _snakePoints = new List<Point>();
        private readonly Brush _snakeColor = Brushes.Orange;
        private Boolean IsOpened = false;

        private enum Movingdirection
        {
            Upwards = 8,
            Downwards = 2,
            Toleft = 4,
            Toright = 6
        };

        public string name1;

        private Point _startingPoint = new Point(100, 100);
        private Point _currentPosition = new Point();

        private int _direction = 5;
        private int _previousDirection = 0;

        //Размер змейки
        private readonly int _headSize = 8;

        private int _length = 30;
        private int _score = 0;
        public bool trigger = true;
        public string name;
        private readonly Random _rnd = new Random();
        public DispatcherTimer timer = new DispatcherTimer();
        Dictionary<string, int> rating = new Dictionary<string, int>();
        //private System.Media.SoundPlayer death = new System.Media.SoundPlayer("C:/Users/vedn13/source/repos/Snake-Game-WPF/death.mp3");

        public MainWindow()
        {
            
            InitializeComponent();

            timer.Tick += new EventHandler(timer_Tick);

            //Скорость
            timer.Interval = new TimeSpan(169000);
            timer.Start();

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            PaintSnake(_startingPoint);
            _currentPosition = _startingPoint;

            for (var n = 0; n < 10; n++)
            {
                PaintBonus(n);
            }

            

        }

        private void PaintSnake(Point currentposition)
        {
            Ellipse newEllipse = new Ellipse
            {
                Fill = _snakeColor,
                Width = _headSize,
                Height = _headSize
            };

            Canvas.SetTop(newEllipse, currentposition.Y);
            Canvas.SetLeft(newEllipse, currentposition.X);

            int count = PaintCanvas.Children.Count;
            PaintCanvas.Children.Add(newEllipse);
            _snakePoints.Add(currentposition);

            if (count > _length)
            {
                PaintCanvas.Children.RemoveAt(count - _length + 9);
                _snakePoints.RemoveAt(count - _length);
            }
        }

        private void PaintBonus(int index)
        {
            Point bonusPoint = new Point(_rnd.Next(5, 780), _rnd.Next(5, 480));

            Ellipse newEllipse = new Ellipse
            {
                Fill = Brushes.Red,
                Width = _headSize,
                Height = _headSize
            };

            Canvas.SetTop(newEllipse, bonusPoint.Y);
            Canvas.SetLeft(newEllipse, bonusPoint.X);
            PaintCanvas.Children.Insert(index, newEllipse);
            _bonusPoints.Insert(index, bonusPoint);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (_direction)
            {
                case (int)Movingdirection.Downwards:
                    _currentPosition.Y += 2;
                    PaintSnake(_currentPosition);
                    break;
                case (int)Movingdirection.Upwards:
                    _currentPosition.Y -= 2;
                    PaintSnake(_currentPosition);
                    break;
                case (int)Movingdirection.Toleft:
                    _currentPosition.X -= 2;
                    PaintSnake(_currentPosition);
                    break;
                case (int)Movingdirection.Toright:
                    _currentPosition.X += 2;
                    PaintSnake(_currentPosition);
                    break;
            }
            //Границы
            if ((_currentPosition.X < 1)||(_currentPosition.X > 780)||(_currentPosition.Y < 1) || (_currentPosition.Y > 480))
                GameOver();

            int n = 0;
            foreach (Point point in _bonusPoints)
            {
                if ((Math.Abs(point.X - _currentPosition.X) < _headSize) &&
                    (Math.Abs(point.Y - _currentPosition.Y) < _headSize))
                {
                    _length += 10;
                    _score += 10;


                    ScoreLabel.Text = $"Счет: {_score}";

                    _bonusPoints.RemoveAt(n);
                    PaintCanvas.Children.RemoveAt(n);
                    PaintBonus(n);
                    break;
                }
                n++;
            }

            for (int q = 0; q < (_snakePoints.Count - _headSize * 2); q++)
            {
                Point point = new Point(_snakePoints[q].X, _snakePoints[q].Y);
                if ((Math.Abs(point.X - _currentPosition.X) < (_headSize)) && (Math.Abs(point.Y - _currentPosition.Y) < (_headSize)))
                {
                    GameOver();
                    break;
                }
            }
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {

                case Key.Down:
                    if (_previousDirection != (int)Movingdirection.Upwards) _direction = (int)Movingdirection.Downwards;
                    break;

                case Key.Up:
                    if (_previousDirection != (int)Movingdirection.Downwards) _direction = (int)Movingdirection.Upwards;
                    break;

                case Key.Left:
                    if (_previousDirection != (int)Movingdirection.Toright) _direction = (int)Movingdirection.Toleft;
                    break;

                case Key.Right:
                    if (_previousDirection != (int)Movingdirection.Toleft) _direction = (int)Movingdirection.Toright;
                    break;


                case Key.S:
                    if (_previousDirection != (int)Movingdirection.Upwards) _direction = (int)Movingdirection.Downwards;
                    break;

                case Key.W:
                    if (_previousDirection != (int)Movingdirection.Downwards) _direction = (int)Movingdirection.Upwards;
                    break;

                case Key.A:
                    if (_previousDirection != (int)Movingdirection.Toright) _direction = (int)Movingdirection.Toleft;
                    break;

                case Key.D:
                    if (_previousDirection != (int)Movingdirection.Toleft) _direction = (int)Movingdirection.Toright;
                    break;

                case Key.Space:
                    if (trigger == true)
                    {
                        pause.Visibility = Visibility.Visible;
                        timer.Stop();
                        trigger = false;
                    }
                    else if (trigger == false)
                    {
                        pause.Visibility = Visibility.Hidden;
                        timer.Start();
                        trigger = true;
                    }
                    break;

            }

            _previousDirection = _direction;
        }
        //public MainWindow(string NameFromWindow1)
        //{
        //    name = NameFromWindow1;
        //}
        private void GameOver()
        {

            if (IsOpened == false)
            {
                if (rating.ContainsKey(name1) && rating[name1] < _score)
                {
                    rating[name1] = _score;
                }
                else if (rating.ContainsKey(name1) && rating[name1] > _score) { }
                else
                {
                    rating.Add(name1, _score);
                }


                Window2 win2 = new Window2();
                win2.Score.Text = ScoreLabel.Text;
                win2.name = name1;
                win2.Show();
                IsOpened = true;
                this.Close();





                //if (MessageBox.Show($@"Ваш рекорд {_score} очков. Хотите начать сначала?", "Вы убили Кенни! Это на вашей совести.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                //{
                //    _score = 0;
                //    ScoreLabel.Text = $"Счет: {_score}";
                //    _length = 30;
                //    _direction = 5;
                //    trigger = true;
                //    InitializeComponent();
                //    PaintCanvas.Children.Clear();
                //    _snakePoints.Clear();
                //    Point _startingPoint = new Point(100, 100);
                //    _currentPosition = _startingPoint;
                //    this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

                //    PaintSnake(_startingPoint);

                //    for (var n = 0; n < 10; n++)
                //    {
                //        PaintBonus(n);
                //    }






                //    BestScoreLabel.Text = $"Таблица лидеров \n{string.Join("\n", rating)}";
                //}
                //else
                //{
                //    this.Close();
                //}
            }
            else
            {
                return;
            }




        }

       
    }
}