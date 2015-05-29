using APPI.Meetball;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ResultWrappers
{
	[DataContract]
	public struct CreateResult : IResult
	{
		///<summary>ID of the item created.</summary>
		[DataMember]
		public int ID;

		///<summary>MbSpecificError with info about the operation.</summary>
		[DataMember]
		public MbSpecificError MBResult { get; set; }

		/// <summary>
		/// CreateResult Constructor.
		/// </summary>
		/// <param name="id">ID of the item created.</param>
		/// <param name="result">MbSpecificError with info about the operation.</param>
		public CreateResult(int id, MbSpecificError result)
			: this()
		{
			ID = id;
			MBResult = result;
		}

		/// <summary>
		/// CreateResult Constructor.
		/// </summary>
		/// <param name="id">ID of the item created.</param>
		public CreateResult(int id)
			: this(id, MbSpecificError.NoError()) { }

		/// <summary>
		/// CreateResult Constructor.
		/// </summary>
		/// <param name="result">MbSpecificError with info about the operation.</param>
		public CreateResult(MbSpecificError result)
			: this(-1, result) { }

		public CreateResult(Exception e)
			: this(-1, new MbSpecificError(e)) { }

		public CreateResult(Exception e, string friendlyMessage)
			: this(-1, new MbSpecificError(e, friendlyMessage)) { }

		public CreateResult(Enums.MBException e)
			: this(-1, new MbSpecificError(e)) { }
	}

	/// <summary>
	/// Structure which represents the result of an Insert operation.
	/// </summary>
	[DataContract]
	public struct CreateResult<T> : IResult
	{
		///<summary>ID of the item created.</summary>
		[DataMember]
		public T ID;

		///<summary>MbSpecificError with info about the operation.</summary>
		[DataMember]
		public MbSpecificError MBResult { get; set; }

		/// <summary>
		/// CreateResult Constructor.
		/// </summary>
		/// <param name="id">ID of the item created.</param>
		/// <param name="result">MbSpecificError with info about the operation.</param>
		public CreateResult(T id, MbSpecificError result)
			: this()
		{
			ID = id;
			MBResult = result;
		}

		/// <summary>
		/// CreateResult Constructor.
		/// </summary>
		/// <param name="id">ID of the item created.</param>
		public CreateResult(T id)
			: this(id, MbSpecificError.NoError()) { }

		/// <summary>
		/// CreateResult Constructor.
		/// </summary>
		/// <param name="result">MbSpecificError with info about the operation.</param>
		public CreateResult(MbSpecificError result)
			: this(default(T), result) { }

		public CreateResult(Exception e)
			: this(default(T), new MbSpecificError(e)) { }

		public CreateResult(Exception e, string friendlyMessage)
			: this(default(T), new MbSpecificError(e, friendlyMessage)) { }

		public CreateResult(Enums.MBException e)
			: this(default(T), new MbSpecificError(e)) { }
	}
}