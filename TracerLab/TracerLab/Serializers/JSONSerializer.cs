using Newtonsoft.Json;
using System.IO;
using TracerLab.TraceUtil;

namespace TracerLab.Serializers
{
    public class JSONSerializer:ISerializer
    {
        public void Serialize(TraceResult traceResult, string path)
        {
            string text = JsonConvert.SerializeObject(traceResult, Formatting.Indented);
            File.WriteAllText(path, text);

        }
    }

}
