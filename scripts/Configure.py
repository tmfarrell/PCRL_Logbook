##
##	Configure.py
##
##	generates new config file in PCRL_Logbook base dir
##
import json
from datetime import datetime


##################################################
# 					MAIN						 #
##################################################

# base directory 
base_dir = 'C:\\Users\\Server1\\Desktop\\PCRL_Logbook\\'

# read base configuration file
baseconfig_f = open('base_config.json', 'r')
data = json.load(baseconfig_f)
baseconfig_f.close() 

# write new config file 
config_f = open(base_dir + 'config.json', 'w')
config_f.write(json.dumps(data, indent=2))
config_f.close()

