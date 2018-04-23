#  Copyright 2017-2018 AluminiumTech
#  This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.
#
#  This script restores the dependencies of and builds CSMark.
from subprocess import call

if __name__ == '__main__':
    call('dotnet restore')
    call('dotnet build')
