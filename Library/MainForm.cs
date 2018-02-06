using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Library
{
    public partial class mainForm : Form
    {
        MySqlConnection connection;
        AutoComplete auto = new AutoComplete();
        Grid gv = new Grid();
        public mainForm()
        {
            InitializeComponent();

            try
            {
                string connectionString = "datasource='127.0.0.1'; port='3306'; username='root'; password=''; database='library';";
                connection = new MySqlConnection(connectionString);

                //Bookcollection auto 
                auto.bookcollection(bookName_tb_addBook);

                //Book collection gridview
                string query = string.Format($@"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection");
                gv.update(query, books_gv_bookCollection);

                //Book issue gridview
                query = string.Format($@"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE available='yes'");
                gv.update(query, books_gv_bookIssue);

                //Student gridview update in bookreturn
                query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                                FROM student s,bookcollection bc, bookissue bi
                                                WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL");
                gv.update(query, student_gv_bookReturn);

                //Teacher gridiew update in bookreturn
                query = string.Format($@"SELECT TeacherName,t.TeacherID,bc.BookID,bc.BookName
                                                FROM teacher t,bookcollection bc,teacherissue ti
                                                WHERE t.teacherID=ti.teacherID && ti.bookID=bc.bookID && returnDate IS NULL");
                gv.update(query, teacher_gv_bookReturn);

                //Student gridview update in bookrenew
                query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                                 FROM student s,bookcollection bc, bookissue bi
                                                 WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL");
                gv.update(query, student_gv_bookRenew);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Create student profile
        private void student_b_profile_Click(object sender, EventArgs e)
        {
            string query = string.Format($"INSERT INTO student (usn,studentName) VALUES ('{usn_TB_studentProfile.Text}','{studentName_TB_studentProfile.Text}')");
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();

                MessageBox.Show("Student Profile Created");
                studentName_TB_studentProfile.Clear();
                usn_TB_studentProfile.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Create teacher profile
        private void teacher_b_profile_Click(object sender, EventArgs e)
        {
            string query = string.Format($"INSERT INTO teacher (teacherName,teacherID) VALUES ('{teacherName_TB_teacherProfile.Text}','{teacherID_TB_teacherProfile.Text}')");
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();

                MessageBox.Show("Teacher Profile Created");
                teacherName_TB_teacherProfile.Clear();
                teacherID_TB_teacherProfile.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        
        //Create login account
        //TODO: Check if text is available before creating
        private void create_b_account_Click(object sender, EventArgs e)
        {
            if (password_tb_createAccount.Text == rePassword_tb_createAccount.Text)
            {
                string query = string.Format($"INSERT INTO login (loginName,password) VALUES ('{name_tb_createAccount.Text}','{password_tb_createAccount.Text}')");
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Account created!");

                    name_tb_createAccount.Clear();
                    password_tb_createAccount.Clear();
                    rePassword_tb_createAccount.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match");
            }
        }

        //Delete login account
        private void delete_b_account_Click(object sender, EventArgs e)
        {
            string query = string.Format($"DELETE FROM login WHERE loginName='{name_tb_deleteAccount.Text}' && password='{password_tb_deleteAccount.Text}'");
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                int numberOfLine=command.ExecuteNonQuery();
                if (numberOfLine>0)
                {
                    MessageBox.Show("Account deleted!");
                    name_tb_deleteAccount.Clear();
                    password_tb_deleteAccount.Clear();
                }
                else
                {
                    MessageBox.Show("Check the entered User Name and Password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Add books to the book collection
        private void add_b_addBook_Click(object sender, EventArgs e)
        {
            string query = string.Format($"INSERT INTO bookcollection (bookName,bookID,edition,author) VALUES ('{bookName_tb_addBook.Text}','{bookID_tb_addBook.Text}','{edition_tb_addBook.Text}','{author_tb_addBook.Text}')");
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();

                MessageBox.Show("Book added to the Collection");

                bookName_tb_addBook.Clear();
                bookID_tb_addBook.Clear();
                edition_tb_addBook.Clear();
                author_tb_addBook.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

                //Bookcollection autocomplete
                auto.bookcollection(bookName_tb_addBook);

                //Bookcollection gridview update
                query = string.Format($"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection");
                gv.update(query, books_gv_bookCollection);

                //Book issue gridview
                query = string.Format($@"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE available='yes'");
                gv.update(query, books_gv_bookIssue);
            }
        }

        //Remove books from book collection
        //TODO: Check available condition before removing
        private void remove_b_removeBook_Click(object sender, EventArgs e)
        {
            string query = string.Format($"DELETE FROM bookcollection WHERE bookID='{bookID_tb_removeBook.Text}'");
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                int numberOfLine=command.ExecuteNonQuery();
                if (numberOfLine > 0)
                {
                    MessageBox.Show("Book Removed!");
                    bookID_tb_removeBook.Clear();
                }
                else
                {
                    MessageBox.Show("Entered Book ID does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

                //Bookcollection gridview update
                query = string.Format($"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection");
                gv.update(query, books_gv_bookCollection);

                //Book issue gridview
                query = string.Format($@"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE available='yes'");
                gv.update(query, books_gv_bookIssue);
            }
        }

        //Book collection grid view
        private void bookName_tb_removeBook_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format($"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection WHERE bookName LIKE '{bookName_tb_removeBook.Text}%'");
            gv.update(query, books_gv_bookCollection);
        }

        //Issue books to students
        private void issueStudent_b_bookIssue_Click(object sender, EventArgs e)
        {
            int success=0;
            string query, today = DateTime.Today.ToString("yyyy/MM/dd");
            MySqlCommand command;
            MySqlDataReader reader;
            try
            {
                connection.Open();
                query = string.Format($"SELECT usn FROM student WHERE usn='{usn_tb_issue.Text}'");
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();

                    query = string.Format($"SELECT usn FROM student WHERE usn='{usn_tb_issue.Text}' && noOfBooks<'2'");
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();

                        query = string.Format($"SELECT available FROM bookcollection WHERE bookID='{bookIDStudent_tb_issue.Text}'");
                        command = new MySqlCommand(query, connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();

                            query = string.Format($"SELECT available FROM bookcollection WHERE bookID='{bookIDStudent_tb_issue.Text}' && available='yes'");
                            command = new MySqlCommand(query, connection);
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                reader.Close();

                                query = string.Format($"INSERT INTO bookissue (usn,bookID,issueDate) VALUES ('{usn_tb_issue.Text}','{bookIDStudent_tb_issue.Text}','{today}')");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 1;

                                query = string.Format($"UPDATE bookcollection SET available='no' WHERE bookID='{bookIDStudent_tb_issue.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 2;

                                query = string.Format($"UPDATE student SET noOfBooks=noOfBooks+1 WHERE usn='{usn_tb_issue.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 3;
                            }
                            else
                            {
                                MessageBox.Show("Book is taken");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Book is not available in the database");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Student has already taken 2 books");
                    }
                }
                else
                {
                    MessageBox.Show("USN number does not exist in the database");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                switch (success)
                {
                    case 0:
                        break;
                    case 1:
                        query = string.Format($"DELETE FROM bookissue WHERE usn='{usn_tb_issue.Text}' && bookID='{bookIDStudent_tb_issue.Text}' && issueDate='{today}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 2:
                        query = string.Format($"DELETE FROM bookissue WHERE usn='{usn_tb_issue.Text}' && bookID='{bookIDStudent_tb_issue.Text}' && issueDate='{today}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();

                        query = string.Format($"UPDATE bookcollection SET available='yes' WHERE bookID='{bookIDStudent_tb_issue.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 3:
                        MessageBox.Show("Book issue successful");
                        usn_tb_issue.Clear();
                        bookIDStudent_tb_issue.Clear();

                        //Book collection gridview update
                        query = string.Format($@"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection");
                        gv.update(query, books_gv_bookCollection);

                        //Book issue gridview update
                        query = string.Format($@"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE available='yes'");
                        gv.update(query, books_gv_bookIssue);

                        //Student gridview update in bookreturn
                        query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                            FROM student s,bookcollection bc, bookissue bi
                                            WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL");
                        gv.update(query, student_gv_bookReturn);

                        //Student gridview update in bookrenew
                        query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                                 FROM student s,bookcollection bc, bookissue bi
                                                 WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL");
                        gv.update(query, student_gv_bookRenew);
                        break;
                }
                connection.Close();
            }
        }

        //Teacher book issue
        private void issueTeacher_b_bookIssue_Click(object sender, EventArgs e)
        {
            string query, today = DateTime.Today.ToString("yyyy/MM/dd");
            MySqlCommand command;
            MySqlDataReader reader;
            int success = 0;
            try
            {
                connection.Open();
                query = string.Format($"SELECT bookID FROM bookcollection WHERE bookID='{bookIDTeacher_tb_issue.Text}'");
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();

                    query = string.Format($"SELECT available FROM bookcollection WHERE bookID='{bookIDTeacher_tb_issue.Text}' && available='yes'");
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        reader.Close();

                        query = string.Format($"SELECT teacherID FROM teacher where teacherID='{teacherID_tb_issue.Text}'");
                        command = new MySqlCommand(query, connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();

                            query = string.Format($"SELECT teacherID FROM teacher WHERE teacherID='{teacherID_tb_issue.Text}' && noOfBooks<'5'");
                            command = new MySqlCommand(query, connection);
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                reader.Close();

                                query = string.Format($"INSERT INTO teacherissue (teacherID,issueDate,bookID) VALUES ('{teacherID_tb_issue.Text}','{today}','{bookIDTeacher_tb_issue.Text}')");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 1;

                                query = string.Format($"UPDATE bookcollection SET available='no' WHERE bookID='{bookIDTeacher_tb_issue.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 2;

                                query = string.Format($"UPDATE teacher SET noOfBooks=noOfBooks+1 WHERE teacherID='{teacherID_tb_issue.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 3;
                            }
                            else
                            {
                                MessageBox.Show("Teacher has already taken 5 books");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Teacher ID does not exist in the database");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Book is taken");
                    }
                }
                else
                {
                    MessageBox.Show("Book does not available in the database");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                switch (success)
                {
                    case 0:
                        break;
                    case 1:
                        query = string.Format($"DELETE FROM teacherissue WHERE teacherID='{teacherID_tb_issue.Text}' && issueDate='{today}' && bookID='{bookIDTeacher_tb_issue.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 2:
                        query = string.Format($"DELETE FROM teacherissue WHERE teacherID='{teacherID_tb_issue.Text}' && issueDate='{today}' && bookID='{bookIDTeacher_tb_issue.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();

                        query = string.Format($"UPDATE bookcollection SET available='yes' WHERE bookID='{bookIDTeacher_tb_issue.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 3:
                        MessageBox.Show("Book issue successful");
                        teacherID_tb_issue.Clear();
                        bookIDTeacher_tb_issue.Clear();

                        //Book issue gridview update
                        query = string.Format($@"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE available='yes'");
                        gv.update(query, books_gv_bookIssue);

                        //Book collection gridview update
                        query = string.Format($@"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection");
                        gv.update(query, books_gv_bookCollection);

                        //Teacher gridiew update in bookreturn
                        query = string.Format($@"SELECT TeacherName,t.TeacherID,bc.BookID,bc.BookName
                                            FROM teacher t,bookcollection bc,teacherissue ti
                                            WHERE t.teacherID=ti.teacherID && ti.bookID=bc.bookID && returnDate IS NULL");
                        gv.update(query, teacher_gv_bookReturn);
                        break;
                }
                connection.Close();
            }
        }

        //Books gridview change in book issue
        private void bookName_tb_bookIssue_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format($@"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE bookName LIKE '{bookName_tb_bookIssue.Text}%' && available='yes'");
            gv.update(query, books_gv_bookIssue);
        }

        //Student gridview update in bookreturn using usn
        private void usn_tb_bookReturn_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                            FROM student s,bookcollection bc, bookissue bi
                                            WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL && s.usn LIKE '{usn_tb_bookReturn.Text}%'");
            gv.update(query, student_gv_bookReturn);
        }

        //Student gridview update in bookreturn using student name
        private void studentName_tb_bookReturn_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                        FROM student s,bookcollection bc, bookissue bi
                                        WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL && s.studentName LIKE '{studentName_tb_bookReturn.Text}%'");
            gv.update(query, student_gv_bookReturn);
        }

        //Student book return
        private void studentReturn_b_bookReturn_Click(object sender, EventArgs e)
        {
            int success = 0,fine = 0;
            string query;
            DateTime today = DateTime.Today;
            MySqlCommand command;
            MySqlDataReader reader;
            try
            {
                connection.Open();
                query = string.Format($"SELECT usn FROM student WHERE usn='{usn_tb_bookReturn.Text}'");
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();

                    query = string.Format($"SELECT usn FROM student WHERE usn='{usn_tb_bookReturn.Text}' && noOfBooks>'0'");
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();

                        query = string.Format($"SELECT bookID FROM bookcollection WHERE bookID='{bookIDStudent_tb_bookReturn.Text}' && available='no'");
                        command = new MySqlCommand(query, connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();
                            
                            //Finding if the student has taken that particular book and getting the issue date
                            query = string.Format($"SELECT issueDate FROM bookissue WHERE usn='{usn_tb_bookReturn.Text}' && bookId='{bookIDStudent_tb_bookReturn.Text}' && returnDate IS NULL");
                            command = new MySqlCommand(query, connection);
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                string issueDateString = reader.GetString(0);
                                DateTime issueDate = DateTime.Parse(issueDateString);
                                reader.Close();

                                query = string.Format($"UPDATE bookcollection SET available='yes' WHERE bookId='{bookIDStudent_tb_bookReturn.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 1;

                                query = string.Format($"UPDATE student SET noOfBooks=noOfBooks-1 WHERE usn='{usn_tb_bookReturn.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 2;

                                //Finding the no. of days that the student has taken the book and calculating the fine ammount
                                //TODO: Code review
                                /*query = string.Format($"SELECT DATEDIFF('{today}','{issueDate}')");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteReader();
                                if (reader.Read())
                                {
                                    int days = Convert.ToInt32(reader.GetString(0));
                                    MessageBox.Show("oohhh"+Convert.ToString(days));
                                    reader.Close();
                                    if(days > 14)
                                    {
                                        fine = days - 14;
                                    }
                                }*/

                                String days = (today - issueDate).TotalDays.ToString();
                                if (int.Parse(days) > 14)
                                {
                                    fine = int.Parse(days) - 14;
                                }

                                query = string.Format($"UPDATE bookissue SET returnDate='{today:yyyy/MM/dd}', fine='{fine}' WHERE usn='{usn_tb_bookReturn.Text}' && bookId='{bookIDStudent_tb_bookReturn.Text}' && returnDate IS NULL");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 3;
                            }
                            else
                            {
                                MessageBox.Show("Student has not taken the book");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wrong Book ID");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Student has not taken any book");
                    }
                }
                else
                {
                    MessageBox.Show("USN number does not exist in the database");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                switch (success)
                {
                    case 0:
                        break;
                    case 1:
                        query = string.Format($"UPDATE bookcollection SET available='no' WHERE bookId='{bookIDStudent_tb_bookReturn.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 2:
                        query = string.Format($"UPDATE bookcollection SET available='no' WHERE bookId='{bookIDStudent_tb_bookReturn.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        query = string.Format($"UPDATE student SET noOfBooks=noOfBooks+1 WHERE usn='{usn_tb_bookReturn.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 3:
                        MessageBox.Show($"Book Returned Successfully{'\n'}Fine amount = {fine}");
                        usn_tb_bookReturn.Clear();
                        bookIDStudent_tb_bookReturn.Clear();

                        //Student gridview update in bookreturn
                        query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                                    FROM student s,bookcollection bc, bookissue bi
                                                    WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL");
                        gv.update(query, student_gv_bookReturn);

                        //Book collection gridview update
                        query = string.Format($"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection");
                        gv.update(query, books_gv_bookCollection);

                        //Book issue gridview update
                        query = string.Format($"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE available='yes'");
                        gv.update(query, books_gv_bookIssue);

                        //Student gridview update in bookrenew
                        query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                                 FROM student s,bookcollection bc, bookissue bi
                                                 WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL");
                        gv.update(query, student_gv_bookRenew);
                        break;
                }
                connection.Close();
            }
        }

        //Teacher gridview update in bookreturn using teacher id
        private void teacherID_tb_bookReturn_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format($@"SELECT TeacherName,t.TeacherID,bc.BookID,bc.BookName
                                            FROM teacher t,bookcollection bc,teacherissue ti
                                            WHERE t.teacherID=ti.teacherID && ti.bookID=bc.bookID && returnDate IS NULL && t.teacherID LIKE '{teacherID_tb_bookReturn.Text}%'");
            gv.update(query, teacher_gv_bookReturn);
        }

        //Teacher gridview update in bookreturn using teacher name
        private void teacherName_tb_bookReturn_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format($@"SELECT TeacherName,t.TeacherID,bc.BookID,bc.BookName
                                            FROM teacher t,bookcollection bc,teacherissue ti
                                            WHERE t.teacherID=ti.teacherID && ti.bookID=bc.bookID && returnDate IS NULL && t.teacherName LIKE '{teacherName_tb_bookReturn.Text}%'");
            gv.update(query, teacher_gv_bookReturn);
        }

        //Teacher book return
        private void teacherReturn_b_bookReturn_Click(object sender, EventArgs e)
        {
            string query, today = DateTime.Today.ToString("yyyy/MM/dd");
            int success = 0;
            MySqlCommand command;
            MySqlDataReader reader;
            try
            {
                connection.Open();
                query = string.Format($"SELECT teacherID FROM teacher WHERE teacherID='{teacherID_tb_bookReturn.Text}'");
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();

                    query = string.Format($"SELECT teacherName FROM teacher WHERE noOfBooks>'0' && teacherID='{teacherID_tb_bookReturn.Text}'");
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        reader.Close();

                        query = string.Format($"SELECT bookID FROM bookcollection WHERE bookID='{bookIDTeacher_tb_bookReturn.Text}' && available='no'");
                        command = new MySqlCommand(query, connection);
                        reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            reader.Close();

                            query = string.Format($"SELECT teacherID FROM teacherissue WHERE teacherID='{teacherID_tb_bookReturn.Text}' && bookID='{bookIDTeacher_tb_bookReturn.Text}' && returnDate IS NULL");
                            command = new MySqlCommand(query, connection);
                            reader = command.ExecuteReader();
                            if(reader.Read())
                            {
                                reader.Close();

                                query = string.Format($"UPDATE bookcollection SET available='yes' WHERE bookID='{bookIDTeacher_tb_bookReturn.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 1;

                                query = string.Format($"UPDATE teacher SET noOfBooks=noOfBooks-1 WHERE teacherID='{teacherID_tb_bookReturn.Text}'");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 2;

                                query = string.Format($"UPDATE teacherissue SET returnDate='{today}' WHERE teacherID='{teacherID_tb_bookReturn.Text}' && bookID='{bookIDTeacher_tb_bookReturn.Text}' && returnDate IS NULL");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 3;
                            }
                            else
                            {
                                MessageBox.Show("Book is not issued to the teacher");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wrong BookID");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Teacher has not taken any book");
                    }
                }
                else
                {
                    MessageBox.Show("TeacherID does not exist in the database");
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                switch (success)
                {
                    case 0:
                        break;
                    case 1:
                        query = string.Format($"UPDATE bookcollection SET available='no' WHERE bookID='{bookIDStudent_tb_bookReturn.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 2:
                        query = string.Format($"UPDATE bookcollection SET available='no' WHERE bookID='{bookIDStudent_tb_bookReturn.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        query = string.Format($"UPDATE teacher SET noOfBooks=noOfBooks+1 WHERE teacherID='{teacherID_tb_bookReturn.Text}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 3:
                        MessageBox.Show("Book Returned Successfully");

                        teacherID_tb_bookReturn.Clear();
                        bookIDTeacher_tb_bookReturn.Clear();

                        //Teacher gridiew update in bookreturn
                        query = string.Format($@"SELECT TeacherName,t.TeacherID,bc.BookID,bc.BookName
                                                FROM teacher t,bookcollection bc,teacherissue ti
                                                WHERE t.teacherID=ti.teacherID && ti.bookID=bc.bookID && returnDate IS NULL");
                        gv.update(query, teacher_gv_bookReturn);

                        //Book collection gridview update
                        query = string.Format($@"SELECT BookName,BookID,Edition,Author,Available FROM bookcollection");
                        gv.update(query, books_gv_bookCollection);

                        //Book issue gridview update
                        query = string.Format($@"SELECT BookName,BookID,Edition,Author FROM bookcollection WHERE available='yes'");
                        gv.update(query, books_gv_bookIssue);
                        break;
                }
                connection.Close();
            }
        }

        // Student book renew
        private void renew_b_bookRenew_Click(object sender, EventArgs e)
        {
            string query;
            int success = 0, fine=0;
            DateTime today = DateTime.Today;
            MySqlCommand command;
            MySqlDataReader reader;
            try
            {
                connection.Open();
                query = string.Format($"SELECT usn FROM student WHERE usn='{usn_tb_bookRenew.Text}'");
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                if(reader.Read())
                {
                    reader.Close();

                    query = string.Format($"SELECT bookID FROM bookcollection WHERE bookID='{bookID_tb_bookRenew.Text}' && available='no'");
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        reader.Close();

                        query = string.Format($"SELECT usn FROM student WHERE noOfBooks>'0' && usn='{usn_tb_bookRenew.Text}'");
                        command = new MySqlCommand(query, connection);
                        reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            reader.Close();

                            query = string.Format($"SELECT issueDate FROM bookissue WHERE usn='{usn_tb_bookRenew.Text}' && bookID='{bookID_tb_bookRenew.Text}' && returnDate IS NULL");
                            command = new MySqlCommand(query, connection);
                            reader = command.ExecuteReader();
                            if(reader.Read())
                            {
                                string issueDateString = reader.GetString(0);
                                DateTime issueDate = DateTime.Parse(issueDateString);
                                reader.Close();

                                String days = (today - issueDate).TotalDays.ToString();
                                if (int.Parse(days) > 14)
                                {
                                    fine = int.Parse(days) - 14;
                                }

                                query = string.Format($"UPDATE bookissue SET returnDate='{today:yyyy/MM/dd}', fine='{fine}' WHERE usn='{usn_tb_bookRenew.Text}' && bookID='{bookID_tb_bookRenew.Text}' && returnDate IS NULL");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 1;

                                query = string.Format($"INSERT INTO bookissue (usn,bookID,issueDate) VALUES ('{usn_tb_bookRenew.Text}','{bookID_tb_bookRenew.Text}','{today:yyyy/MM/dd}')");
                                command = new MySqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                success = 2;
                            }
                            else
                            {
                                MessageBox.Show($"Book has not been issued to the student");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Student has not taken any books");
                        }
                    }
                    else
                    {
                        //TODO: Separeate filters for bookid and available in all click functions
                        MessageBox.Show("Wrong Book ID");
                    }
                }
                else
                {
                    MessageBox.Show("Student USN does not exist in the Database");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                switch(success)
                {
                    case 0:
                        break;
                    case 1:
                        query = string.Format($"UPDATE bookissue SET returnDate=NULL, fine=NULL WHERE usn='{usn_tb_bookRenew.Text}' && bookID='{bookID_tb_bookRenew.Text}' && returnDate='{today:yyyy/MM/dd}'");
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        break;
                    case 2:
                        MessageBox.Show($"Book Renewed Successfully{'\n'}Fine amount = {fine}");
                        usn_tb_bookRenew.Clear();
                        bookID_tb_bookRenew.Clear();

                        //Student gridview update in bookrenew
                        query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                                 FROM student s,bookcollection bc, bookissue bi
                                                 WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL");
                        gv.update(query, student_gv_bookRenew);
                        break;
                }
                connection.Close();
            }
        }

        // Student gridviewe update in bookrenew using student name
        private void studentName_tb_bookRenew_TextChanged(object sender, EventArgs e)
        {
            String query = string.Format($@"SELECT s.StudentName,s.USN,bc.BookName,bi.BookID
                                                 FROM student s,bookcollection bc, bookissue bi
                                                 WHERE s.usn = bi.usn && bi.bookid = bc.bookid && bi.bookID = bc.bookID && returnDate IS NULL && s.studentName LIKE '{studentName_tb_bookRenew.Text}%'");
            gv.update(query, student_gv_bookRenew);
        }
    }
}
