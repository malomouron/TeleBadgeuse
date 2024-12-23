using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;

namespace TeleBadgeuse
{
    /// <summary>
    /// Interaction logique pour ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        #region Champs privés

        /// <summary>
        /// Modèle de vue pour les badges
        /// </summary>
        private BadgeViewModel _badgeViewModel;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ProfilePage"/>.
        /// </summary>
        public ProfilePage()
        {
            InitializeComponent();
            _badgeViewModel = new BadgeViewModel(new BadgeService(MainWindow.dbPath), this);
            DataContext = _badgeViewModel;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Masque l'élément AddTime.
        /// </summary>
        public void TurnOff()
        {
            AddTime.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Affiche l'élément AddTime.
        /// </summary>
        public void TurnOn()
        {
            AddTime.Visibility = System.Windows.Visibility.Visible;
        }

        private void ButtonShare_Click(object sender, RoutedEventArgs e)
        {
            string file = "config.json";
            string f =  File.ReadAllText(file);
            Dictionary<string, Dictionary<string, string>> mail = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(f);

            string br = "%0D%0A";
            string name = mail["mail"]["name"];
            string nameTo = mail["mail"]["nameTo"];
            string to = mail["mail"]["to"];
            string cc = mail["mail"]["cc"];
            string date = _badgeViewModel.SelectedDatePreview;
            
            if (date.Length == 10)
            {
                //on converti la date en format français "yyyy-MM-dd" en format anglais "MM/dd/yyyy
                date = date.Substring(8, 2) + "/" + date.Substring(5, 2) + "/" + date.Substring(0, 4);
            }

            List<Badge> badges = _badgeViewModel.Badges.OrderBy(b => b.Date).ToList();

            string badgesTexte = "";

            for (int i = 0; i < badges.Count; i += 2)
            {
                if (i + 1 < badges.Count)
                {
                    badgesTexte += badges[i].Date.ToString("HH:mm") + " -> " + badges[i + 1].Date.ToString("HH:mm") + br;
                }
                else
                {
                    badgesTexte += badges[i].Date.ToString("HH:mm") + br;
                }
            }



            string body = "Bonjour " + nameTo + "," + br + br +
                          "Voici mes badge pour la journée." + br + br + 
                          date + br +
                          badgesTexte + br +
                          "Cordialement,";
            string subject = "Badge " + name + " du " + date;

            string mailto = "mailto:" + to + "?subject=" + subject + "&body=" + body + "&cc=" + cc ;
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = mailto;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.RedirectStandardOutput = false;
            myProcess.Start();
        }
        #endregion


    }
}