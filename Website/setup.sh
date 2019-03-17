#!/bin/bash
if ["$#" -ne "3"]
then
	echo "USAGE: ./setup.sh [password] [vuforia access keys] [vuforia secret key]"
	exit 1
fi
sed -i "s/<PASSWORD>/$0/g" submit_code.php
sed -i "s/<ACCESSKEY>/$1/g" submit_code.php
sed -i "s/<SERVERKEY>/$2/g" submit_code.php