echo "Starting build for project"
echo "Dir: $PWD"

DIR=$PWD

xbuild src/LightPRSensorCalibratedLight.sln /p:Configuration=Release
