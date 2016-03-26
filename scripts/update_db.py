#!/usr/local/bin/python
'''
update_db.py: 	extracts activity, feeder, cognitive and scale (plus entertain
				when available) data from files generated by SimpleFeeder
				program and saves to the db

				each data type (scale, feeder and cognitive) is represented by a
				single table in the db with relations corresponding directly to the
				fields of each file type
'''
import re
import os
import sys
import csv
import time
import json
import sqlite3
import datetime

##
## MAIN
##
base_dir = "C:\Users\Server1\Desktop\PCRL_Logbook\\"

# get configuration options
opts = {}
config_f = open(base_dir + 'config.json', 'r')
opts = json.load(config_f)
config_f.close()

# get configurations
archive_dir = opts["data_dir"]
MONKEYS = opts['monkey_data']
STATIONS = [MONKEYS[m]['station'] for m in MONKEYS]

DATES = []
if opts['report_custom']:
	from_ = opts['custom_dates']['from']
	to_ = opts['custom_dates']['to']
	days = (datetime.datetime.strptime(to_, '%Y-%m-%d') - datetime.datetime.strptime(from_, '%Y-%m-%d')).days
	DATES = [(datetime.datetime.strptime(to_, '%Y-%m-%d') - datetime.timedelta(days=d)) \
			  for d in list(reversed(range(days + 1)))]
else:
	days = int(opts['days'])
	DATES = [(datetime.datetime.today() - datetime.timedelta(days=1)) - \
		  datetime.timedelta(days=d) for d in list(reversed(range(days + 1)))]
	

# connect to db
conn = sqlite3.connect(opts['db_file'])
c = conn.cursor()			# and declare cursor (obj that interacts directly with db)

# filter out (table,date,monk) tuples for which there is already data in the db 
# so to not insert data from previous updates
tables = ['activity', 'cognitive', 'scale', 'feeder']

table_date_monks = [(t, d, m) for t in tables \
				for d in map(lambda d: d.strftime('%Y-%m-%d'), DATES) \
				for m in MONKEYS] 

table_date_monk_yes_data = c.execute("SELECT * FROM table_date_monk_yes_data").fetchall()
					
table_date_monk_no_data = [t for t in table_date_monks \
				if not t in table_date_monk_yes_data]
							
dates_no_data = set([datetime.datetime.strptime(t[1], '%Y-%m-%d') \
							for t in table_date_monk_no_data])

