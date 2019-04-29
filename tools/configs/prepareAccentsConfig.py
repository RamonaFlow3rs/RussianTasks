#!/usr/bin/python

import json

WORKING_FOLDER = '../../RussianTasks/res/data/'
INPUT_FILE_NAME = WORKING_FOLDER + 'accents.tsv'
OUTPUT_FILE_NAME = WORKING_FOLDER + 'accents.json'
OUTPUT_FILE_NAME_ENCODED = WORKING_FOLDER + 'accents'

getPositionType = {
    'Сверху': 1,
    'Справа': 2,
    'Снизу': 3,
    'Слева': 4
}

getSectionId = {
    'Существительные': 1,
    'Существительные с неподвижным ударением': 2,
    'Глаголы': 3,
    'Прилагательные': 4,
    'Причастия': 5,
    'Деепричастия': 6,
    'Наречия': 7
}

def getAccentPosition(word: str):
	for i in range(0, len(word)):
		if word[i].isupper():
			return i + 1
#def getAccentPosition

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
	add_text_ 	= parts[3]
	add_pos_ 	= parts[4]
	wordDict = {}
	wordDict['id'] = int(id_)
	wordDict['section'] = getSectionId[section_]
	wordDict['accentPosition'] = getAccentPosition(word_)
	wordDict['word'] = word_.lower()

	if add_text_ != '':
		additions = []
		additionItem = {}
		additionItem['text'] = add_text_
		additionItem['position'] = getPositionType[add_pos_]
		additions.append(additionItem)
		wordDict['additions'] = additions

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