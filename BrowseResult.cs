using APPI.Meetball;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ResultWrappers
{
	/// <summary>
	/// Structure which store a Typed Specific Structure along with the MbSpecificError which indicates error details in case the operation is not successful.
	/// </summary>
	[DataContract]
	public struct BrowseResult<t> : IResult
	{
		///<summary>Object Collection.</summary>
		[DataMember]
		public List<t> Items;

		///<summary>MbSpecificError with info about the operation.</summary>
		[DataMember]
		public MbSpecificError MBResult { get; set; }

		public BrowseResult(List<t> items, MbSpecificError result)
			: this()
		{
			Items = items;
			MBResult = result;
		}

		/// <summary>
		/// BrowseResult Constructor.
		/// </summary>
		/// <param name="items">Object Collection.</param>
		public BrowseResult(List<t> items) : this(items, MbSpecificError.NoError()) { }

		public BrowseResult(t[] items) : this(new List<t>(items), MbSpecificError.NoError()) { }

		/// <summary>
		/// BrowseResult Constructor.
		/// </summary>
		/// <param name="result">MbSpecificError with info about the operation.</param>
		public BrowseResult(MbSpecificError result) : this(null, result) { }

		public BrowseResult(Exception e) : this(null, new MbSpecificError(e)) { }

		public BrowseResult(Exception e, string friendlyMessage)
			: this(null, new MbSpecificError(e, friendlyMessage)) { }

		public BrowseResult(Enums.MBException e) : this(null, new MbSpecificError(e)) { }
	}
}