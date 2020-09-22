using Newtonsoft.Json;
using System.IO;
using TracerLib.TraceUtil;

namespace TracerLab.Serializers
{
    public class JSONSerializer:ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            string text = JsonConvert.SerializeObject(traceResult, Formatting.Indented);
            return text;
        }
    }

}
