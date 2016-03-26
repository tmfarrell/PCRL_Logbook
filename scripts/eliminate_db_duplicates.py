##
## (a) get distinct data from db 
## (b) create db (similar name) with proper table unique constraints
## (c) save distinct data to new db 
## 
import json
import sqlite3
'''
base_dir = 'C:\\Users\\Server1\\Desktop\\PCRL_Logbook\\pcrl_data_management\\'

# get configuration options
opts = {}
config_f = open(base_dir + 'config.json', 'r')
opts = json.load(config_f)
config_f.close()

# connect to old db
old_conn = sqlite3.connect(opts['db_file'])
old_c = old_conn.cursor()			

# get distinct values for all tables 
schemas = {'scale': ('(date_time, weight, mid)', '(?,?,?)'),\
		   'activity': ('(date_time, activity, mid)', '(?,?,?)'),\
		   'cognitive': ('(date_time, time, event, mid)', '(?,?,?,?)'),\
		   'feeder': ('(date_time, rxn_time, feeder, mid)', '(?,?,?,?)'),\
		   'entertain': ('(date_time, type, since, x, y, mid)', '(?,?,?,?,?,?)'),\
		   'loginlog':   ('(datetime_in, datetime_out, labmember)', '(?,?,?)'),\
		   'supp_feed': ('(date_time, labmember, name, amount, mid)', '(?,?,?,?,?)'),\
		   'tables_with_monkdata_from_dates': ('(table_, date_, mid)', '(?,?,?)'),\
		   'labroomlog': ('(datetime_in, datetime_out, labroom, labmember)', '(?,?,?,?)'),\
		   'observation': ('(date_time, labmember, labroom, behavior, stool, training, comments, equip, mid)', '(?,?,?,?,?,?,?,?,?)')}
distinct_data = {}
for table in schemas: 
	distinct_data[table] = old_c.execute("SELECT DISTINCT * FROM " + table).fetchall()
	
old_conn.commit()
old_conn.close()
'''
# connect to new db 
newdb = 'F:\\pcrl_logbook_data\\PCRL_2016_data_test.db'
new_conn = sqlite3.connect(newdb)
new_c = new_conn.cursor()

# create tables 
sql_f = open('fresh_db.sql', 'r')
sql = sql_f.read()
sql_f.close()

new_c.executescript(sql)
'''
# replace old names 
del(schemas['tables_with_monkdata_from_dates'])
del(schemas['supp_feed'])
schemas['suppfeed'] = ('(date_time, labmember, name, amount, mid)', '(?,?,?,?,?)')
schemas['table_date_monk_yes_data'] = ('(table_, date_, mid)', '(?,?,?)')

# add distinct data 
for table, schema  in schemas.items(): 
	try: 
		if table == 'table_date_monk_yes_data': 
			new_c.executemany("INSERT INTO " + table + " " + schema[0] + " VALUES " + schema[1],\
				[(t[1], t[0], t[2]) for t in distinct_data['tables_with_monkdata_from_dates']])
		elif table =='suppfeed': 
			new_c.executemany("INSERT INTO " + table + " " + schema[0] + " VALUES " + schema[1], distinct_data['supp_feed'])
		else: 
			new_c.executemany("INSERT INTO " + table + " " + schema[0] + " VALUES " + schema[1], distinct_data[table])
	except sqlite3.IntegrityError: 
		pass
	
'''
new_conn.commit()
new_conn.close()

