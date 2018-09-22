// threadPool.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "Thread.h"
#include "ThreadPoolExecutor.h"


class R : public Runnable
{
public:
	~R(){}
	void Run(){
		Sleep(1000);
		printf("输出内容打印\n");
	}
};


int _tmain(int argc, _TCHAR* argv[])
{
	CThreadPoolExecutor * pExecutor = new CThreadPoolExecutor();
	pExecutor->Init(1, 5, 50);
	R r;
	for (int i=0; i< 10;i++)
	{
		while (!pExecutor->Execute(&r))
		{

		}
	}
	pExecutor->Terminate();
	delete pExecutor;
	getchar();
	return 0;
}

