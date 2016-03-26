# inits new sqlite database with schema's needed 
# for PCRL 2015-16 data management system

import sys 
import sqlite3

# get db file name 
try: 
	db_name = sys.argv[1]
except: 
	print "Needs db name input."
	sys.exit(0)

# connect db	
con = sqlite3.connect(db_name)
cur = con.cursor()

# get 'new database' sql commands from file
sql_f = open("fresh_db.sql",'r')
sql = sql_f.read()
sql_f.close()

# execute those commands on database
con.executescript(sql)

# closeout
con.commit()
con.close()