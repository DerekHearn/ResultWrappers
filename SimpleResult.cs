using APPI.Meetball;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ResultWrappers
{

	[DataContract]
	public struct SimpleResult<T> : IResult
	{
		///<summary>MbSpecificError of the GenericResult.</summary>
		[DataMember]
		public MbSpecificError MBResult { get; set; }

		[DataMember]
		public T Item { get; set; }

		public SimpleResult(T item, MbSpecificError result)
			: this()
		{
			Item = item;
			MBResult = result;
		}

		public SimpleResult(T item)
			: this(item, MbSpecificError.NoError()) { }

		/// <summary>
		/// CreateResult Constructor.
		/// </summary>
		/// <param name="result">MbSpecificError with info about the operation.</param>
		public SimpleResult(MbSpecificError result)
			: this(default(T), result) { }

		public SimpleResult(Exception e)
			: this(default(T), new MbSpecificError(e)) { }

		public SimpleResult(Exception e, string friendlyMessage)
			: this(default(T), new MbSpecificError(e, friendlyMessage)) { }

		public SimpleResult(Enums.MBException e)
			: this(default(T), new MbSpecificError(e)) { }
	}

}