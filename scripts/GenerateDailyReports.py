'''
GenerateDailyReports.py: 	generates daily reports and saves to folder named
							report_dir + '/[mmm]_[dd]_[yyyy]_to_[mmm]_[dd]_[yyyy]' directory, where report_dir
							is the dir specified thru the PCRL Reports app.
							reports include:
							(a) one report with plots for the whole group
								"PCRL Group Report [date].pdf"
							(b) reports for each individual monkey
								"PCRL [monkey] Report [date].pdf"

Tim Farrell, tmf@bu.edu
PCRL, BU
20150827
'''

import os
import sys
import time
import json
import random
import string
import sqlite3
import datetime
import itertools
import numpy as np
from numpy import *
from html import HTML
import scipy.stats.mstats as mstats

import matplotlib as mpl
import matplotlib.pyplot as plt
import matplotlib.collections as collections
from matplotlib.font_manager import FontProperties
from matplotlib.backends.backend_pdf import PdfPages
from matplotlib.ticker import LinearLocator, FixedLocator, FormatStrFormatter, \
							  OldScalarFormatter, MultipleLocator, MaxNLocator


base_dir = 'C:\\Users\\Server1\\Desktop\\PCRL_Logbook\\'

# import local packages 
sys.path.append(base_dir + 'libs\\PyPDF2-master')
sys.path.append(base_dir + 'libs\\xhtml2pdf')
import PyPDF2
from xhtml2pdf import pisa


#############################################################
#				  		Functions 							#
#############################################################

# takes a list of tuples, resulting from SELECT query of
# PCRL_phizer_study.db (sqlite3) and builds and returns a dict data struct where
# keys = monkeys, values = dicts(dates, lists(rows))
def TableToDailyDict(table):
	daily = {}

	for row in table:
		monkey = row[-1]
		date = row[0].split(' ')[0]

		if monkey not in daily.keys():
			daily[monkey] = {date: [row]}

		else:
			if date not in daily[monkey].keys():
				daily[monkey][date] = [row]
			else:
				daily[monkey][date].append(row)

	return daily


# takes a list of tuples, resulting from SELECT query of
# PCRL_phizer_study.db (sqlite3) and builds and returns a dict data struct where
# keys = monkeys, values = dicts(dates, dicts(hours, rows))
def TableToHourlyDict(table):
	hourly = {}

	for row in table:
		monkey = row[-1]
		date = row[0].split(' ')[0]
		hour = row[0].split(' ')[-1].split(':')[0]

		if monkey not in hourly.keys():
			hourly[monkey] = {date: {hour: [row]}}

		else:
			if date not in hourly[monkey].keys():
				hourly[monkey][date] = {hour: [row]}
			else:
				if hour not in hourly[monkey][date].keys():
					hourly[monkey][date][hour] = [row]
				else:
					hourly[monkey][date][hour].append(row)
	return hourly


# imports data for last n_days from each PCRL_phizer_study.db table
# returns all tables in lists(tuples) format
def ImportDataForLast(n_days):
	n_days += 2
	Tables = {}
	table_names = ['activity', 'cognitive', 'feeder', 'scale', 'observation', 'suppfeed']

	conn = sqlite3.connect(DB_PATH)
	
	for table_name in table_names:
		Tables[table_name] = conn.execute("SELECT * FROM " + table_name + \
		" WHERE date(date_time) > date('now', '-"+ str(n_days) \
		+" days')").fetchall()

	conn.close()

	return Tables


# imports data from each PCRL_phizer_study.db table
# between the two input dates; returns all tables in lists(tuples) format
def ImportData(from_, to_):
	Tables = {}
	table_names = ['activity', 'cognitive', 'feeder', 'scale', 'observation', 'suppfeed']

	conn = sqlite3.connect(DB_PATH)

	for table_name in table_names:
		Tables[table_name] = conn.execute("SELECT * FROM " + table_name + \
		" WHERE date(date_time) BETWEEN '" + from_ + "' AND '" + to_ + "'").fetchall()

	conn.close()

	return Tables



# generates stats from cognitive dict for an individual monkey
# returns cognitive_stats: keys=fields, values=lists(daily_stat)
def CalcDailyCognitiveStats(cognitive_monk_dict):

	# Abbreviations:	corr=correct, comp=complete, succ=success, rxn=reaction
	# 		 			t=test, c=cognitive, x=example, no=number
	corr, incorr, t_pellets, comp_pellets, incomp_pellets = 0.0, 0.0, 0.0, 0.0, 0.0
	choice_rxn, ex_rxn, ov_rxn, corr_rxn, incorr_rxn  = [], [], [], [], []
	t_attempts, comp_succ_rate, incomp_succ_rate = [], [], []
	start_c, end_c, start_x, end_x, end_corr = 0, 0, 0, 0, 0
	end_incorr, no_comp_tests, no_incomp_tests = 0, 0, 0

	cognitive_fields = ['succ_rate', 'corr', 'choice_rxn', 'ex_rxn', \
	'ov_rxn', 'choice_rxn_sd', 'ex_rxn_sd', 'ov_rxn_sd', 'corr_rxn', \
	'corr_std', 'incorr_rxn', 'incorr_std', 'comp_succ_rate', \
	'incomp_succ_rate', 'comp_pellets', 'incomp_pellets']

	#image_stats = {}
	cognitive_stats = {field: [] for field in cognitive_fields}
	#reaction_times = { 'example':{} ,'corr':{}, 'incorr':{}, 'free':{} }

	random.seed()
	for date, row_list in cognitive_monk_dict.items():
		#image_stats[date] = {}

		for row in row_list:
			event = row[2].strip()

			'''
			if "Matching Button Image" in event:
				match = event.split(':')[-1].strip()
				if match not in daily_image_stats.keys():
					daily_image_stats[date][match] = {'hit_corr': 0, \
					  'hit_incorr': 0, 'no_corr': 0, 'no_incorr': 0}
				continue

			if "Non-matching Button Image" in line:
				nonmatch = event.split(':')[-1].strip()
				if nonmatch not in daily_image_stats.keys():
					daily_image_stats[date][nonmatch] = {'hit_corr': 0, \
					     'hit_incorr': 0, 'no_corr': 0, 'no_incorr': 0}
				continue
			'''

			if "Matching Button Clicked" in event:
				corr += 1.0  # number of correct += 1
				t_attempts.append(1)
				t_pellets += 1.0

			if "Non-matching Button Clicked" in event:
				incorr += 1.0  # number of incorrect += 1
				t_attempts.append(0)

			if "Choice Icons Displayed" in event:
			# record the time the choice icons are displayed
				start_c = row[1]

			if ("Matching Button Clicked" in event) or \
			   ("Non-matching Button Clicked" in event):

				if start_c != 0:
					# record time button is clicked
					end_c = row[1]
					# record choice rxn time
					rxn_ct = random.randint(0,40) 	#float(end_c - start_c)

					choice_rxn.append(rxn_ct)
					ov_rxn.append(rxn_ct)

					if "Matching Button Clicked" in event:
						#time_of_choice = line.split(' ')[3] +' '+ line.split(' ')[4]
						corr_rxn.append(rxn_ct)
						'''
						try:
							#reaction_times['corr'].append([time_of_choice,\
							#				(end_corr - start_c)/1000.0, match_icon])
							daily_image_stats[match]['hit_corr'] +=1
							daily_image_stats[match]['no_corr'] +=1
							daily_image_stats[nonmatch]['no_incorr'] +=1

						except KeyError:
							pass
						'''
					elif "Non-matching Button Clicked" in event:
						#time_of_choice = line.split(' ')[3] +' '+ line.split(' ')[4]
						incorr_rxn.append(rxn_ct)
						'''
						try:
							#reaction_times['incorr'].append([time_of_choice,\
							#				rxn_t, match_icon])
							daily_image_stats[match]['no_corr'] +=1
							daily_image_stats[nonmatch]['hit_incorr'] +=1
							daily_image_stats[nonmatch]['no_incorr'] +=1

						except KeyError:
							pass
						'''


			if ("Test Started" in event) or ("Example Icon displayed" in event):
				start_x = row[1]  # record time

			if "Example Button Clicked" in event:
			# record time ex button clicked
				if start_x != 0:
					end_x = row[1]
					rxn_xt = random.randint(0,40) 	#float(end_x - start_x)
					#time_of_choice = line.split(' ')[3] +' '+ line.split(' ')[4]
					ex_rxn.append(rxn_xt)  # record ex rxn time
					ov_rxn.append(rxn_xt)  # add ex rxn time to overall

					#reaction_times['Example'][fname].append([time_of_choice, \
					#						(end_x - start_x)/1000.0, "Example"])

			if "rounds remaining" in event:   # if a test rd finishes
				if "0 rounds remaining" in event:  # if test is complete
					comp_succ_rate.extend(t_attempts)
					comp_pellets += t_pellets
					t_attempts = []
					t_pellets = 0.0
					no_comp_tests += 1
				else:
				# if more than 0 rds left, test is incomp
					incomp_succ_rate.extend(t_attempts)
					t_attempts = []
					incomp_pellets += t_pellets
					t_pellets = 0.0
					no_incomp_tests += 1

		try:
			cognitive_stats['succ_rate'].append(100.0*corr/(corr + incorr))
		except ZeroDivisionError:
			cognitive_stats['succ_rate'].append(0.0)


		if comp_succ_rate == []:
			cognitive_stats['comp_succ_rate'].append(0)
		else:
			cognitive_stats['comp_succ_rate'].append(100.0 * \
												 float(average(comp_succ_rate)))

		if incomp_succ_rate == []:
			cognitive_stats['incomp_succ_rate'].append(0)
		else:
			cognitive_stats['incomp_succ_rate'].append(100.0 * \
												float(average(incomp_succ_rate)))

		if ex_rxn == []:
			cognitive_stats['ex_rxn'].append(0)
			cognitive_stats['ex_rxn_sd'].append(0)
		else:
			cognitive_stats['ex_rxn'].append(sum(ex_rxn) / len(ex_rxn))
			cognitive_stats['ex_rxn_sd'].append(std(ex_rxn))

		if choice_rxn == []:
			cognitive_stats['choice_rxn'].append(0)
			cognitive_stats['choice_rxn_sd'].append(0)
		else:
			cognitive_stats['choice_rxn'].append(sum(choice_rxn) / \
												 len(choice_rxn))
			cognitive_stats['choice_rxn_sd'].append(std(choice_rxn))

		if ov_rxn == []:
			cognitive_stats['ov_rxn'].append(0)
			cognitive_stats['ov_rxn_sd'].append(0)
		else:
			cognitive_stats['ov_rxn'].append(sum(ov_rxn) / len(ov_rxn))
			cognitive_stats['ov_rxn_sd'].append(std(ov_rxn))

		if corr_rxn == []:
			cognitive_stats['corr_rxn'].append(0)
			cognitive_stats['corr_std'].append(0)
		else:
			cognitive_stats['corr_rxn'].append(average(corr_rxn))
			cognitive_stats['corr_std'].append(std(corr_rxn))

		if incorr_rxn == []:
			cognitive_stats['incorr_rxn'].append(0)
			cognitive_stats['incorr_std'].append(0)
		else:
			cognitive_stats['incorr_rxn'].append(average(incorr_rxn))
			cognitive_stats['incorr_std'].append(std(incorr_rxn))

	return cognitive_stats


