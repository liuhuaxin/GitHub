// threadPool.cpp : �������̨Ӧ�ó������ڵ㡣
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
		printf("������ݴ�ӡ\n");
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

