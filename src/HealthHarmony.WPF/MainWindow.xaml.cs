using System;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;
using HealthHarmony.WPF.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace HealthHarmony.WPF
{
    public partial class MainWindow
    {
        readonly HttpClient _client = new HttpClient();

        public MainWindow()
        {
            _client.BaseAddress = new Uri("https://localhost:5000/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            InitializeComponent();
        }

        private void btnLoadRecords_Click(object sender, RoutedEventArgs e)
        {
            GetRecords();
        }
        
        private async void GetRecords()
        {
            var response = await _client.GetStringAsync("records");

            var records = JsonConvert.DeserializeObject<ObservableCollection<Record>>(response);

            DgRecord.DataContext = records;
        }

        private async Task<bool> SaveRecord(Record record)
        {
            var result = await _client.PostAsJsonAsync("records", record);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }
           
            return false;
        }

        private async Task<bool> UpdateRecord(Record record)
        {
            var result = await _client.PutAsJsonAsync("records/" + record.Id, record);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            
            return false;
        }

        private async void DeleteRecord(Guid id)
        {
            await _client.DeleteAsync("records/" + id);

            GetRecords();
        }

        private async void btnSaveRecord_Click(object sender, RoutedEventArgs e)
        {
            var record = new Record()
            {
                Title = TxtTitleBox.Text,
                Text = TxtTextBox.Text
            };

            var result = await this.SaveRecord(record);

            if (result)
            {
                GetRecords();

                MessageBox.Show("Запись сохранена");
            }
            else
            {
                var dialogResult = MessageBox.Show("Обновить запись?", "Обновление Записи", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    record.Id = new Guid($"{TxtRecordId.Text}");
                    var resultUpdate = await UpdateRecord(record);

                    if (resultUpdate)
                    {
                        GetRecords();

                        MessageBox.Show("Запись обновлена");
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка при обновлении", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnEditRecord_Click(object sender, RoutedEventArgs e)
        {
            Record? record = ((FrameworkElement)sender).DataContext as Record;
            TxtRecordId.Text = record?.Id.ToString();
            TxtTextBox.Text = record?.Text;
            TxtTitleBox.Text = record?.Title;
        }
        
        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            Record? record = ((FrameworkElement)sender).DataContext as Record;

            if (record != null) this.DeleteRecord(id: record.Id);
        }
    }
}