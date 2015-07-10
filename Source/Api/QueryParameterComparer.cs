using System;
using System.Collections.Generic;

namespace CCSWE.FiveHundredPx
{
    public class QueryParameterComparer : IComparer<QueryParameter>
    {
        #region IComparer<QueryParameter> Members
        public int Compare(QueryParameter x, QueryParameter y)
        {
            return x.Name == y.Name ? string.Compare(x.Value, y.Value, StringComparison.InvariantCultureIgnoreCase) : string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion
    }
}
