CREATE TABLE feeder (
	date_time text, 
	rxn_time real, 
	feeder int, 
	mid text, 
	CONSTRAINT unique_feeder UNIQUE(date_time, rxn_time, feeder, mid));

CREATE TABLE activity (
	date_time text, 
	activity big unsigned int, 
	mid text, 
	CONSTRAINT unique_activity UNIQUE(date_time, activity, mid));

CREATE TABLE scale (
	date_time text, 
	weight real, 
	mid text,
	CONSTRAINT unique_scale UNIQUE(date_time, weight, mid));
	
CREATE TABLE suppfeed (
	date_time text, 
	labmember text, 
	name text, 
	amount real, 
	mid text, 
	CONSTRAINT unique_suppfeed UNIQUE(date_time, labmember, name, amount, mid));

CREATE TABLE observation (
	date_time text, 
	labmember text, 
	labroom text, 
	behavior text, 
	stool text, 
	training text, 
	comments text, 
	equip text, 
	mid text, 
	CONSTRAINT unique_observation UNIQUE(date_time, labmember, labroom, behavior, stool, training, comments, equip, mid));
	
CREATE TABLE table_date_monk_yes_data (
	table_ text,
	date_ text,  
	mid text, 
	CONSTRAINT unique_tdm UNIQUE(table_, date_, mid));
	
CREATE TABLE cognitive (
	date_time text, 
	time unsigned big int, 
	event text, 
	mid text, 
	CONSTRAINT unique_cognitive UNIQUE(date_time, time, event, mid));

CREATE TABLE entertain (
	date_time text, 
	type int, 
	since real, 
	x int, 
	y int, 
	mid text, 
	CONSTRAINT unique_entertain UNIQUE(date_time, type, since, x, y, mid));
	
CREATE TABLE loginlog (
	datetime_in text, 
	datetime_out text, 
	labmember text, 
	CONSTRAINT unique_login UNIQUE(datetime_in, datetime_out, labmember));

CREATE TABLE labroomlog (
	datetime_in text, 
	datetime_out text,  
	labroom text, 
	labmember text,
	CONSTRAINT unique_labroomlog UNIQUE(datetime_in, datetime_out, labroom, labmember));



