using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace LernAppIS_WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Frage
        {
            public string Kategorie { get; set; }
            public string Titel { get; set; }
        }

        public ObservableCollection<Frage> Fragen = new ObservableCollection<Frage>();

        public MainWindow()
        {
            InitializeComponent();

            Fragen.Add(new Frage { Kategorie = "WPF/XAML", Titel = "Frage 1: Was ist XAML?" });
            Fragen.Add(new Frage { Kategorie = "WPF/XAML", Titel = "Frage 2: Wofür benutzt man Grid?" });
            Fragen.Add(new Frage { Kategorie = ".NET", Titel = "Frage 1: Was ist .NET?" });
            Fragen.Add(new Frage { Kategorie = ".NET", Titel = "Frage 2: Was ist CLR?" });
            Fragen.Add(new Frage { Kategorie = "C#", Titel = "Frage 1: Was ist eine Klasse?" });
            Fragen.Add(new Frage { Kategorie = "C#", Titel = "Frage 2: Was ist eine Methode?" });
            Fragen.Add(new Frage { Kategorie = "All in One", Titel = "Frage 1: Zeigen Sie den Einsatz von Attached Properties und Property-Elemente-Syntax." });
            Fragen.Add(new Frage { Kategorie = "All in One", Titel = "Frage 2: Erklären Sie DependencyProperties." });

            FragenListe.ItemsSource = Fragen;
        }

        // Menüpunkt-Klick: wechselt Tab
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menu)
            {
                switch (menu.Header.ToString())
                {
                    case "WPF/XAML":
                        TabBereich.SelectedIndex = 0;
                        break;
                    case ".NET":
                        TabBereich.SelectedIndex = 1;
                        break;
                    case "C#":
                        TabBereich.SelectedIndex = 2;
                        break;
                    case "All in One":
                        TabBereich.SelectedIndex = 3;
                        break;
                }
            }
        }
        // Überarbeitetes SelectionChanged Event
        private void FragenListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FragenListe.SelectedItem == null)
                return;

            // Cast SelectedItem auf Frage
            var ausgewählteFrage = FragenListe.SelectedItem as Frage;
            if (ausgewählteFrage == null) return;

            StatusText.Text = $"Ausgewählte Frage: {ausgewählteFrage.Titel}";

            // Wähle StackPanel basierend auf Kategorie
            StackPanel panel = null;
            if (ausgewählteFrage.Kategorie == "WPF/XAML")
                panel = WPFContentPanel;
            else if (ausgewählteFrage.Kategorie == ".NET")
                panel = DotNetContentPanel;
            else if (ausgewählteFrage.Kategorie == "C#")
                panel = CSharpContentPanel;
            else
                panel = AllInOnePanel;

            // Alle Expander durchlaufen und passenden öffnen
            foreach (UIElement child in panel.Children)
            {
                Expander expander = child as Expander;
                if (expander != null)
                {
                    if (expander.Header.ToString() == ausgewählteFrage.Titel)
                        expander.IsExpanded = true;
                    else
                        expander.IsExpanded = false;
                }
            }

            // Tab wechseln
             if (ausgewählteFrage.Kategorie == "WPF/XAML")
                TabBereich.SelectedIndex = 0;
            else if (ausgewählteFrage.Kategorie == ".NET")
                 TabBereich.SelectedIndex = 1;
            else if (ausgewählteFrage.Kategorie == "C#")
                TabBereich.SelectedIndex = 2;
            else
                TabBereich.SelectedIndex = 3;
            }

        // Checkbox Event
        private void Verstanden_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.IsChecked == true)
            {
                StatusText.Text = "Frage als verstanden markiert!";
            }
        }
    }
}