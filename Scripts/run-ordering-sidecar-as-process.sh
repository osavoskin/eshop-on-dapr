PATH_TO_PROJECT="/mnt/c/Repositories/EShopOnDapr"
PATH_TO_DLL="$PATH_TO_PROJECT/EShop.Ordering/bin/Debug/net5.0/EShop.Ordering.dll"

dapr run\
	--components-path $PATH_TO_PROJECT/Dapr/Components \
	--config $PATH_TO_PROJECT/Dapr/Config/config.yaml \
	--placement-host-address 127.0.0.1:50005 \
	--dapr-http-port 3553  \
	--app-id ordering \
	--app-port 5053 \
	-- dotnet $PATH_TO_DLL --urls=http://localhost:5053