# generates stats from cognitive dict for an individual monkey
# returns 	cognitive_hr_totals: keys=fields, values=lists(hourly_stat_total)
# and 		cognitive_hr_avgs:   keys=fields, values=lists(hourly_stat_avg)
def CalcHourlyCognitiveStats(cognitive_monk_dict):

	cognitive_fields = ['succ_rate', 'corr', 'corr_std', 'incorr_rxn', \
	'incorr_std', 'comp_succ_rate', 'incomp_succ_rate', 'comp_pellets', \
	'incomp_pellets']	#'choice_rxn', 'ex_rxn', \
						#'ov_rxn', 'choice_rxn_sd', 'ex_rxn_sd', 'ov_rxn_sd', \
						#'corr_rxn',

	#image_stats = {}
	cognitive_hr_totals = {field: [] for field in cognitive_fields}
	#reaction_times = {'example':{} ,'corr':{}, 'incorr':{}, 'free':{}}

	random.seed()
	no_days = 0
	cognitive_hr_sums = {field: [0]*24 for field in cognitive_fields}
	for cog_dict in cognitive_monk_dict.values():
		#image_stats[date] = {}

		# count date
		no_days += 1

		# holder to calc avgs
		cognitive_hr_sums = {key: [0]*24 for key in cognitive_hr_sums}	# reset back to zero

		for hr in range(24):
			## zero all metrics
			#  abbreviations: corr=correct, comp=complete, succ=success, rxn=reaction-time
			# 				  t=test, c=cognitive, x=example, no=number
			corr, incorr, t_pellets, comp_pellets, incomp_pellets = 0.0,0.0,0.0,0.0,0.0
			choice_rxn, ex_rxn, ov_rxn, corr_rxn, incorr_rxn  = [],[],[],[],[]
			t_attempts, comp_succ_rate, incomp_succ_rate = [],[],[]
			start_c, end_c, start_x, end_x, end_corr = 0,0,0,0,0
			end_incorr, no_comp_tests, no_incomp_tests = 0,0,0
		
		
			hr_str = '0' + str(hr) if hr < 10 else str(hr)
			try:
				for row in cog_dict[hr_str]:
					event = row[2].strip()

					'''
					if "Matching Button Image" in event:
						match = event.split(':')[-1].strip()
						if match not in daily_image_stats.keys():
							daily_image_stats[date][match] = {'hit_corr': 0, \
							  'hit_incorr': 0, 'no_corr': 0, 'no_incorr': 0}
						continue

					if "Non-matching Button Image" in line:
						nonmatch = event.split(':')[-1].strip()
						if nonmatch not in daily_image_stats.keys():
							daily_image_stats[date][nonmatch] = {'hit_corr': 0, \
							     'hit_incorr': 0, 'no_corr': 0, 'no_incorr': 0}
						continue
					'''

					if "Matching Button Clicked" in event:
						corr += 1.0  # number of correct += 1
						t_attempts.append(1)
						t_pellets += 1.0

					if "Non-matching Button Clicked" in event:
						incorr += 1.0  # number of incorrect += 1
						t_attempts.append(0)

					if "Choice Icons Displayed" in event:
					# record the time the choice icons are displayed
						start_c = row[1]

					if ("Matching Button Clicked" in event) or \
					   ("Non-matching Button Clicked" in event):

						if start_c != 0:
							# record time button is clicked
							end_c = row[1]
							# record choice rxn time
							rxn_ct = float(end_c - start_c)

							choice_rxn.append(rxn_ct)
							ov_rxn.append(rxn_ct)

							if "Matching Button Clicked" in event:
								#time_of_choice = line.split(' ')[3] +' '+ \
								# 								line.split(' ')[4]
								corr_rxn.append(rxn_ct)
								'''
								try:
									#reaction_times['corr'].append([time_of_choice,\
									#				(end_corr - start_c)/1000.0, match_icon])
									daily_image_stats[match]['hit_corr'] +=1
									daily_image_stats[match]['no_corr'] +=1
									daily_image_stats[nonmatch]['no_incorr'] +=1

								except KeyError:
									pass
								'''
							elif "Non-matching Button Clicked" in event:
								#time_of_choice = line.split(' ')[3] +' '+ \
								# 								line.split(' ')[4]
								incorr_rxn.append(rxn_ct)
								'''
								try:
									#reaction_times['incorr'].append([time_of_choice,\
									#				rxn_t, match_icon])
									daily_image_stats[match]['no_corr'] +=1
									daily_image_stats[nonmatch]['hit_incorr'] +=1
									daily_image_stats[nonmatch]['no_incorr'] +=1

								except KeyError:
									pass
								'''


					if ("Test Started" in event) or \
					   ("Example Icon displayed" in event):
						start_x = row[1]  # record time

					if "Example Button Clicked" in event:
					# record time ex button clicked
						if start_x != 0:
							end_x = row[1]
							rxn_xt = float(end_x - start_x)
							#time_of_choice = line.split(' ')[3] +' '+ \
							# 									 line.split(' ')[4]
							ex_rxn.append(rxn_xt)  # record ex rxn time
							ov_rxn.append(rxn_xt)  # add ex rxn time to overall

							#reaction_times['Example'][fname].append([time_of_choice, \
							#						(end_x - start_x)/1000.0, "Example"])

					if "rounds remaining" in event:   # if a test round finishes
						if "0 rounds remaining" in event:  # if test is complete
							comp_succ_rate.extend(t_attempts)
							comp_pellets += t_pellets
							t_attempts = []
							t_pellets = 0.0
							no_comp_tests += 1
						else:
						# if more than 0 rounds left, means the test is incomp
							incomp_succ_rate.extend(t_attempts)
							t_attempts = []
							incomp_pellets += t_pellets
							t_pellets = 0.0
							no_incomp_tests += 1

				cognitive_hr_totals['comp_pellets'].append(comp_pellets)
				cognitive_hr_totals['incomp_pellets'].append(incomp_pellets)

				cognitive_hr_sums['comp_pellets'][hr] += comp_pellets
				cognitive_hr_sums['incomp_pellets'][hr] += incomp_pellets
				
				stat = 0
				try:
					stat = 100.0 * corr/(corr + incorr)
					cognitive_hr_totals['succ_rate'].append(stat)
					cognitive_hr_sums['succ_rate'][hr] += stat
				except ZeroDivisionError:
					cognitive_hr_totals['succ_rate'].append(0.0)

				if comp_succ_rate == []:
					cognitive_hr_totals['comp_succ_rate'].append(0)
				else:
					stat = 100.0 * float(average(comp_succ_rate))
					cognitive_hr_sums['comp_succ_rate'][hr] += stat
					cognitive_hr_totals['comp_succ_rate'].append(stat)

				if incomp_succ_rate == []:
					cognitive_hr_totals['incomp_succ_rate'].append(0)
				else:
					stat = 100.0 * float(average(incomp_succ_rate))
					cognitive_hr_sums['incomp_succ_rate'][hr] += stat
					cognitive_hr_totals['incomp_succ_rate'].append(stat)

				if ex_rxn == []:
					cognitive_hr_totals['ex_rxn'].append(0)
					cognitive_hr_totals['ex_rxn_sd'].append(0)
				else:
					stat = sum(ex_rxn) / len(ex_rxn)
					cognitive_hr_sums['ex_rxn'][hr] += stat
					cognitive_hr_totals['ex_rxn'].append(stat)
					cognitive_hr_totals['ex_rxn_sd'].append(std(ex_rxn))

				if choice_rxn == []:
					cognitive_hr_totals['choice_rxn'].append(0)
					cognitive_hr_totals['choice_rxn_sd'].append(0)
				else:
					stat = sum(choice_rxn) / len(choice_rxn)
					cognitive_hr_sums['choice_rxn'][hr] += stat
					cognitive_hr_totals['choice_rxn'].append(stat)
					cognitive_hr_totals['choice_rxn_sd'].append(std(choice_rxn))

				if ov_rxn == []:
					cognitive_hr_totals['ov_rxn'].append(0)
					cognitive_hr_totals['ov_rxn_sd'].append(0)
				else:
					stat = sum(ov_rxn) / len(ov_rxn)
					cognitive_hr_sums['ov_rxn'][hr] += stat
					cognitive_hr_totals['ov_rxn'].append(stat)
					cognitive_hr_totals['ov_rxn_sd'].append(std(ov_rxn))

				if corr_rxn == []:
					cognitive_hr_totals['corr_rxn'].append(0)
					cognitive_hr_totals['corr_std'].append(0)
				else:
					stat = average(corr_rxn)
					cognitive_hr_sums['corr_rxn'][hr] += stat
					cognitive_hr_totals['corr_rxn'].append(stat)
					cognitive_hr_totals['corr_std'].append(std(corr_rxn))

				if incorr_rxn == []:
					cognitive_hr_totals['incorr_rxn'].append(0)
					cognitive_hr_totals['incorr_std'].append(0)
				else:
					stat = average(incorr_rxn)
					cognitive_hr_sums['incorr_rxn'][hr] += stat
					cognitive_hr_totals['incorr_rxn'].append(stat)
					cognitive_hr_totals['incorr_std'].append(std(incorr_rxn))

			except KeyError:

				cognitive_hr_totals['comp_pellets'].append(comp_pellets)
				cognitive_hr_totals['incomp_pellets'].append(incomp_pellets) 

				stat = 0
				if (corr + incorr) > 0: 
					stat = 100.0 * corr/(corr + incorr)
				cognitive_hr_totals['succ_rate'].append(stat)
				cognitive_hr_sums['succ_rate'][hr] += stat

				stat = 100.0 * float(average(comp_succ_rate))
				cognitive_hr_sums['comp_succ_rate'][hr] += stat
				cognitive_hr_totals['comp_succ_rate'].append(stat)

				stat = 100.0 * float(average(incomp_succ_rate))
				cognitive_hr_sums['incomp_succ_rate'][hr] += stat
				cognitive_hr_totals['incomp_succ_rate'].append(stat)

				stat = sum(ex_rxn) / len(ex_rxn)
				cognitive_hr_sums['ex_rxn'][hr] += stat
				cognitive_hr_totals['ex_rxn'].append(stat)
				cognitive_hr_totals['ex_rxn_sd'].append(std(ex_rxn))

				stat = sum(choice_rxn) / len(choice_rxn)
				cognitive_hr_sums['choice_rxn'][hr] += stat
				cognitive_hr_totals['choice_rxn'].append(stat)
				cognitive_hr_totals['choice_rxn_sd'].append(std(choice_rxn))

				stat = sum(ov_rxn) / len(ov_rxn)
				cognitive_hr_sums['ov_rxn'][hr] += stat
				cognitive_hr_totals['ov_rxn'].append(stat)
				cognitive_hr_totals['ov_rxn_sd'].append(std(ov_rxn))

				stat = average(corr_rxn)
				cognitive_hr_sums['corr_rxn'][hr] += stat
				cognitive_hr_totals['corr_rxn'].append(stat)
				cognitive_hr_totals['corr_std'].append(std(corr_rxn))

				stat = average(incorr_rxn)
				cognitive_hr_sums['incorr_rxn'][hr] += stat
				cognitive_hr_totals['incorr_rxn'].append(stat)
				cognitive_hr_totals['incorr_std'].append(std(incorr_rxn))


	cognitive_hr_avgs = {}
	try: 	
		cognitive_hr_avgs = {f: [float(s)/float(no_days) for s in \
						  cognitive_hr_sums[f]] for f in cognitive_fields }
	except ZeroDivisionError: 
		cognitive_hr_avgs = {f: [0]*24 for f in cognitive_fields }

	return (cognitive_hr_totals, cognitive_hr_avgs)


