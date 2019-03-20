echo "Injecting version into sketch..."

VERSION=$(cat version.txt)
BUILD_NUMBER=$(cat buildnumber.txt)

FULL_VERSION="$VERSION-$BUILD_NUMBER"

echo "Version: $FULL_VERSION"
SOURCE_FILE="src/LightPRSensorCalibratedLight/LightPRSensorCalibratedLight.ino"

sed -i "s/#define VERSION .*/#define VERSION \"$FULL_VERSION\"/" $SOURCE_FILE
