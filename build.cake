var target = Argument("Target", "Default");
var Configuration=Argument("Configuration", "Release");

Task("Restore")
    .Does(()=>{

    NuGetRestore("src/BitFrameworks.sln");

    });

Task("Default")
  .IsDependentOn("Restore")
  .Does(() =>
{
  Information("Hello World! Cake");
  DotNetBuild("src/BitFrameworks.sln",settings=>settings.SetConfiguration(Configuration)
                                                         .WithTarget("Build"));
});

RunTarget(target);