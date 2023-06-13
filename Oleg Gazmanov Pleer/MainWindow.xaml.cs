using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Forms;
using Microsoft.Win32;


namespace Oleg_Gazmanov_Pleer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        bool song_playing;

        private void startPlay()
        {
            media.Play();
            song_playing = true;
            Play_BT.Content = "| |";
        }

        private void pausePlay()
        {
            media.Pause();
            song_playing = false;
            Play_BT.Content = ">";
        }
        public MainWindow()
        {
            InitializeComponent();
            back_BT.Content = "<<";
            next_BT.Content = ">>";
            Play_BT.Content = ">";
            
            media.Volume = 0.5;
            //startPlay();
        }
        
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Position = new TimeSpan(Convert.ToInt64(ProgressSlider.Value));
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            dialog.ShowDialog();
            choose_path.Content = dialog.SelectedPath;
            Files_list.Items.Clear();
            foreach (var file in Directory.GetFiles(dialog.SelectedPath))
            {
                Files_list.Items.Add(System.IO.Path.GetFileName(file));
            }
        }

        private void Files_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string path = dialog.SelectedPath.ToString() +"\\"+ Files_list.SelectedValue.ToString();
            media.Source = new Uri(path);
            media.Volume = 0.5;
            startPlay();
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            ProgressSlider.Maximum = media.NaturalDuration.TimeSpan.Ticks;
        }

        private void Play_BT_Click(object sender, RoutedEventArgs e)
        {
            if (song_playing)
            {
                pausePlay();      
            }
            else
            {
                startPlay();
            }
        }

        private void next_BT_Click(object sender, RoutedEventArgs e)
        {
            pausePlay();
            Files_list.SelectedIndex += 1;
            string path = dialog.SelectedPath.ToString() + "\\" + Files_list.SelectedValue.ToString();
            media.Source = new Uri(path);
            if (song_playing) { startPlay(); }

        }

        private void back_BT_Click(object sender, RoutedEventArgs e)
        {
            pausePlay();
            Files_list.SelectedIndex -= 1;
            string path = dialog.SelectedPath.ToString() + "\\" + Files_list.SelectedValue.ToString();
            media.Source = new Uri(path);
            if (song_playing) { startPlay(); }
           
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = volume.Value/100.0;
        }
    }
}
