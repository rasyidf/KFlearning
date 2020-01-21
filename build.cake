// ------- ARGUMENTS
var target = Argument("target", "Build");
var platform = Argument("platform", "x86");
var configuration = Argument("configuration", "Release");

// ------ BUILD CONFIGURATION
var buildSettings = new MSBuildSettings
{
	Verbosity = Verbosity.Minimal,
	ToolVersion = MSBuildToolVersion.VS2019,
	Configuration = configuration,
	PlatformTarget = platform == "x86" ? PlatformTarget.x86 : PlatformTarget.x64
};

// ----- BUILD SEQUENCE

// Build only
Task("Build")
.Does(()=>
{
	Information("Cleaning /build folder...");
	CleanDirectories("./build");
	CreateDirectory("./build");
	CreateDirectory("./build/staging");

	Information("Restoring Nuget dependecies...");
	NuGetRestore("./KFlearning.sln");

	Information("Builidng solution...");
	MSBuild("./KFlearning.sln", buildSettings);

	Information("Copying build output...");
	CopyDirectory($"./src/KFlearning/bin/{platform}/{configuration}", "./build/staging");
});

// Create MSI installer
Task("Publish")
.IsDependentOn("Build")
.Does(()=>
{
    Information("Builidng setup installer...");
	MSBuild("./src/KFlearning.Setup/KFlearning.Setup.wixproj", buildSettings);

	Information("Moving setup installer...");
	MoveFile("./src/KFlearning.Setup/bin/Release/KFlearning.Setup.msi", "./build/KFlearning.Setup.msi");
});

RunTarget(target);