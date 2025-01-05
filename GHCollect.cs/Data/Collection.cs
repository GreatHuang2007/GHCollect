using Microsoft.VisualBasic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace GHCollect.Data
{
    public class Collection
    {

        private string filePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public List<List<string>> Detail { get; set; }
        public List<List<string>> Content { get; set; }

        public static Collection Read(string filePath)
        {
            if (!filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                filePath += ".json";
            }
            string jsonString = File.ReadAllText(filePath);
            Collection result = JsonSerializer.Deserialize<Collection>(jsonString);
            result.filePath = filePath;
            return result;
        }
        private void ChFilePath(string filePath)
        {
            if (!filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                filePath += ".json";
            }
            if (File.Exists(filePath))
            {
                File.Move(this.filePath, filePath);
            }
            this.filePath = filePath;
        }

        public void Write(string? filePath = null)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                ChFilePath(filePath);
            }
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(this.filePath, jsonString);
        }

        public string ToMarkdown()
        {
            string result = "";
            result += string.Format("#%s\n\n%s", Name);
            return result;
        }

    }

}
