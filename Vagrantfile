Vagrant.configure("2") do |config|
  config.vm.define "linux" do |linux|
    linux.vm.box = "bento/ubuntu-22.04"
    linux.vm.hostname = "ubuntuvm"
    linux.vm.network "forwarded_port", guest: 5000, host: 6000
    linux.vm.network "private_network", type: "static", ip: "192.168.56.10"
    linux.vm.provider "virtualbox" do |virutalbox|
      virutalbox.name = "CrossPlatform-Ubuntu-VM"
      virutalbox.memory = "2048"
      virutalbox.cpus = 2
    end
    linux.vm.provision "shell", path: "linux-provision.sh"
  end

  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    windows.vm.hostname = "windowsvm"
    windows.vm.network "forwarded_port", guest: 5000, host: 7000
    windows.vm.network "private_network", type: "static", ip: "192.168.56.20"
    windows.vm.provider "virtualbox" do |virutalbox|
      virutalbox.name = "CrossPlatform-Windows-VM"
      virutalbox.memory = "2048"
      virutalbox.cpus = 4
      virutalbox.customize ["modifyvm", :id, "--nictype1", "82540EM"]
      virutalbox.customize ["modifyvm", :id, "--nictype2", "82540EM"]
    end
    windows.vm.synced_folder ".", "C:/lab"
    windows.vm.provision "shell", path: "windows-provision.sh"
  end
end