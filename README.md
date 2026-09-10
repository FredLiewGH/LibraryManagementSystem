# Introduction 
Basic Library Management System .NET Web API
#=====================================================

# Getting Started
1. Manually create a .env file in project root and set the following
	- MYSQL_ROOT_PASSWORD=<your_mysql_root_password>
	- MYSQL_PASSWORD=<your_mysql_dba_password>
	- MYSQL_DATABASE=lmsdb
	- MYSQL_USER=lms_dba

2. Run docker compose at project root
	- docker compose up --build -d

3. Docker url: http://localhost:5003

# Endpoints
1. /default/health
	- For health check

2. /books/getall
	- To get all book records from database

3. /books/getbyid/{bookid}
	- To get book record by BookID

4. /books/addnew
	- To create new book record
	- Input params:
		a. bookname
		b. author
		c. publisher

5. /books/modify/{bookid}
	- To update book record by BookID
	- Input params:
		a. bookname
		b. author
		c. publisher
		
6. /books/remove/{bookid}
	- To delete book record by BookID

7. /members/getall
	- To get all book members from database

8. /members/getbyid/{memberid}
	- To get book member by MemberID

9. /members/addnew
	- To create new member record
	- Input params:
		a. membername
		b. gender
		c. phoneno
		d. email
		e. address

10. /members/modify/{memberid}
	- To update member record by MemberID
	- Input params:
		a. membername
		b. gender
		c. phoneno
		d. email
		e. address
		
11. /members/remove/{memberid}
	- To delete member record by MemberID
	
12. /borrows/borrow
	- To borrow a book for a member
	- Input params:
		a. bookid
		b. memberid
		c. borrowstartdate
		d. borrowenddate

12. /borrows/return/{bookid}
	- To return a book from a member