#!/usr/bin/python

import json

WORKING_FOLDER = '../../RussianTasks/res/data/'
INPUT_FILE_NAME = WORKING_FOLDER + 'repeating.tsv'
OUTPUT_FILE_NAME = WORKING_FOLDER + 'repeating.json'
OUTPUT_FILE_NAME_ENCODED = WORKING_FOLDER + 'repeating'

getSectionId = {
    '№12 (НЕ/НИ слитно и раздельно)': 1,
    '№13 (слитно/раздельно/дефис)': 2
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
	parts 		= line.split('	')
	id_ 		= parts[0]
	section_ 	= parts[1]
	word_ 		= parts[2]
	answer_ 	= parts[3]
	wordDict = {}
	wordDict['id'] = int(id_)
	wordDict['section'] = getSectionId[section_]
	wordDict['word'] = word_
	wordDict['answer'] = answer_
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