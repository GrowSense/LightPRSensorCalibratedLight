echo "Getting library files..."
echo "  Dir: $PWD"

sh install-package.sh NUnit 2.6.4
sh install-package.sh NUnit.Runners 2.6.4
sh install-package.sh Newtonsoft.Json 11.0.2
sh install-package.sh ArduinoSerialControllerClient 1.1.1.2
sh install-package.sh duinocom.core 1.0.6

echo "Finished getting library files."
