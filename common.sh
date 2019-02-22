GARDEN_HOSTNAME=garden
GARDEN_USER=j

ILLUMINATOR_PORT=$ILLUMINATOR_PORT
SIMULATOR_PORT=$ILLUMINATOR_SIMULATOR_PORT

if [ ! "ILLUMINATOR_PORT" ]; then
  ILLUMINATOR_PORT="/dev/ttyUSB0"
fi
if [ ! "SIMULATOR_PORT" ]; then
  SIMULATOR_PORT="/dev/ttyUSB1"
fi

# TODO: Remove if not needed. Ports should be specified via environment variables
## If multiple devices are detected then this becomes the second device pair
#if pio device list | grep -q 'ttyUSB1'; then
#  ILLUMINATOR_PORT="/dev/ttyUSB1"
#fi

## If multiple devices pairs are detected then this becomes the second device pair
#if pio device list | grep -q 'ttyUSB2'; then
#  VENTILATOR_PORT="/dev/ttyUSB2"
#  SIMULATOR_PORT="/dev/ttyUSB3"
#fi

