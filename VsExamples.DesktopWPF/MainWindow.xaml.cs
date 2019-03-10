using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;
using System.Web;
using System.Net;
using System.IO;
using VsExamples.Standard;

namespace VsExamples.DesktopWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += onWindowLoaded;
        }

        ObservableCollection<Person> Persons = new ObservableCollection<Person>();


        private void onWindowLoaded(object sender, RoutedEventArgs e)
        {
            Persons.Add(new Person()
            {
                Name = "Jack",
                Age = 5
            });
            Persons.Add(new Person()
            {
                Name = "Terry",
                Age = 12
            });

            dgPersons.ItemsSource = Persons;

        }

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();
        }

        private async void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient1 = new WebClient();
            WebClient webClient2 = new WebClient();
            var bytes1Task = webClient1.DownloadDataTaskAsync("https://cdn2.iconfinder.com/data/icons/iconfinder-logo/69/eye_black-512.png");
            var bytes2Task = webClient2.DownloadDataTaskAsync("https://cdn2.iconfinder.com/data/icons/ios7-inspired-mac-icon-set/1024/Finder_5122x.png");
            var timeoutTask = Timeout();

            await Task.WhenAll(new Task[] { bytes1Task, bytes2Task, timeoutTask });

            // var results = await Task.WhenAll(new Task<byte[]>[] { bytes1Task, bytes2Task });

            File.WriteAllBytes($"{AppContext.BaseDirectory}/eye-black.png", bytes1Task.Result);
            File.WriteAllBytes($"{AppContext.BaseDirectory}/mac.png", bytes2Task.Result);

        }

        private Task Timeout()
        {
            var task = new Task(() =>{
                Thread.Sleep(3000);
                // return true;
            });
            task.Start();
            return task;
        }

        //private void DgPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    gdPerson.Content = dgPersons.SelectedItem;
        //}
    }
}
