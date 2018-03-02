### Defter

### Sample server deployment
msbuild /p:DeployOnBuild=true;DeployTarget=PipelinePreDeployCopyAllFilesToOneFolder;PackageTempRootDir="C:\Temp\DeployTemp";AutoParameterizationWebConfigConnectionStrings=false
msdeploy -verb:sync -source:contentPath=C:\temp\DeployTemp\PackageTmp\ -dest:package=C:\temp\DeployTemp\WcfServer.zip -declareParam:name="IIS WebApplication Name",defaultValue="WcfServer"
msdeploy -verb:sync -source:package=C:\temp\DeployTemp\WcfServer.zip -dest:contentPath=WcfServer