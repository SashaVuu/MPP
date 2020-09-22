
namespace TracerLib.TraceUtil
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();

    }
}
