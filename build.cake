// ------ BEGIN NUGET DEPENDENCIES
#tool nuget:?package=7-Zip.CommandLine&version=18.1.0

// ------ BEGIN BUILD CONFIGURATION
var platform = "x86";
var configuration = "Release";
var buildSettings = new MSBuildSettings
{
	Verbosity = Verbosity.Minimal,
	ToolVersion = MSBuildToolVersion.VS2019,
	Configuration = configuration,
	PlatformTarget = PlatformTarget.x86
};

Setup(context => {
    context.Tools.RegisterFile("./tools/7-Zip.CommandLine.18.1.0/tools/7za.exe");
});

// ----- BEGIN BUILD SEQUENCE

// Prepare output folders
Task("Prepare")
.Does(()=>
{
	Information("Cleaning /build folder...");
	CleanDirectories("./build");
	CreateDirectory("./build");
	CreateDirectory("./build/ide");
	CreateDirectory("./build/installer");
	CreateDirectory("./build/staging");
});

// Restore NuGet packages - solution wide
Task("RestoreNuGet")
.IsDependentOn("Prepare")
.Does(() => 
{
	Information("Restoring Nuget dependecies...");
	NuGetRestore("./KFlearning.sln");
});

// Build solution
Task("Build")
.IsDependentOn("RestoreNuGet")
.Does(() =>
{
	Information("Builidng solution...");
	MSBuild("./KFlearning.sln", buildSettings);
});

// Copy build outputs
Task("CopyBuild")
.IsDependentOn("Build")
.Does(() =>
{
	Information("Copying build output...");

	CopyDirectory($"./src/KFlearning.IDE/bin/{platform}/{configuration}", "./build/ide");
	CopyDirectory($"./src/KFlearning.Installer/bin/{platform}/{configuration}", "./build/installer");
});

// Build package archives
Task("BuildArchives")
.IsDependentOn("CopyBuild")
.Does(() =>
{
	FilePath sevenZipPath = Context.Tools.Resolve("7za.exe");
	Debug(sevenZipPath);

    Information("Building KFlearning IDE archive...");
	var exitCode = StartProcess(sevenZipPath, @"a .\build\installer\data\KFlearning-IDE-latest.zip .\build\ide\*");
	Information("7z exit code: {0}", exitCode);

	Information("Integrating dependencies to Installer...");
	CopyDirectory("./KFlearning-bin", "./build/installer/data");

	Information("Building KFlearning Installer archive...");
	exitCode = StartProcess(sevenZipPath, @"a .\build\staging\KFlearning-Installer-latest.7z .\build\installer\*");
	Information("7z exit code: {0}", exitCode);
});

// Build self-extracting archive
Task("BuildSfx")
.IsDependentOn("BuildArchives")
.Does(() =>
{
	Information("Copying configuration...");
	CopyFile("./config.txt", "./build/staging/config.txt");

	Information("Copying SFX binary...");
	CopyFile("./7zSD.sfx", "./build/staging/7zSD.sfx");

	Information("Builidng SFX archive...");
	var settings = new ProcessSettings
	{ 
		Arguments = "/c copy /b 7zSD.sfx + config.txt + KFlearning-Installer-latest.7z KFlearning-installer.exe",
		WorkingDirectory = Directory("./build/staging")
	};
	var exitCode = StartProcess("cmd.exe", settings);
	Information("COPY exit code: {0}", exitCode);

	Information("Moving installer...");
	MoveFile("./build/staging/KFlearning-installer.exe", "./build/KFlearning-installer.exe");
});

RunTarget("BuildSfx");