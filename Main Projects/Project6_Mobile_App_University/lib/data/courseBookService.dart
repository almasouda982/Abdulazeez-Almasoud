import 'dart:convert';
import 'package:http/http.dart' as http;
import 'connection.dart';

class CourseBookService {
  Future<List<dynamic>> getBooksForCourse(int courseId) async {
    final response = await http.get(
      Uri.parse('${ConnectionString.courseBooks}/$courseId'),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    }

    throw Exception('Failed to load books for course');
  }

  Future<void> addBookToCourse(int courseId, int bookId) async {
    final response = await http.post(
      Uri.parse(ConnectionString.courseBooks),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({
        'courseId': courseId,
        'bookId': bookId,
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to assign book to course');
    }
  }

  Future<void> removeBookFromCourse(int courseId, int bookId) async {
    final response = await http.delete(
      Uri.parse('${ConnectionString.courseBooks}/$courseId/$bookId'),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to remove book from course');
    }
  }
}