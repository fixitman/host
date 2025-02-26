using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;


namespace host;

    public class SettingsManager<T> where T : class{
        private readonly string _filePath;
        private readonly JsonSerializerOptions options = new()
        {
            IncludeFields=true,
            WriteIndented=true
        };

        public SettingsManager(string fileName)
        {
            _filePath = GetLocalFilePath(fileName);
        }

        private string GetLocalFilePath(string fileName)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appData, fileName);
        }

        public async Task<T?> LoadSettingsAsync() => 
            File.Exists(_filePath) ?
            JsonSerializer.Deserialize<T>(await File.ReadAllTextAsync(_filePath),options):
            null;
        

        public async Task SaveSettingsAsync(T settings)
        {   
            string json = JsonSerializer.Serialize<T>(settings,options);
            var d = Path.GetDirectoryName(_filePath);
            if(d != null) Directory.CreateDirectory(d);
            await File.WriteAllTextAsync(_filePath, json);
        }
}
