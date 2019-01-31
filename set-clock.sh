echo "Setting device clock"

DATE_TIME=$(date '+%d/%m/%Y %T');
echo "$DATE_TIME"

echo "C$DATE_TIME" > /dev/ttyUSB0