#####################################################################
# 								Main 								#
#####################################################################

# Read options from config file
opts = {}
config_f = open(base_dir + 'config.json', 'r')
opts = json.load(config_f)
config_f.close()

MONKEYS = opts['monkey_data']
STATIONS = [m['station'] for m in MONKEYS.values()]
STATION_MONKEYS = zip(STATIONS, MONKEYS)
STATION_MONKEYS.sort()
DB_PATH = opts['db_file']

# Import data based on configuration
TABLES = {};  DATES = []; YESTERDAY_STR = '';
if opts['report_custom']:
	from_ = opts['custom_dates']['from']
	to_ = opts['custom_dates']['to']

	TABLES = ImportData(from_, to_)

	days = (datetime.datetime.strptime(to_, '%Y-%m-%d') - datetime.datetime.strptime(from_, '%Y-%m-%d')).days
	DATES = [(datetime.datetime.strptime(to_, '%Y-%m-%d') - datetime.timedelta(days=d)) \
			  for d in list(reversed(range(days + 1)))]
	YESTERDAY_STR = DATES[-1].strftime('%Y%m%d')
else:
	days = int(opts['days'])

	TABLES = ImportDataForLast(days)

	DATES = [(datetime.datetime.today() - datetime.timedelta(days=d)) \
				for d in list(reversed(range(days + 1)))]
	YESTERDAY_STR = DATES[-1].strftime('%Y%m%d')

