#!/usr/bin/python

import json
import subprocess
import shutil
import os

#=====================
#		Tools
#=====================
MSBUILD = "C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe"
OBFUSCAR = "packages\\Obfuscar.2.2.3\\tools\\Obfuscar.Console.exe"
ILMERGE = "C:\\Program Files (x86)\\Microsoft\\ILMerge\\ILMerge.exe"

#=====================
#		Folders
#=====================
BUILD_FOLDER = "build"
DISTRIBUTION_FOLDER = os.path.join(BUILD_FOLDER, "ЕГЭ-Тренажёр")
OBFUSCAR_INPUT_FOLDER = os.path.join(BUILD_FOLDER, "obfuscar_input")
OBFUSCAR_OUTPUT_FOLDER = os.path.join(BUILD_FOLDER, "obfuscar_output")
EXE_FILE_NAME = os.path.join(OBFUSCAR_OUTPUT_FOLDER, "ЕГЭ-Тренажёр.exe")
INSTALL_ACTIONS_DLL_NAME = os.path.join(OBFUSCAR_OUTPUT_FOLDER, "InstallActions.dll")
DLL_FILE_NAME = os.path.join(OBFUSCAR_INPUT_FOLDER, "Newtonsoft.Json.dll")


for folder in os.listdir(BUILD_FOLDER):
	shutil.rmtree(os.path.join(BUILD_FOLDER, folder), True)


subprocess.call([MSBUILD, "RussianTasks.sln", "/target:RussianTasks", "/property:Configuration=Release", "/property:OutDir=..\\" + OBFUSCAR_INPUT_FOLDER])
subprocess.call([OBFUSCAR, "obfuscar\\obfuscar.xml"])
subprocess.call([ILMERGE, "/target:winexe", "/targetplatform:v4,C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319", "/out:" + EXE_FILE_NAME, EXE_FILE_NAME, DLL_FILE_NAME])
subprocess.call([MSBUILD, "RussianTasks.sln", "/target:InstallActions", "/property:Configuration=Release", "/property:OutDir=..\\" + OBFUSCAR_OUTPUT_FOLDER])

#copying program files
os.mkdir(DISTRIBUTION_FOLDER)
shutil.copyfile(EXE_FILE_NAME, os.path.join(DISTRIBUTION_FOLDER, "ЕГЭ-Тренажёр.exe"))
shutil.copyfile(INSTALL_ACTIONS_DLL_NAME, os.path.join(DISTRIBUTION_FOLDER, "InstallActions.dll"))

RESOUCRES_TO_COPY = [
"res\\data\\accents",
"res\\data\\spelling",
"res\\data\\repeating"
]

os.makedirs(os.path.join(DISTRIBUTION_FOLDER, "res\\data"))
for resource in RESOUCRES_TO_COPY:
	shutil.copyfile(os.path.join("RussianTasks", resource), os.path.join(DISTRIBUTION_FOLDER, resource))

shutil.copytree("RussianTasks\\res\\images", os.path.join(DISTRIBUTION_FOLDER, "res\\images"))

#removing temporary directories
shutil.rmtree(OBFUSCAR_INPUT_FOLDER, True)
shutil.rmtree(OBFUSCAR_OUTPUT_FOLDER, True)

print("\nDone!")