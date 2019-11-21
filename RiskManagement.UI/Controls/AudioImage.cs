using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RiskManagement.UI.Controls
{
    public class AudioImage : Image
    {
        static AudioImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AudioImage),
                new FrameworkPropertyMetadata(typeof(AudioImage)));
        }

        private SoundPlayer player = null;
        private bool playing = false;

        public AudioImage(UnmanagedMemoryStream audio)
        {
            player = new SoundPlayer(audio);
            MouseDown += ImageClicked;
        }
        public AudioImage()
        {
            MouseDown += ImageClicked;
        }

        public UnmanagedMemoryStream Audio
        {
            set
            {
                player = new SoundPlayer(value);
            }
        }

        private void ImageClicked(object sender, MouseButtonEventArgs e)
        {
            if (player == null)
                return;
            if (playing)
                player.Stop();
            else
                player.Play();
            playing = !playing;
        }
    }
}
