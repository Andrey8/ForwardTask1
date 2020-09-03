using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using ForwardTask.Engines;
using ForwardTask.Base;



namespace ForwardTask.TestBench
{
	public class InternalCombustionEngineSimulator : IEngineSimulator
	{
		readonly Base.Temperature ambientTemperature;
		readonly SimulationParameters parameters;
		readonly TimeSpan modelTimeStep;
		readonly Point[] points;
		bool stop;
		readonly double tolerance;

		public TimeSpan EngineWorkingTime { get; private set; }



		public InternalCombustionEngineSimulator( SimulationParameters parameters, Base.Temperature ambientTemperature )
		{
			this.ambientTemperature = ambientTemperature;
			this.parameters = parameters;
			this.modelTimeStep = TimeSpan.FromMilliseconds( 2000 );
			this.points = this.parameters.CrankshaftSpeedToTorquePoints.OrderBy( point => point.X ).ToArray();
			this.stop = false;
			this.tolerance = 1e-1;
		}
		
		public void Simulate( AbstractEngine engine )
		{						
			engine.Start( ambientTemperature );
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			Thread heatingThread = new Thread( new ParameterizedThreadStart( HeatEngine ) );
			heatingThread.Start( engine );
			Thread coolingThread = new Thread( new ParameterizedThreadStart( CoolEngine ) );
			coolingThread.Start( engine );
			
			int checkTimeStep = 1000;
			while ( true )
			{
				Thread.Sleep( checkTimeStep );

				double diff = engine.Temperature.Celsius - parameters.OverheatTemperature.Celsius;
				if ( Math.Abs( diff ) <= tolerance || diff > 0 )
				{
					stop = true;
					EngineWorkingTime = stopwatch.Elapsed;

					Console.WriteLine( "Engine check ... Temperature equals {0}", engine.Temperature.Celsius );

					Thread.Sleep( modelTimeStep );
					Thread.Sleep( modelTimeStep );

					return;
				}
				Console.WriteLine( "Engine check ... Temperature equals {0}", engine.Temperature.Celsius );
			}
		}



		void HeatEngine( object engineObj )
		{
			var engine = ( InternalCombustionEngine ) engineObj;

			while ( !stop )
			{
				Thread.Sleep( modelTimeStep );

				Base.Temperature temp = GetHeatingTemperature( engine );				
				engine.Heat( temp );

				Console.WriteLine( "Heat engine \t by {0} degrees celsius", temp.Celsius );
			}
		}

		void CoolEngine( object engineObj )
		{
			var engine = ( InternalCombustionEngine ) engineObj;

			while ( !stop )
			{
				Thread.Sleep( modelTimeStep );

				Base.Temperature temp = GetCoolingTemperature( engine );				
				engine.Cool( temp );

				Console.WriteLine( "Cool engine \t by {0} degrees celsius", temp.Celsius );
			}
		}



		Base.Temperature GetHeatingTemperature( InternalCombustionEngine engine )
		{
			double v = engine.CrankshaftSpeed;
			double m = GetTorque( v );
			double hm = parameters.HeatingSpeedOfTorqueCoeff;
			double hv = parameters.HeatingSpeedOfCrankshaftSpeedCoeff;

			engine.CrankshaftSpeed += modelTimeStep.Seconds * ( m / parameters.MomentOfInertia );

			return new Base.Temperature( modelTimeStep.Seconds * ( m * hm + v * v * hv ) );
		}

		Base.Temperature GetCoolingTemperature( InternalCombustionEngine engine )
		{
			double c = parameters.CoolingSpeedOfTemperaturesCoeff;

			return new Base.Temperature( -modelTimeStep.Seconds * c * ( ambientTemperature.Celsius - engine.Temperature.Celsius ) );
		}

		double GetTorque( double crankShaftSpeed )
		{			
			int i = GetIndex( points.Select( p => p.X ).ToArray(), crankShaftSpeed );

			Point p1 = points[ i ];
			Point p2 = points[ i + 1 ];

			return GetLinearFunctionValue( crankShaftSpeed, p1, p2 );
		}



		static int GetIndex( double[] ascendinglySortedArray, double value )
		{
			int n = ascendinglySortedArray.Length;

			if ( !( new Range<double>( ascendinglySortedArray[ 0 ], ascendinglySortedArray[ n - 1 ] ) ).Contains( value ) )
			{
				throw new ArgumentOutOfRangeException( "The value is out of array range." );
			}
			
			int minIndex = 0;
			int maxIndex = n - 1;
			int middleIndex = ( n - 1 ) / 2;
			while ( maxIndex - minIndex > 1 )
			{
				middleIndex = ( minIndex + maxIndex ) / 2;
				if ( ascendinglySortedArray[ middleIndex ] <= value )
				{
					minIndex = middleIndex;
				}
				else
				{
					maxIndex = middleIndex;
				}
			}

			return minIndex;
		}

		static double GetLinearFunctionValue( double x, Point p1, Point p2 )
		{
			double x1 = p1.X;
			double y1 = p1.Y;
			double x2 = p2.X;
			double y2 = p2.Y;

			if ( x1 == x2 )
			{
				throw new ArgumentException( "X coords of points are equal." );
			}

			if ( !new Range<double>( x1, x2 ).Contains( x ) )
			{
				throw new ArgumentOutOfRangeException( "The x value is out of allowable range." );
			}			

			return ( x * ( y1 - y2 ) + ( x1 * y2 - x2 * y1 ) ) / ( x1 - x2 );
		}
	}
}