# Setup directory for saving reports
DAYS_STR = DATES[0].strftime('%b_%d_%Y') + '-' + \
		   DATES[-1].strftime('%b_%d_%Y')
REPORT_DIR = opts['report_dir'] + DAYS_STR + '\\'
if not os.access(REPORT_DIR, os.F_OK):
	os.mkdir(REPORT_DIR)


############################
## Generate Group Plots   ##
############################
##
##	saves Average Activity, Feeding, Cognitive Test Success,
##	and Scale plots to "PCRL Group Report YYYYMMDD.pdf"
##
num_days = len(DATES)
feed_data = opts['feed_data']
feed_in_feeders = opts['feed_in_feeders']

pp = PdfPages(REPORT_DIR + 'PCRL Group Report ' + YESTERDAY_STR + '.pdf')

# transform tables to nested dicts as {mid: [{date: [table_tuples]}]}
activity = TableToDailyDict(TABLES['activity'])
cognitive = TableToDailyDict(TABLES['cognitive'])
feeder = TableToDailyDict(TABLES['feeder'])
scale = TableToDailyDict(TABLES['scale'])
supp_feed = TableToDailyDict(TABLES['suppfeed'])

##
## Stats Data Structs
##
activity_avg, activity_std, feeder_counts, feed_sums = {}, {}, {}, {}
activity_norm, supp_feed_sums, supp_feed_cts = {}, {}, {} 
scale_avg, scale_std, cognitive_stats = {}, {}, {}
 
feeder0_cals = sum(map(float, [(feed_data[feed_in_feeders['feeder0']]['carb_cals_per_gram']),\
					feed_data[feed_in_feeders['feeder0']]['prot_cals_per_gram'],\
					feed_data[feed_in_feeders['feeder0']]['fat_cals_per_gram']]))
feeder1_cals = sum(map(float, [feed_data[feed_in_feeders['feeder1']]['carb_cals_per_gram'],\
					feed_data[feed_in_feeders['feeder1']]['prot_cals_per_gram'],\
					feed_data[feed_in_feeders['feeder1']]['fat_cals_per_gram']]))
feeder2_cals = sum(map(float, [feed_data[feed_in_feeders['feeder2']]['carb_cals_per_gram'],\
					feed_data[feed_in_feeders['feeder2']]['prot_cals_per_gram'],\
					feed_data[feed_in_feeders['feeder2']]['fat_cals_per_gram']]))

for monkey in MONKEYS: 
	station = MONKEYS[monkey]['station']

	## activity 
	activity_avg[monkey], activity_std[monkey], act_date_dict = [], [], {}
	try: 				
		act_date_dict = activity[monkey]
	except KeyError:	
		try: 				act_date_dict = activity[station]
		except KeyError:	pass
	for act_tuple_list in act_date_dict.values():
		act_vals = [t[1] for t in act_tuple_list]
		activity_avg[monkey].append(sum(act_vals))
		activity_std[monkey].append(std(act_vals))
		
	# normalize 
	activity_norm[monkey] = [(a - average(activity_avg[monkey]))/std(activity_avg[monkey])\
							for a in activity_avg[monkey]]

	## feeder
	feeder_counts[monkey] = {'0':[], '1':[], '2':[], 'total':[]}
	feed_sums[monkey], feed_date_dict = [], {}
	try: 				
		feed_date_dict = feeder[monkey]
	except KeyError:	
		try: 				feed_date_dict = feeder[station]
		except KeyError:	pass
	for feed_tuple_list in feed_date_dict.values():
		feed_count_0, feed_count_1, feed_count_2 = 0, 0, 0
		for t in feed_tuple_list:
			feeder_num = t[2]
			if feeder_num == 0: feed_count_0 += 1
			elif feeder_num == 1: feed_count_1 += 1
			elif feeder_num == 2: feed_count_2 += 1
		
		feeder_counts[monkey]['0'].append(feed_count_0)
		feeder_counts[monkey]['1'].append(feed_count_1)
		feeder_counts[monkey]['2'].append(feed_count_2)
		feeder_counts[monkey]['total'].append(feed_count_0 + feed_count_1 \
														   + feed_count_2)
		feed_sums[monkey].append(sum([feed_count_0*feeder0_cals,\
				feed_count_1*feeder1_cals, feed_count_2*feeder2_cals]))
				
	## supp feed
	supp_feed_sums[monkey], supp_feed_cts[monkey] = [0]*num_days, [0]*num_days
	supp_feed_date_dict = {} 
	try: 				
		supp_feed_date_dict = supp_feed[monkey]
	except KeyError:	
		try: 				supp_feed_date_dict = supp_feed[station]
		except KeyError: 	pass
	if supp_feed_date_dict: 
		for i, supp_feed_tuple_list in enumerate(supp_feed_date_dict.values()): 
			count, total = 0, 0
			for t in supp_feed_tuple_list: 
				count += t[3]
				try: 
					cals = float(feed_data[t[2]]['carb_cals_per_gram']) \
						+ float(feed_data[t[2]]['prot_cals_per_gram']) \
						+ float(feed_data[t[2]]['fat_cals_per_gram'])
					total += t[3] * cals
				except: 
					print "Feed data for " + t[2] + " is unavailable."
					pass 
			supp_feed_sums[monkey][i] = total
			supp_feed_cts[monkey][i] = count

	## cognitive
	cognitive_stats[monkey] = {}
	try:	
		cognitive_stats[monkey] = CalcDailyCognitiveStats(cognitive[monkey])
	except KeyError: 
		try: 				cognitive_stats[monkey] = CalcDailyCognitiveStats(cognitive[station])
		except KeyError: 	cognitive_stats[monkey] = CalcDailyCognitiveStats({})

	## scale
	scale_avg[monkey], scale_std[monkey], scale_date_dict = [], [], {}
	try: 				
		scale_date_dict = scale[monkey]
	except KeyError:	
		try: 				scale_date_dict = scale[station]
		except KeyError:	pass
	for scale_tuple_list in scale_date_dict.values():
		scale_vals = [t[1] for t in scale_tuple_list]
		scale_avg[monkey].append(average(scale_vals))
		scale_std[monkey].append(std(scale_vals))


##
## Activity
##
fig = plt.figure()
fig.suptitle("Activity (normalized)", fontsize=15)
#fig.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)
sec_fig = False
mpl.rc('xtick', labelsize=8) 
mpl.rc('ytick', labelsize=8)  

rows, columns = 3, 4
positions = [(i+1) for i in range(rows*columns)]
no_days = len(DATES)

try: 
	max_act = max([max(a) for a in activity_norm.values()])
	min_act = min([min(a) for a in activity_norm.values()])
