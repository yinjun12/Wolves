# Wolves
狼人杀上帝版本

////2017年5月7日更改

1：实现了什么功能？

用C#实现了狼人杀的上帝视角。在进行狼人杀游戏时上帝不需要去记忆每个人的角色，台词，功能和每个时间需要做的事，只需要按照提示把结果输入到程序即可。
Wolves有两个模式，一个是九人模式，即三个狼人，三个平民，一个预言家，一个猎人，一个女巫。具体的功能可以网上搜下。第二种模式是十二人模式，在九人模式的基础上增加了守卫的角色，要添加守卫与其它角色的配合，以及警长竞选，撕或移交警徽的问题。

2：怎么运行？

直接下载Wolves，在windows下，用Vs2017版本打开Wolves.sln文件；把里的图片和声音的绝对径改成你的位置。图片放在image文件里，声音放在sound文件里。改好之后就能直接运行了

3：代码实现

关于玩家身份的playInfo类和游戏的类定义和实现在program.cs里，九人模式的实现在Nine里，十二人模式的实现在twelve里，里面注释我写的不太多，每个函数的功能可能通过函数名来了解。

4：后续的实现

正在实现十二人模式中的警长竞选，狼人自爆的问题。