for date in dates_no_data: 

	d_str = date.strftime('%Y-%m-%d')
	date_str = date.strftime('%b %d, %Y')
	date_activity_str = '{d.year}-{d.month}-{d.day:02}'.format(d=date)
	date_scale_str = date.strftime("%B_%d_%Y")
	
	#monks_that_need_data_for_date = [t[2] for t in date_table_monk_no_data if date == t[1]]]
	#stations_for_those_monks = [MONKEYS[m]['station'] for m in monks_that_need_data_for_date]
	
	# get activity, feeding, cognitive and scale data for each station (monkey)
	# and commit data changes for each one of those stations
	for monkey, station in zip(MONKEYS, STATIONS):

		# find data directories
		activity_dir, feeder_dir, scale_dir = '','','' 
		data_dirs = [archive_dir+d+'\\' for d in os.listdir(archive_dir) if station in d]
		try: 	activity_dir = [d for d in data_dirs if 'Activity' in d][0]
		except: pass 
		try: 	feeder_dir = [d for d in data_dirs if 'Feed' in d][0]
		except: pass
		try: 	scale_dir = [d for d in data_dirs if 'Scale' in d][0]
		except: pass 
					
		
		## ACTIVITY
		if ('activity', d_str, monkey) in table_date_monk_no_data and activity_dir: 
			
			activity_files = [activity_dir+d+'\\'+f for d in os.listdir(activity_dir) for f in os.listdir(activity_dir+d+'\\')\
							if d_str in d if 'cell' in f]	
							
			activity_values = []
			for activity_file in activity_files: 
				f = open(activity_file, 'r') 													#open file
				activity_lines = map( lambda line: line.replace('\n', ''), f.readlines()[7:] )	#skip first 8 lines; and clean
				f.close()

				date_time = date
				fifteen_secs = datetime.timedelta(seconds=15)
				for line in activity_lines:
					date_time = date_time + fifteen_secs
					act = 0 
					try: 
						act = int(line)
						activity_values.append((str(date_time), int(line), monkey))
					except ValueError: 
						pass

			if activity_values: 
				try: 
					c.executemany("INSERT INTO activity (date_time, activity, mid) VALUES (?,?,?)", activity_values)
					c.execute("INSERT INTO table_date_monk_yes_data (date_, table_, mid) VALUES ('"+d_str+"', 'activity', '"+monkey+"')")
				except sqlite3.IntegrityError:
					print "The activity data provided is duplicate. Skipping..."

		## COGNITIVE
		if ('cognitive', d_str, monkey) in table_date_monk_no_data and feeder_dir: 
			
			cognitive_files = [feeder_dir+f for f in os.listdir(feeder_dir) if date_str in f if 'DMS' in f]
			
			cognitive_values = []
			for cognitive_file in cognitive_files: 
				f = open(cognitive_file, 'r')
				cognitive_lines = map( lambda line: line.replace('\n', ''), f.readlines())
				f.close()

				for line in cognitive_lines:
					fields = line.split('@')
					date_time = str(datetime.datetime.strptime(fields[0][:fields[0].find('M') + 1], '%b %d, %Y %I:%M:%S %p'))
					time = 0
					try:
						time = int(fields[1])
					except ValueError:
						pass
					cognitive_values.append((date_time, time, fields[2], monkey))
			
			if cognitive_values: 
				try: 
					c.executemany("INSERT INTO cognitive (date_time, time, event, mid) VALUES (?,?,?,?)", cognitive_values)
					c.execute("INSERT INTO table_date_monk_yes_data (date_, table_, mid) VALUES ('"+d_str+"', 'cognitive', '"+monkey+"')")
				except sqlite3.IntegrityError: 
					print "The cognitive data provided is duplicate. Skipping..."
	
		## FEEDER 
		if ('feeder', d_str, monkey) in table_date_monk_no_data and feeder_dir: 
			
			feeder_files = [feeder_dir+f for f in os.listdir(feeder_dir) if date_str in f if 'Feed' in f]
			
			feeder_values = [] 
			for feeder_file in feeder_files: 
				f = open(feeder_file, 'r')
				feeder_lines = map( lambda line: line.replace('\n', ''), f.readlines())
				f.close()

				for line in feeder_lines:
					fields = line.split('@')
					#print fields
					try: 
						date_time = str(datetime.datetime.strptime(fields[0], '%b %d, %Y %I:%M:%S %p '))
						feeder_values.append((date_time, float(fields[1]), int(fields[3]), monkey))
					except: 
						pass
			
			if feeder_values: 
				try: 
					c.executemany("INSERT INTO feeder (date_time, rxn_time, feeder, mid) VALUES (?,?,?,?)", feeder_values)
					c.execute("INSERT INTO table_date_monk_yes_data (date_, table_, mid) VALUES ('"+d_str+"', 'feeder', '"+monkey+"')")
				except sqlite3.IntegrityError: 
					print "The feeder data provided is duplicate. Skipping..."
		
		## SCALE 
		if ('scale', d_str, monkey) in table_date_monk_no_data and scale_dir: 
			
			scale_files = [scale_dir+f for f in os.listdir(scale_dir) if date_scale_str in f]
		
			scale_values = []
			for scale_file in scale_files: 
				f = open(scale_file, 'r')
				scale_lines = map( lambda line: line.replace('\n', ''), f.readlines())
				f.close()

				for line in scale_lines:
					fields = re.split(': |, ', line)
					try:
						weight = float(fields[2])
						if weight > 0 and weight < 200:  
							date_time = str(datetime.datetime.strptime(date_scale_str + \
										' ' + fields[0].split('\0')[-1], '%B_%d_%Y %H:%M'))
							scale_values.append((date_time, float(fields[2]), monkey))
					except ValueError:
						pass
					except: 
						pass 
			
			if scale_values: 
				try: 
					c.executemany("INSERT INTO scale (date_time, weight, mid) VALUES (?,?,?)", scale_values)
					c.execute("INSERT INTO table_date_monk_yes_data (date_, table_, mid) VALUES ('"+d_str+"', 'scale', '"+monkey+"')")
				except sqlite3.IntegrityError: 
					print "The scale data provided is duplicate. Skipping..."
conn.commit()
conn.close()