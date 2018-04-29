#  Copyright 2017-2018 AluminiumTech
#  This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.
#
# This script creates binaries for the CSMark CLI app.
from subprocess import call
import os
import shutil
import glob

if __name__ == '__main__':
    call('dotnet restore')
    call('dotnet publish -c Release -r win10-x64')
    call('dotnet publish -c Release -r win10-arm')
    call('dotnet publish -c Release -r win10-arm64')
    call('dotnet publish -c Release -r osx-x64')
    call('dotnet publish -c Release -r linux-arm')
    call('dotnet publish -c Release -r linux-x64')