using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using ForwardTask.Engines;



namespace ForwardTask
{
	public class TestBench
	{
		double ambientTemperature;
		EngineParameters parameters;

		Stopwatch stopwatch;
		Task task;
		bool testHasStopped;
		Timer timer;
		


		public TestBench( EngineParameters parameters, double ambientTemperature )
		{
			this.ambientTemperature = ambientTemperature;
			this.parameters = parameters;
			this.stopwatch = new Stopwatch();
			this.task = null;
			this.testHasStopped = false;
			this.timer = null;
		}
		
		public TimeSpan Simulate( ICEngine engine )
		{						
			engine.Start( ambientTemperature );
			stopwatch.Start();

			//task = Task.Run( () => HeatAndCoolEngine( engine ) );

			int timeStep = 1000;
			timer = new Timer( new TimerCallback( CheckEngine ), engine, 0, timeStep );
			
			while ( true )
			{
				HeatAndCoolEngine( engine );

				if ( testHasStopped )
				{
					return stopwatch.Elapsed;
				}
			}
		}



		void HeatEngine( IEngine engine, double temp )
		{
			engine.Heat( temp );
		}

		void CoolEngine( IEngine engine, double temp )
		{
			engine.Cool( temp );
		}

		void HeatAndCoolEngine( ICEngine engine )
		{
			var time1 = stopwatch.Elapsed.Seconds;
			double heatingTemp = 0;
			HeatEngine( engine, heatingTemp );

			var time2 = stopwatch.Elapsed.Seconds;
			double coolingTemp = time2 * parameters.CoolingSpeedOfTemperaturesCoeff * ( ambientTemperature - engine.Temperature );
			CoolEngine( engine, coolingTemp );
		}

		void CheckEngine( object obj )
		{
			Console.WriteLine( "Engine check ... " );

			var engine = ( ICEngine ) obj;
			if ( engine.Temperature >= parameters.OverheatTemperature )
			{
				testHasStopped = true;

				timer.Change( Timeout.Infinite, Timeout.Infinite );
			}
		}



		double GetTorque( double crankShaftSpeed )
		{
			var points = parameters.CrankshaftSpeedToTorquePoints;



			return 0;
		}
	}
}
