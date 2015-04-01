param($cmd = 'E:\Coding\PicTureen\tools\ResharperTools\inspectcode.exe',
      $output = 'E:\result.xml',
      $repoFolder = 'E:\Coding\PicTureen\')
$inspectOption = '/o='+$output
&$repofolder'\src\Pictureen\.nuget\nuget.exe' 'restore' $repofolder'src\PicTureen\PicTureen.sln' 
&$cmd $repofolder'src\PicTureen\PicTureen.sln' $inspectOption

[xml]$result = Get-Content $output
$issueTypes = @{}
foreach ($issuetype in $result.Report.IssueTypes.IssueType) 
{
    if ($issuetype.Severity -like 'WARNING' -or $issuetype.Severity -like 'ERROR')
    {
        $issueTypes.Add($issuetype.Id, $issuetype.Severity.substring(0,1).toupper()+$issuetype.Severity.substring(1).tolower())
    }
}

foreach ($project in $result.Report.Issues) 
{
    foreach ($issue in $project.Project.Issue) 
    {
        if ($issueTypes.ContainsKey($issue.TypeId))
        {
            Add-AppveyorCompilationMessage -Message $issue.Message -Category $issueTypes.Get_Item($issue.TypeId) -Filename $issue.File -ProjectName $project.Name
        }
    }
}