cd ..
docker run -v $(pwd):/app/mnt --env "config_file=$1" coflows/ce aci remove