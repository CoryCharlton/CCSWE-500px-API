using System.Collections.Generic;

namespace CCSWE.FiveHundredPx.Interfaces
{
	public interface IPhotoFilter
	{
		#region Properties
		Categories Categories { get; set; }
		Feature Feature { get; set; }
		FilterMode FilterMode { get; set; }
		List<int> Sizes { get; set; }
		Sort Sort { get; set; }
		SortDirection SortDirection { get; set; }
		long UserId { get; set; }
		#endregion

		#region Methods
		string ToString(); 
		#endregion
	}
}