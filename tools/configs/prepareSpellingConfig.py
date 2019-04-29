#!/usr/bin/python

import json

WORKING_FOLDER = '../../RussianTasks/res/data/'
INPUT_FILE_NAME = WORKING_FOLDER + 'spelling.csv'
OUTPUT_FILE_NAME = WORKING_FOLDER + 'spelling.json'
OUTPUT_FILE_NAME_ENCODED = WORKING_FOLDER + 'spelling'

getSectionId = {
    '№8 (гласные в корне)': 1,
    '№9 (приставки слов)': 2,
    '№10 (суффиксы слов)': 3,
    '№11 (глаголы и причастия)': 4,
    '№14 (Н и НН в словах)': 5,
}

def createParts(word: str):
	ret = []
	hiddenPart = ''
	visiblePart = ''
	isLastUpper = False
	hiddenCount = 0

	for i in range(0, len(word)):
		letter = word[i]
		isUpper = letter.isupper()
		if (i == 0 and isUpper):
			isLastUpper = True
		if (isUpper == True):
			if (isLastUpper == False):
				#Upper letter goes after lower one(s)
				obj = {}
				obj['part'] = visiblePart
				visiblePart = ''
				ret.append(obj)
			hiddenPart += letter.lower()
		else:
			if (isLastUpper == True):
				#Lower letter goes after upper one(s)
				obj = {}
				obj['part'] = hiddenPart
				obj['hidden'] = True
				hiddenPart = ''
				ret.append(obj)
				hiddenCount += 1
			visiblePart += letter
		isLastUpper = isUpper

	if (hiddenPart != ''):
		obj = {}
		obj['part'] = hiddenPart
		obj['hidden'] = True
		hiddenPart = ''
		ret.append(obj)
		hiddenCount += 1
	elif (visiblePart != ''):
		obj = {}
		obj['part'] = visiblePart
		visiblePart = ''
		ret.append(obj)

	if (hiddenCount == 0):
		assert False, ("no hidden letters in word \"" + word + "\"")

	return ret
#def createParts



#==============================================
#entry point
#==============================================
print('file opening...')
file = open(INPUT_FILE_NAME, 'r', encoding='UTF8')
lines = file.read().split('\n')
file.close()

print('working...')
#removing title row
del lines[0]

contentsArray = []

for line in lines:
	parts 		= line.split(',')
	id_ 		= parts[0]
	section_ 	= parts[1]
	word_ 		= parts[2]
	wordDict = {}
	wordDict['id'] = int(id_)
	wordDict['section'] = getSectionId[section_]
	wordDict['parts'] = createParts(word_)
	contentsArray.append(wordDict)

rootDict = {}
rootDict['contents'] = contentsArray

jsonStr = json.dumps(rootDict,
					sort_keys=False,
					indent=2,
					ensure_ascii=False)

print('writing...')
open(OUTPUT_FILE_NAME, 'wb').write(jsonStr.encode('UTF8'))

encodedArray = bytearray(jsonStr, 'UTF8')
for i in range(len(encodedArray)):
	encodedArray[i] ^= 0xA2

open(OUTPUT_FILE_NAME_ENCODED, 'wb').write(encodedArray)
print('done...')