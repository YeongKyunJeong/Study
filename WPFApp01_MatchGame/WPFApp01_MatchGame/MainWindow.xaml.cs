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

namespace WPFApp01_MatchGame
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> animalEmojiOrigin = new List<string>() {
            "🐈", "🐈",
            "🐫", "🐫",
            "🐇", "🐇",
            "🦔", "🦔",
            "🦒", "🦒",
            "🐘", "🐘",
            "🐁", "🐁",
            "🐕", "🐕"
        };
        private List<string> animalEmoji;
        private string timeTextBlockName = "timeTextBlock";
        private int index;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            animalEmoji = animalEmojiOrigin.ToList();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != timeTextBlockName)
                {
                    textBlock.Visibility = Visibility.Visible;
                    index = random.Next(animalEmoji.Count);
                    textBlock.Text = animalEmoji[index];
                    animalEmoji.RemoveAt(index);
                    //
                }

            }
        }
    }
}
