PORT=$1

if [ ! $PORT ]; then
  PORT="/dev/ttyUSB0"
fi

echo "Setting device clock to the current time"
echo "Device port: $PORT"

DATE_TIME=$(date '+%d/%m/%Y %T');
echo "Clock: $DATE_TIME"

echo "C$DATE_TIME" > "$PORT"
