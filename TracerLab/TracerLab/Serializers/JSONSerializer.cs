using Newtonsoft.Json;
using System.IO;
using TracerLab.TraceUtil;

namespace TracerLab.Serializers
{
    public class JSONSerializer
    {
        public void Serialize(TraceResult traceResult, string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            string text = JsonConvert.SerializeObject(traceResult, Formatting.Indented);
            File.WriteAllText(path, text);

        }
    }

}
