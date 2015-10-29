import os
import sys
import time
import json
import string
import calendar
from numpy import *
import datetime as dt
from datetime import datetime

'''
class MyFrame(wx.Frame):
	def __init__(self, parent, title):
		wx.Frame.__init__(self, parent, title=title, size=(1300,700))
		wx.StaticText(self, label="T or Tau", pos=(120, 10))
		wx.StaticText(self, label="Date", pos=(240, 10))
		wx.StaticText(self, label="Subj Day Start", pos=(305, 10))
		wx.StaticText(self, label="Subj Day End", pos=(400, 10))

		self.subj_incr = 'NA'
		wx.StaticText(self, label="Increment (minutes)", pos=(1030, 10))
		self.Increment = wxMasked.NumCtrl(self, pos = (1030, 30), size=wx.Size(10, 10), allowNegative=False)
		self.Bind(wx.EVT_TEXT, self.Get_Increment, self.Increment)

		self.window, self.IPI, self.min_pel = 0, 0, 0

		wx.StaticText(self, label="Past N Days (with last day)", pos=(650, 20))
		if o_options['Past N Days'] == 'NA': self.Window = wxMasked.NumCtrl(self, 115, pos=(650, 40), allowNegative=False)
		else:
			self.Window = wxMasked.NumCtrl(self, 115, pos=(650, 40), allowNegative=False, value=o_options['Past N Days'])
			self.window = o_options['Past N Days']
		self.Bind(wx.EVT_TEXT, self.Window_Days, self.Window)

		wx.StaticText(self, label="Bout Definition: Min Pellets", pos=(650, 80))
		if o_options['Min Weight'] == 'NA': self.Min = wxMasked.NumCtrl(self, 125, pos=(650, 100 ), allowNegative=False)
		else:
			self.Min = wxMasked.NumCtrl(self, 125, pos=(650, 100 ), allowNegative=False, value=o_options['Min Weight'])
			self.min_pel = o_options['Min Weight']
		self.Bind(wx.EVT_TEXT, self.Min_Pel, self.Min)

		wx.StaticText(self, label="Bout Definition: Max IPI (minutes)", pos=(800, 80))
		if o_options['Max IPI'] == 'NA':  self.mIPI = wxMasked.NumCtrl(self, 135, pos=(800, 100 ), allowNegative=False)
		else:
			self.mIPI = wxMasked.NumCtrl(self, 135, pos=(800, 100 ), allowNegative=False, value=o_options['Max IPI'])
			self.IPI = value=o_options['Max IPI']
		self.Bind(wx.EVT_TEXT, self.Max_IPI, self.mIPI)

		#wx.StaticText(self, label="Remarks", pos=(750, 400))
		#wx.StaticText(self, label="Date", pos=(687,425))
		#wx.StaticText(self, label="Time", pos=(815,425))
		#wx.StaticText(self, label="Monkey", pos=(935,425))
		#wx.StaticText(self, label="Remark", pos=(1055,425))

		#self.new_remark_date = wxMasked.NumCtrl(self, 3000, pos=(650, 440), allowNegative=False, groupDigits=False)
		#self.Bind(wx.EVT_TEXT, self.NRD, self.new_remark_date)
		#self.new_remark_time = wxMasked.NumCtrl(self, 3001, pos=(775, 440), allowNegative=False, groupDigits=False)
		#self.Bind(wx.EVT_TEXT, self.NRT, self.new_remark_time)
		#self.new_remark_monkey = wx.TextCtrl(self, 3002, pos=(900, 440))
		#self.Bind(wx.EVT_TEXT, self.NRM, self.new_remark_monkey)
		#self.new_remark_remark = wx.TextCtrl(self, 3003, pos=(1025, 440))
		#self.Bind(wx.EVT_TEXT, self.NRR, self.new_remark_remark)
		#self.lock_remark = wx.Button(self, label="Save Remark", pos=(550, 440))
		#self.Bind(wx.EVT_BUTTON, self.Lock_Remark, self.lock_remark)

		# Mac or PC
		if o_options['Comp'] == 'NA':
			self.default = 0
			self.comp = 'Mac'
		elif o_options['Comp'] == 'PC':
			self.default = 1
			self.comp = 'PC'
		elif o_options['Comp'] == 'Mac':
			self.default = 0
			self.comp = 'Mac'

		radioList = (['Mac', 'PC'], ['PC', 'Mac'])
		rb = wx.RadioBox(self, label="Mac or PC?", pos=(620, 167), choices=radioList[self.default])
		self.Bind(wx.EVT_RADIOBOX, self.EvtRadioBox, rb)
		self.version = '2003'

		# Open schedule file
		self.sked = wx.Button(self, label="Choose Schedule File", pos=(850,165))
		self.Bind(wx.EVT_BUTTON, self.OnOpen, self.sked)
		if o_options['Sked'] == 'NA': self.wxFile = wx.StaticText(self, label='No file chosen', pos=(850, 190))
		else:
			self.wxFile = wx.StaticText(self, label=o_options['Sked'], pos=(850, 190))
			self.filename = o_options['Sked'].split('\\')[-1]
			self.dirname = string.join(o_options['Sked'].split('\\')[:-1], '\\')
			self.sked_file_name = o_options['Sked']

			if '.txt' in o_options['Sked']:
				self.fp = open(o_options['Sked'], 'r')
				self.whole_sked = ''
				for line in self.fp:
					line = line.replace(' ', '')
					self.whole_sked += line.replace('\t', '-').replace('"', '').replace(',', '#').strip() + '~'

		# Choose directory where monkey files are
		self.mon_dir = wx.Button(self, label="Choose Directory For Monkey Files", pos=(850, 225))
		self.Bind(wx.EVT_BUTTON, self.MonkeyDir, self.mon_dir)
		if o_options['FilePath'] == 'NA':
			self.wxDir = wx.StaticText(self, label="No directory chosen", pos=(850, 250))
		else:
			self.wxDir = wx.StaticText(self, label=o_options['FilePath'], pos=(850, 250))
			self.base_data = o_options['FilePath']

		# Choose output directory
		self.out_dir = wx.Button(self, label="Choose Output Directory", pos=(850, 290))
		self.Bind(wx.EVT_BUTTON, self.OutputDir, self.out_dir)
		if o_options['OutPath'] == 'NA': self.wxOutDir = wx.StaticText(self, label="No directory chosen", pos=(850, 315))
		else:
			self.wxOutDir = wx.StaticText(self, label=o_options['OutPath'], pos=(850, 315))
			self.output_dir = o_options['OutPath']

		# Choose Log File Directory
		self.mdb_dir = wx.Button(self, label="Choose Directory For Log Files", pos=(850, 365))
		self.Bind(wx.EVT_BUTTON, self.Access_Dir, self.mdb_dir)
		if o_options['MDB Dir'] == 'NA': self.wxMdbDir = wx.StaticText(self, label="No directory chosen", pos=(850, 390))
		else:
			self.wxMdbDir = wx.StaticText(self, label=o_options['MDB Dir'], pos=(850, 390))
			self.MDB_DIR = o_options['MDB Dir']

		self.Taus = {}
		self.St_Date = {}
		self.Names = {}
		self.St_Times = {}
		self.End_Times = {}
		self.Tau_Check = {}
		self.Local_Check = True

		wx.StaticText(self, label="example:                                                       YYYYMMDD             military time           military time", pos=(10, 630))
		for self.i,self.line in enumerate(p):
			self.Names[self.i] = self.line.strip()

			wx.StaticText(self, label=self.line, pos=(10,30+25*self.i))
			self.control = wxMasked.NumCtrl(self, self.i, pos=(80,30+25*self.i), allowNegative=False, value=24, decimalChar='.', fractionWidth=2)
			self.Bind(wx.EVT_TEXT, self.GetTau, self.control, id=self.i)

			self.control2 = wxMasked.NumCtrl(self, self.i+1000, pos=(210, 30+25*self.i), allowNegative=False, groupDigits=False)
			self.Bind(wx.EVT_TEXT, self.GetStDate, self.control2, id=self.i+1000)

			self.control3 = wxMasked.NumCtrl(self, self.i+1051, pos=(300, 30+25*self.i), allowNegative=False, groupDigits=False)
			self.Bind(wx.EVT_TEXT, self.GetStTime, self.control3, id=self.i+1051)

			self.control4 = wxMasked.NumCtrl(self, self.i+2100, pos=(390, 30+25*self.i), allowNegative=False, groupDigits=False)
			self.Bind(wx.EVT_TEXT, self.GetEndTime, self.control4, id=self.i+2100)

			self.Chars = wx.CheckBox(self, self.i+2000, label="Tau", pos=(480 ,35+25*self.i))
			self.Bind(wx.EVT_CHECKBOX, self.New_Chars, self.Chars, id=self.i+2000)
			self.Tau_Check[self.line +'_'+ str(self.i+2000)] = False

		self.execute = wx.Button(self, label="Execute", pos=(727, 167), size=(100,50))
		self.Bind(wx.EVT_BUTTON, self.Execute, self.execute)

		self.quit = wx.Button(self, label="Quit", pos=(740, 230))
		self.Bind(wx.EVT_BUTTON, self.Quit, self.quit)

		self.set_vals = wx.Button(self, label="Set Values", pos=(640, 230))
		self.Bind(wx.EVT_BUTTON, self.Set_Values, self.set_vals)

		wx.StaticText(self, label='Custom Chosen End Date', pos=(680, 300))
		wx.StaticText(self, label='Year', pos=(690, 330))
		wx.StaticText(self, label='Month', pos=(735, 330))
		wx.StaticText(self, label='Day', pos=(775, 330))

		self.years = map(lambda x: str(x), range(1999, 2100)[1:])
		self.endyear = wx.ComboBox(self, pos=(670, 350), size=(55, -1), choices=self.years, style=wx.CB_READONLY)
		self.Bind(wx.EVT_TEXT, self.END_YEAR, self.endyear)

		self.months= map(lambda x: str(x), range(1)[1:])
		self.endmonth = wx.ComboBox(self, pos=(730, 350), size=(35, -1), choices=self.months, style=wx.CB_READONLY)
		self.Bind(wx.EVT_TEXT, self.END_MONTH, self.endmonth)

		self.days = map(lambda x:str(x), range(1)[1:])
		self.endday   = wx.ComboBox(self, pos=(770, 350), size=(40, -1), choices=self.days, style=wx.CB_READONLY)
		self.Bind(wx.EVT_TEXT, self.END_DAY, self.endday)

		#self.local = wx.CheckBox(self, label="Execution For Specified Date", pos=(820,352))
		#self.Bind(wx.EVT_CHECKBOX, self.Local, self.local)


	def Get_Increment(self, event):
		self.subj_incr = str(int(event.GetString()))

	def Access_Dir(self, event):
		dlg = wx.DirDialog(self, message = "Choose directory", style=wx.DD_DEFAULT_STYLE|wx.DD_NEW_DIR_BUTTON)
		if dlg.ShowModal() == wx.ID_OK:
			self.wxMdbDir.Destroy()
			self.MDB_DIR = dlg.GetPath()
			self.wxMdbDir = wx.StaticText(self, label=self.MDB_DIR, pos=(850, 390))

	def OutputDir(self, event):
		dlg = wx.DirDialog(self, message = "Choose directory", style=wx.DD_DEFAULT_STYLE|wx.DD_NEW_DIR_BUTTON)
		if dlg.ShowModal() == wx.ID_OK:
			self.wxOutDir.Destroy()
			self.output_dir = dlg.GetPath()
			self.wxOutDir = wx.StaticText(self, label=self.output_dir, pos=(850, 315))

	def EvtRadioBox(self, event):
		self.comp = event.GetString()

	def Check_Sked(self):
		sked_error = False
		message = []
		all_dates = []
		split_sked = self.whole_sked.split('-~')

		for c, line in enumerate(split_sked):
			line1 = line.split('-')
			if c==0:                   # make sure the header is right
				if line1[0].strip() != "Date":
					sked_error = True
					message.append('First row, first column must read "Date" ')
					break

				elif line1[1].strip() != "CC":
					sked_error = True
					message.append('First row, second column must read "CC" ')

				elif line1[2].strip() != "PanWash":
					sked_error = True
					message.append('First row, third column must read "PanWash" ')

				elif line1[3].strip() != "Noise":
					sked_error = True
					message.append('First row, fourth column must read "Noise" ')

				elif line1[4].strip() != "FoodChange":
					sked_error = True
					message.append('First row, fifth column must read "FoodChange" ')

				elif line1[5].strip() != "PowerOutage":
					sked_error = True
					message.append('First row, sixth column must read "PowerOutage" ')

				elif line1[6].strip() != "Trouble":
					sked_error = True
					message.append('First row, seventh column must read "Trouble" ')

				elif line1[7].strip() != "CageWash":
					sked_error = True
					message.append('First row, eighth column must read "Trouble" ')

				for d, cell in enumerate(line1[8:]):
					if d%3==0:
						if cell.strip() not in p:
							sked_error = True
							message.append('Monkey on schedule not in monkey list')
							break
					elif d%3==1:
						if cell[-2:] != '_C':
							sked_error = True
							message.append('Titles for cage columns must end in: "_C"')
							break

						elif cell[:-2] != line1[d+7]:
							sked_error = True
							message.append('Consecutive cells in the header must contain the monkey name in the first cell, and the monkey name followed by "_C" in the second cell')
							break
					elif d%3==2:
						if cell.split('_')[1] != "Procedure":
							sked_error = True
							message.append('Titles for procedure columns must end in: "_Procedure" ')

			else: # make sure entries in the schedule are correct
				used_cages = []               # clear at every lines
				for d, cell in enumerate(line1):
					if (d==0) and (len(cell)>0):  # make sure the first entry in each line is a date in the proper format
						if '.' not in line1[d]:   # if no dots are in the date
							message.append("Date has to be in format MM.DD.YYYY")
							sked_error = True
							break

						elif len(line1[d].split('.')) != 3:   # if date is entered in improper format
							message.append("Date has to be in format MM.DD.YYYY")
							sked_error = True
							break

						else:
							try:
								map(lambda x: int(x), line1[d].split('.'))  # if non-numerical values are in the date
							except ValueError:
								message.append("Non-numerical values entered in at least one date")
								sked_error = True
								break
							all_dates.append(cell)

					elif d in (1,2,3,4,5,6,7):  # make proper external stuff going in to the right format
						if cell != "None":
							cell2 = cell.split('#')
							try:
								for cell3 in cell2:
									if int(cell3[:-2]) > 23:
										message.append("Time of event needs to be between 0000-2400")
										sked_error = True

									elif int(cell3[-2:]) > 59:
										message.append("Time of event needs to be between 0000-2400")
										sked_error = True

									elif len(cell3) > 4:
										message.append("Time of event needs to be between 0000-2400")
										sked_error = True
							except ValueError:
								message.append("Time of CC, PanWash, Noise, FoodChange, PowerOutage, Trouble, and CageWash must be in numerical form")
								sked_error = True
					elif d>=8:
						if d%3 == 2:
							if cell != 'CDL':
								if str(cell) == "None": continue
								LD = cell.split('#')
								for ld in LD:
									if (ld[-1] not in ('L', 'D')) or (int(ld[:-1]) > 23):
										message.append("LD conditions not in the right format")
										sked_error = True
						elif d%3 == 0:
							try:
								int(cell)
							except:
								if str(cell) == "None": continue
								message.append("Cage must be a number between 0-24")
								sked_error = True
								break
							if int(cell) > 24:
								message.append("Cage number cannot be greater than 24")
								sked_error = True
								break
							else:
								if int(cell) in used_cages:
									message.append("Cannot have two or more monkeys occupying the same cage")
									sked_error = True
									break
								else: used_cages.append(int(cell))
						elif (d%3 == 1) and (cell != 'None'):
							treatments = cell.split('+')
							for tr in treatments:
								if tr[0:2] not in symbols.keys():
									message.append("Unexpected symbol in treatment column")
									sked_error = True
								elif tr[0:2] == 'MT':
									if (int(tr[-4:-2]) > 24) or (int(tr[-2:]) > 59):
										message.append("Treatment time must be in military time")
										sked_error = True
								elif tr[0:2] != 'MT':
									if (int(tr[2:4]) > 24) or (int(tr[4:6]) > 59):
										message.append("Treatment time must be in military time")
										sked_error = True

		all_dates = [datetime.strptime(d, "%m.%d.%Y") for d in all_dates]
		all_dates = set([d.toordinal() for d in all_dates])
		if max(all_dates) - min(all_dates) != len(all_dates) - 1:
			message.append("Schedule Dates are not consecutive")
			sked_error = True
		return sked_error, message

	def Lock_Remark(self, event):
		error = False
		try:
			e.Destroy()
		except NameError: pass
		except AttributeError: pass

		try:
			self.new_remark_date1, self.new_remark_time1, self.new_remark_monkey1, self.new_remark_remark1
		except NameError:
			e = wx.StaticText(self, label="Not all remark parameters entered", pos=(550, 465))
			error = True
		except AttributeError:
			e = wx.StaticText(self, label="Not all remark parameters entered", pos=(550, 465))
			error = True

		if error == False:
			if len(self.new_remark_date1) != 8: e = wx.StaticText(self, label="Date needs to be in YYYYMMDD format", pos=(550, 465))
			elif (len(self.new_remark_time1) == 4) and (int(self.new_remark_time1[0:2]) > 23): e = wx.StaticText(self, label="Time is written wrong", pos=(550, 465))
			elif (len(self.new_remark_time1) > 1) and (int(self.new_remark_time1[-2:]) > 59): e = wx.StaticText(self, label="Time is written wrong", pos=(550, 465))
			elif self.new_remark_monkey1 not in p: e = wx.StaticText(self, label="Monkey not in List", pos=(550, 465))

			else:
				if self.Local_Check == False:
					fp_recur = open("Remarks_Recursive.txt", 'a')
					fp_recur.write(string.join([self.new_remark_date1, self.new_remark_time1, self.new_remark_monkey1, self.new_remark_remark1], '\t'))
					fp_recur.write('\n')
					fp_recur.close()

				elif self.Local_Check == True:
					fp_local = open("Remarks_Specific.txt", 'a')
					fp_local.write(string.join([self.new_remark_date1, self.new_remark_time1, self.new_remark_monkey1, self.new_remark_remark1], '\t'))
					fp_local.write('\n')
					fp_local.close()

				self.new_remark_date.Destroy()
				self.new_remark_time.Destroy()
				self.new_remark_monkey.Destroy()
				self.new_remark_remark.Destroy()
				self.new_remark_date = wxMasked.NumCtrl(self, 3000, pos=(650, 440), allowNegative=False, groupDigits=False)
				self.Bind(wx.EVT_TEXT, self.NRD, self.new_remark_date)
				self.new_remark_time = wxMasked.NumCtrl(self, 3001, pos=(775, 440), allowNegative=False, groupDigits=False)
				self.Bind(wx.EVT_TEXT, self.NRT, self.new_remark_time)
				self.new_remark_monkey = wx.TextCtrl(self, 3002, pos=(900, 440))
				self.Bind(wx.EVT_TEXT, self.NRM, self.new_remark_monkey)
				self.new_remark_remark = wx.TextCtrl(self, 3003, pos=(1025, 440))
				self.Bind(wx.EVT_TEXT, self.NRR, self.new_remark_remark)

	def NRD(self, event):
		self.new_remark_date1 = event.GetString().strip()

	def NRT(self, event):
		self.new_remark_time1 = event.GetString().strip()

	def NRM(self, event):
		self.new_remark_monkey1 = event.GetString().strip()

	def NRR(self, event):
		self.new_remark_remark1 = event.GetString().strip()

	def Set_Values(self, event):
		try:
			(self.window, self.min_pel, self.IPI, self.filename, self.base_data, self.comp, self.output_dir)
		except NameError:
			try:
				self.p.kill()
			except NameError: pass
			except AttributeError: pass
			sys.exit("Specify all necessary values before locking them")
		except AttributeError:
			try:
				self.p.kill()
			except NameError: pass
			except AttributeError: pass
			sys.exit("Specify all necessary values before locking them")

		op = open("Options.txt", 'w')
		op.write('Past N Days: %s\n' % self.window)
		op.write('Min Weight: %s\n' % self.min_pel)
		op.write('Max IPI: %s\n' % self.IPI)
		op.write('Sked: %s\n' % os.path.join(self.dirname, self.filename))
		op.write('FilePath: %s\n' % self.base_data)
		op.write('Comp: %s\n' % self.comp)
		op.write('OutPath: %s\n' % self.output_dir)
		op.write('MDB Dir: %s\n' % self.MDB_DIR)

	def MonkeyDir(self, event):
		dlg = wx.DirDialog(self, message = "Choose directory", style=wx.DD_DEFAULT_STYLE|wx.DD_NEW_DIR_BUTTON)
		if dlg.ShowModal() == wx.ID_OK:
			self.wxDir.Destroy()
			self.base_data = dlg.GetPath()
			self.wxDir = wx.StaticText(self, label=self.base_data, pos=(850, 250))

	def OnOpen(self, event):
		self.dirname = ''
		dlg = wx.FileDialog(self, "Choose a file", self.dirname, "", "*.*", wx.OPEN)

		if dlg.ShowModal() == wx.ID_OK:
			self.wxFile.Destroy()
			self.filename = dlg.GetFilename()
			self.dirname = dlg.GetDirectory()

			if '.txt' in self.filename:
				self.fp = open(os.path.join(self.dirname, self.filename), 'r')
				self.whole_sked = ''
				self.wxFile = wx.StaticText(self, label=os.path.join(self.dirname, self.filename), pos=(850, 190))
				self.sked_file_name = os.path.join(self.dirname, self.filename)
				for line in self.fp:
					line = line.replace(' ', '')
					self.whole_sked += line.replace('\t', '-').replace('"', '').replace(',', '#').strip() + '~'

			elif '.xlsx' in self.filename:
				self.fp = load_workbook(os.path.join(self.dirname, self.filename))
				self.whole_sked = ''
				self.wxFile = wx.StaticText(self, label=os.path.join(self.dirname, self.filename), pos=(850, 190))
				ws2 = self.fp.get_sheet_by_name("Sheet1")

				start_r, start_c = 0, 0
				while ws2.cell(row=start_r,column=0).value != None:
					start_r += 1
				while ws2.cell(row=0, column=start_c).value != None:
					start_c += 1

				for row_r in range(0, start_r):
					for col_c in range(0, start_c):
						self.whole_sked += str(ws2.cell(row=row_r, column=col_c).value).replace('"', '').replace(',', '#').replace(' ', '').strip() +'-'
					self.whole_sked += '~'
		dlg.Destroy()

	def Quit(self, event):
		try:
			self.p.kill()
		except NameError: pass
		except AttributeError: pass
		sys.exit()

	def END_YEAR(self, event):
		self.end_year = event.GetString()

		try:
			self.endmonth.Destroy()
		except: pass
		self.months= map(lambda x:str(x), range(13)[1:])
		self.end_month = wx.ComboBox(self, pos=(730, 350), size=(35, -1), choices=self.months, style=wx.CB_READONLY)
		self.Bind(wx.EVT_TEXT, self.END_MONTH, self.end_month)

	def END_MONTH(self, event):
		self.end_month = event.GetString()

		self.endday.Destroy()
		self.days = map(lambda x:str(x), range(1+ calendar.monthrange(int(self.end_year), int(self.end_month))[1] )[1:])
		self.endday   = wx.ComboBox(self, pos=(770, 350), size=(40, -1), choices=self.days, style=wx.CB_READONLY)
		self.Bind(wx.EVT_TEXT, self.END_DAY, self.endday)

	def END_DAY(self, event):
		self.end_day = event.GetString()

	def Window_Days(self, event):
		self.window = str(int(event.GetString()))

	def Min_Pel(self, event):
		self.min_pel = str(int(event.GetString()))

	def Max_IPI(self, event):
		self.IPI = str(int(event.GetString()))

	def Execute(self, event):
		self.Local_Check = True
		try:
			self.whole_sked
			self.base_data
			self.output_dir
			self.MDB_DIR
		except AttributeError: sys.exit("Specify Schedule File and/or Certain Directories")
		except NameError: sys.exit("Specify Schedule File and/or Certain Directories")

		sked_error, message = self.Check_Sked()
		if sked_error == True:
			try:
				self.p.kill()
			except AttributeError: pass
			except NameError: pass
			msg_error = wx.StaticText(self, label=message[0], pos=(547,567))
			print message[0]
			sys.exit()

		elif sked_error == False:
			error = False

			try:
				self.p.kill()
			except AttributeError: pass
			except NameError: pass

			for t in self.Tau_Check.keys():
				if self.Tau_Check[t] == True:
					monkey = t.split('_')[0]

					if self.subj_incr == 'NA':
						error = True
						message = "Increment Not Entered"

					if sum([len(filter(lambda x:(monkey in x), self.St_Date.keys())), len(filter(lambda x:(monkey in x), self.St_Times.keys())), len(filter(lambda x:(monkey in x), self.End_Times.keys()))]) not in (0, 3):
						error = True
						message = "Either enter all information Date, Subj Day Start, and Subj Day End or none at all."
						sys.exit(message)
					else:
						d_key = filter(lambda x:(monkey in x), self.St_Date.keys())[0]
						i_key = filter(lambda x:(monkey in x), self.St_Times.keys())[0]
						e_key = filter(lambda x:(monkey in x), self.End_Times.keys())[0]

					if len(str(self.St_Date[d_key])) != 8:   # the date has to be 8 characters long
						error = True
						message = "Date needs to be in the format YYYYMMDD"

					elif (int(str(self.St_Date[d_key])[0:4]) < 2000) or (int(str(self.St_Date[d_key])[0:4]) > 2099):
						error = True
						message = "Chosen year must be between 2000 and 2099."

					elif (int(str(self.St_Date[d_key])[4:6]) < 1) or (int(str(self.St_Date[d_key])[4:6]) > 12):
						error = True
						message = "Chosen month must be between 1 and 12."

					elif int(str(self.St_Date[d_key])[-2:]) > calendar.monthrange(int(str(self.St_Date[d_key])[0:4]), int(str(self.St_Date[d_key])[4:6]) )[1]:
						error = True
						message = "Chosen day does not exist for chosen year and month."

					elif len(str(self.St_Times[i_key])) > 4:  # the time has to be at most 4 characters long
						error = True
						message = "Start Time needs to be in military time"

					elif len(str(self.End_Times[e_key])) > 4:  # the time has to be at most 4 characters long
						error = True
						message = "End Time needs to be in military time"

					elif int(self.St_Times[i_key]) > 2400:  # 0000 to 2400 hours
						error = True
						message = "Start Time needs to be between 0000 and 2400"

					elif int(self.End_Times[e_key]) > 2400:  # 0000 to 2400 hours
						error = True
						message = "End Time needs to be between 0000 and 2400"


					elif len( str(self.St_Times[i_key]) )>2 and int(str(self.St_Times[i_key])[-2:]) > 59:  # can be no more than 59 minutes
						error = True
						message = "Start time not correctly entered."


					elif len( str(self.End_Times[e_key]) )>2 and int(str(self.End_Times[e_key])[-2:]) > 59:  # can be no more than 59 minutes
						error = True
						message = "End time not correctly entered."

					elif len(str(self.St_Times[i_key]))<=2 and int(self.St_Times[i_key])>59:
						error=True
						message = "Start time not correctly entered."

					elif len(str(self.End_Times[e_key]))<=2 and int(self.End_Times[e_key])>59:
						error=True
						message = "End time not correctly entered."
					if error == True:
						try:
							self.p.kill()
						except AttributeError: pass
						except NameError: pass
						sys.exit(message)



			if (int(self.window)==0) or (int(self.IPI)==0): pass
			else:
				t_keys = self.Taus.keys()
				self.big_string = 'None'
				d_keys = self.St_Date.keys()
				self.date_string = 'None'
				i_keys = self.St_Times.keys()
				self.time_string = 'None'
				tau_keys = self.Tau_Check.keys()
				self.tau_string = 'None'
				e_keys = self.End_Times.keys()
				self.end_string = 'None'

				if self.Local_Check == False:
					self.end_year, self.end_month, self.end_day = 'X', 'X', 'X'

				elif self.Local_Check == True:
					try:
						(self.end_year, self.end_month, self.end_day)
					except AttributeError:
						Month_Index = {'Jan':'01', 'Feb':'02', 'Mar':'03', 'Apr':'04', 'May':'05', 'Jun':'06', 'Jul':'07', 'Aug':'08', 'Sep':'09', 'Oct':'10', 'Nov':'11', 'Dec':'12'}
						month1 = time.asctime().split(' ')[1]
						self.end_year, self.end_month, self.end_day = str(time.asctime().split(' ')[4]), Month_Index[month1] , time.asctime().split(' ')[2]

				for t in t_keys:
					if self.big_string == 'None': self.big_string = ''
					self.big_string = self.big_string + t + '_' + str(self.Taus[t]) + '__'

				for t in d_keys:
					if self.date_string == 'None': self.date_string = ''
					self.date_string = self.date_string + t + '_' + str(self.St_Date[t]) + '__'

				for t in i_keys:
					if self.time_string == 'None': self.time_string = ''
					self.time_string = self.time_string + t + '_' + str(self.St_Times[t]) + '__'

				for t in e_keys:
					if self.end_string == 'None': self.end_string = ''
					self.end_string = self.end_string + t + '_' + str(self.End_Times[t]) + '__'

				for t in self.Tau_Check.keys():
					if self.tau_string == 'None': self.tau_string = ''
					try:
						self.tau_string = self.tau_string + t + '_' + str(self.Tau_Check[t.strip()]) + '__'
					except KeyError:
						self.tau_string = self.tau_string + t + '_' + 'False__'

				if self.Local_Check == True:
					self.end_year = 'X'
					self.end_month = 'X'
					self.end_day = 'X'
					self.p = os.system('python CollectAllData2.py -t %s -d %s -T %s -u %s -e %s -w %s' + \
									   '-m %s -I %s -Y %s -M %s -D %s -F %s -X %s -C %s -O %s -A %s -K %s -U %s' \
									   % (self.big_string, self.date_string, self.time_string, self.tau_string, \
									   self.end_string, self.window, self.min_pel, self.IPI, self.end_year, \
									   self.end_month, self.end_day, self.base_data, self.version, self.comp, \
									   self.output_dir, self.MDB_DIR, self.sked_file_name, self.subj_incr) )
					sys.exit()

				elif self.Local_Check == False:
					while (True):  # this infinite loop keeps running
						moment = ''

						while moment != '08:00:00':      # execute the whole code at 02:00:00 AM
							moment = time.asctime().split(' ')[3]
						time.sleep(3)  # sleep for 3 seconds to make sure we see a different time each time "moment" is recalculated
						self.p = os.system('python CollectAllData2.py -t %s -d %s -T %s -u %s -e %s -w %s' + \
										   '-m %s -I %s -Y %s -M %s -D %s -F %s -X %s -C %s -O %s -A %s -K %s -U %s' \
										   % (self.big_string, self.date_string, self.time_string, self.tau_string, \
										   self.end_string, self.window, self.min_pel, self.IPI, self.end_year, \
										   self.end_month, self.end_day, self.base_data, self.version, self.comp, \
										   self.output_dir, self.MDB_DIR, self.sked_file_name, self.subj_incr) )
				#self.Local_Check = False
				#self.local.Destroy()
				#self.local = wx.CheckBox(self, label="Execution For Specified Date", pos=(820,352))
				#self.Bind(wx.EVT_CHECKBOX, self.Local, self.local)

				##
				## 	Notes on Execute:
				##		- Probably most can be commented out
				##		- Having imported CollectAllData2 as local module, probably
				##		with name change and conversion to function with no input
				## 		as well, will simply just call CollectAllData2() or more
				##		likely GeneratePlots()
				##		- Then self.local.Destroy or whatever
				##

	def GetTau(self, event):
		self.Taus[self.Names[event.GetId()] +'_'+ str(event.GetId())] = float(event.GetString())

	def GetStDate(self, event):
		self.St_Date[self.Names[event.GetId()-1000] +'_'+ str(event.GetId())] = int(event.GetString())

	def GetStTime(self, event):
		self.St_Times[self.Names[event.GetId()-1051] +'_'+ str(event.GetId())] = int(event.GetString())

	def GetEndTime(self, event):
		self.End_Times[self.Names[event.GetId()-2100] +'_'+ str(event.GetId())] = int(event.GetString())

	def Local(self, event):
		self.Local_Check = event.Checked()

	def New_Chars(self, event):
		self.Tau_Check[self.Names[event.GetId()-2000] +'_'+ str(event.GetId())] = event.Checked()

		
app = wx.App(False)
frame = MyFrame(None, 'Monkey Program')
frame.Show(True)
app.MainLoop()

'''

