 SET ProjectDir=%1%
 SET ProjectName=%2%
 SET ConfigurationName=%3%
 rem SET OutDirPath="C:\ProjectsOutput\"

 echo %ProjectDir% 
 echo %ProjectName%
 echo %ConfigurationName%
 rem echo %OutDirPath%

 
 xcopy /f /y C:\ProjectsOutput\Debug\DataAccessLib.dll %ProjectDir%\References\