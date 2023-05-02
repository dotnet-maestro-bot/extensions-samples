
# Build and publish the project.
echo ">>> Building and publishing the sample project... <<<"
dotnet publish --framework net6.0
echo "#####################################################"
echo ""

# Build the docker image.
echo ">>> Building the docker image... <<<"
docker image build . -t resource-utilization-net6
echo "#####################################################"
echo ""

# Run the container.
echo ">>> Runnig the container... <<<"
docker container run resource-utilization-net6
