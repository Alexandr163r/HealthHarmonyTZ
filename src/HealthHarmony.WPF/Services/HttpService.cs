using HealthHarmony.WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthHarmony.WPF.Services
{
    public class HttpService
    {
        readonly HttpClient _client = new HttpClient();

        public HttpService()
        {
            _client.BaseAddress = new Uri("https://localhost:5000/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<RecordModel>> GetRecords()
        {
            var response = await _client.GetStringAsync("records");

            var records = JsonConvert.DeserializeObject<List<RecordModel>>(response);

            return records;
        }

        public async Task<bool> AddRecord(RecordModel record)
        {
            var result = _client.PostAsJsonAsync("records", record).Result;

            if (result.IsSuccessStatusCode) 
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateRecord(RecordModel record)
        {
            var result = _client.PutAsJsonAsync("records/" + record.Id, record).Result;

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRecord(RecordModel record)
        {
            var result = _client.DeleteAsync("records/" + record.Id).Result;

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
