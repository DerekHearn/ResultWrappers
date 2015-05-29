using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultWrappers
{
	public interface IResult
	{
		APPI.Meetball.MbSpecificError MBResult { get; set; }
	}
}