except: 
	max_act = 30000
	min_act = 0
	
fontP = FontProperties()
fontP.set_size(6)
for n, (station, monkey) in enumerate(STATION_MONKEYS, start=1):
	
	# make sure that more than one day is being plotted
	if len(activity_norm[monkey]) > 1:
		if n <= 12:
		#plot first
			ax = fig.add_subplot(rows, columns, n)

		elif n > 12 and n <= 24:
			if sec_fig == False:
				figB = plt.figure()
				figB.suptitle('Activity (normalized)', fontsize=15)
			ax = figB.add_subplot(rows, columns, n - 12)
			sec_fig = True
		else:
			break

		l1, = ax.plot(activity_norm[monkey])
		ax.set_xticklabels(range(no_days))
		#leg = plt.figlegend((l1),
		#					('Normalized Activity'), \
		#					'upper right', prop=fontP)
		ax.set_title(monkey+'-'+MONKEYS[monkey]['station'], fontsize=10)
		ax.set_xlabel('Day', fontsize=7)
		if n in [1, 5, 9, 13, 17, 21]: ax.set_ylabel('Deviation from Mean', fontsize=6)
		plt.xlim(1, no_days)
		plt.ylim(min_act, max_act)
		plt.gca().set_autoscale_on(False)
		ax.xaxis.set_major_locator(MultipleLocator(1))
		ax.yaxis.set_major_locator(MaxNLocator(10))

fig.subplots_adjust(wspace=0.5, hspace=0.85)
pp.savefig(fig)
if sec_fig == True:
	figB.subplots_adjust(wspace=0.5, hspace=0.85)
	pp.savefig(figB)

##
## Feeding  
##
fig2 = plt.figure()
sec_fig = False
fig2.suptitle("Feeding", fontsize=15)
#fig2.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)

max_feed_cal, max_feed, min_feed_cal, min_feed = 0, 0, 0, 0
for m in MONKEYS: 
	try: 
		max_ = max([f+s for f,s in zip(feed_sums[m], supp_feed_sums[m])])
		min_ = min([f+s for f,s in zip(feed_sums[m], supp_feed_sums[m])]) 
		if max_ > max_feed_cal: 
			max_feed_cal = max_
		if min_ < min_feed_cal: 
			min_feed_cal = min_
		
		max_ = max([f+s for f,s in zip(feeder_counts[m]['total'], supp_feed_cts[m])])
		min_ = min([f+s for f,s in zip(feeder_counts[m]['total'], supp_feed_cts[m])]) 
		if max_ > max_feed: 
			max_feed = max_
		if min_ < min_feed: 
			min_feed = min_
	except: 
		try: 
			max_ = max(feed_sums[m])
			min_ = min(feed_sums[m]) 
			if max_ > max_feed_cal: 
				max_feed = max_
			if min_ < min_feed_cal: 
				min_feed = min_
				
			max_ = max(feeder_counts[m]['total'])
			min_ = min(feeder_counts[m]['total']) 
			if max_ > max_feed: 
				max_feed = max_
			if min_ < min_feed: 
				min_feed = min_
		except: 
			pass
		
for n, (station, monkey) in enumerate(STATION_MONKEYS, start=1):

	if len(feeder_counts[monkey]['0']) > 1:
	# make sure that more than one day is being plotted
		if n <= 12:
			ax2 = fig2.add_subplot(rows, columns, n)
		elif n > 12 and n <= 24:
			if sec_fig == False:
				fig2B = plt.figure()
				fig2B.suptitle("Feeding", fontsize=15)
				#fig2B.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)
			ax2 = fig2B.add_subplot(rows, columns, n - 12)
			sec_fig = True
		else:
			break

		# total feed = feed + supp feed
		total_feed = [f + s for f, s in zip(feed_sums[monkey], supp_feed_sums[monkey])]	
		
		# average feed line
		avg_line = [average(total_feed) for d in range(len(total_feed))]

		l1, = ax2.plot(total_feed)
		l2, = ax2.plot(avg_line)
		l3, = ax2.plot([ct * feeder0_cals\
						for ct in feeder_counts[monkey]['0']])
		l4, = ax2.plot([ct * feeder1_cals\
						for ct in feeder_counts[monkey]['1']])
		l5, = ax2.plot([ct * feeder2_cals\
						for ct in feeder_counts[monkey]['2']])
		l6, = ax2.plot(supp_feed_sums[monkey])
		leg = plt.figlegend((l1,l2,l3,l4,l5,l6),
				('Total','Average','Feeder 0','Feeder 1','Feeder 2','Supplemental'), \
				'upper right', prop=fontP)
				
		ax2.set_xticklabels(range(no_days))
		ax2.set_title(monkey+'-'+MONKEYS[monkey]['station'], fontsize=10)
		ax2.set_ylim([min_feed_cal, max_feed_cal])
		plt.gca().set_autoscale_on(False)
		ax2.xaxis.set_major_locator(MultipleLocator(1))
		ax2.yaxis.set_major_locator(MaxNLocator(10))
		if n in [1, 5, 9, 13, 17, 21]: ax2.set_ylabel('Calories', color='b', fontsize=7)
		ax2.set_xlabel('Day', fontsize=7)
		
		axF2 = ax2.twinx() 
		axF2.set_ylim([min_feed, max_feed])
		if n in [4, 8, 12, 16, 20, 24]: axF2.set_ylabel('Grams', color='g', fontsize=7)

fig2.subplots_adjust(wspace=0.8, hspace=0.85)
pp.savefig(fig2)
if sec_fig == True:
	fig2B.subplots_adjust(wspace=0.8, hspace=0.85)
	pp.savefig(fig2B)
	
##
## Scale 
##
fig5 = plt.figure()
#fig5.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)
sec_fig = False
fig5.suptitle("Weight (kg)", fontsize=15)

min_wt, max_wt = 0, 0
for w in scale_avg.values(): 
	try: 
		ma = max(w)
		if ma > max_wt: 
			max_wt = ma
	except: 
		pass 

for n, (station, monkey) in enumerate(STATION_MONKEYS, start=1):

	# make sure that more than one day is being plotted
	if len(scale_avg[monkey]) > 1:
		scale_avg_line = [average(scale_avg[monkey]) \
						for d in range(len(scale_avg[monkey]))]
		if n <= 12:
			ax5 = fig5.add_subplot(rows, columns, n)
		elif n > 12 and n <= 24:
			if sec_fig == False:
				fig5B = plt.figure()
				fig5B.suptitle('Weight (kg)', fontsize=15)
			ax5 = fig5B.add_subplot(rows, columns, n - 12)
			sec_fig = True
		else:
			break

		l1, = ax5.plot(scale_avg[monkey])
		l2, = ax5.plot(scale_avg_line)
		ax5.set_xticklabels(range(no_days))
		leg = plt.figlegend((l1,l2),
							('Daily Average','Average'), \
							'upper right', prop=fontP)
		ax5.set_title(monkey+'-'+MONKEYS[monkey]['station'], fontsize=10)
		ax5.set_xlabel('Day', fontsize=7)
		plt.ylim(min_wt, max_wt)
		plt.gca().set_autoscale_on(False)
		ax5.xaxis.set_major_locator(MultipleLocator(1))
		ax5.yaxis.set_major_locator(MaxNLocator(10))

fig5.subplots_adjust(wspace=0.5, hspace=0.85)
pp.savefig(fig5)
if sec_fig == True:
	fig5B.subplots_adjust(wspace=0.5, hspace=0.85)
	pp.savefig(fig5B)

	
##
## Cognitive Test Success
##
fig3 = plt.figure()
sec_fig = False
fig3.suptitle("Cognitive Test: Success Rate", fontsize=15)
#fig3.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)

