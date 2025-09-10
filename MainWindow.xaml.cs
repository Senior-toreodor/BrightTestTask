using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BrightTestTask.Data;

namespace BrightTestTask
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var db = new NumbersDbContext())
            {
                db.Database.Initialize(false);
            }
        }

        private void GenerateData(object sender, RoutedEventArgs e)
        {
            using var db = new NumbersDbContext();

            db.Numbers.RemoveRange(db.Numbers);
            db.SortedNumbers.RemoveRange(db.SortedNumbers);
            db.SaveChanges();

            var random = new Random();
            var values = Enumerable.Range(1, 10).OrderBy(x => random.Next()).ToList();

            var numbers = new List<NumbersTable>();
            for (int i = 0; i < values.Count; i++) 
                numbers.Add(new NumbersTable { Value = values[i] }); 

            db.Numbers.AddRange(numbers);
            db.SaveChanges();

            NumbersGrid.ItemsSource = db.Numbers.ToList();
            SortedGrid.ItemsSource = db.SortedNumbers.ToList(); 

            StatusText.Text = "Table 1 filled.";
        }

        private void SortCopyData(object sender, RoutedEventArgs e)
        {
            using var db = new NumbersDbContext();

            db.SortedNumbers.RemoveRange(db.SortedNumbers);
            db.SaveChanges();
                 
            var sorted = db.Numbers
                .Select(n => new { n.Id, n.Value })    
                .OrderBy(n => n.Value)               
                .ToList();

             var toInsert = new List<SortedNumbersTable>(sorted.Count);
             for (int i = 0; i < sorted.Count; i++)
             {
                 var item = sorted[i];
                 toInsert.Add(new SortedNumbersTable{ Value = item.Value, Position = item.Id });
             }

             db.SortedNumbers.AddRange(toInsert);
             db.SaveChanges();
              
             SortedGrid.ItemsSource = db.SortedNumbers.OrderBy(s => s.Value).ToList();
             StatusText.Text = "Table 2 filled and sorted."; 
        }
    }
}