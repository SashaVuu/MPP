using System;
using TracerLab.TraceUtil;

namespace TracerLab.Serializers
{
    public interface ISerializer
    {
        public string Serialize(TraceResult traceResult);
    }
}