for n, (station, monkey) in enumerate(STATION_MONKEYS, start=1):

	if len(cognitive_stats[monkey]['succ_rate']) > 1:
		if n <= 12:
			ax3 = fig3.add_subplot(rows, columns, n)

		elif n > 12 and n <= 24:
			if sec_fig == False:
				fig3B = plt.figure()
				fig3B.suptitle("Cognitive Test: Success Rate", fontsize=15)
				#fig3B.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)
			ax3 = fig3B.add_subplot(rows, columns, n - 12)
			sec_fig = True
			
		else:
			break

		# plot the monkey success rate
		ax3.plot(cognitive_stats[monkey]['succ_rate'], 'b')
		for ticky in ax3.get_yticklabels():
			ticky.set_color('b')
		ax3.set_title(monkey+'-'+MONKEYS[monkey]['station'], fontsize=10)
		ax3.set_ylabel("Success (%)", fontsize=7, color='b')
		ymin, ymax = plt.ylim()
		plt.ylim(0, 100)
		ax32 = ax3.twinx()
		ax3.yaxis.set_major_locator(MaxNLocator(10))
		
		for ticky in ax32.get_yticklabels():
			ticky.set_color('m')
		for ticky in ax3.yaxis.get_major_ticks():
			ticky.label1.set_fontsize(7)
		for ticky in ax32.yaxis.get_major_ticks():
			ticky.label1.set_fontsize(7)

		ax3.set_xlabel('Day', fontsize=7)
		# plot number of pellets monkey collected from cognitive test (feeder 2)
		ax32.plot([ ct * feeder2_cals\
					for ct in feeder_counts[monkey]['2']], 'm')
		ax32.set_ylabel('Calories', fontsize=7, color='m')
		plt.ylim(ymin=0, ymax=1.25*max(feeder_counts[monkey]['2']))
		ax3.set_xticklabels(range(no_days))
		ax32.yaxis.set_major_locator(MaxNLocator(10))

fig3.subplots_adjust(wspace=1.0, hspace=0.85)
pp.savefig(fig3)

if sec_fig == True:
	fig3B.subplots_adjust(wspace=1.0, hspace=0.85)
	pp.savefig(fig3B)

##
## Cognitive Rxn Times 
##
fig4 = plt.figure()
sec_fig = False
fig4.suptitle("Cognitive Test: Avg Rxn Time (s)", fontsize=15)
#fig4.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)

for n, (station, monkey) in enumerate(STATION_MONKEYS, start=1):

	if len(cognitive_stats[monkey]['succ_rate']) > 1:
		if n <= 12:
			ax4 = fig4.add_subplot(rows, columns, n)

		elif n > 12 and n <= 24:
			if sec_fig == False:
				fig4B = plt.figure()
				fig4B.suptitle("Cognitive Test: Avg Rxn Time (s)", fontsize=15)
				#fig4B.suptitle(DAYS_STR, x=0.10, y=0.99, fontsize=7)

			ax4 = fig4B.add_subplot(rows, columns, n - 12)
			sec_fig = True

		else:
			break

		ax4.set_title(monkey+'-'+MONKEYS[monkey]['station'], fontsize=10)
		# plot avg rxn times for example, correct and incorrect test runs
		l1, = ax4.plot(cognitive_stats[monkey]['ex_rxn'], 'b')
		l2, = ax4.plot(cognitive_stats[monkey]['corr_rxn'], 'g')
		l3, = ax4.plot(cognitive_stats[monkey]['incorr_rxn'], 'm')
		plt.figlegend((l1,l2,l3), ('Example','Correct','Incorrect'), \
					  'upper right', prop=fontP)
		ax4.set_xticklabels(range(no_days))
		ax4.set_xlabel("Day", fontsize=7)
		ax4.set_ylabel("Avg Rxn Time (s)", fontsize=7)
		
		maxes = [max(cognitive_stats[monkey]['ex_rxn']),
			max(cognitive_stats[monkey]['corr_rxn']),
			max(cognitive_stats[monkey]['incorr_rxn'])]

		plt.ylim(ymin=0, ymax=1.25*max(maxes))  # remove ymax
		ax4.xaxis.set_major_locator(MultipleLocator(1))
		ax4.yaxis.set_major_locator(MaxNLocator(10))

fig4.subplots_adjust(wspace=0.5, hspace=0.85)

pp.savefig(fig4)
if sec_fig == True:
	fig4B.subplots_adjust(wspace=0.5, hspace=0.85)
	pp.savefig(fig4B)
pp.close()
############################################################################



############################################
##		Generate Individual Reports:	  ##
############################################ 
##	
## For each monkey, save Hourly Activity, Hourly Feeding and
## Cognitive Test Success plots to "PCRL Individual Report [MID] YYYYMMDD.pdf"
##
# transform tables to dict, with granularity to hours
activity = TableToHourlyDict(TABLES['activity'])
cognitive = TableToHourlyDict(TABLES['cognitive'])
feeder = TableToHourlyDict(TABLES['feeder'])
scale = TableToHourlyDict(TABLES['scale'])

# [field]_hr_totals: 	hourly total for all hrs recorded
# [field]_hr_avgs: 		hourly avg for 0-23 hrs
activity_hr_totals, activity_hr_avgs = {}, {}
cognitive_hr_totals, cognitive_hr_avgs = {}, {}
feeder_hr_totals, feeder_hr_avgs, feeding_hours = {}, {}, {}
feeding_hour_threshold = opts['feeding_hour_threshold']

for monkey in MONKEYS:
#
# Calculate Hourly Stats
#
	random.seed() 	# for simulating data
	station = MONKEYS[monkey]['station']
	## Activity
	activity_hr_totals[monkey] = []
	hr_totals = [0] * 24
	no_days = 0

	# loop thru each value of activity[monkey]
	# note: each val has corresponding date
	monk_activity = {} 
	try: 
		monk_activity = activity[monkey]
	except KeyError: 
		try: monk_activity = activity[station]
		except KeyError: pass
	for act_dict in monk_activity.values():

		# loop thru hours as strings, giving zero pad
		for hr in range(24):
			hr_str = '0' + str(hr) if hr < 10 else str(hr)	# zero pad

			# sum activity #s for that hour
			# and add to hr_totals at that hr's index
			try:
				# get total activity for that hour
				hr_total = sum([tup[1] for tup in act_dict[hr_str]])

				# append total to totals
				activity_hr_totals[monkey].append(hr_total)

				# add totals to total for that hr
				hr_totals[hr] += hr_total

			except KeyError:
				pass

		# count date
		no_days += 1

	# calc avg for each hr
	activity_hr_avgs[monkey] = [] 
	try: 
		activity_hr_avgs[monkey] = [total/no_days for total in hr_totals]
	except ZeroDivisionError: 
		activity_hr_avgs[monkey] = [0] * 24
		
		
	## Feeder
	no_days = 0
	feeder_hr_totals[monkey] = { '0':[], '1':[], '2':[] }
	feeding_hours[monkey] = {'day_num': [], 'hour': []}
	hr_sums = { '0': [0]*24, '1': [0]*24, '2': [0]*24 }
	
	monk_feeder = {} 
	try: 
		monk_feeder = feeder[monkey]
	except KeyError: 
		try: monk_feeder = feeder[station]
		except KeyError: pass
	for feed_dict in monk_feeder.values():
		# hourly counts for each feeder for that date
		hr_cts = {'0': [0]*24, '1': [0]*24, '2': [0]*24}

		# loop thru hours as strings, giving zero pad
		for hr in range(24):
			hr_str = '0' + str(hr) if hr < 10 else str(hr)	# zero pad

			try:
				for tup in feed_dict[hr_str]:
					feeder_num = str(tup[2])
					# record feed at feeder for that hr for that date
					hr_cts[feeder_num][hr] += 1
					# record feed at feeder for that hr
					hr_sums[feeder_num][hr] += 1
			except KeyError:
				pass

		# append hr counts for that date to totals
		feeder_hr_totals[monkey]['0'].extend(hr_cts['0'])
		feeder_hr_totals[monkey]['1'].extend(hr_cts['1'])
		feeder_hr_totals[monkey]['2'].extend(hr_cts['2'])

		# get hr total counts
		hr_total_cts = [x + y + z for x, y, z in zip(hr_cts['0'], \
													 hr_cts['1'], \
													 hr_cts['2'])]

		# find feeding hours, based on input threshold
		for hr, total_ct in enumerate(hr_total_cts):
			if total_ct > feeding_hour_threshold:
				feeding_hours[monkey]['day_num'].append(no_days)
				feeding_hours[monkey]['hour'].append(hr)

		# count date
		no_days += 1

	# calc and store avgs for each feeder
	
							  
	feeder_hr_avgs[monkey] = {}
	try: 
		feeder_hr_avgs[monkey] = {'0': [s/no_days for s in hr_sums['0']], \
							  '1': [s/no_days for s in hr_sums['1']], \
							  '2': [s/no_days for s in hr_sums['2']]}
	except ZeroDivisionError: 
		feeder_hr_avgs[monkey] = {'0':[0]*24, '1':[0]*24, '2':[0]*24}


	## Cognitive
	cognitive_hr_totals[monkey], cognitive_hr_avgs[monkey] = {}, {} 
	try: 
		cognitive_hr_totals[monkey], cognitive_hr_avgs[monkey] = \
								CalcHourlyCognitiveStats(cognitive[monkey])
	except KeyError: 
		try: cognitive_hr_totals[monkey], cognitive_hr_avgs[monkey] = \
								CalcHourlyCognitiveStats(cognitive[station])
		except KeyError: cognitive_hr_totals[monkey], cognitive_hr_avgs[monkey] = \
								CalcHourlyCognitiveStats({})

