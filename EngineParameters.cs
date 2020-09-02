using System;
using System.Collections.Generic;
using System.Linq;

using ForwardTask.Data;



namespace ForwardTask
{
	public class EngineParameters
	{
		public int MomentOfInertia;
		public List< Point > CrankshaftSpeedToTorquePoints;
		public int OverheatTemperature;
		public double HeatingSpeedOfTorqueCoeff;
		public double HeatingSpeedOfCrankshaftSpeedCoeff;
		public double CoolingSpeedOfTemperaturesCoeff;
	}
}
