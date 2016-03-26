<# get_data_from_laptops.ps1: 	
 # 		for each laptop, maps a local directory to that laptop's 
 # 		SimpleFeeder data directory (makes assumptions on that path)
 # 		then executes save_to_db.py on that directory
 #> 
 
 # assuming there are 24 laptops
 <#1..24 | foreach { 
	$comp_path = "\\MONKEYCOMPUTER",$_,"\Users\MComputer",$_ -join ""
	net use X: $comp_path 
	python save_to_db.py $comp_path $_
} #>

$comp_path = 'F:\dist2\data\'
python save_to_db.py $comp_path 1



 