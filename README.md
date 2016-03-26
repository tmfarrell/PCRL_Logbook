# PCRL Logbook Manual 
-------------------

### OUTLINE 
	A. Overview
	B. Directory Structure
	C. Software 
	D. Configuration
	E. Database 
	F. Working with the Database 
	G. Contact 



### A. OVERVIEW 
The PCRL Logbook is a data management application that supports/ automates several 
routine daily data entry and analysis tasks for this study. The intended purpose/ function of the system is three-fold: 
	
	(1)	digital entry and storage of observational data;
	(2)	efficient structured storage of project data in a central database (db); and 
	(3)	daily automated data analysis and reporting.  

Of these, (2) was the original/ most important motivator. 

First, (2) allows for efficient storage of data, as db's have much smaller memory 
footprints than flat files. Consider the current memory usage 
of `'DataArchive2/'`, which holds this project's flat files. As of today
(2/2/2016) it uses `288 GB`. Assuming roughly 1/3 of that is used for the data files 
(it contains other items as well), that's about `83 GB`. Compare that to
`'PCRL_2016_data_0.db'`, which contains the exact same data: `0.65 GB`. That's 125 times as small! 
And this difference is not trivial, given previous memory issues. 

Second, and most important, having a single file contain all the data tremendously 
simplifies analysis: no need to waste time or introduce error wrangling 
up all the data; it's already in one place. Further, Structured Query 
Language ([SQL](https://www.khanacademy.org/computing/computer-programming/sql)) is a powerful read/writeable interface to the data, and adds to the analysis. (See E and F for details.)     

In sum, the main purpose is to facilitate a centralized,  structured data store (in this case, an [SQLite](https://www.sqlite.org/cli.html) db) for the study, as this adds significant value to the ultimate goal of proper analysis. And as it comes closer to that, this value will become increasingly appreciated. 



### B. DIRECTORY STRUCTURE
Note: ignore (and do not modify) files/ directories not listed here. 

	C:\Users\Server1\Desktop\PCRL_Logbook\
	# base directory	
		
		README.md
		# the markdown version

		README.html
		# the HTML version (*use this one*)
		
		config.json
		# configuration file (see D for details)
		
		scripts\
		# contains all scripts, mostly python(.py) and powershell(.ps1)
		# as well as some apps for dealing with the database 
			
			update_db.py 
			# parses flat files and transfers data to db
			
			GenerateDailyReports.py
			# generates reports from db
			
			update_db_n_report.ps1
			# calls 'update_db.py' and 'GenerateDailyReports.py' 
			# executed by Task Manager at 9am each morning 
			
			SqliteBrower
			# this is a user-friendly way to work with the db
			# double-click and open the db using 'Open Database' button

			access_db.ps1
			# right-click this file and select "Run with Powershell" to
			# open the db in an SQLite terminal
			
			sqlite3.exe
			# double-click to open an SQLite terminal
			
			init_db.py
			# creates an empty database with project's schema
			# (see E for details on schema)
			
			Configure.py
			# resets config file
			# for use when config file has been ill-formatted, 
			# or permanently deleted/ disfigured 
			  
			base_config.json
			# basic config file used by 'Configure.py' to 
			# reset the config file in the base dir
			  
			update_db_from_laptops.ps1
			# old powershell script for transferring raw data
			# across the network from laptops to db 
		
		PCRLLogbook[final]\
		# main directory of PCRL Logbook C# application 
		# (see C for more details)
		
		monk_data\
		# excel and text files with some monkey/diet data
		
		libs\
		# a few python modules, dependencies of report script
		
		
		
### C. SOFTWARE
The system has three components, whose functions correspond to those outlined in A. 

1. ####Observational Data Recording

		Directory: "PCRL_Logbook\PCRLLogbook[final]\PCRLLogbook[final]\" 	
		File: 	   "PCRL_Logbook\PCRLLogbook[final]\PCRLLogbook[final]\bin\Release\PCRLLogbook.exe"
		# Note: a short-cut to the exe exists at "C:\Users\Server1\Desktop\PCRLLogbook.exe-Shortcut"

	PCRL_Logbook.exe is a Windows application written in C#. It allows observational data to be recorded digitally, via data logging forms with simple interfaces. The main intent is to allow a lab member outside the control room to record data through TeamViewer.   
	
	For a more detailed description, see its dedicated `README` at
	`"C:\Users\Server1\Desktop\PCRL_Logbook\PCRLLogbook[final]\README.html"`



2. ####Importing Data in DB   
	
		File: "PCRL_Logbook\scripts\update_db.py"

	[Description here.]
  

		
3. ####Report Generation

		File: "PCRL_Logbook\scripts\GenerateDailyReports.py"

	[Description here.]

	
  
### D. CONFIGURATION
1. ####Overview  
The config file is a JSON (JavaScript Object Notation) file, which is a popular file format for storing software configurations, and resource description framework (RDF) data more generally. Basically, it consists of a list of `"<field>": <value>` pairs, where each field is a string (i.e. has quotes around it) and each value is one of various data types (strings, arrays/ lists, dictionaries, etc). For more information, see [www.json.org](www.json.org). 



2. ####Notes on Editing  
The main idea is: **leave all formatting as is, only change value**. For instance, say you want to change where reports are saved `report_dir` from `"E:\\Dropbox\\PCRL Reports\\"` to `"E:\\OtherDir\\PCRL_Reports"`. Only change what is inside the quotes, and make sure to keep the trailing comma as this separates `"<field>":<value>, "<field>":<value>` pairs. The change would look like: 

		# from
		"report_dir": "E:\\Dropbox\\PCRL_Reports",
		# to
		"report_dir": "E:\\Some_Other_Dir\\New_PCRL_Report_Dir",

	Another example, say you want to change how many days are included in the daily reports `days` from `10` to `5`. Here there are no quotes around the value (because it's numeric not string type); so to keep the format the same, just replace the plain number. 
		
		# from
		"days": 10,
		# to
		"days": 5,
	
	Finally, if the value is a list (i.e. enclosed by brackets with multiple subvalues), preserve the structure and only change the subvalues inside. 
	
		# from 
		"behavior_list": [
			"BAR", 
			"pacing", 
			"self-injury"
		],
		# to 
		"behavior_list": [
			"bad"
		],

	And so on.

	If it happens that the config file gets ill-formatted or deleted, run 

		C:\Python27\python.exe C:\Users\Server1\Desktop\PCRL_Logbook\scripts\Configure.py
	
	in Powershell. This will generate a new config file in the base directory. Only do this as a last resort as this will result in the loss of all changes to the config file.  


3. ####Fields 

		"days":  
		- How many days from yesterday do you want included in the daily reports? 
		- Any positive integer that makes sense (e.g. 5, 7, 14). 
		
		"data_dir": 
		- In which directory are the raw/ flat data files (e.g. .FeedTest.txt, etc) stored? 
		- Any valid path within Server1 or on the network (e.g. "Z:\\").
		
		"db_file":
		- What is the path (i.e. exact directory location) of the database file? 
		- Any valid path within Server1 or network (e.g. "F:\\pcrl_logbook_data\\PCRL_2016_data_0.db"). 
		
		"report_dir":
		- In which directory do you want the reports saved? 
		- Any valid path within Server1 or network. 
		
		"report_custom":
		- Do you want the report program to generate reports between custom dates, instead of using 
		  the "days" field? 
		- Either true or false, where false means that you want to use the "days" field. 
		
		"custom_dates": 
		- If "report_custom" is true, over what span of dates ("from"-"to") do you want the report 
		  to analyze?
		- Strings of the form "YYYY-MM-DD" (e.g. "2016-01-05"), where the "from" date is earlier 
		  than the "to" date. 
		
		"feeding_hour_threshold":
		- How many pellets does a monkey need to eat in an hour for that hour to be labelled 
		  "feeding hour"? 
		- Any positive integer that makes sense (e.g. 30, 50) 
		
		"behavior_list":
		- What behaviors do you want listed in the behavior field of the observational data recording 
		  form (in PCLRLogbook.exe)? 
		- Any valid list of strings relating behaviors (e.g. ['BAR', 'pacing'], etc)
		
		"training_list": 
		- Same as "behavior_list", except relating training. 
		
		"stool_list": 
		- Same as "behavior_list", except relating stool.
		
		"equipment_list": 
		- What equipment do you want listed so you can record malfunctions/ deficiencies? 
		- Otherwise, same as "behavior_list" except relating equipment.  
		
		"feed_data": 
		- What foods do you want to be able to include in the "supp_feed_templates" and the 
		  "feed_in_feeders" fields? 
		- Dictionaries of the form, where the field describes what the value should quantify: 
			"<some food>": {
				"category": "<some food category (e.g. "fruit")>",
				"water_percent": "<some fraction of 1 (e.g. "0.77")>",
				"prot_cals_per_gram": "<some integer or float val (e.g. "0.064")>",
				"fiber_percent": "<some fraction of 1 (e.g. "0.03")>",
				"fat_cals_per_gram": "<some integer or float val (e.g. "2.05")>",
				"carb_cals_per_gram": "<some integer or float val (e.g. "1")>" 
			}
			Note: note the quotations around the numerics; these happen to be string datatypes, 
			but their values need be numeric, else the report generator will throw an error. 
			
		"feed_in_feeders": 
		- Which foods from "feed_data" are being dispensed from each feeder? 
		- Must be of the form: 
			"feeder0": "<some food in "feed_data">", 
			"feeder1": "<some food in "feed_data">",
			"feeder2": "<some food in "feed_data">"
		
			
		"supp_feed_templates": 
		- What custom templates do you want to be able to choose from when recording supplemental feeds 
		  in PCRLLogbook? 
		- Must be of the form, where the numerical values indicate grams of that food: 
			"<some name for the template>": { 
				"<some food in "feed_data">: <some float (e.g 1.3)>, 
				"<some other food in "feed_data">": <some float (e.g. 2.6)>
				...
			}
			Note: Every template must have at least one food.  
			
			
		"monkey_data": 
		- Basic information for each monkey? (Note: this should only need to be altered when new monkeys 
		  are added to the study.) 
		- Dictionaries of the form: 
		    "<monkey id>": {
		      "dob": "<MM/DD/YYYY>",
		      "station": "<station #>",
		      "room": "<room # (e.g. "W718")>",
		      "sex": "<either "M" or "F">"
		    }


### E. DATABASE
1. ####Working with the db
`SqliteBrowser` is a user-friendly interfaceß for `SQLite` dbs. It allows for easy data browsing, query execution and basic plotting. To view the project db:    

	+ Open `SqliteBrowser` in `"C:\Users\Server1\Desktop\PCRL_Logbook\scripts\"`. 
	+ Select `Open Database` and browse to `"F:\pcrl_logbook_data\PCRL_2016_data_0.db"`.

	The primary tabs of interest are `Browse Data` and `Execute SQL`. Also see the secondary tabs at bottom right that are useful `SQL Log` and `Plots`. 	

2. ####Programmatically querying the db
Although `SqliteBrowser` is very convenient and a nice interface to test queries with, as the db gets bigger it will be prohibitively harder to work with. On the other hand, `SQLite`'s command-line interface is fast and useful for automating data export to other formats (such as `csv` aka `xlxs`). For example, to export feeding data for monkey `JB67` on `2016-03-15` to `csv`, open `PCRL_Logbook\scripts\sqlite3.exe` and do:  

		SQLite version 3.8.10.2 2015-05-20 18:17:19
		Enter ".help" for usage hints.
		Connected to a transient in-memory database. 
		Use ".open FILENAME" to reopen a persistent database. 
		sqlite> .open F:\\pcrl_logbook_data\\PCRL_2016_data_0.db
		sqlite> .tables
		activity				labroomlog					suppfeed
		cognitive				loginlog					table_data_monk_yes_data
		entertain				observation 
		feeder					scale						
		sqlite> .mode csv
		sqlite> .headers on
		sqlite> .output E:\JB67_20160315_feed.csv
		sqlite> SELECT * FROM feeder WHERE mid="JB67" AND date_time LIKE "%2016-03-16%"; 
		sqlite> .quit
		
	For a detailed tutorial on `SQLite`, see `PCRL_Logbook\libs\SQLite.pdf`. 

	`Python` also has a very easy to work with interface for `SQLite`. Here's the same example in `Python`. Open `Windows Powershell` and do: 
	
		C:\Users\Server1> C:\Python27\python.exe
		Python 2.7.8 (default, Jun 30 2014, 16:03:49) [MSC v.1500 32 bit (Intel)] on win32
		Type "help", "copyright", "credits" or "license" for more information. 
		>>> import sqlite3
		>>> conn = sqlite3.connect('F:\\pcrl_logbook_data\PCRL_2016_data_0.db')
		>>> c = conn.cursor()
		>>> query = c.execute('SELECT * FROM feeder WHERE mid="JB67" AND date_time LIKE "%2016-03-16%"')
		>>> result = query.fetchall()
		>>> print result

	See documentation [here](https://docs.python.org/2/library/sqlite3.html).  


### G. CONTACT 
With any errors/ bugs/ questions, feel free to contact <tfarrell01@gmail.com>. 

 
