import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class CourseService {

  Future<List<dynamic>> getCourses() async {

    final response = await http.get(
      Uri.parse(ConnectionString.course),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load courses');
    }
  }


}