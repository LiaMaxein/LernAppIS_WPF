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

        // Wenn eine Frage in der ListBox ausgewählt wird: Fragenliste SelectionChanged
        private void FragenListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FragenListe.SelectedItem == null) return;

            string ausgewählteFrage = FragenListe.SelectedItem.ToString();
            StatusText.Text = $"Ausgewählte Frage: {ausgewählteFrage}";

            // Wähle StackPanel basierend auf TabIndex oder Frage
            StackPanel panel = null;

            if (ausgewählteFrage.Contains("XAML") || ausgewählteFrage.Contains("Grid"))
                panel = WPFContentPanel;
            else if (ausgewählteFrage.Contains(".NET") || ausgewählteFrage.Contains("CLR"))
                panel = DotNetContentPanel;
            else if (ausgewählteFrage.Contains("Klasse") || ausgewählteFrage.Contains("Methode"))
                panel = CSharpContentPanel;
            else
                panel = AllInOnePanel;

            // Alle Expander durchlaufen
            foreach (var child in panel.Children)
            {
                if (child is Expander expander)
                {
                    if (expander.Header.ToString() == ausgewählteFrage)
                        expander.IsExpanded = true;
                    else
                        expander.IsExpanded = false;
                }
            }

            // Optional: Tab wechseln, damit der passende Tab angezeigt wird
            if (panel == WPFContentPanel) TabBereich.SelectedIndex = 0;
            else if (panel == DotNetContentPanel) TabBereich.SelectedIndex = 1;
            else if (panel == CSharpContentPanel) TabBereich.SelectedIndex = 2;
            else TabBereich.SelectedIndex = 3;
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