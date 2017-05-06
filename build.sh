#!/usr/bin/env bash

set -e

if [ $TRAVIS_OS_NAME = "osx" ]; then
  ulimit -n 1024
  dotnet restore --disable-parallel
else
  dotnet restore
fi

export FrameworkPathOverride=$(dirname $(which mono))/../lib/mono/4.5/

dotnet test ./test/Multiformats.Base.Tests/Multiformats.Base.Tests.csproj -c Release -f netcoreapp1.1
dotnet build ./test/Multiformats.Base.Tests/Multiformats.Base.Tests.csproj -c Release -f net461

mono $HOME/.nuget/packages/xunit.runner.console/2.2.0/tools/xunit.console.exe ./test/Multiformats.Base.Tests/bin/Release/net461/Multiformats.Base.Tests.dll
