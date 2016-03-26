#!/usr/local/bin/python

'''
sim_data.py:    gets 100 rows from each table in PCRL_phizer_study.db and
                replicates that data for each monkey and for the past ten days;
                then inserts that replicated data into the db

Tim Farrell, tmf@bu.edu
PCRL, BU
20150829
'''

import random
import sqlite3
import datetime

base_dir = "C:\\Users\\Server1\\Desktop\\PCRL_Logbook\\pcrl_data_management\\"

opts = {}
config_f = open(base_dir + 'config.json', 'r')
opts = json.load(config_f)
config_f.close()

MONKEYS = opts['monkey_data']
STATIONS = [m['station'] for m in MONKEYS.values()]
DB_PATH = base_dir + 'PCRL_2015-16_study_test.db'

# Import data based on configuration
DATES = []; YESTERDAY_STR = '';
if opts['report_custom']:
	from_ = opts['custom_dates']['from']
	to_ = opts['custom_dates']['to']
	days = (datetime.datetime.strptime(to_, '%Y-%m-%d') - datetime.datetime.strptime(from_, '%Y-%m-%d')).days
	DATES = [(datetime.datetime.strptime(to_, '%Y-%m-%d') - datetime.timedelta(days=d)) \
			  for d in list(reversed(range(days + 1)))]
	YESTERDAY_STR = DATES[-1].strftime('%Y%m%d')
else:
	days = int(opts['days'])
	DATES = [(datetime.datetime.today() - datetime.timedelta(days=1)) - \
		  datetime.timedelta(days=d) for d in list(reversed(range(days + 1)))]
	YESTERDAY_STR = DATES[-1].strftime('%Y%m%d')

#open db
conn = sqlite3.connect(DB_PATH)
'''
# select
feeder = conn.execute("SELECT * FROM feeder LIMIT 100").fetchall()
activity = conn.execute("SELECT * FROM activity LIMIT 100").fetchall()
scale = conn.execute("SELECT * FROM scale LIMIT 100").fetchall()
observation = conn.execute("SELECT * FROM observation").fetchall()
'''
# insert
c = conn.cursor()

feeder, cognitive_, scale, activity, observation = [],[],[],[],[]
for date in DATES: 
	for monk in MONKEYS: 
		feeder.append((date.strftime('%Y-%m-%d %H:%M:%S'),random.random(),random.randint(0,2)), monk)

c.executemany("INSERT INTO feeder VALUES (?,?,?,?)", feeder)
		
''' 
# feeder
feeder_ = []
for monk in STATIONS:
    for date in DATES:
        for row in feeder:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            feeder_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
                            row[1], row[2], monk))
'''

cognitive = conn.execute("SELECT * FROM cognitive LIMIT 100").fetchall()
# cognitive
cognitive_ = []
for monk in STATIONS:
    for date in DATES:
        for row in cognitive:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            cognitive_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
                               row[1], row[2], monk))

c.executemany("INSERT INTO cognitive VALUES (?,?,?,?)", cognitive_)
'''
# activity
activity_ = []
for monk in STATIONS:
    for date in DATES:
        for row in activity:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            activity_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
                               row[1], monk))

c.executemany("INSERT INTO activity VALUES (?,?,?)", activity_)

#scale
scale_ = []
for monk in STATIONS:
    for date in DATES:
        for row in scale:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            scale_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
                               row[1], monk))

c.executemany("INSERT INTO scale VALUES (?,?,?)", scale_)
'''
#observation
observation_ = []
for monk in STATIONS:
    for date in DATES:
        for row in observation:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            observation_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
								row[1], row[2], row[3], row[4], row[5], row[6], row[7], monk))

c.executemany("INSERT INTO observation VALUES (?,?,?,?,?,?,?,?,?)", observation_)

conn.commit()
conn.close()
