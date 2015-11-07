import os
import sys
import time
import json
import string
import calendar
#from numpy import *
import datetime as dt
from datetime import datetime


##################################################
# 					MAIN						 #
##################################################

days = 10
feeding_hour_threshold = 26

data_dir = "X:\Data_Archive1"

report_custom = False 
custom_dates = {"from": "10-10-2015", "to": "10-19-2015"}

feeders = {f[0]: {'calories_per_dispense': f[1], 'dispense_amt':f[2]} for \
	f in zip(['feeder0','feeder1','feeder2'],[0.5, 0.75, 0.32], [10.0, 10.0, 13.0])}

monkeys = [l.split(',') for l in open('X:\Documents\projects\PCRL_Logbook\\' + \
				'pcrl_data_management\Monkey.txt').readlines()]
				
monkey_data = {m[0]: {'room':m[2], 'station':m[1], 'dob':m[3], 'sex':'M'} for m in monkeys} 

caloric_densities =[[s.strip() for s in l.split(',')] for l in open('X:\Documents\projects\PCRL_Logbook\\' + \
			'pcrl_data_management\caloric_densities.csv').readlines()]
				
supplemental_feed_data = {f[1]: {'category':f[0], 'E_carb':f[2], 'E_prot':f[3], \
		'E_fat':f[4], 'fraction_fiber':f[5], 'fraction_water':f[6]} for f in caloric_densities}
		
lab_member_data = {'admin': {'first':'PCRL', 'last':'admin', 'password':'PCRLBostonU'}}

# init json data struct
data = {'days': days, 'feeding_hour_threshold': feeding_hour_threshold, \
		'data_dir': data_dir, 'feeders': feeders, 'report_custom': report_custom, \
		'custom_dates': custom_dates, 'monkey_data': monkey_data, \
		'supplemental_feed_data': supplemental_feed_data, \
		'labmember_data': lab_member_data }

# write to configuration file
config_f = open('X:\Documents\projects\PCRL_Logbook\pcrl_data_management\config.json', 'w')
config_f.write(json.dumps(data, indent=2))
config_f.close()