##################################################
# 					MAIN						 #
##################################################

days = 10
feeding_hour_threshold = 26

data_dir = "X:\Data_Archive1"
report_dir = "C:\Users\Server1\PCRL_Logbook\reports\\"
db_dir = "C:\Users\Server1\PCRL_Logbook\\"

report_custom = False 
custom_dates = {"from": "10-10-2015", "to": "10-19-2015"}

feeders = {f[0]: {'calories_per_dispense': f[1], 'dispense_amt':f[2]} for \
	f in zip(['feeder0','feeder1','feeder2'],[0.5, 0.75, 0.32], [10.0, 10.0, 13.0])}

monkeys = [l.split(',') for l in open('X:\Documents\projects\PCRL_Logbook\\' + \
				'pcrl_data_management\Monkey.txt').readlines()]
				
monkey_data = {m[0]: {'room':m[2], 'station':m[1], 'dob':m[3]} for m in monkeys} 

caloric_densities =[[s.strip() for s in l.split(',')] for l in open('X:\Documents\projects\PCRL_Logbook\\' + \
			'pcrl_data_management\caloric_densities.csv').readlines()]
				
supplemental_feed_data = {f[1]: {'category':f[0], 'E_carb':f[2], 'E_prot':f[3], \
		'E_fat':f[4], 'fraction_fiber':f[5], 'fraction_water':f[6]} for f in caloric_densities}
		
lab_member_data = {'admin': {'first':'PCRL', 'last':'admin', 'password':'PCRLBostonU'}}

# init json data struct
data = {'days': days, 'feeding_hour_threshold': feeding_hour_threshold, \
		'report_dir': report_dir, 'db_dir': db_dir, 'data_dir': data_dir, \
		'feeders': feeders, 'report_custom': report_custom, \
		'custom_dates': custom_dates, 'monkey_data': monkey_data, \
		'supplemental_feed_data': supplemental_feed_data, \
		'labmember_data': lab_member_data }

# write to configuration file
config_f = open('X:\Documents\projects\PCRL_Logbook\pcrl_data_management\config.json', 'w')
config_f.write(json.dumps(data, indent=2))
config_f.close()

