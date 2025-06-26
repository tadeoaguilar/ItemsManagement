#!/bin/bash

# Build script for core projects (excluding mobile)
# This script builds all projects except the mobile project to avoid Xcode version issues

echo "Building eItems Core Solution (excluding mobile)..."
echo "=================================================="

# Build main application projects
echo "Building API Service..."
dotnet build "src/eItems.ApiService/eItems.ApiService.csproj"

echo "Building Web Frontend..."
dotnet build "src/eItems.Web/eItems.Web.csproj"

echo "Building Aspire AppHost..."
dotnet build "src/eItems.AppHost/eItems.AppHost.csproj"

# Build test projects
echo "Building Application Tests..."
dotnet build "tests/eItems.Catalog.Application.Tests/eItems.Catalog.Application.Tests.csproj"

echo "Building Infrastructure Tests..."
dotnet build "tests/eItems.Catalog.Infrastructure.Tests/eItems.Catalog.Infrastructure.Tests.csproj"

echo "Running Tests..."
echo "==============="

echo "Running Application Layer Tests..."
dotnet test "tests/eItems.Catalog.Application.Tests/eItems.Catalog.Application.Tests.csproj" --verbosity minimal

echo "Running Infrastructure Layer Tests..."
dotnet test "tests/eItems.Catalog.Infrastructure.Tests/eItems.Catalog.Infrastructure.Tests.csproj" --verbosity minimal

echo ""
echo "✅ Core solution build and tests completed successfully!"
echo ""
echo "Note: Mobile project excluded due to Xcode version requirements."
echo "To build mobile project, upgrade to Xcode 16.3 or later."