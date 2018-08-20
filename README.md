# Chatter / 碎碎念
一个纯粹的```txt小说```朗读工具，基于```百度语音合成```REST API

## 使用说明
打开文件 ```碎碎念.exe.config``` 按注释进行修改

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    ...
    <!-- 音量，取值0-15，默认为5中音量 -->
    <add key="vol" value="15"/>
    <!-- 发音人选择, 0为女声，1为男声，3为情感合成-度逍遥，4为情感合成 -度丫丫，默认为普通女-->
    <add key="per" value="3"/>
    <!-- 语速，取值0-9，默认为5中语速 -->
    <add key="spd" value="9"/>
    <!-- 音调，取值0-9，默认为5中语调 -->
    <add key="pit" value="5"/>
    <!-- txt文件路径 -->
    <add key="fileName" value="./xxx.txt"/>
	<!-- 章节正则表达式-->
    <add key="chapterRegex" value="^第\d{4}章"/>
  </appSettings>
</configuration>
```
