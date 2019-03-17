#!/bin/bash
cd ..
git stash
cd Website

if [ "$#" -ne 3 ]; then
        echo "USAGE: ./setup.sh [password] [vuforia access keys] [vuforia secret key]"
        exit 1
fi
sed -i "s/PASSWORD/${1}/g" submit_code.php
sed -i "s/ACCESSKEY/${2}/g" submit_code.php
sed -i "s/SERVERKEY/${3}/g" submit_code.php