for n, monkey in enumerate(MONKEYS):
#
# Plot Results
#
	pp = PdfPages(REPORT_DIR + 'PCRL ' + monkey + \
				  ' Report ' + YESTERDAY_STR + '.pdf')

	##
	## Period Report
	##
	fig1 = plt.figure()
	fig1.suptitle("%s-%s Hourly Averages on %s" % (MONKEYS[monkey]['station'],\
													monkey, DAYS_STR), fontsize=15)
	#fig1.suptitle(DAYS_STR, x=0.1, y=0.99, fontsize=7)

	# TODO: add feeder 2 to "free pellets by hour"
	totals = [activity_hr_totals[monkey],\
			  feeder_hr_totals[monkey]['0'],\
			  feeder_hr_totals[monkey]['1'], \
			  cognitive_hr_totals[monkey]['succ_rate'], \
			  cognitive_hr_totals[monkey]['comp_succ_rate'], \
			  cognitive_hr_totals[monkey]['incomp_succ_rate'], \
			  cognitive_hr_totals[monkey]['comp_pellets'], \
			  cognitive_hr_totals[monkey]['incomp_pellets']]

	labels = [('Activity By Hour','Activity'), \
			  ('Free Pellets By Hour','Free Pellets'), \
			  ('Cognitive Pellets By Hour', 'Cognitive Pellets'), \
			  ('Cognitive Success Rate By Hour', 'Success (%)'), \
			  ('Complete-Test Success By Hour', 'Success (%)'), \
			  ('Incomplete-Test Success By Hour', 'Success (%)'), \
			  ('Total Complete-Test Dispenses', 'Dispenses'), \
			  ('Total Incomplete-Test Dispenses', 'Dispenses')]

	for n, total, label in zip(range(1,9), totals, labels):
		ax11 = fig1.add_subplot(3,3,n)

		if 'Success' in label[1]: 		ax11.plot(total, 'm-')
		elif 'Dispense' in label[1]: 	ax11.plot(total, 'g-')
		else: 							ax11.plot(total)

		ax11.set_xlabel("Hour", fontsize=7)
		ax11.set_ylabel(label[1], fontsize=7)
		ax11.set_title(label[0], fontsize=7)
		ax11.yaxis.set_major_locator(MaxNLocator(10))

		plt.xticks(fontsize=7)
		plt.yticks(fontsize=7)
		plt.xlim(xmin=0, xmax=len(total))
		try: 	plt.ylim(ymax=1.25*max(total))
		except: pass
		
	## Feeding Hours 
	ax11 = fig1.add_subplot(3,3,9)
	ax11.plot(feeding_hours[monkey]['hour'], \
			  feeding_hours[monkey]['day_num'], '.')
	ax11.set_title("Feeding Hours Over Days", fontsize=7)
	ax11.set_xlabel("Hour", fontsize=7)
	ax11.set_ylabel("Day", fontsize=7)

	plt.ylim(ymin=-1, ymax=len(DATES))
	plt.ylim(ax11.get_ylim()[::-1])
	plt.xlim(0, 24)
	plt.xticks(filter(lambda x: x%2==0, range(24)), fontsize=7)

	##
	## Yesterday's Report 
	##
	fig2 = plt.figure()
	fig2.suptitle("%s Hourly Averages on %s" % (monkey, YESTERDAY_STR), fontsize=15)
	#fig2.suptitle(YESTERDAY_STR, x=0.1, y=0.99, fontsize=7)

	# add feeder 2 to "free pellets by hour"
	avgs = [activity_hr_avgs[monkey],\
			feeder_hr_avgs[monkey]['0'],\ 
			feeder_hr_avgs[monkey]['1'], \
			cognitive_hr_avgs[monkey]['succ_rate'], \
			cognitive_hr_avgs[monkey]['comp_succ_rate'], \
			cognitive_hr_avgs[monkey]['incomp_succ_rate'], \
			cognitive_hr_avgs[monkey]['comp_pellets'], \
			cognitive_hr_avgs[monkey]['incomp_pellets']]

	labels = [('Activity','Avg Activity'), \
			  ('Free Pellets By Hour','Avg Free Pellets'), \
			  ('Cognitive Pellets', 'Avg Cognitive Pellets'), \
			  ('Cognitive Test Success', 'Avg Success (%)'), \
			  ('Complete-Test Success', 'Avg Success (%)'), \
			  ('Incomplete-Test Success', 'Avg Success (%)'), \
			  ('Complete-Test Dispenses', 'Dispenses'), \
			  ('Incomplete-Test Dispenses', 'Dispenses')]

	for n, avg, label in zip(range(1,9), avgs, labels):
		ax22 = fig2.add_subplot(3, 3, n)

		if "Success" in label[0]: 		ax22.plot(avg, 'm-')
		elif "Dispenses" in label[0]: 	ax22.plot(avg, 'g-')
		else: 							ax22.plot(avg)

		ax22.set_title(label[0], fontsize=7)
		ax22.set_ylabel(label[1], fontsize=7)
		ax22.set_xlabel("Hour", fontsize=7)

		plt.xlim(0, 24)
		plt.ylim(ymin=0, ymax=1.25*max(avg))
		plt.xticks(filter(lambda x: x%2==0, range(24)), fontsize=7)
		plt.yticks(fontsize=7)

	## Latest Feeding Hours 
	latest_feeding_times = [hr for hr, day in \
							zip(feeding_hours[monkey]['hour'], \
								feeding_hours[monkey]['day_num']) \
							if day == max(feeding_hours[monkey]['day_num'])]
	
	ax22 = fig2.add_subplot(3, 3, 9)
	ax22.plot(latest_feeding_times, len(latest_feeding_times)*[1], '.')
	ax22.set_xlabel("Hour", fontsize=7)
	#ax22.set_ylabel("Feeding Times", fontsize=7)
	ax22.set_title("Yesterday's Feeding Times", fontsize=7)
	ax22.set_yticks([])
	plt.xticks(filter(lambda x:x%2==0, range(24)), fontsize = 6)

	fig1.subplots_adjust(wspace=0.5, hspace=0.5)
	fig2.subplots_adjust(wspace=0.5, hspace=0.5)
	pp.savefig(fig1)
	pp.savefig(fig2)
	pp.close()
