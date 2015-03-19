/*********************************************************************************************************************************************************
*
*	Assumptions: 1) The storage that can be filled in (bufferSizeInBytes) is 1500
*				 2) The return value of API is assumed to be 1000
*				 3) The value taken from the driver using API is stored in variable 'item' one at a time
*
*															Code by Keval Rajesh Shah on 3rd March 2015
*
*********************************************************************************************************************************************************/

#include <stdio.h>
#include <time.h>
#include <pthread.h>
#include <semaphore.h>
#include <sys/neutrino.h>
// global data structures to be used

pthread_mutex_t  mutex = PTHREAD_MUTEX_INITIALIZER;										//mutex for writing to memory
pthread_mutex_t  readmutex = PTHREAD_MUTEX_INITIALIZER;									//mutex for critical area or reader
pthread_rwlock_t lock = PTHREAD_RWLOCK_INITIALIZER;										//read and write locks to access critical area/shared buffer
static int size;																		//API's return value
char *output;
char item ;
sem_t s;																				//semaphore for writer as an attempt to a possible error as described in word
struct node																				// structure for implementing a queue using linked list
{
char info;
struct node *link;
}*front = NULL, *rear = NULL;

int get_external_data(char *buffer, int bufferSizeInBytes)								// Assume that this is the given API that provides us with the size of the data obtaied
	{
		return 1000;																		// from the buffer after being filled.This API also puts the value of Device driver into item variable one at a time.

	}

/*************************************************************
					READER THREAD
 * This thread is responsible for pulling data off of the shared data
 * area and processing it using the process_data() API.
 **************************************************************/
void *reader_thread(void *arg) {
	char buf[size];																		// a temporary storage of data from queue
	char *output = &buf[0];
	int pthread_rwlock_rdlock (pthread_rwlock_t *lock);									//read lock for shared buffer/queue
	while(1) {
		struct node *ptr;																//**********************************************************************
		if(front == NULL)																//
		printf("\n\nQueue is empty.\n");												//
		else                                                                            //
		{	pthread_mutex_lock(&readmutex);												//	mutex to protect critical area and increment
		 ptr = front;																	//	the pointer of temporary storage array
		*output = front->info;															//
																						//
		printf("\nItem stored from shared buffer to memory: %d\n", *output);			//	we need this mutex to help increament i
		output++;																		//
		//																				//
		char *buffer = *output;															//
		void process_data(char *buffer, int bufferSizeInBytes)	;						//	API for processing the data which will take input
																						//	from the temporary storage array one char at a time
		front = front->link;															//	
		free(ptr);																		//
		pthread_mutex_unlock(&readmutex);												//
																						//		 IMPLEMENTING QUEUE LINKED LIST AS A READER
																						//											FROM SHARED BUFFER
		if(front == NULL)																//
		rear = NULL;																	//
		}																				//
	}																					//********************************************************************

	int pthread_rwlock_unlock(pthread_rwlock_t *lock);
	return NULL;
}


/**************************************************************************
						WRITER THREAD
 * This thread is responsible for pulling data from a device using
 * the get_external_data() API and placing it into a shared area
 * for later processing by one of the reader threads.
 ***************************************************************************/
void *writer_thread(void *arg) {
	int sem_wait(sem_t *s);																// helps with problem described in thread.docx
	pthread_mutex_lock(&mutex);															// ensure that when the writer writes,there is no other read/ write operation occurs

	int pthread_rwlock_wrlock (pthread_rwlock_t *lock);									//Write lock ensures that only one write thread can access the shared buffer/queue

	while(1) {

		char *buffer = &rear->info;														//pointer to the first data element in queue OR pointer to the data structure

		int bufferSizeInBytes;															// ASSUME that the data to be extracted from the device driver is 1500
		bufferSizeInBytes = 1500;

		size = get_external_data(*buffer, bufferSizeInBytes);							// The API that will give the size that is extracted from driver to shared queue

		if (size<0){
			return -1;
		}
		else {
		if(rear == NULL){                                                               //****************************************************************************
		rear = (struct node *)malloc(sizeof(struct node));								//
		rear->info = item;																//
		rear->link = NULL;																//		ASSUME: the value taken from the driver using API is stored in
		front = rear;																	//				variable 'item' one at a time
		}																				//
		else																			//
		{																				//
		rear->link = (struct node *)malloc(sizeof(struct node));						//
		rear = rear->link;																//				IMPLEMENTING QUEUE LINKED LIST 'A WRITER FROM DRIVER TO
		rear->info = item;																//														SHARED BUFFER'
		rear->link = NULL;																//
		}																				//******************************************************************************
			}	}

	int pthread_rwlock_unlock(pthread_rwlock_t *lock);									//unlocks all the locks

	pthread_mutex_unlock(&mutex);
	int sem_post(sem_t *s);

	return NULL;
}


#define M 10
#define N 20

int main(int argc, char **argv) {
	int i;

	int pthread_rwlock_init(pthread_rwlock_t *lock, const pthread_rwlockattr_t *attr);	//Initialize the read / write locks

	for(i = 0; i < N; i++) {
		pthread_create(NULL, NULL, reader_thread, NULL);
	}

	sem_init(&s, 0, M);																	//initialize semaphores for shared memory

	for(i = 0; i < M; i++) {
		pthread_create(NULL, NULL, writer_thread, NULL);
	}

	return 0;
}

/*********************************************************************************************************************************************************
*
*
*															Code by Keval Rajesh Shah on 3rd March 2015
*
*********************************************************************************************************************************************************/
