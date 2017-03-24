using System;
using trackingRoom.interfaces;

namespace AssemblyCSharp
{
	public class OnOffSwitcher : IMode
	{
		public OnOffSwitcher ()
		{
		}

		#region IMode implementation

		public void OnUserDetectionChange (LampBehaviour originScript, string userTag, bool inRange)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

