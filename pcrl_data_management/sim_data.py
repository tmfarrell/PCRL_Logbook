#!/usr/local/bin/python

'''
sim_data.py:    gets 100 rows from each table in PCRL_phizer_study.db and
                replicates that data for each monkey and for the past ten days;
                then inserts that replicated data into the db

Tim Farrell, tmf@bu.edu
PCRL, BU
20150829
'''

import sqlite3
import datetime

# stations/ monkeys
STATIONS = map(str, range(401, 405) + range(501, 509) + range(601, 609) + \
                    range(801, 805))

# simulate dates: ten days from yesterday
yesterday = datetime.datetime.today() - datetime.timedelta(days=1)
dates = [yesterday - datetime.timedelta(days=d) for d in range(10)]

#open db
conn = sqlite3.connect('PCRL_phizer_study.db')

# select
feeder = conn.execute("SELECT * FROM feeder LIMIT 100").fetchall()
cognitive = conn.execute("SELECT * FROM cognitive LIMIT 100").fetchall()
activity = conn.execute("SELECT * FROM activity LIMIT 100").fetchall()
scale = conn.execute("SELECT * FROM scale LIMIT 100").fetchall()

# insert
c = conn.cursor()

# feeder
feeder_ = []
for monk in STATIONS:
    for date in dates:
        for row in feeder:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            feeder_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
                            row[1], row[2], monk))

c.executemany("INSERT INTO feeder VALUES (?,?,?,?)", feeder_)

# cognitive
cognitive_ = []
for monk in STATIONS:
    for date in dates:
        for row in cognitive:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            cognitive_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
                               row[1], row[2], monk))

c.executemany("INSERT INTO cognitive VALUES (?,?,?,?)", cognitive_)

# activity
activity_ = []
for monk in STATIONS:
    for date in dates:
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
    for date in dates:
        for row in scale:
            datetime_ = datetime.datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S')
            new_datetime = datetime_.replace(year=date.year, \
                                             month=date.month, day=date.day)
            scale_.append((new_datetime.strftime('%Y-%m-%d %H:%M:%S'), \
                               row[1], monk))

c.executemany("INSERT INTO scale VALUES (?,?,?)", scale_)

conn.commit()
conn.close()
