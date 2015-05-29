using APPI.Meetball;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ResultWrappers
{
	/// <summary>
	/// Structure which represents a Generic Result for an operation.
	/// </summary>
	[DataContract]
	public struct GenericResult : IResult
	{
		///<summary>MbSpecificError of the GenericResult.</summary>
		[DataMember]
		public MbSpecificError MBResult { get; set; }

		/// <summary>
		/// GenericResult Constructor.
		/// </summary>
		/// <param name="result">MbSpecificError of the GenericResult. </param>
		public GenericResult(MbSpecificError result)
			: this()
		{
			MBResult = result;
		}

		public GenericResult(Exception e)
			: this(new MbSpecificError(e)) { }

		public GenericResult(Exception e, string friendlyMessage)
			: this(new MbSpecificError(e, friendlyMessage)) { }

		public GenericResult(Enums.MBException e)
			: this(new MbSpecificError(e)) { }

		/// <summary>
		/// GenericResult Constructor Success case.
		/// </summary>
		/// <returns>GenericResult.</returns>
		public static GenericResult Success()
		{
			return new GenericResult(MbSpecificError.NoError());
		}
	}

}