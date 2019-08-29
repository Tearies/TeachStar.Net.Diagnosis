using System.Collections.Generic;

namespace TeachStar.Net.Diagnosis.Common.Net
{ 
    public class DiagnosisResult
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DiagnosisResult(List<NetAddressInfo> results, NetAddressInfo recommend)
        {
            Results = results;
            Recommend = recommend;
        }

        public List<NetAddressInfo> Results { get; set; }

        public NetAddressInfo Recommend { get; set; }
    }
} 
 