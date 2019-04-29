#!/usr/bin/python

import json

WORKING_FOLDER = '../../RussianTasks/res/data/'
INPUT_FILE_NAME = WORKING_FOLDER + 'variant.tsv'
OUTPUT_FILE_NAME = WORKING_FOLDER + 'variant.json'
OUTPUT_FILE_NAME_ENCODED = WORKING_FOLDER + 'variant'

getSectionId = {
	'Раздел1': 1,
    'Раздел2': 2
	#'№5 (паронимы)': 1,
    #'№6 (грамматические нормы)': 2
}

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
	variantsArray = []
	notEmpryVariantsArray = []
	parts 		= line.split('	')
	id_ 		= parts[0]
	section_ 	= parts[1]
	question_ 	= parts[2]
	variantsArray.append(parts[3])
	variantsArray.append(parts[4])
	variantsArray.append(parts[5])
	variantsArray.append(parts[6])
	variantsArray.append(parts[7])
	variantsArray.append(parts[8])
	answer_		= parts[9]
	wordDict = {}
	wordDict['id'] = int(id_)
	wordDict['section'] = getSectionId[section_]
	wordDict['question'] = question_
	
	for i in variantsArray :
		if i == "" :
			break
		else :
			notEmpryVariantsArray.append(i)

	wordDict['variants'] = notEmpryVariantsArray

	wordDict['answer'] = int(answer_) - 1
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