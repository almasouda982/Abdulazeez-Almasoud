class ConnectionString {
  static String baseUrl = 'https://knlmhmhg-7130.inc1.devtunnels.ms/api';
  static String dashboard = 'https://knlmhmhg-7199.inc1.devtunnels.ms/';
  static String login = '$baseUrl/Users/Login';
  static String register = '$baseUrl/Users/Register';

  // Student related
  static String students = '$baseUrl/Students';
  static String enrollment ='$baseUrl/Enrollments';

  // Course related
  static String course ='$baseUrl/Courses';
  static String courseSection ='$baseUrl/CourseSections';
  static String coursePrerequisite ='$baseUrl/CoursePrerequisites';
  static String waitListEntry = '$baseUrl/waitListEntrys';

  // Degree related
  static String degreeProgram = '$baseUrl/DegreePrograms';
  static String degreeRequirement = '$baseUrl/DegreeRequirements';

  // Book related
  static String books = '$baseUrl/Books';
  static String courseBooks = '$baseUrl/CourseBooks';


}