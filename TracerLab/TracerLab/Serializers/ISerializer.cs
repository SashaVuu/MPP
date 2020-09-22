using System;
using TracerLib.TraceUtil;

namespace TracerLab.Serializers
{
    public interface ISerializer
    {
        public string Serialize(TraceResult traceResult);
    }
}
