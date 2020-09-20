using System;
using TracerLab.TraceUtil;

namespace TracerLab.Serializers
{
    public interface ISerializer
    {
        public void Serialize(TraceResult traceResult, string path);
    }
}
