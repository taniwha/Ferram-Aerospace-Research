﻿/*
Ferram Aerospace Research v0.15.4 "Glauert"
=========================
Aerodynamics model for Kerbal Space Program

Copyright 2015, Michael Ferrara, aka Ferram4

   This file is part of Ferram Aerospace Research.

   Ferram Aerospace Research is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   Ferram Aerospace Research is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with Ferram Aerospace Research.  If not, see <http://www.gnu.org/licenses/>.

   Serious thanks:		a.g., for tons of bugfixes and code-refactorings   
				stupid_chris, for the RealChuteLite implementation
            			Taverius, for correcting a ton of incorrect values  
				Tetryds, for finding lots of bugs and issues and not letting me get away with them, and work on example crafts
            			sarbian, for refactoring code for working with MechJeb, and the Module Manager updates  
            			ialdabaoth (who is awesome), who originally created Module Manager  
                        	Regex, for adding RPM support  
				DaMichel, for some ferramGraph updates and some control surface-related features  
            			Duxwing, for copy editing the readme  
   
   CompatibilityChecker by Majiir, BSD 2-clause http://opensource.org/licenses/BSD-2-Clause

   Part.cfg changes powered by sarbian & ialdabaoth's ModuleManager plugin; used with permission  
	http://forum.kerbalspaceprogram.com/threads/55219

   ModularFLightIntegrator by Sarbian, Starwaster and Ferram4, MIT: http://opensource.org/licenses/MIT
	http://forum.kerbalspaceprogram.com/threads/118088

   Toolbar integration powered by blizzy78's Toolbar plugin; used with permission  
	http://forum.kerbalspaceprogram.com/threads/60863
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FerramAerospaceResearch.FARAeroComponents;

namespace FerramAerospaceResearch.FARGUI.FAREditorGUI.Simulation
{
    class EditorSimManager
    {
        InstantConditionSim _instantCondition;

        StabilityDerivCalculator _stabDerivCalculator;
        public StabilityDerivCalculator StabDerivCalculator
        {
            get { return _stabDerivCalculator; }
        }

        StabilityDerivLinearSim _stabDerivLinearSim;
        public StabilityDerivLinearSim StabDerivLinearSim
        {
            get { return _stabDerivLinearSim; }
        }

        SweepSim _sweepSim;
        public SweepSim SweepSim
        {
            get { return _sweepSim; }
        }
        EditorAeroCenter _aeroCenter;

        public StabilityDerivOutput vehicleData;

        public EditorSimManager()
        {
            _instantCondition = new InstantConditionSim();
            _stabDerivCalculator = new StabilityDerivCalculator(_instantCondition);
            _stabDerivLinearSim = new StabilityDerivLinearSim(_instantCondition);
            _sweepSim = new SweepSim(_instantCondition);
            _aeroCenter = new EditorAeroCenter();
            vehicleData = new StabilityDerivOutput();
        }

        public EditorSimManager(InstantConditionSim _instantSim)
        {
            _instantCondition = _instantSim;
            _stabDerivCalculator = new StabilityDerivCalculator(_instantCondition);
            _stabDerivLinearSim = new StabilityDerivLinearSim(_instantCondition);
            _sweepSim = new SweepSim(_instantCondition);
            _aeroCenter = new EditorAeroCenter();
            vehicleData = new StabilityDerivOutput();
        }

        public void UpdateAeroData(VehicleAerodynamics vehicleAero, List<ferram4.FARWingAerodynamicModel> wingAerodynamicModel)
        {
             List<FARAeroPartModule> aeroModules;
             List<FARAeroSection> aeroSections;
             vehicleAero.GetNewAeroData(out aeroModules, out aeroSections);
             _instantCondition.UpdateAeroData(aeroModules, aeroSections, vehicleAero, wingAerodynamicModel);
             _aeroCenter.UpdateAeroData(aeroModules, aeroSections);
        }
    }
}
