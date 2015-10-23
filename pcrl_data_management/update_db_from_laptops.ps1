<# update_db_from_laptops.ps1: 	
 # 		for each laptop, maps a local directory to that laptop's 
 # 		SimpleFeeder data directory (making some assumptions on that path)
 # 		then executes update_db.py on that directory
 #> 
 
<## for each station (assumes 24)
1..24 | foreach { 
	# build path to SimpleFeeder data directory 
	$comp_path = "\\MONKEYCOMPUTER",$_,"\Users\MComputer",$_,"\Desktop\SimpleFeeder\data" -join "" 
	net use X: $comp_path 										#map laptop directory to local drive
	python update_db.py $comp_path $_							#execute python script
} #>
# NOTE: the above section is commented out until actual laptop config is available for testing

# for now, test on data in F drive
0..1 | foreach { 
	$comp_path = 'F:\dist2\data\'
	python update_db.py $comp_path $_
} 



 