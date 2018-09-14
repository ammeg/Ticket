[T4Scaffolding.ViewScaffolder("Razor", Description = "Adds an ASP.NET MVC view using the Razor view engine", IsRazorType = $true, LayoutPageFilter = "*.cshtml,*.vbhtml|*.cshtml,*.vbhtml")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true, Position = 0)][string]$Controller,
	[string]$ModelType,
	[string]$Area,
	[alias("MasterPage")]$Layout,	# If not set, we'll use the default layout
 	[string[]]$SectionNames,
	[string]$PrimarySectionName,
	[switch]$ReferenceScriptLibraries = $false,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)
# Ensure we have a controller name, plus a model type if specified
if ($ModelType) {
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
	$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
}

# Decide where to put the output
$outputFolderName = Join-Path Views $Controller
if ($Area) {
	# We don't create areas here, so just ensure that if you specify one, it already exists
	$areaPath = Join-Path Areas $Area
	if (-not (Get-ProjectItem $areaPath -Project $Project)) {
		Write-Error "Cannot find area '$Area'. Make sure it exists already."
		return
	}
	$outputFolderName = Join-Path $areaPath $outputFolderName
}

#if ($foundModelType) { $relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) }
if (!$relatedEntities) { $relatedEntities = @() }


$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$areaNamespace = if ($Area) { [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + ".Areas.$Area") } else { $defaultNamespace }
$viewModelsNamespace = [T4Scaffolding.Namespaces]::Normalize($areaNamespace + ".ViewModels")

# Render the T4 template, adding the output to the Visual Studio project
@("Indice", "_Pesquisa", "_ParametrosPesquisa", "_CriarOuEditar", "Criar", "Editar", "Detalhes", "Excluir", "_Scripts", "_Detalhes") | %{
	$outputPath = Join-Path $outputFolderName $_
	Add-ProjectItemViaTemplate $outputPath -Template $_ -Model @{
		IsContentPage = [bool]$Layout;
		Layout = $Layout;
		SectionNames = $SectionNames;
		PrimarySectionName = $PrimarySectionName;
		ReferenceScriptLibraries = $ReferenceScriptLibraries.ToBool();
		ViewName = $_;
		PrimaryKeyName = $primaryKeyName;
		ViewDataType = [MarshalByRefObject]$foundModelType;
		ViewDataTypeName = $foundModelType.Name;
		RelatedEntities = $relatedEntities;
		ModelType = [MarshalByRefObject]$foundModelType;
		DefaultNamespace = $defaultNamespace;
		ViewModelsNamespace = $viewModelsNamespace;
		ControllerName = $Controller;
		Area = $Area;
	} -SuccessMessage "Added $_ view at '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
}

#@("Indice", "_Pesquisa", "_ParametrosPesquisa", "_CriarOuEditar", "Criar", "Editar", "Detalhes", "Excluir", "_Scripts") | %{
#	Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
#}