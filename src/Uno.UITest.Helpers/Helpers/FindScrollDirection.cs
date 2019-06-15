using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno.UITest.Helpers
{
	public enum FindScrollDirection
	{
		/// <summary>
		/// Searches downwards from the current scroll position
		/// </summary>
		Down,
		/// <summary>
		/// Searches upwards from the current scroll position
		/// </summary>
		Up,
		/// <summary>
		/// Searches downwards from the current 
		/// </summary>
		DownThenUp,

		UpThenDown
	}
}
