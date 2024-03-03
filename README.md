## hashViewer

看哈希的GUI小工具。自学WPF的结果，代码乱得一批，能用就行。

## 右键菜单配置

1. 打开注册表编辑器，转到路径`计算机\HKEY_CLASSES_ROOT\*\shell`
2. 新建项目`hashViewer`，`hashViewer`内新建项目`command`
3. 编辑`hashViewer`的`(默认)`值为右键菜单显示的名称：如`ViewHash`
4. 编辑`command`的`(默认)`值为`"D:\...程序的路径...\hashViewer.exe" "%1"`
5. 随便找个文件测试一下