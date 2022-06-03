PATH_TO_PROJECT="/mnt/c/Repositories/EShopOnDapr"
PATH_TO_DLL="$PATH_TO_PROJECT/EShop.Catalog/bin/Debug/net5.0/EShop.Catalog.dll"

dapr run\
	--components-path $PATH_TO_PROJECT/Dapr/Components \
	--config $PATH_TO_PROJECT/Dapr/Config/config.yaml \
	--dapr-http-port 3551  \
	--app-id catalog \
	--app-port 5051 \
	-- dotnet $PATH_TO_DLL --urls=http://localhost:5051