######################################################################


##	Finish Reports: 
## 	(a) build group summary page and merges with group plot page; 
##	(b) for each monkey, build observational report summary page, and merge 
## 	with monkey's individual plots
##
## calc group stats
activity_max = {m: int(max(avgs)) if not np.isnan(mean(avgs)) else 0 \
					for m, avgs in activity_norm.items()}
#activity_var  = {m: int(std(avgs)**2) if not np.isnan(std(avgs)) else 0 \
#					for m, avgs in activity_avg.items()}
act_ranks = mstats.rankdata(np.ma.masked_invalid(activity_max.values()))
activity_ranks = {m: int(len(act_ranks)-rank+1) for m, rank in zip(activity_max, list(act_ranks))}

feed_mean = {m: int(mean(avgs)) if not np.isnan(mean(avgs)) else 0 \
					for m, avgs in feed_sums.items()}
feed_var  = {m: int(std(avgs)) if not np.isnan(mean(avgs)) else 0 \
					for m, avgs in feed_sums.items()}
feed_counts = {m: int(mean(feeder_counts[m]['total'])) \
					if not np.isnan(mean(feeder_counts[m]['total'])) else 0 \
					for m in feed_sums.keys()}
fd_ranks = mstats.rankdata(np.ma.masked_invalid(feed_mean.values()))
feed_ranks = {m: int(len(fd_ranks)-rank+1) for m, rank in zip(feed_mean, list(fd_ranks))}

## (a) Group Report
lab_member = "admin"
datetime_str = datetime.datetime.now().strftime("%I:%M%p on %B %d, %Y")

# generate observational report  in HTML
pg = HTML()
body = pg.body(style="font-family:Sans-Serif")
header = body.header()
header.h2("GROUP REPORT  for " + YESTERDAY_STR, style="text-align:center")
header.img(width="120", height="50", src=base_dir+"\\scripts\\bu_logo.jpg")

p = header.p(style="text-align:center")
p.b.text("Primate Circadian Rhythm Laboratory"); p.br;
p.text("Dept. of Anatomy and Neurobiology"); p.br;
p.text("Boston University School of Medicine"); p.br;
p.text("72 East Concord St (L 1004)"); p.br;
p.text("Boston, Massachusetts 02118"); p.br;
p.text("617-638-4200"); p.br;
p.a("zhdanova@bu.edu", href="mailto:zhdanova@bu.edu")

body.hr()	# line separator

# stats summary table
# TODO:
# 
# a) change mean for both grams and kcals to yesterday total 
# b) take away std and rank
# c) separate supplemental and feeder 
# 7 total columns: 2 per feeder + 1 cals supplemental 
#
body.section().h2("Summary:")
sec = body.section()
t = sec.table()
r = t.tr()		# add column names
for col in ['Monkey','Mean Daily Feeding (g)','Mean Daily Calories (kcals)',\
			'Std Daily Calories (kcals)','Mean Calories Rank']: 
	r.td.b(col)
# add stats
for m in sorted(feed_ranks, key=feed_ranks.get): 
	r = t.tr()
	for s in [m] + map(lambda f: str(f) if not np.isnan(f) else 'N/a',\
							[feed_counts[m],feed_mean[m],feed_var[m],feed_ranks[m]]): 
		r.td(s)

body.br
t = body.table
r = t.tr()
r = t.tr()
r.td("Electronically Signed By:")
r.td(lab_member)
r.td(datetime_str)

#write HTML to pdf
f = open(REPORT_DIR +"group_summary.pdf", 'wb')
pisa.showLogging()
pisa.CreatePDF(str(pg), dest=f)
f.close()

# merge with group plots pdf
input1 = open(REPORT_DIR + "group_summary.pdf", 'rb')
input2 = open(REPORT_DIR + 'PCRL Group Report ' +\
		YESTERDAY_STR + '.pdf', "rb")
merger = PyPDF2.PdfFileMerger()
merger.append(input1)
merger.append(input2)
output = open(REPORT_DIR + 'PCRL Group Report ' +\
			YESTERDAY_STR + '.pdf', 'wb')
merger.write(output)
output.close() 
input1.close() 
input2.close()
os.remove(REPORT_DIR +"group_summary.pdf")


## (b) Individual Reports
# import observation data from tables
observations = TABLES['observation']

# for each monkey
for monk, data in MONKEYS.items():
	mid = monk
	dob = data['dob']
	sex = data['sex']
	lab_member = "admin"
	datetime_str = datetime.datetime.now().strftime("%I:%M%p on %B %d, %Y")
	
	# generate observational report  in HTML
	pg = HTML()
	body = pg.body(style="font-family:Sans-Serif")
	header = body.header()
	header.h2("REPORT for " + mid + " on " + YESTERDAY_STR, style="text-align:center")
	header.img(width="120", height="50", src=base_dir+"\\scripts\\bu_logo.jpg")

	p = header.p(style="text-align:center")
	p.b.text("Primate Circadian Rhythm Laboratory"); p.br;
	p.text("Dept. of Anatomy and Neurobiology"); p.br;
	p.text("Boston University School of Medicine"); p.br;
	p.text("72 East Concord St (L 1004)"); p.br;
	p.text("Boston, Massachusetts 02118"); p.br;
	p.text("617-638-4200"); p.br;
	p.a("zhdanova@bu.edu", href="mailto:zhdanova@bu.edu")

	table = body.section().table(style="margin-left:130px;", cellspacing="0", cellpadding="0")
	rows = [("MID: " + mid, "Report No: " + YESTERDAY_STR), \
			("DOB: " + dob, "Sex: M"), \
			("Compiled by: " + lab_member, "Datetime: " + datetime_str)]
	for row in rows:
		r = table.tr()
		r.td(row[0])
		r.td(row[1])

	body.hr()	# horizontal line

	# observation summary table
	body.section().h2("Observations:")
	sec = body.section()
	t = sec.table()
	r = t.tr()		# add column names
	for col, name in enumerate(['Datetime','Lab member','Labroom','Behavior','Stool',\
				'Training','Comments','Equipment Deficiencies']): 
		if col in [1,2,3,4]: 	r.td(style='width=50').b(name)
		else: 					r.td.b(name)
	# add observations
	for observation in [o for o in observations if monk == o[-1]]: 
		r = t.tr()
		for col, o in enumerate(observation[:-1]): 
			if col in [1,2,3,4]: 	r.td(o, style='width=50')
			else: 					r.td(o)
			
	## Add statistical summaries table HERE
	
	body.br
	t = body.table()
	t.tr()
	r = t.tr()
	r.td("Electronically Signed By:")
	r.td(lab_member)
	r.td(datetime_str)

	#write HTML to pdf
	f = open(REPORT_DIR + mid +"-observational.pdf", 'wb')
	pisa.showLogging()
	pisa.CreatePDF(str(pg), dest=f)
	f.close()

	input1 = open(REPORT_DIR + mid +"-observational.pdf", 'rb')
	input2 = open(REPORT_DIR + 'PCRL ' + monk + \
		  ' Report ' + YESTERDAY_STR + '.pdf', "rb")

	merger = PyPDF2.PdfFileMerger()
	merger.append(input1)
	merger.append(input2)
	output = open(REPORT_DIR + 'PCRL ' + monk + \
		  ' Report ' + YESTERDAY_STR + '.pdf', 'wb')
	merger.write(output)
	output.close() 
	input1.close() 
	input2.close()
	os.remove(REPORT_DIR + mid +"-observational.pdf")


