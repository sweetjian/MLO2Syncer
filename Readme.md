[打造自己的MyLifeOrganized 2（MLO2）云同步](http://www.cnblogs.com/djian/p/mylifeorganized-own-clound-sync.html)

- 域名指向路由器外网IP（DDNS）；
- 路由器将21030转发到服务器21031；
- 服务器安装FTP服务，并在路由器映射出FTP端口；
- 服务器安装MLO，并将 Toos|Options|Behaviour|Close to system tray 对勾去掉；
- 服务器安装MLO2Syncer代理软件，并正确配置；

域名 ==> 路由器21030转发到服务器21031 ==> 服务器21031到小软件 ==> 小软件再转到21030

最终实现：在家、公司、外出、PC、手机任何时候任何地点一键同步数据。

todo:
1. 暂不支持带密码的文件同步
2. 暂不支持一个文件同步多个手机
