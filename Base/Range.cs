using System;
using System.Collections.Generic;
using System.Linq;



namespace ForwardTask.Base
{
	public class Range< T >
		where T : IComparable
	{
		T min;
		T max;

		public Range( T value1, T value2 )
		{
			if ( value1.CompareTo( value2 ) <= 0 )
			{
				this.min = value1;
				this.max = value2;
			}
			else
			{
				this.min = value2;
				this.max = value1;
			}			
		}

		public bool Contains( T value )
		{
			return ( min.CompareTo( value ) <= 0 && value.CompareTo( max ) <= 0 );
		}
	}
}
