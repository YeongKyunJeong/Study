using System;
using System.Collections;
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

namespace WPFApp01_MatchGame
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private float accumulatedTime = 0;
        private float oneMatchTimeLimitSetter = 10;
        private const float ONE_MATCH_TIME_LIMIT_DELTA = 0.1f;
        private const float ONE_MATCH_TIME_LIMIT_MIN = 5;

        private float totalMatchTimeLimitSetter = 30;
        private const float TOTAL_MATCH_TIME_LIMIT_DELTA1 = 0.5f;
        private const float TOTAL_MATCH_TIME_LIMIT_DELTA2 = 0.1f;
        private const float TOTAL_MATCH_TIME_LIMIT_MIN1 = 20;
        private const float TOTAL_MATCH_TIME_LIMIT_MIN2 = 10f;

        private float totalMatchTimeLimit;
        private float oneMatchTimeLimit;
        private int level = 0;

        private int matchesFound;
        private string matchesCountString = "";


        private List<string> animalEmojiOrigin = new List<string>() {
            "🐈", "🐈",
            "🐫", "🐫",
            "🐇", "🐇",
            "🦔", "🦔",
            "🦘", "🦘",
            "🐘", "🐘",
            "🐁", "🐁",
            "🐕", "🐕"
        };
        private List<string> animalEmoji;
        private int index;
        private Random random = new Random();
        private TextBlock lastClickedTextBlock;
        private TextBlock thisTimeClickedTextBlock;
        private bool isFindingMatch = false;

        private Brush redColorBrush = Brushes.Red;
        private Brush blackColorBrush = Brushes.Black;

        private TextBlock[] animalBlocks = new TextBlock[0];
        private TextBlock oneMatchTimer;
        private TextBlock totalMatchTimer;
        private TextBlock countDownBlock;
        private TextBlock levelBlock;
        private bool isCountDowning = false;

        public MainWindow()
        {
            InitializeComponent();
            SetParameter(true);
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += TimerTick;
            isCountDowning = true;
            SetUpGame(true);
            StartCountDown(true);
        }

        private void SetParameter(bool isFirst = false, bool isReset = false)
        {
            if (isFirst)
            {
                if (!isReset)   // First Time, not reset
                {
                    SetTextBlockReference();
                }
                level = 1;
            }
            else
            {
                level++;
                if (oneMatchTimeLimitSetter > ONE_MATCH_TIME_LIMIT_MIN)
                {
                    oneMatchTimeLimitSetter -= ONE_MATCH_TIME_LIMIT_DELTA;

                }

                if (totalMatchTimeLimitSetter > TOTAL_MATCH_TIME_LIMIT_MIN1)
                {
                    totalMatchTimeLimitSetter -= TOTAL_MATCH_TIME_LIMIT_DELTA1;
                }
                else if (totalMatchTimeLimitSetter > TOTAL_MATCH_TIME_LIMIT_MIN2)
                {
                    totalMatchTimeLimitSetter -= TOTAL_MATCH_TIME_LIMIT_DELTA2;
                }
            }

            levelBlock.Text = level.ToString();
            totalMatchTimeLimit = totalMatchTimeLimitSetter;
            totalMatchTimer.Text = totalMatchTimeLimit.ToString("0.0s");
            oneMatchTimeLimit = oneMatchTimeLimitSetter;
            oneMatchTimer.Text = oneMatchTimeLimit.ToString("0.0s");
            accumulatedTime = 0;

        }

        private void SetTextBlockReference()
        {
            IEnumerable<TextBlock> allTextBlocks = mainGrid.Children.OfType<TextBlock>();
            animalBlocks = new TextBlock[16];

            for (int i = 0; i < allTextBlocks.Count(); i++)
            {
                if (allTextBlocks.ElementAt(i).Tag == null)
                {

                }
                else
                {
                    switch (allTextBlocks.ElementAt(i).Tag.ToString())
                    {
                        case "animal":
                            {
                                animalBlocks[i] = allTextBlocks.ElementAt(i);
                                break;
                            }
                        case "leftTimer":
                            {
                                oneMatchTimer = allTextBlocks.ElementAt(i);
                                break;
                            }
                        case "rightTimer":
                            {
                                totalMatchTimer = allTextBlocks.ElementAt(i);
                                break;
                            }
                        case "countDown":
                            {
                                countDownBlock = allTextBlocks.ElementAt(i);
                                break;
                            }
                        case "level":
                            {
                                levelBlock = allTextBlocks.ElementAt(i);
                                break;
                            }
                        default: break;
                    }
                }
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {

            if (isCountDowning)
            {
                if (index == 20)
                {
                    countDownBlock.Text = "2";
                }
                else if (index == 10)
                {
                    countDownBlock.Text = "1";
                }
                else if (index == 0)
                {
                    StartCountDown(false);
                    return;
                }
                index--;
            }
            else
            {
                accumulatedTime += 0.1f;
                oneMatchTimeLimit -= 0.1f;

                oneMatchTimer.Text = oneMatchTimeLimit.ToString("0.0s");
                if (matchesFound == 8)
                {
                    matchesFound = 0;
                    SetParameter();
                    StartCountDown(true);
                    SetUpGame();
                    StartCountDown(true);
                }

                if (oneMatchTimeLimit < 0)
                {
                    oneMatchTimer.Text = "0.0s";
                    foreach (TextBlock textBlock in animalBlocks)
                    {
                        textBlock.Visibility = Visibility.Hidden;
                    }
                    timer.Stop();
                }
            }
        }

        private void StartCountDown(bool isStart)
        {
            if (isStart)
            {
                countDownBlock.Text = "3";
                countDownBlock.Visibility = Visibility.Visible;
                foreach (TextBlock textBlock in animalBlocks)
                {
                    textBlock.Visibility = Visibility.Hidden;
                }
                index = 30;
                isCountDowning = true;
            }
            else
            {
                countDownBlock.Visibility = Visibility.Hidden;
                foreach (TextBlock textBlock in animalBlocks)
                {
                    textBlock.Visibility = Visibility.Visible;
                }
                isCountDowning = false;
            }
        }

        private void SetUpGame(bool isFirst = false)
        {
            animalEmoji = animalEmojiOrigin.ToList();
            foreach (TextBlock textBlock in animalBlocks)
            {
                textBlock.Visibility = Visibility.Visible;
                index = random.Next(animalEmoji.Count);
                textBlock.Text = animalEmoji[index];
                animalEmoji.RemoveAt(index);

            }
            if (isFirst)
            {
                timer.Start();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            thisTimeClickedTextBlock = sender as TextBlock;
            if (!isFindingMatch)
            {
                thisTimeClickedTextBlock.Foreground = redColorBrush;
                lastClickedTextBlock = thisTimeClickedTextBlock;
                isFindingMatch = true;
            }
            else if (thisTimeClickedTextBlock.Text.Equals(lastClickedTextBlock.Text))   // Correct match
            {
                thisTimeClickedTextBlock.Visibility = Visibility.Hidden;
                lastClickedTextBlock.Visibility = Visibility.Hidden;
                lastClickedTextBlock.Foreground = blackColorBrush;
                isFindingMatch = false;

                matchesFound++;
                totalMatchTimeLimit -= accumulatedTime;
                oneMatchTimeLimit = oneMatchTimeLimitSetter;
                accumulatedTime = 0;
                if (matchesFound < 8)
                {
                    totalMatchTimer.Text = totalMatchTimeLimit.ToString("0.0s");
                    oneMatchTimer.Text = oneMatchTimeLimit.ToString("0.0s");
                }
            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;   // Wrong match
                lastClickedTextBlock.Foreground = blackColorBrush;
                lastClickedTextBlock = null;

                isFindingMatch = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetParameter(true, true);
            isCountDowning = true;
            SetUpGame(true);
            StartCountDown(true);
        }
    }
}
