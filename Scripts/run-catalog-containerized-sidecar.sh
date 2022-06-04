PATH_TO_PROJECT="/mnt/c/Repositories/EShopOnDapr"
DAPR_APP_NAME="catalog-dapr"

trap "docker stop $DAPR_APP_NAME" SIGINT SIGTERM

docker run --rm -p 127.0.0.1:3551:3551/tcp --name $DAPR_APP_NAME \
	--net="host" \
	--mount type=bind,source=$PATH_TO_PROJECT/Dapr/Components/,target=/components \
	--mount type=bind,source=$PATH_TO_PROJECT/Dapr/Config/,target=/config \
	daprio/daprd:1.7.4 ./daprd \
	--components-path /components \
	--config /config/config.yaml \
	--dapr-http-port 3551  \
	--app-id catalog \
	--app-port 5051 &

dotnet $PATH_TO_PROJECT/EShop.Catalog/bin/Debug/net5.0/EShop.Catalog.dll \
	--urls=http://localhost:5051/

