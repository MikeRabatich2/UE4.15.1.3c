﻿// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.Collections.Generic;

[SupportedPlatforms(UnrealPlatformClass.Server)]
public class TestPALTarget : TargetRules
{
	public TestPALTarget(TargetInfo Target)
	{
		Type = TargetType.Program;
		LinkType = TargetLinkType.Monolithic;
	}

	//
	// TargetRules interface.
	//

	public override void SetupBinaries(
		TargetInfo Target,
		ref List<UEBuildBinaryConfiguration> OutBuildBinaryConfigurations,
		ref List<string> OutExtraModuleNames
		)
	{
		OutBuildBinaryConfigurations.Add(
			new UEBuildBinaryConfiguration(	InType: UEBuildBinaryType.Executable,
											InModuleNames: new List<string>() { "TestPAL" } )
			);
	}

	public override void SetupGlobalEnvironment(
		TargetInfo Target,
		ref LinkEnvironmentConfiguration OutLinkEnvironmentConfiguration,
		ref CPPEnvironmentConfiguration OutCPPEnvironmentConfiguration
		)
	{
		// Lean and mean
		UEBuildConfiguration.bCompileLeanAndMeanUE = true;

		// No editor or editor-only data is needed
		UEBuildConfiguration.bBuildEditor = false;
		//UEBuildConfiguration.bBuildWithEditorOnlyData = false;

		// Compile out references from Core to the rest of the engine
		UEBuildConfiguration.bCompileAgainstEngine = false;	// compiling without engine is broken (overridden functions do not override base class)
		UEBuildConfiguration.bCompileAgainstCoreUObject = false;

		// Make a console application under Windows, so entry point is main() everywhere
		OutLinkEnvironmentConfiguration.bIsBuildingConsoleApplication = true;
	}
}
