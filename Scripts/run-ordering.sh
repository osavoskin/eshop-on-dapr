PATH_TO_PROJECT="/mnt/c/Repositories/EShopOnDapr"
PATH_TO_DLL="$PATH_TO_PROJECT/EShop.Ordering/bin/Debug/net5.0/EShop.Ordering.dll"

dapr run\
	--components-path $PATH_TO_PROJECT/Dapr/Components \
	--config $PATH_TO_PROJECT/Dapr/Config/config.yaml \
	--dapr-http-port 3552  \
	--app-id ordering \
	--app-port 5052 \
	-- dotnet $PATH_TO_DLL --urls=http://localhost:5052
