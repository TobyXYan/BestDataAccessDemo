SET ProjectDir=%1%
SET OutDir=%2%
SET TargetName=%3%
SET ConfigurationName=%4%
SET OutputDir=C:\ProjectsOutput\

echo %ProjectDir% 
echo %OutDir%
echo %TargetName%
echo %ConfigurationName%
echo %OutputDir%

xcopy /f /y  %OutDir%%TargetName%.dll	   %OutputDir%%ConfigurationName%
xcopy /f /y  %OutDir%%TargetName%.pdb	   %OutputDir%%ConfigurationName%